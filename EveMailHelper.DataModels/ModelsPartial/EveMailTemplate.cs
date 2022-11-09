namespace EveMailHelper.DataModels
{
    public partial class EveMailTemplate
    {
        public void CreateMail(out EveMail eveMail)
        {
            eveMail = new EveMail
            {
                Subject = Subject,
                Content = Content
            };
        }
    }
}
