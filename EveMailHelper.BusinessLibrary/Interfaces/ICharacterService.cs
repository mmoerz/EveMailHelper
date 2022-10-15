﻿using EveMailHelper.DataAccessLayer.Models;

using MudBlazor;

namespace EveMailHelper.BusinessLibrary.Services
{
    public interface ICharacterService
    {
        ICollection<Character> CreateOrRetrieveExisting(ICollection<string> characterNames);
        ICollection<Character> GetCharactersByName(ICollection<string> characterNames);
        Task<TableData<Character>> GetPaginated(string searchString, TableState state);
    }
}