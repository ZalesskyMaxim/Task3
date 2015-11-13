using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneExchange;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ATS ats = new ATS();
            Contract c1 = ats.RegisterContract(new Subscriber("Vasia", "Pupkin"), Enums.TariffType.Light);
            Contract c2 = ats.RegisterContract(new Subscriber("Dima", "Pupkin"), Enums.TariffType.Light);
            Contract c3 = ats.RegisterContract(new Subscriber("Petya", "Pupkin"), Enums.TariffType.Light);
            var t1 = ats.GetNewTerminal(c1);
            var t2 = ats.GetNewTerminal(c2);
            var t3 = ats.GetNewTerminal(c3);
            t1.ConnectToPort();
            t2.ConnectToPort();
            t3.ConnectToPort();
            t1.Call(t2.Number);
            t2.AnswerToCall(t1.Number, Enums.CallState.Rejected);
            t2.Call(t3.Number);
            t3.AnswerToCall(t2.Number, Enums.CallState.Answered);
            t2.Call(t2.Number);
            t2.Call(1234569);
            Console.ReadKey();
            

        }
    }
}
