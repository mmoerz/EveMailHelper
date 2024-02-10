
using EveMailHelper.DataModels.Sde;

namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface IEveTypeManager
    {
        Task<EveType> GetByName(string eveTypeName);
        Task<IList<string>> SearchForEveTypeName(string regionNamePart);
    }
}