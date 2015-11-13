using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3
{
    public class Tariff
    {
        public readonly int CostOfMonth;
        public readonly int CostOfMinutes;
        public TariffType TariffType { get; private set; }
        public Tariff(TariffType type)
        {
            TariffType = type;
            switch (TariffType)
            {
                case Enums.TariffType.Light:
                    {
                        CostOfMonth = 10;
                        CostOfMinutes = 1;
                        break;
                    }
                case Enums.TariffType.Standart :
                    {
                        CostOfMonth = 20;
                        CostOfMinutes = 2;
                        break;
                    }
                case Enums.TariffType.Pro :
                    {
                        CostOfMonth = 30;
                        CostOfMinutes = 3;
                        break;
                    }
                default :
                    {
                        CostOfMonth = 0;
                        CostOfMinutes = 0;
                        break;
                    }
            }
        }
    }
}
