using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessDataAccess.Utilities;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class AddEsCharactersAction : IBizActionAsync<ICollection<int>, IDictionary<int, Character>>
    {
        readonly CharacterDbAccess _dbAccess;
        private readonly EVEStandardAPI _esiClient;
        private List<ValidationResult> _errors = new();

        public AddEsCharactersAction(CharacterDbAccess dbAccess, EVEStandardAPI esiClient)
        {
            _dbAccess = dbAccess;
            _esiClient = esiClient;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<IDictionary<int, Character>> ActionAsync(ICollection<int> eveIds)
        {
            IDictionary<int, Character> characters = _dbAccess.GetByEveId(eveIds).ToEveIdCharacterDictionary();

            foreach (var eveId in eveIds)
            {
                if (!characters.ContainsKey(eveId))
                {
                    var charInfo = await _esiClient.Character.GetCharacterPublicInfoV5Async(eveId);

                    Character character = new()
                    {
                        Name = charInfo.Model.Name,
                        EveId = eveId
                    };
                    _dbAccess.Add(character);
                    characters.Add(character.EveId, character);
                }
            }

            return characters;
        }
    }
}
