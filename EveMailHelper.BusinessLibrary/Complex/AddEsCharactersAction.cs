using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.BusinessDataAccess.Utilities;
using System.Linq;

//using EVEStandard.Models;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsCharactersAction : IBizActionAsync<ICollection<int>, IDictionary<int, Character>>
    {
        private readonly CharacterDbAccess _characterdbAccess;
        private readonly CorporationDbAccess _corpDbAccess;
        private readonly AllianceDbAccess _allianceDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();
        DateTime _notOlderThan;

        public AddEsCharactersAction(
            CharacterDbAccess dbAccess,
            CorporationDbAccess corporationDbAccess,
            AllianceDbAccess allianceDbAccess,
            EVEStandardAPI esiClient,
            DateTime? notOlderThan = null
            )
        {
            _characterdbAccess = dbAccess;
            _corpDbAccess = corporationDbAccess;
            _allianceDbAccess = allianceDbAccess;
            _esiClient = esiClient;
            _notOlderThan = notOlderThan ?? DateTime.UtcNow - new TimeSpan(24, 0, 0);
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        EveDownloader<EVEStandard.Models.CharacterInfo, Character, CharacterDbAccess> characterDL = null!;
        EveDownloader<EVEStandard.Models.CorporationInfo, Corporation, CorporationDbAccess> corporationDL = null!;
        EveDownloader<EVEStandard.Models.Alliance, Alliance, AllianceDbAccess> allianceDL = null!;

        EveDataUpdater<EVEStandard.Models.CharacterInfo, Character, CharacterDbAccess> characterUpdater = null! ;
        EveDataUpdater<EVEStandard.Models.CorporationInfo, Corporation, CorporationDbAccess> corporationUpdater = null!;
        EveDataUpdater<EVEStandard.Models.Alliance, Alliance, AllianceDbAccess> allianceUpdater = null!;

        public async Task<IDictionary<int, Character>> ActionAsync(
            ICollection<int> peveCharacterIds //, ICollection<int> peveCorporationIds, ICollection<int> peveAllianceIds
            )
        {
            characterDL = new(_characterdbAccess, peveCharacterIds);
            corporationDL = new(_corpDbAccess, new List<int>());
            allianceDL = new(_allianceDbAccess, new List<int>());

            while (characterDL.HasEveIds() || corporationDL.HasEveIds() || allianceDL.HasEveIds())
            {
                await characterDL.Download(GetCharacterInfo);
                await corporationDL.Download(GetCorporationInfo);
                await allianceDL.Download(GetAlliance);
            }
            characterUpdater = new(_characterdbAccess, characterDL.GetData());
            corporationUpdater = new(_corpDbAccess, corporationDL.GetData());
            allianceUpdater = new(_allianceDbAccess, allianceDL.GetData());

            // finally use the eve infos to correct the dependencies by setting the objects
            characterUpdater.CopyInfo(CharacterCopyShallow, CharacterFactory);
            corporationUpdater.CopyInfo(CorporationCopyShallow, CorporationFactory);
            allianceUpdater.CopyInfo(AllianceCopyShallow, AllianceFactory);

            // another round for updates (after alliance ids should be present)
            characterUpdater.Update(TransformEveCharInfo);
            corporationUpdater.Update(TransformEveCorporationInfo);
            allianceUpdater.Update(TransformEveAllianceInfo);

            return characterUpdater.Result();
        }

        private async Task<EVEStandard.Models.CharacterInfo> GetCharacterInfo(int eveId)
        {
            var charInfo = (await _esiClient.Character.GetCharacterPublicInfoV5Async(eveId)).Model;

            corporationDL.AddEveId(charInfo.CorporationId);
            if (charInfo.AllianceId != null)
                allianceDL.AddEveId(charInfo.AllianceId.Value);
            return charInfo;
        }

        private async Task<EVEStandard.Models.CorporationInfo> GetCorporationInfo(int eveId)
        {
            var corpInfo = (await _esiClient.Corporation.GetCorporationInfoV5Async(eveId)).Model;

            characterDL.AddEveId(corpInfo.CreatorId);
            characterDL.AddEveId(corpInfo.CeoId);
            if (corpInfo.AllianceId != null)
                allianceDL.AddEveId(corpInfo.AllianceId.Value);
            return corpInfo;
        }

        private async Task<EVEStandard.Models.Alliance> GetAlliance(int eveId)
        {
            var allianceInfo = (await _esiClient.Alliance.GetAllianceInfoV3Async(eveId)).Model;

            characterDL.AddEveId(allianceInfo.CreatorId);
            corporationDL.AddEveId(allianceInfo.CreatorCorporationId);
            if (allianceInfo.ExecutorCorporationId != null)
                corporationDL.AddEveId(allianceInfo.ExecutorCorporationId.Value);
            return allianceInfo;
        }

        private Character CharacterCopyShallow(EVEStandard.Models.CharacterInfo info, Character oldModel)
        {
            return oldModel.CopyShallow(info);
        }

        private Character TransformEveCharInfo(EVEStandard.Models.CharacterInfo info, Character oldModel)
        {
            var result = oldModel;
            if (corporationDL.HasDbId(info.CorporationId))
                result.Corporation = corporationDL.GetModelById(info.CorporationId);
            //if (info.AllianceId != null && allianceDL.HasDbId(info.AllianceId.Value))
            //    result.Alliance = allianceDL.GetModelById(info.AllianceId.Value);
            return result;
        }

        private Character CharacterFactory(bool wasDeleted)
        {
            if (wasDeleted)
                return new Character()
                {
                    Name = "Deleted",
                    Title = "Deleted",
                    Description = "Deleted"
                };
            return new Character()
            {
                Name = "Unknown",
                Title = "Unknown",
                Description = "Unknown",
            };
        }

        private Corporation CorporationCopyShallow(EVEStandard.Models.CorporationInfo info, Corporation oldModel)
        {
            return oldModel.CopyShallow(info);
        }

        private Corporation TransformEveCorporationInfo(EVEStandard.Models.CorporationInfo info, Corporation oldModel)
        {
            var result = oldModel;
            if (characterDL.HasDbId(info.CreatorId))
                result.Creator = characterDL.GetModelById(info.CreatorId);
            if (characterDL.HasDbId(info.CeoId))
                result.Ceo = characterDL.GetModelById(info.CeoId);
            if (info.AllianceId != null && allianceDL.HasDbId(info.AllianceId.Value))
                result.Alliance = allianceDL.GetModelById(info.AllianceId.Value);
            return result;
        }

        private Corporation CorporationFactory(bool wasDeleted)
        {
            if (wasDeleted)
                return new Corporation()
                {
                    Name = "Deleted",
                    Description = "Deleted",
                    Ticker = "Deleted",
                };
            return new Corporation()
            {
                Name = "Unknown",
                Description = "Unknown",
                Ticker = "Unknown",
            };
        }

        private Alliance AllianceCopyShallow(EVEStandard.Models.Alliance info, Alliance oldModel)
        {
            return oldModel.CopyShallow(info);
        }

        private Alliance TransformEveAllianceInfo(EVEStandard.Models.Alliance info, Alliance oldModel)
        {
            var result = oldModel.CopyShallow(info);
            //if (characterDL.HasDbId(info.CreatorId))
            result.Creator = characterDL.GetModelById(info.CreatorId);
            result.CreatorCorporation = corporationDL.GetModelById(info.CreatorCorporationId);
            if (info.ExecutorCorporationId != null && corporationDL.HasDbId(info.ExecutorCorporationId.Value))
                result.ExecutorCorporation = corporationDL.GetModelById(info.ExecutorCorporationId.Value);
            return result;
        }

        private Alliance AllianceFactory(bool wasDeleted)
        {
            if (wasDeleted)
                return new Alliance()
                {
                    Name = "Deleted",
                    Ticker = "Deleted",
                };
            return new Alliance()
            {
                Name = "Unknown",
                Ticker = "Unknown",
            };
        }
    }
}
