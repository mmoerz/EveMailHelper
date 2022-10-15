using EveMailHelper.BusinessLibrary.Complex.dbAccess;
using EveMailHelper.BusinessLibrary.Complex.dto;
using EveMailHelper.DataAccessLayer.Models;

using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessLibrary.Complex
{
    public class SendEveMailBasedOnTemplateAction : IBizActionAsync<SendTemplateToDto, EveMail>
    {
        readonly EveMailDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public SendEveMailBasedOnTemplateAction(EveMailDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public async Task<EveMail> ActionAsync(SendTemplateToDto dto)
        {
            dto.Template.CreateMail(out EveMail eveMail);
            foreach (var receiver in dto.Characters)
            {
                eveMail.SentTo.Add(new EveMailSentTo() { Character = receiver });
            }
            await _dbAccess.AddAsync(eveMail);
            return eveMail;
        }
    }
}
