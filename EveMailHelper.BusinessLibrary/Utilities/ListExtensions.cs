using EveMailHelper.DataModels;
using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.BusinessDataAccess.Utilities
{
    public static class ListExtensions
    {
        public static ICollection<DataModels.MailLabel> ToEveMailLabelList
            (this ICollection<EVEStandard.Models.MailLabel> mailLabels, Character character)
        {
            List<DataModels.MailLabel> resultLabels = new();
            foreach (var label in mailLabels)
            {
                var newLabel = new DataModels.MailLabel().CopyFrom(label);
                newLabel.Character = character;
                resultLabels.Add(newLabel);
            }
            return resultLabels;
        }

        public static IDictionary<int, T> ToEveIdDictionary<T>   
            (this ICollection<T> collection) where T : IBaseEveId
        {
            IDictionary<int, T> result = 
                new Dictionary<int, T>();

            foreach(var item in collection)
            {
                result.Add(item.EveId, item);
            }
            return result;
        }
    }
}
