using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddCharactersAction : IBizAction<ICollection<string>, ICollection<Character>>
    {
        readonly CharacterDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public AddCharactersAction(CharacterDbAccess characterDbAccess)
        {
            _dbAccess = characterDbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public ICollection<Character> Action(ICollection<string> characterNames)
        {
            ICollection<Character> characterList = _dbAccess.GetByNames(characterNames);

            foreach (string charName in characterNames)
            {
                if (!characterList.Any(x => x.Name == charName))
                {
                    Character character = new() { Name = charName };
                    _dbAccess.Add(character);
                    characterList.Add(character);
                }
            }
            
            return characterList;
        }
    }
}
