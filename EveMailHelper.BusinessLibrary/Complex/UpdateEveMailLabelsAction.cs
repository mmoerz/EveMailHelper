using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessDataAccess.Utilities;
using EveMailHelper.DataModels;
using EveMailHelper.BusinessLibrary.Complex.dto;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class UpdateEveMailLabelsAction : IBizAction<AddLabelDTO, IDictionary<long, MailLabel>>
    {
        readonly MailLabelDbAccess _dbAccess;
        readonly CharacterDbAccess _dbAccessCharacter;
        private List<ValidationResult> _errors = new();

        public UpdateEveMailLabelsAction(MailLabelDbAccess dbAccess, CharacterDbAccess dbAccessCharacter)
        {
            _dbAccess = dbAccess;
            _dbAccessCharacter = dbAccessCharacter;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public IDictionary<long, MailLabel> Action(AddLabelDTO labelDTO)
        {
            var character = _dbAccessCharacter.GetById(labelDTO.CharacterId);
            ICollection<MailLabel> labelList = labelDTO.MailLabels.ToEveMailLabelList(character);
            IDictionary<long, MailLabel> result = new Dictionary<long, MailLabel>();

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
