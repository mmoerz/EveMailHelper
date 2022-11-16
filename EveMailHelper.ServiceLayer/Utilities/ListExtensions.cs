using EveMailHelper.DataModels;
using EveMailHelper.ServiceLayer.ModelExtensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class ListExtensions
    {
        public static ICollection<DataModels.EveMailLabel> toEveMailLabelList
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
    }
}
