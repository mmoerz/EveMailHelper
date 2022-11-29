using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
            this IDictionary<TKey, TValue> master, IDictionary<TKey, TValue> slave)
        {
            slave.Where(x => !master.ContainsKey(x.Key))
                    .ToList()
                    .ForEach(x => master.Add(x.Key, x.Value));
            return master;
        }

        //public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        //    this Dictionary<TKey, TValue> master, Dictionary<TKey, TValue> slave) 
        //{
        //    slave.Where(x => !master.ContainsKey(x.Key))
        //            .ToList()
        //            .ForEach(x => master.Add(x.Key, x.Value));
        //    return master;
        //}

    }
}
