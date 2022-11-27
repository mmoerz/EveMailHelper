using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;
using EveMailHelper.BusinessLibrary.Utilities;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsCorporationsAction : IBizActionAsync<ICollection<int>, IDictionary<int, Corporation>>
    {
        readonly CorporationDbAccess _dbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEsCorporationsAction(CorporationDbAccess dbAccess, EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
            _esiClient = esiClient;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<IDictionary<int, Corporation>> ActionAsync(ICollection<int> eveIds)
        {
            IDictionary<int, Corporation> corporations = _dbAccess.GetByEveIds(eveIds).ToEveIdDictionary();

            foreach (var eveId in eveIds)
            {
                if (!corporations.ContainsKey(eveId))
                {
                    bool deleted = false;
                    var corpInfo = new EVEStandard.Models.CorporationInfo()
                    {
                        Name = "Unknown",
                        Description = "Unknown",

                    };
                    try
                    {
                        var dto = await _esiClient.Corporation.GetCorporationInfoV5Async(eveId);
                        corpInfo = dto.Model;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Unhandled error: {\"error\":\"Corporation has been deleted!\"}")
                        {
                            // this eveId will stay unknown forever and ever
                            corpInfo.Name = "Deleted";
                            corpInfo.Ticker = "";
                            corpInfo.Description = "Deleted";
                            deleted = true;
                        }
                    }
                    Corporation corporation = new();
                    corporation.CopyShallow(eveId, corpInfo);
                    corporation.EveDeletedInGame = deleted;
                    corporation = _dbAccess.Add(corporation);
                    corporations.Add(corporation.EveId, corporation);
                }
            }

            return corporations;
        }
    }
}
