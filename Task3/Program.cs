using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ATS ats = new ATS();
            Contract c1 = new Contract("vvv", 1234567, Enums.TariffType.Light, ats);
            Contract c2 = new Contract("www", 7654321, Enums.TariffType.Light, ats);
            Contract c3 = new Contract("ccc", 5479222, Enums.TariffType.Light, ats);
            var t1 = c1.RegisterContract();
            var t2 = c2.RegisterContract();
            var t3 = c3.RegisterContract();
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
