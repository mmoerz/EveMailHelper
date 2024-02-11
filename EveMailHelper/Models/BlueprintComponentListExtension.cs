using System.Runtime.CompilerServices;

using EveMailHelper.ServiceLayer.Models;

namespace EveMailHelper.Web.Models
{
    public static class BlueprintComponentListExtension
    {
        public static void ToFlatList<T>(this IList<T> list) where T : BlueprintComponents
        {
            int i = 0;
            int insertPos = 0;
            T item;
            while (i < list.Count)
            {
                item = list[i];
                insertPos = i + 1;
                foreach (T subitem in item.SubComponents)
                {
                    list.Insert(insertPos, subitem);
                    insertPos++;
                }
                i++;
            }
        }
    }
}
