﻿using MudBlazor;

using EveMailHelper.DataModels;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IChatService
    {
        Chat AddOrUpdate(Chat chat);
        Task<Chat?> GetById(Guid id);
        Task<ChatFile> GetChatFileById(Guid id);
        Task<TableData<Chat>> GetPaginated(string searchString, TableState state);
        Task RemoveChatFile(Guid id);
        Task<ChatFile> UpdateChatFile(ChatFile chatFile);
        Task<Chat> UpdateTracked(Chat chat);
    }
}