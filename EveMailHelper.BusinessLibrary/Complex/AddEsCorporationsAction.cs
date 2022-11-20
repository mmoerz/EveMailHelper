using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;

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
                    var corpInfo = await _esiClient.Corporation.GetCorporationInfoV5Async(eveId);

                    Corporation corporation = new()
                    {
                        Name = corpInfo.Model.Name,
                        EveId = eveId,
                        DateFounded = corpInfo.Model.DateFounded,
                        Description = corpInfo.Model.Description,
                        MemberCount = corpInfo.Model.MemberCount,
                        Shares = corpInfo.Model.Shares,
                        TaxRate = corpInfo.Model.TaxRate,
                        Url = corpInfo.Model.Url,
                        WarEligible = corpInfo.Model.WarEligible,
                    };
                    corporation = _dbAccess.Add(corporation);
                    corporations.Add(corporation.EveId, corporation);
                }
            }

            return corporations;
        }
    }
}
