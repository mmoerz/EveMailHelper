using EveMailHelper.DataAccessLayer.Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Services
{
    public class CharacterService
    {
        #region injected
        private IDbContextFactory<EveMailHelperContext> dbFactory = null!;
        #endregion
        private EveMailHelperContext dbContext = null!;

        public CharacterService(IDbContextFactory<EveMailHelperContext> dbContextFactory)
        {
            dbFactory = dbContextFactory;
            dbContext = dbFactory.CreateDbContext();
        }



    }
}
