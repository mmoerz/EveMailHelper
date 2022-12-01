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
        private readonly HashSet<int> eveDeletedIds = new();
        private readonly IDictionary<int, TEveInfo> eveData = new Dictionary<int, TEveInfo>();
        private readonly IDictionary<int, TModel> models = new Dictionary<int, TModel>();

        public HashSet<int> DeletedIds { get { return eveDeletedIds; } }
        public IDictionary<int, TEveInfo> EveData { get { return eveData; } }
        public IDictionary<int, TModel> Models { get { return models; } }

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
            if (!eveDeletedIds.Contains(Key)
                && !eveData.ContainsKey(Key) 
                && !models.ContainsKey(Key))
            {
                eveIds.Add(Key);
            }
        }

        public void AddDeleted(int Key)
        {
            if (eveData.ContainsKey(Key))
            {
                eveData.Remove(Key);
            }
            eveDeletedIds.Add(Key);
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

        /// <summary>
        /// stores data about the given key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool HasAnyDataForKey(int Key)
        {
            return  eveDeletedIds.Contains(Key) 
                || eveData.ContainsKey(Key)
                || models.ContainsKey(Key);
        }

        public bool HasModel(int Key)
        {
            return models.ContainsKey(Key);
        }

        public void ClearEveIds()
        {
            eveIds.Clear();
        }

        public void UnionWith(ICollection<int> collection)
        {
            eveIds.UnionWith(collection);
        }
    }
}
