using EveMailHelper.BusinessDataAccess.Interfaces;
using EveMailHelper.DataModels.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public class EveDataUpdater<TEveInfo, TModel, TdbAccess>
        where TdbAccess : IEveId<TModel>, IUpdateModel<TModel>
        where TModel : IBaseEveId, IBaseEveObject
    {
        private TdbAccess _dbAccess;
        private EveDownloaderData<TEveInfo, TModel> _data;
        private DateTime _notOlderThan;

        public delegate TModel CopyShallow(TEveInfo info, TModel oldModel);
        public delegate TModel Transformer(TEveInfo info, TModel oldModel);
        public delegate TModel ModelFactory(bool wasDeleted);

        public EveDataUpdater(TdbAccess dbAccess, EveDownloaderData<TEveInfo, TModel> data, DateTime? notOlderThan = null)
        {
            _dbAccess = dbAccess;
            _data = data;
            _notOlderThan = notOlderThan ?? DateTime.UtcNow - new TimeSpan(24, 0, 0);
        }

        public void CopyInfo(CopyShallow copy, ModelFactory factory)
        {
            // TODO: check that no eveIds have been left behind
            if (_data.HasEveIds())
                throw new Exception("Not all EveIds have been resolved");

            // now work through all downloaded eve infos
            foreach(var info in _data.EveData)
            {
                // check wether there is an object in db
                if (_data.Models.ContainsKey(info.Key))
                {
                    var model = _data.Models[info.Key];
                    _data.Models[info.Key] = copy(info.Value, model);
                    //_data.Models[info.Key] = _dbAccess.Update(_data.Models[info.Key]);
                }
                else
                {
                    var newmodel = copy(info.Value, factory(wasDeleted:false));
                    _data.Models.Add(info.Key, newmodel); // _dbAccess.Update(newmodel));
                }
            }
            foreach (var deletedEveId in _data.DeletedIds)
            {

                if (_data.Models.ContainsKey(deletedEveId))
                {
                    _data.Models[deletedEveId].EveDeletedInGame = true;
                    //_data.Models[deletedEveId] = _dbAccess.Update(_data.Models[deletedEveId]);
                }
                else
                {
                    TModel newModel = factory(wasDeleted:true);
                    newModel.EveDeletedInGame = true;
                    _data.Models.Add(deletedEveId, newModel); // _dbAccess.Update(newModel));
                }
            }
        }

        public void Update(Transformer transformer)
        {
            // TODO: check that no eveIds have been left behind
            if (_data.HasEveIds())
                throw new Exception("Not all EveIds have been resolved");

            // now work through all downloaded eve infos
            foreach (var info in _data.EveData)
            {
                // check wether there is an object in db
                if (_data.Models.ContainsKey(info.Key))
                {
                    var model = _data.Models[info.Key];
                    _data.Models[info.Key] = transformer(info.Value, model);
                    _data.Models[info.Key] = _dbAccess.Update(_data.Models[info.Key]);
                }
                else
                {
                    // must not happen
                    throw new Exception("missing data in models, run CopyShallow first");
                }
            }
            foreach (var deletedEveId in _data.DeletedIds)
            {
                if (_data.Models.ContainsKey(deletedEveId))
                {
                    _dbAccess.Update(_data.Models[deletedEveId]);
                }
            }
        }

        public IDictionary<int, TModel> Result()
        {
            return _data.Models;
        }
    }
}
