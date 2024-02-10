
using EveMailHelper.DataModels.Sde.Map;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IMapManager
    {
        Task<Region> GetRegionByName(string eveTypeName);
        Task<IList<string>> SearchForRegionName(string regionNamePart);
    }
}