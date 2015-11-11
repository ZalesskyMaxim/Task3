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
            var t1 = ats.GetNewTerminal();
            var t2 = ats.GetNewTerminal();
            t1.ConnectToPort();
            t2.ConnectToPort();
            t1.Call(t2.Number);
        }
    }
}
