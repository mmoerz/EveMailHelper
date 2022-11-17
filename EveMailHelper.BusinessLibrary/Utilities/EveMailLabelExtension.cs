using EveMailHelper.DataModels;

using Microsoft.Data.SqlClient.DataClassification;

namespace EveMailHelper.BusinessDataAccess.Utilities
{
    public static class EveStandardModelsTranslation
    {
        public static EveMailLabel CopyFrom(this EveMailLabel label, EVEStandard.Models.MailLabel copyFrom)
        {
            label.EveLabelId = copyFrom.LabelId;
            label.Name = copyFrom.Name;
            label.Color = copyFrom.Color;
            label.UnreadCount = copyFrom.UnreadCount;
            return label;
        }

        public static EveMail BasicCopyFrom(this EveMail mail, EVEStandard.Models.Mail copyFrom)
        {
            mail.Subject = copyFrom.Subject;
            mail.CreatedDate = copyFrom.Timestamp ?? DateTime.Now;
            mail.IsRead = copyFrom.IsRead ?? false;
            mail.EveMailId =  copyFrom.MailId;
            return mail;
        }
    }
}
