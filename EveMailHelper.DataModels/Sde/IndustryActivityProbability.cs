using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryActivityProbability
    {
        public int TypeId { get; set; }
        public int ActivityId { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Probability { get; set; }

        public virtual IndustryActivity IndustryActivity { get; set; }
        public virtual InvType ProductType { get; set; }
    }
}
