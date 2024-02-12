namespace EveMailHelper.ServiceLayer.Models
{
    public interface IBlueprintComponentTree
    {
        int ProductionDepth { get; }

        void Add(BlueprintComponent component);

        public IList<BlueprintComponent> SubComponents { get; set; }
    }
}