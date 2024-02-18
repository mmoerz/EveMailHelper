namespace EveMailHelper.ServiceLayer.Interfaces
{
    public interface ITaxManager
    {
        double AddBrokerTax(double isk, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double AddSalesTax(double isk, int AccountSkillLevel);
        double CalculateBrokerTax(double isk, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double CalculateImmediateSellTaxes(double isk, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double CalculateSalesTax(double isk, int AccountSkillLevel);
        double CalculateSellOrderTaxes(double isk, int AccountSkillLevel, int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double GetBrokerTaxPercent(int BrokerRelationsLevel, double FactionStanding, double CorpStanding);
        double GetSalesTaxPercent(int AccountSkillLevel);
    }
}