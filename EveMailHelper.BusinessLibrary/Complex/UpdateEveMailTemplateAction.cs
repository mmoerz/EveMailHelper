﻿using FluentValidation.Results;

using System.Collections.Immutable;

using EveMailHelper.BusinessDataAccess;
using EveMailHelper.DataModels;

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
