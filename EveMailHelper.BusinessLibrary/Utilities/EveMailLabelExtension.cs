using EveMailHelper.DataModels;

using Microsoft.Data.SqlClient.DataClassification;

namespace EveMailHelper.BusinessDataAccess.Utilities
{
    public static class EveStandardModelsTranslation
    {
        public static MailLabel CopyFrom(this MailLabel label, EVEStandard.Models.MailLabel copyFrom)
        {
            label.EveLabelId = copyFrom.LabelId;
            label.Name = copyFrom.Name;
            label.Color = copyFrom.Color;
            label.UnreadCount = copyFrom.UnreadCount;
            return label;
        }

        public static Mail BasicCopyFrom(this Mail mail, EVEStandard.Models.Mail copyFrom)
        {
            mail.Subject = copyFrom.Subject;
            mail.CreatedDate = copyFrom.Timestamp ?? DateTime.Now;
            mail.IsRead = copyFrom.IsRead ?? false;
            mail.EveId =  copyFrom.MailId;
            return mail;
        }

        public static string ToString(this ISet<MailLabel> mailLabels, string delimiter=",")
        {
            _ = delimiter ?? throw new ArgumentNullException(nameof(delimiter));

            return string.Join(delimiter, mailLabels);
        }
    }
}
