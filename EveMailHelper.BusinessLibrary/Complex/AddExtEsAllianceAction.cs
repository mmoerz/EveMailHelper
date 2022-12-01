using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.BusinessDataAccess.Utilities;
using System.Linq;
using EveMailHelper.BusinessLibrary.Complex.dto;

//using EVEStandard.Models;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddExtEsAllianceAction : IBizAction<CharCorpAllianceDTO, CharCorpAllianceDTO>
    {
        private readonly CharacterDbAccess _characterdbAccess;
        private readonly CorporationDbAccess _corpDbAccess;
        private readonly AllianceDbAccess _allianceDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();
        DateTime _notOlderThan;

        public AddExtEsAllianceAction(
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

        EveDataUpdater<EVEStandard.Models.CharacterInfo, Character, CharacterDbAccess> characterUpdater = null! ;
        EveDataUpdater<EVEStandard.Models.CorporationInfo, Corporation, CorporationDbAccess> corporationUpdater = null!;
        EveDataUpdater<EVEStandard.Models.Alliance, Alliance, AllianceDbAccess> allianceUpdater = null!;

        public CharCorpAllianceDTO Action(CharCorpAllianceDTO inputDTO)
        {
            characterUpdater = new(_characterdbAccess, inputDTO.CharactersDD);
            corporationUpdater = new(_corpDbAccess, inputDTO.CorporationsDD);
            allianceUpdater = new(_allianceDbAccess, inputDTO.AlliancesDD);

            allianceUpdater.CopyInfo(AllianceCopyShallow, AllianceFactory);
            allianceUpdater.Update(TransformEveAllianceInfo);

            CharCorpAllianceDTO resultDTO = new()
            {
                CharactersDD = characterUpdater.Data,
                CorporationsDD = corporationUpdater.Data,
                AlliancesDD = allianceUpdater.Data,
            };

            return resultDTO;
        }

        private Alliance AllianceCopyShallow(int eveId, EVEStandard.Models.Alliance info, Alliance oldModel)
        {
            return oldModel.CopyShallow(info);
        }

        private Alliance TransformEveAllianceInfo(EVEStandard.Models.Alliance info, Alliance oldModel)
        {
            var result = oldModel.CopyShallow(info);
            //if (characterDL.HasDbId(info.CreatorId))
            result.Creator = characterUpdater.GetModelById(info.CreatorId);
            result.CreatorCorporation = corporationUpdater.GetModelById(info.CreatorCorporationId);
            if (info.ExecutorCorporationId != null && corporationUpdater.HasDbId(info.ExecutorCorporationId.Value))
                result.ExecutorCorporation = corporationUpdater.GetModelById(info.ExecutorCorporationId.Value);
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
