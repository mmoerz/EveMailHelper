namespace EveMailHelper.DataModels
{
    /// <summary>
    /// different states a character can be in regards to 
    /// recruitment (and the corporation)
    /// </summary>
    public enum CharacterStatus
    {
        None,
        Newbie,
        InRecruitment,
        CorpMember,
        WasInRecruitment,
        WasCorpMember
    }
}
