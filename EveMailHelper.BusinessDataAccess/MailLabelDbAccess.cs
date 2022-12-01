using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess
{
    public class MailLabelDbAccess
    {
        private readonly EveMailHelperContext _context;
        public MailLabelDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MailLabel eveMailLabel)
        {
            await _context.MailLabels.AddAsync(eveMailLabel);
        }

        public MailLabel Update(MailLabel eveMailLabel)
        {
            var foundLabels = _context.MailLabels
                .Where(x => x.EveLabelId == eveMailLabel.EveLabelId);

            MailLabel result = eveMailLabel;
            if (foundLabels.Any())
                result = foundLabels.First();

            return _context.MailLabels
                .Update(result)
                .Entity;
        }


    }
}
