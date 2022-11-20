namespace EveMailHelper.DataModels
{
    public partial class EveMailTemplate
    {
        public void CreateMail(out Mail eveMail, Character character)
        {
            eveMail = new Mail
            {
                From = character,
                Subject = Subject,
                Content = Content
            };
        }
    }
}
