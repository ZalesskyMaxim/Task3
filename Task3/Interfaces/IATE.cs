using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.AutomaticTelephoneExchange;
using Task3.Enums;

namespace Task3.Interfaces
{
    public interface IATE : IStorage<CallInformation>
    {
        Terminal GetNewTerminal(IContract contract);
        IContract RegisterContract(Subscriber subscriber, TariffType type);
        void CallingTo(object sender, ICallingEventArgs e);
    }
}
