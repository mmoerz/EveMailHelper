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
        readonly AllianceDbAccess _dbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEsAlliancesAction(AllianceDbAccess dbAccess, EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
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
                    Alliance alliance = new();
                    var allianceInfo = new EVEStandard.Models.Alliance()
                    {
                        Name = "Unknown"
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
                    alliance = _dbAccess.Add(alliance);
                    alliances.Add(alliance.EveId, alliance);
                }
            }

            return alliances;
        }
    }
}
