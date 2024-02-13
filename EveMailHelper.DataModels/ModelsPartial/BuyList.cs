using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.DataModels.Market
{
    public partial class BuyList
    {
        public void Merge(BuyList buyList)
        {
            foreach(var item in buyList.ItemList) 
            {
                ItemList.Add(item);
            }
        }

        public double Sum()
        {
            double result = 0;
            foreach(var item in ItemList)
            {
                result += item.Price;
            }
            return result;
        }

        public void CopyShallow(BuyList buyList)
        {
            Id = buyList.Id;
            Name = buyList.Name;
            CreateDate = buyList.CreateDate;
            ItemList = buyList.ItemList;
        }
    }
}
