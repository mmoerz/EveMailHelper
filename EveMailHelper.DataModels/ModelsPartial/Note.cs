namespace EveMailHelper.DataModels
{
    public partial class Note
    {
        public void CopyShallow(Note note)
        {
            Id = note.Id;
            Title = note.Title;
            Content = note.Content;
            CreatedDate = note.CreatedDate;
            AttachedTo = note.AttachedTo;
            AttachedToId = note.AttachedToId;
        }
    }
}
