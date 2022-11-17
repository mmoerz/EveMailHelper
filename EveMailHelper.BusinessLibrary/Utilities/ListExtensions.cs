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

        public static IDictionary<int, DataModels.Character> ToEveIdCharacterDictionary
            (this ICollection<DataModels.Character> characters)
        {
            IDictionary<int, DataModels.Character> result = 
                new Dictionary<int, DataModels.Character>();

            foreach(var character in characters)
            {
                result.Add(character.EveId, character);
            }
            return result;
        }
    }
}
