using EveMailHelper.BusinessDataAccess.Interfaces;
using EveMailHelper.DataModels.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public class EveDataUpdater<TEveInfo, TModel, TdbAccess>
        where TdbAccess : IEveId<TModel>, IUpdateModel<TModel>
        where TModel : IBaseEveId, IBaseEveObject
    {
        private TdbAccess _dbAccess;
        public EveDownloaderData<TEveInfo, TModel> Data { get; set; }
        private DateTime _notOlderThan;

        public delegate TModel CopyShallow(TEveInfo info, TModel oldModel);
        public delegate TModel Transformer(TEveInfo info, TModel oldModel);
        public delegate TModel ModelFactory(bool wasDeleted);

        public EveDataUpdater(TdbAccess dbAccess, EveDownloaderData<TEveInfo, TModel> data, DateTime? notOlderThan = null)
        {
            _dbAccess = dbAccess;
            Data = data;
            _notOlderThan = notOlderThan ?? DateTime.UtcNow - new TimeSpan(24, 0, 0);
        }


        /// <summary>
        /// only copy new info to db models
        /// DO NOT UPDATE - a cross db model id update might follow first!
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="factory"></param>
        /// <exception cref="Exception"></exception>
        public void CopyInfo(CopyShallow copy, ModelFactory factory)
        {
            // TODO: check that no eveIds have been left behind
            if (Data.HasEveIds())
                throw new Exception("Not all EveIds have been resolved");

            // now work through all downloaded eve infos
            foreach(var info in Data.EveData)
            {
                // check wether there is an object in db
                if (Data.Models.ContainsKey(info.Key))
                {
                    var model = Data.Models[info.Key];
                    Data.Models[info.Key] = copy(info.Value, model);
                }
                else
                {
                    var newModel = copy(info.Value, factory(wasDeleted:false));
                    Data.Models.Add(info.Key, newModel);
                }
            }
            foreach (var deletedEveId in Data.DeletedIds)
            {

                if (Data.Models.ContainsKey(deletedEveId))
                {
                    Data.Models[deletedEveId].EveDeletedInGame = true;
                }
                else
                {
                    TModel newModel = factory(wasDeleted:true);
                    newModel.EveDeletedInGame = true;
                    Data.Models.Add(deletedEveId, newModel);
                }
                Data.Models[deletedEveId].EveLastUpdated = DateTime.UtcNow;
            }
        }

        public void Update(Transformer transformer)
        {
            // TODO: check that no eveIds have been left behind
            if (Data.HasEveIds())
                throw new Exception("Not all EveIds have been resolved");

            // now work through all downloaded eve infos
            foreach (var info in Data.EveData)
            {
                // check wether there is an object in db
                if (Data.Models.ContainsKey(info.Key))
                {
                    var model = Data.Models[info.Key];
                    Data.Models[info.Key] = transformer(info.Value, model);
                    Data.Models[info.Key] = _dbAccess.Update(Data.Models[info.Key]);
                }
                else
                {
                    // must not happen
                    throw new Exception("missing data in models, run CopyShallow first");
                }
            }
            foreach (var deletedEveId in Data.DeletedIds)
            {
                if (Data.Models.ContainsKey(deletedEveId))
                {
                    _dbAccess.Update(Data.Models[deletedEveId]);
                }
            }
        }

        public IDictionary<int, TModel> Result()
        {
            return Data.Models;
        }

        public bool HasDbId(int key) => Data.HasModel(key);

        public TModel GetModelById(int key) => Data.Models[key];
    }
}
