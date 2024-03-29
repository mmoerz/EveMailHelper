﻿using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels
{
    public partial class Mail : IBaseEveLongId
    {
        public Mail()
        {
            SentTo = new HashSet<EveMailSentTo>();
            Recipients = new HashSet<MailRecipient>();
            Labels = new HashSet<MailLabel>();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid FromId { get; set; }
        public Guid OwnerId { get; set; }

        /// <summary>
        /// this id comes from EvE (Rest API)
        /// </summary>
        public long? EveId { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public Guid? EveMailTemplateId { get; set; }
        public virtual EveMailTemplate? EveMailTemplate { get; set; } = null!;

        public Character From { get; set; } = null!;
        public Character Owner { get; set; } = null!;
        public virtual ICollection<EveMailSentTo> SentTo { get; set; }
        public virtual ICollection<MailRecipient> Recipients { get; set; }
        public virtual ICollection<MailLabel> Labels { get; set; }
    }
}
