using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Utilities
{
    public static class ModelExtensions
    {
        public static Character CopyShallow(this Character character, EVEStandard.Models.CharacterInfo info)
        {
            character.Name = info.Name;
            character.Description = info.Description;
            character.Birthday = info.Birthday;
            character.SecurityStatus= info.SecurityStatus;
            character.Title= info.Title;
            
            return character; 
        }

        public static Corporation CopyShallow(this Corporation corp, int eveId, EVEStandard.Models.CorporationInfo corpInfo)
        {
            corp.Name = corpInfo.Name;
            corp.EveId = eveId;
            corp.Ticker = corpInfo.Ticker;
            corp.DateFounded= corpInfo.DateFounded;
            corp.Description = corpInfo.Description;
            corp.MemberCount= corpInfo.MemberCount;
            corp.Shares= corpInfo.Shares;
            corp.TaxRate= corpInfo.TaxRate;
            corp.Url= corpInfo.Url;
            corp.WarEligible = corpInfo.WarEligible;

            return corp;
        }

        public static Alliance CopyShallow(this Alliance alliance, int eveId, EVEStandard.Models.Alliance eveAlliance)
        {
            alliance.Name = eveAlliance.Name;
            alliance.EveId= eveId;
            alliance.Ticker = eveAlliance.Ticker;
            alliance.DateFounded = eveAlliance.DateFounded;
            // TODO: Rest is missing
            return alliance;
        }
    }
    
}
