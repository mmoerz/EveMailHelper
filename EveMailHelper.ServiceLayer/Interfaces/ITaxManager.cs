namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface ITaxManager
    {
        double AddBrokerTax(double isk, double BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double AddSalesTax(double isk, int AccountSkillLevel);
        double CalculateBrokerTax(double isk, double BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double CalculateSalesTax(double isk, int AccountSkillLevel);
        double GetBrokerTaxPercent(double BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double GetSalesTaxPercent(int AccountSkillLevel);
    }
}