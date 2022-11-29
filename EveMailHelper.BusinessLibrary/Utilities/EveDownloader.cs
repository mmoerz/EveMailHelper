using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public class EveDownloaderData<TEveInfo, TModel>
    {
        //private readonly ILogger<EveDownloaderData<TEveInfo, TModel>> log;
        private readonly HashSet<int> eveIds = new();
        private readonly IDictionary<int, TEveInfo> eveData = new Dictionary<int, TEveInfo>();
        private readonly IDictionary<int, TModel> models = new Dictionary<int, TModel>();

        public EveDownloaderData() { }

        //EveDownloaderData(ILogger<EveDownloaderData<TEveInfo, TModel>> logger)
        //{
        //    log = logger;
        //}

        public EveDownloaderData(
            ICollection<int> initialEveIds)
            //ILogger<EveDownloaderData<TEveInfo, TModel>> logger)
        {
            eveIds = initialEveIds.ToHashSet();
            //log = logger;
        }

        public void Add(int Key)
        {
            if (!eveData.ContainsKey(Key) && !models.ContainsKey(Key))
            {
                eveIds.Add(Key);
            }
        }

        public IDictionary<int, TModel> MergeModels(IDictionary<int, TModel> newModels)
        {
            return models.Merge(newModels);
        }

        public void Add(int Key, TEveInfo Value)
        {
            //if (eveData.ContainsKey(Key))
                //log.LogWarning("trying to add value for existing key");
            eveData.Add(Key, Value);
        }

        public HashSet<int> GetHashEveIds()
        {
            return eveIds.ToHashSet();
        }

        public bool HasEveIds()
        {
            return eveIds.Count > 0;
        }

        public bool ContainsKey(int Key)
        {
            return models.ContainsKey(Key) && eveData.ContainsKey(Key);
        }

        public void ClearEveIds()
        {
            eveIds.Clear();
        }
    }
}
