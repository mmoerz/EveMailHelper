using Microsoft.EntityFrameworkCore;

using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessDataAccess
{
    public class ChatFileDbAccess
    {

        private readonly EveMailHelperContext _context;
        public ChatFileDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task<ChatFile> GetByIdAsync(Guid id)
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

        public async Task RemoveAsync(Guid id)
        {
            var chatFile = await GetByIdAsync(id);
            _context.ChatFiles.Remove(chatFile);
        }
    }
}
