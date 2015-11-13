using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3
{
    public class Contract
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public Tariff Tariff { get; set; }
        public ATS Ats;

        public Contract(string name, int number, TariffType tariffType, ATS ats)
        {
            Name = name;
            Number = number;
            Tariff = new Tariff(tariffType);
            Ats = ats;
        }

        public Terminal RegisterContract()
        {
            return Ats.GetNewTerminal(Number);
        }

    }
}
