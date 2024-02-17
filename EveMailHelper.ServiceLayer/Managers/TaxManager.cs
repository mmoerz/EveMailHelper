using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveMailHelper.ServiceLayer.Interfaces;

using EVEStandard.Models;

namespace EveMailHelper.ServiceLayer.Managers
{
    public class TaxManager : ITaxManager
    {
        public TaxManager() { }

        public double GetBrokerTaxPercent(
             double BrokerRelationsLevel, double FactionStanding, double CorpStanding)
        {
            return 3.0 - (0.3 * BrokerRelationsLevel) - (0.03 * FactionStanding) - (0.02 * CorpStanding);
        }

        public double CalculateBrokerTax(
            double isk, double BrokerRelationsLevel, double FactionStanding, double CorpStanding)
        {
            return isk / 100 * GetBrokerTaxPercent(BrokerRelationsLevel, FactionStanding, CorpStanding);
        }

        public double AddBrokerTax(
            double isk, double BrokerRelationsLevel, double FactionStanding, double CorpStanding)
        {
            return isk + CalculateBrokerTax(isk, BrokerRelationsLevel, FactionStanding, CorpStanding);
        }

        public double GetSalesTaxPercent(int AccountSkillLevel)
        {
            if (AccountSkillLevel < 0)
                throw new Exception("Accountskill cannot be negative");
            if (AccountSkillLevel > 5)
                throw new Exception("Accountskill cannot be greater than 5");
            return 8 - 0.88 * AccountSkillLevel;
        }

        public double CalculateSalesTax(double isk, int AccountSkillLevel)
        {
            return isk / 100 * GetSalesTaxPercent(AccountSkillLevel);
        }

        public double AddSalesTax(double isk, int AccountSkillLevel)
        {
            return isk + CalculateSalesTax(isk, AccountSkillLevel);
        }
    }
}
