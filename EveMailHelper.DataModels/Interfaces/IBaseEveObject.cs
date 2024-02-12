using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataModels.Interfaces
{
    /// <summary>
    /// will become obsolete when converting to UpdateableEveObjects
    /// </summary>
    public interface IBaseEveObject
    {
        /// <summary>
        /// Last fetched from EvE Game at this point of time.
        /// (UTC)
        /// </summary>
        public DateTime EveLastUpdated { get; set; }
        /// <summary>
        /// set to true if item was deleted in game.
        /// </summary>
        public bool EveDeletedInGame { get; set; }
    }
}
