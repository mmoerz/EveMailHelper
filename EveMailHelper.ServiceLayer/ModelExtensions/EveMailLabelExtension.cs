using EveMailHelper.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.ServiceLayer.ModelExtensions
{
    public static class EveMailLabelExtension
    {
        public static EveMailLabel CopyFrom(this EveMailLabel label, EVEStandard.Models.MailLabel copyFrom)
        {
            label.EveLabelId = copyFrom.LabelId;
            label.Name = copyFrom.Name;
            label.Color = copyFrom.Color;
            label.UnreadCount = copyFrom.UnreadCount;
            return label;
        }
    }
}
