using Microsoft.EntityFrameworkCore;

using MudBlazor;

using EveMailHelper.DataModels;
using EveMailHelper.DataAccessLayer.Context;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.ServiceLayer.Interfaces;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class ChatService : IChatService
    {
        #region injected
        #endregion
        private readonly ChatDbAccess _dbAccess;
        private readonly ChatFileDbAccess _dbAccessChatFile;
        private readonly EveMailHelperContext _dbContext;
        //private readonly RunnerWriteDb<Note, Note> _addTemplateRunner;

        public ChatService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _dbAccess = new(_dbContext);
            _dbAccessChatFile = new(_dbContext);
        }

        public Chat AddOrUpdate(Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            _dbAccess.Update(chat);

            return chat;
        }

        public async Task<Chat> UpdateTracked(Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));
            var helper = await _dbAccess.GetById(chat.Id);
            helper.CopyShallowNoId(chat);
            _dbAccess.Update(chat);
            return chat;
        }

        public async Task<TableData<Chat>> GetPaginated(string searchString, TableState state)
        {
            return await _dbAccess.GetPaginated(searchString, state);
        }

        public async Task<Chat?> GetById(Guid id)
        {
            return await _dbAccess.GetById(id);
        }

        public async Task<ChatFile> GetChatFileById(Guid id)
        {
            return await _dbAccessChatFile.GetById(id);
        }

        public async Task<ChatFile> UpdateChatFile(ChatFile chatFile)
        {
            var result =_dbAccessChatFile.Update(chatFile);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task RemoveChatFile(Guid id)
        {
            await _dbAccessChatFile.Remove(id);
            await _dbContext.SaveChangesAsync();
        }
    }
}
