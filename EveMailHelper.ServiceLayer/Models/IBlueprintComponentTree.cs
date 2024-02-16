namespace EveMailHelper.ServiceLayer.Models
{
    public interface IBlueprintComponentTreeNode
    {

        void Add(BlueprintComponent component);

        public IList<IBlueprintComponentTreeNode> SubComponents { get; set; }
    }
}