using EveMailHelper.BusinessDataAccess.Interfaces;
using EveMailHelper.DataModels.Interfaces;
using EveMailHelper.BusinessDataAccess.Utilities;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public class EveDownloader<TEveInfo, TModel, TdbAccess>
        where TdbAccess : IEveId<TModel>
        where TModel : IBaseEveId, IBaseEveObject
    {
        private TdbAccess _dbAccess;
        private EveDownloaderData<TEveInfo, TModel> _data;
        private DateTime _notOlderThan;
        private string _eveWasDeletedString;

        public delegate Task<TEveInfo> GetInfoFromEve(int eveId);

        public EveDownloader(TdbAccess dbAccess,
            ICollection<int> eveIds,
            string eveWasDeletedString = "",
            DateTime? notOlderThan = null)
        {
            _dbAccess = dbAccess;
            _data = new(eveIds);
            _notOlderThan = notOlderThan ?? DateTime.UtcNow - new TimeSpan(24, 0, 0);
            _eveWasDeletedString = string.IsNullOrEmpty(eveWasDeletedString)
                ? "Unhandled error: {\"error\":\"Character has been deleted!\"}"
                : eveWasDeletedString;
        }

        public async Task Download(GetInfoFromEve getInfoFromEve)
        //(Func<int,Task<TEveInfo>> GetInfoFromEve2)
        {
            var workEveIds = _data.GetHashEveIds();
            // fetch data from database
            var dbModels = _dbAccess.GetByEveIds(workEveIds).ToEveIdDictionary();

            foreach (var eveId in workEveIds)
            {
                if (dbModels.ContainsKey(eveId) && dbModels[eveId].EveLastUpdated >= _notOlderThan)
                    continue;
                try
                {
                    var eveInfo = await getInfoFromEve(eveId);
                    _data.Add(eveId, eveInfo);
                }
                catch (Exception ex)
                {
                    if (ex.Message == _eveWasDeletedString)
                    {
                        _data.AddDeleted(eveId);
                    }
                }
            }
            _data.MergeModels(dbModels);
            _data.ClearEveIds();
        }

        public void AddEveId(int Key) => _data.Add(Key);

        public bool HasDbId(int key) => _data.HasModel(key);

        public TModel GetModelById(int key) => _data.Models[key];

        public bool HasEveIds() => _data.HasEveIds();

        public void RegisterEveId(int eveId) => _data.Add(eveId);

        public EveDownloaderData<TEveInfo, TModel> GetData() => _data;

    }
}
