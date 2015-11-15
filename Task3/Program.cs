using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneExchange;
using Task3.BillingSystem;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            ATE ate = new ATE();
            Contract c1 = ate.RegisterContract(new Subscriber("Vasia", "Pupkin"), Enums.TariffType.Light);
            c1.Subscriber.AddMoney(10);
            Contract c2 = ate.RegisterContract(new Subscriber("Dima", "Pupkin"), Enums.TariffType.Light);
            Contract c3 = ate.RegisterContract(new Subscriber("Petya", "Pupkin"), Enums.TariffType.Light);
            ReportRender render = new ReportRender();
            BillingSystem.BillingSystem bs = new BillingSystem.BillingSystem(ate);
            var t1 = ate.GetNewTerminal(c1);
            var t2 = ate.GetNewTerminal(c2);
            var t3 = ate.GetNewTerminal(c3);
            t1.ConnectToPort();
            t2.ConnectToPort();
            t3.ConnectToPort();
            t1.Call(t2.Number);
            //t2.AnswerToCall(t1.Number, Enums.CallState.Rejected);
            Thread.Sleep(2000);
            t1.EndCall();
            t2.Call(t3.Number);
            //t3.AnswerToCall(t2.Number, Enums.CallState.Answered);
            t3.EndCall();
            t2.Call(t2.Number);
            t2.Call(1234569);
            
            render.Render(bs.GetReport(t1.Number));

            Console.ReadKey();
            

        }
    }
}
