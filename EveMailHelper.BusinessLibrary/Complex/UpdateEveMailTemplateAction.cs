using EveMailHelper.BusinessLibrary.Complex.dbAccess;
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
    public class UpdateEveMailTemplateAction : IBizAction<EveMailTemplate, EveMailTemplate>
    {
        readonly EveMailTemplateDbAccess _dbAccess;
        private List<ValidationResult> _errors = new();

        public UpdateEveMailTemplateAction(EveMailTemplateDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public IImmutableList<ValidationResult> Errors => _errors.ToImmutableList();

        public bool HasErrors => _errors.Any();

        public EveMailTemplate Action(EveMailTemplate dto)
        {
            _ = dto ?? throw new ArgumentNullException(nameof(dto));

            _dbAccess.Update(dto);

            return dto;
        }
    }
}
