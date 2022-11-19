using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessDataAccess.Utilities
{
    public static class ListExtensions
    {
        public static ICollection<DataModels.EveMailLabel> ToEveMailLabelList
            (this ICollection<EVEStandard.Models.MailLabel> mailLabels, Character character)
        {
            List<DataModels.EveMailLabel> resultLabels = new();
            foreach (var label in mailLabels)
            {
                var newLabel = new DataModels.EveMailLabel().CopyFrom(label);
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
