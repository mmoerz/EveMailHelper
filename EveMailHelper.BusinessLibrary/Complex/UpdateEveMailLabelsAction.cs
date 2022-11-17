using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessDataAccess.Utilities;
using EveMailHelper.DataModels;
using EveMailHelper.BusinessLibrary.Complex.dto;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class UpdateEveMailLabelsAction : IBizAction<AddLabelDTO, IDictionary<long, EveMailLabel>>
    {
        readonly EveMailLabelDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public UpdateEveMailLabelsAction(EveMailLabelDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public IDictionary<long, EveMailLabel> Action(AddLabelDTO labelDTO)
        {
            ICollection<EveMailLabel> labelList = labelDTO.MailLabels.ToEveMailLabelList(labelDTO.Character);
            IDictionary<long, EveMailLabel> result = new Dictionary<long, EveMailLabel>();

            foreach (var label in labelList)
            {
                if (label.EveLabelId == null)
                    continue;
                var updated = _dbAccess.Update(label);
                result.Add(label.EveLabelId.Value, updated);
            }
            
            return result;
        }
    }
}
