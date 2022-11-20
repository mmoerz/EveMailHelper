using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsMailListsAction : IBizActionAsync<ICollection<int>, IDictionary<int, MailList>>
    {
        readonly MailListDbAccess _dbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEsMailListsAction(MailListDbAccess dbAccess, EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
            _esiClient = esiClient;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<IDictionary<int, MailList>> ActionAsync(ICollection<int> eveIds)
        {
            IDictionary<int, MailList> alliances = _dbAccess.GetByEveIds(eveIds).ToEveIdDictionary();

            foreach (var eveId in eveIds)
            {
                if (!alliances.ContainsKey(eveId))
                {
                    var allianceInfo = await _esiClient.Corporation.GetCorporationInfoV5Async(eveId);

                    MailList corporation = new()
                    {
                        Name = allianceInfo.Model.Name,
                        EveId = eveId,
                    };
                    corporation = _dbAccess.Add(corporation);
                    alliances.Add(corporation.EveId, corporation);
                }
            }

            return alliances;
        }
    }
}
