using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataAccessLayer.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex.dbAccess
{
    public class CharacterDbAccess
    {
        private readonly EveMailHelperContext _context;
        public CharacterDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public Character GetCharactersById(Guid id)
        {
            return _context.Characters.Where(character => character.Id == id).First();
        }

        public ICollection<Character> GetCharactersByName(ICollection<string> characterNames)
        {
            IQueryable<Character> query = from character in _context.Characters
                                          select character;

            query = query.Where(x => characterNames.Contains(x.Name));

            return query.ToList();
        }

        public void Add(Character character)
        {
            _context.Characters.Add(character);
        }

        public void Update(Character character)
        {
            _context.Characters.Update(character);
        }
    }
}
