﻿using System;
using System.Collections.Generic;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryBlueprint : IBaseEveObject
    {
        //public IndustryBlueprint() 
        //{
        //    IndustryActivities = new HashSet<IndustryActivity>();
        //}

        public int TypeId { get; set; }
        public int? MaxProductionLimit { get; set; }

        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;

        public virtual EveType Type { get; set; } = null!;
        //public virtual  ICollection<IndustryActivity> IndustryActivities { get; set;}
    }
}
