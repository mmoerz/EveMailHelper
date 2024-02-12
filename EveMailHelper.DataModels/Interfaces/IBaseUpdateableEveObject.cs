using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Interfaces
{
    public interface IBaseUpdateableEveObject
    {
        /// <summary>
        /// Last fetched from EvE Game at this point of time.
        /// (UTC)
        /// </summary>
        public DateTime LastUpdatedFromEve { get; set; }
    }
}
