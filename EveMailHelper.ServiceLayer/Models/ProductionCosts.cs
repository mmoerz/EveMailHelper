namespace EveMailHelper.ServiceLayer.Models
{
    public class ProductionCosts
    {
        /// <summary>
        /// sum of all jobcosts of the production
        /// </summary>
        public double JobCosts { get; set; }
        /// <summary>
        /// sum of all component costs of the production
        /// </summary>
        public double ComponentCosts { get; set; }

        public double SumOfCosts()
        {
            return JobCosts + ComponentCosts;
        }
    }
}