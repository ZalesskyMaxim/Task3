using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;
using Task3.Interfaces;

namespace Task3
{
    public class Contract : IContract
    {
        static Random rnd = new Random();

        public Subscriber Subscriber { get; private set; }
        public int Number { get; private set; }
        public Tariff Tariff { get; private set; }
        private DateTime LastTariffUpdateDate;
        

        public Contract(Subscriber subscriber, TariffType tariffType)
        {
            LastTariffUpdateDate = DateTime.Now;
            Subscriber = subscriber;
            Number = rnd.Next(1000000, 9999999);
            Tariff = new Tariff(tariffType);
        }

        public bool ChangeTariff(TariffType tariffType)
        { 
            if(DateTime.Now.AddMonths(-1) >= LastTariffUpdateDate)
            {
                LastTariffUpdateDate = DateTime.Now;
                Tariff = new Tariff(tariffType);
                Console.WriteLine("Tariff has changed!");
                return true;
            }
            else
            {
                Console.WriteLine("Цait until the end of the month!");
                return false;
            }
            
        }
    }
}
