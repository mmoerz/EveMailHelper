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
    public class ChatFileDbAccess
    {

        private readonly EveMailHelperContext _context;
        public ChatFileDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ChatFile> GetById(Guid id)
        {
            var result = await _context.ChatFiles.Where(x => x.Id == id)
                .FirstAsync();
            _ = result ?? throw new NullReferenceException($"guid {id} not a valid ChatId");
            return result;
        }

        public ChatFile Update(ChatFile chatfile)
        {
            _context.ChatFiles.Update(chatfile);
            return chatfile;
        }

        public async Task Remove(Guid id)
        {
            var chatFile = await GetById(id);
            _context.ChatFiles.Remove(chatFile);
        }
    }
}
