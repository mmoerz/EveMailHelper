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
    public class AddExtEsCharacterAction : IBizAction<CharCorpAllianceDTO, CharCorpAllianceDTO>
    {
        private readonly CharacterDbAccess _characterdbAccess;
        private readonly CorporationDbAccess _corpDbAccess;
        private readonly AllianceDbAccess _allianceDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();
        DateTime _notOlderThan;

        public AddExtEsCharacterAction(
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

        public bool WithAlliance { get; set; } = false;

        EveDataUpdater<EVEStandard.Models.CharacterInfo, Character, CharacterDbAccess> characterUpdater = null! ;
        EveDataUpdater<EVEStandard.Models.CorporationInfo, Corporation, CorporationDbAccess> corporationUpdater = null!;
        EveDataUpdater<EVEStandard.Models.Alliance, Alliance, AllianceDbAccess> allianceUpdater = null!;

        public CharCorpAllianceDTO Action(
            CharCorpAllianceDTO inputDTO
            )
        {
            characterUpdater = new(_characterdbAccess, inputDTO.CharactersDD);
            corporationUpdater = new(_corpDbAccess, inputDTO.CorporationsDD);
            allianceUpdater = new(_allianceDbAccess, inputDTO.AlliancesDD);

            // do the following stuff after committing all characters and corps
            // another round for updates (after alliance ids should be present)
            characterUpdater.Update(TransformEveCharInfo);
            corporationUpdater.Update(TransformEveCorporationInfo);
            // alliance must be done after saving those changes to the database
            // and retrieving new guids for them

            CharCorpAllianceDTO resultDTO = new()
            {
                CharactersDD = characterUpdater.Data,
                CorporationsDD = corporationUpdater.Data,
                AlliancesDD = allianceUpdater.Data,
            };

            return resultDTO;
        }

        private Character TransformEveCharInfo(EVEStandard.Models.CharacterInfo info, Character oldModel)
        {
            var result = oldModel;
            if (corporationUpdater.HasDbId(info.CorporationId))
                result.Corporation = corporationUpdater.GetModelById(info.CorporationId);
            
            return result;
        }

        private Corporation TransformEveCorporationInfo(EVEStandard.Models.CorporationInfo info, Corporation oldModel)
        {
            var result = oldModel;
            if (characterUpdater.HasDbId(info.CreatorId))
                result.Creator = characterUpdater.GetModelById(info.CreatorId);
            if (characterUpdater.HasDbId(info.CeoId))
                result.Ceo = characterUpdater.GetModelById(info.CeoId);
            if (WithAlliance != false && 
                info.AllianceId != null && allianceUpdater.HasDbId(info.AllianceId.Value))
                result.Alliance = allianceUpdater.GetModelById(info.AllianceId.Value);
            return result;
        }
    }
}
