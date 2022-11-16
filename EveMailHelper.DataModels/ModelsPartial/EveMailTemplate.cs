namespace EveMailHelper.DataModels
{
    public partial class EveMailTemplate
    {
        public void CreateMail(out EveMail eveMail, Character character)
        {
            eveMail = new EveMail
            {
                From = character,
                Subject = Subject,
                Content = Content
            };
        }
    }
}
