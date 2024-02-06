using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Complex;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataModels.Sde;

using FluentValidation.Results;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class FetchBlueprints : IBizActionAsync<IndustryBlueprint, List<IndustryBlueprint>>
    {
        readonly BlueprintDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public FetchBlueprints(BlueprintDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();
        
        public async Task<List<IndustryBlueprint>> ActionAsync(IndustryBlueprint dto)

        {
            var res = new List<IndustryBlueprint>();
            return res;
        }
    }
}
