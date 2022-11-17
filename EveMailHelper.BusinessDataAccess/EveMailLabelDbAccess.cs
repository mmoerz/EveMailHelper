using EveMailHelper.DataAccessLayer.Context;
using EveMailHelper.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.BusinessDataAccess
{
    public class EveMailLabelDbAccess
    {
        private readonly EveMailHelperContext _context;
        public EveMailLabelDbAccess(EveMailHelperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EveMailLabel eveMailLabel)
        {
            await _context.EveMailLabels.AddAsync(eveMailLabel);
        }

        public EveMailLabel Update(EveMailLabel eveMailLabel)
        {
            var foundLabels = _context.EveMailLabels
                .Where(x => x.EveLabelId == eveMailLabel.EveLabelId);

            EveMailLabel result = eveMailLabel;
            if (foundLabels.Any())
                result = foundLabels.First();

            return _context.EveMailLabels
                .Update(result)
                .Entity;
        }


    }
}
