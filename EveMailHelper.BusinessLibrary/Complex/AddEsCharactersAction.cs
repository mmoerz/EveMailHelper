using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;
using EVEStandard;
using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.BusinessDataAccess.Utilities;
using Microsoft.EntityFrameworkCore;
//using EVEStandard.Models;

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
            DateTime notOlderThan = DateTime.UtcNow - new TimeSpan(24, 0, 0);
            IDictionary<int, Character> characters = _dbAccess.GetByEveIds(eveIds).ToEveIdDictionary();
            EVEStandard.Models.CharacterInfo charInfo = null;

            // this caching is a pain
            foreach (var eveId in eveIds)
            {
                if (!characters.ContainsKey(eveId))
                {
                    bool deleted = false;
                    charInfo = new EVEStandard.Models.CharacterInfo()
                    {
                        Name = "Unknown",
                        Title = "Unknown",
                    };
                    try
                    {
                        charInfo = (await _esiClient.Character.GetCharacterPublicInfoV5Async(eveId)).Model;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Unhandled error: {\"error\":\"Character has been deleted!\"}")
                        {
                            // this eveId will stay unknown forever and ever
                            charInfo = new EVEStandard.Models.CharacterInfo()
                            {
                                Name = "Deleted",
                                Title = "Deleted",
                            };
                            deleted = true;
                        }
                    }

                    Character character = new();
                    character.CopyShallow(charInfo);
                    character.EveId = eveId;
                    character.EveDeletedInGame = deleted;
                    _dbAccess.Add(character);
                    characters.Add(character.EveId, character);
                }
                else if (characters[eveId].EveLastUpdated < notOlderThan)
                {
                    try
                    {
                        charInfo = (await _esiClient.Character.GetCharacterPublicInfoV5Async(eveId)).Model;
                        characters[eveId].CopyShallow(charInfo);
                    }
                    catch (Exception ex)
                    {
                        // if not char doesnot exit in eve anymore -> update
                        if (ex.Message == "Unhandled error: {\"error\":\"Character has been deleted!\"}")
                        {
                            characters[eveId].EveDeletedInGame = true;
                        }
                    }
                    _dbAccess.Update(characters[eveId]);
                }
            }

            return characters;
        }
    }
}
