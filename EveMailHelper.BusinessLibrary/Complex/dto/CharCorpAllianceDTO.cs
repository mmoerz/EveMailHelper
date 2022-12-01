using EveMailHelper.BusinessLibrary.Utilities;
using EveMailHelper.DataModels;

namespace EveMailHelper.BusinessLibrary.Complex.dto
{
    public class CharCorpAllianceDTO
    {
        public CharCorpAllianceDTO() 
        {
            CharactersDD = new();
            CorporationsDD = new();
            AlliancesDD = new();
        }
        public EveDownloaderData<EVEStandard.Models.CharacterInfo, Character> CharactersDD { get; set; }
        public EveDownloaderData<EVEStandard.Models.CorporationInfo, Corporation> CorporationsDD { get; set; }
        public EveDownloaderData<EVEStandard.Models.Alliance, Alliance> AlliancesDD { get; set; }
    }
}
