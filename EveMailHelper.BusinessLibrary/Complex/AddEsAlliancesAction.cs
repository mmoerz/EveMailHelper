using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;
using EveMailHelper.BusinessLibrary.Utilities;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsAlliancesAction : IBizActionAsync<ICollection<int>, IDictionary<int, Alliance>>
    {
        private readonly AllianceDbAccess _dbAccess;
        private readonly CorporationDbAccess _corporationDbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEsAlliancesAction(
            AllianceDbAccess dbAccess, CorporationDbAccess corpDbAccess, EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
            _corporationDbAccess= corpDbAccess;
            _esiClient = esiClient;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<IDictionary<int, Alliance>> ActionAsync(ICollection<int> eveIds)
        {
            IDictionary<int, Alliance> alliances = _dbAccess.GetByEveIds(eveIds).ToEveIdDictionary();

            foreach (var eveId in eveIds)
            {
                if (!alliances.ContainsKey(eveId))
                {
                    bool deleted = false;
                    Alliance alliance = new()
                    {
                        CreatorCorporation = await _corporationDbAccess.GetDefaultAsync(),
                    };
                    var allianceInfo = new EVEStandard.Models.Alliance()
                    {
                        Name = "Unknown",                        
                    };

                    try
                    {
                        var dto = await _esiClient.Alliance.GetAllianceInfoV3Async(eveId);
                        allianceInfo = dto.Model;

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Unhandled error: {\"error\":\"Alliance has been deleted!\"}")
                        {
                            // this eveId will stay unknown forever and ever
                            allianceInfo.Name = "Deleted";
                            allianceInfo.Ticker = "";
                            deleted = true;
                        }
                    }
                    
                    alliance.CopyShallow(eveId, allianceInfo);
                    alliance.EveDeleteInGame = deleted;
                    alliance.CreatorCorporation = _corporationDbAccess.GetByEveId(allianceInfo.CreatorCorporationId);
                    alliance = _dbAccess.Add(alliance);
                    alliances.Add(alliance.EveId, alliance);
                }
            }

            return alliances;
        }
    }
}
