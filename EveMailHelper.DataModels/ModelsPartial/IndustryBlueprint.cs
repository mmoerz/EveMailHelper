using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

using EveMailHelper.DataModels.Interfaces;

namespace EveMailHelper.DataModels.Sde
{
    public partial class IndustryBlueprint 
    {
        public void CopyShallow(IndustryBlueprint blueprint)
        {
            TypeId = blueprint.TypeId;
            MaxProductionLimit = blueprint.MaxProductionLimit;
            EveLastUpdated = blueprint.EveLastUpdated;
            EveDeletedInGame = blueprint.EveDeletedInGame;
        }
    }
}
