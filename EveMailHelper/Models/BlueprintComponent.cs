namespace EveMailHelper.Web.Models
{
    public class BlueprintComponent
    {
        public string Name { get; set; } = null!;
        public float Volume { get; set; }
        public int Quantity { get; set; }
        public float PricePerUnit { get; set; }
        public float PriceSum { get; set; }
    }
}
