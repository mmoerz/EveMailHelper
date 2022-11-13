using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Security;

using EveNatTools.ServiceLibrary.Utilities;

using Microsoft.EntityFrameworkCore;

using MudBlazor;

namespace EveMailHelper.BusinessDataAccess
{
    public class EveAccountDbAccess
    {
        private readonly EveMailHelperContext _context;
        public EveAccountDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public EveAccount GetById(Guid id)
        {
            return _context.EveAccounts.Where(eveaccount => eveaccount.Id == id)
                .AsTracking()
                .Include(x => x.Account)
                .Include(x => x.Characters)
                .First();
        }
                       
        public void Update(EveAccount account)
        {
            _context.EveAccounts.Update(account);
        }

        public void Remove(EveAccount eveAccount)
        {
            _context.EveAccounts.Remove(eveAccount);
        }
    }
}
