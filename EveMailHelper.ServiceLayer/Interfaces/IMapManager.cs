
namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IMapManager
    {
        Task<IList<string>> SearchForRegionName(string regionNamePart);
    }
}