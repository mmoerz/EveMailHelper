using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Market;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess
{
    public class BuyListDbAccess
    {
        private readonly EveMailHelperContext _context;
        public BuyListDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BuyList buyList)
        {
            buyList.ItemList.ToList().ForEach(x => x.BuyList = buyList);
            await _context.BuyLists.AddAsync(buyList);
        }

        //public BuyList Update(BuyList buyList)
        //{
        //    var foundLabels = _context.MailLabels
        //        .Where(x => x.EveLabelId == eveMailLabel.EveLabelId);

        //    MailLabel result = eveMailLabel;
        //    if (foundLabels.Any())
        //        result = foundLabels.First();

        //    return _context.MailLabels
        //        .Update(result)
        //        .Entity;
        //}


    }
}
