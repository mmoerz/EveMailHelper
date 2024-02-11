using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class UpdateMarketOrderAction : IBizAction<List<MarketOrder>, List<MarketOrder>>
    {
        readonly MarketOrderDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public UpdateMarketOrderAction(MarketOrderDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public List<MarketOrder> Action(List<MarketOrder> dto)
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));
            if (dto.Count == 0)
                throw new ArgumentException($"cannot update empty list of marketorders");

            int eveTypeId = dto.First().TypeId;
            var storedMarketOrdersIds = _dbAccess.GetIdsForEveType(eveTypeId);

            foreach(var marketorder in dto)
            {
                if (storedMarketOrdersIds.Where(x => x == marketorder.EveId).Any())
                {
                    _dbAccess.Update(marketorder);
                }
                else
                    _dbAccess.Add(marketorder);
            }

            // delete all marketorders that have been removed
            foreach(var storedId in storedMarketOrdersIds)
            {
                if (!dto.Where(x => x.EveId == storedId).Any())
                {
                    _dbAccess.DeleteById(storedId);
                }
            }
            
            return dto;
        }
    }
}
