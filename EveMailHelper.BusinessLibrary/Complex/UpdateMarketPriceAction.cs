using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class UpdateMarketPriceAction : IBizActionAsync<List<MarketPrice>, List<MarketPrice>>
    {
        readonly MarketPriceDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public UpdateMarketPriceAction(MarketPriceDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<List<MarketPrice>> ActionAsync(List<MarketPrice> dto)
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));
            if (dto.Count == 0)
                throw new ArgumentException($"cannot update empty list of marketorders");

            var storedMarketPriceIds = await _dbAccess.GetAllIdsAsync();

            foreach(var marketorder in dto)
            {
                if (storedMarketPriceIds.Where(x => x == marketorder.EveTypeId).Any())
                {
                    _dbAccess.Update(marketorder);
                }
                else
                    _dbAccess.Add(marketorder);
            }

            // delete all marketprices that have been removed
            foreach(var storedId in storedMarketPriceIds)
            {
                if (!dto.Where(x => x.EveTypeId == storedId).Any())
                {
                    _dbAccess.DeleteByIdAsync(storedId);
                }
            }
            
            return dto;
        }
    }
}
