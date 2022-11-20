using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;

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
                    var allianceInfo = await _esiClient.Corporation.GetCorporationInfoV5Async(eveId);

                    Alliance corporation = new()
                    {
                        Name = allianceInfo.Model.Name,
                        EveId = eveId,
                        DateFounded = allianceInfo.Model.DateFounded,
                    };
                    corporation = _dbAccess.Add(corporation);
                    alliances.Add(corporation.EveId, corporation);
                }
            }

            return alliances;
        }
    }
}
