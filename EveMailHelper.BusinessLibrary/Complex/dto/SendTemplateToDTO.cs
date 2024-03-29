﻿using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class SendTemplateToDTO
    {
        //public Guid templateId;
        public EveMailTemplate Template { get; set; } = null!;
        public Character FromCharacter { get; set; } = null!; 
        public ICollection<Character> Characters { get; set; } = null!;
        //public ICollection<string> receiverNames = null!;
    }
}
