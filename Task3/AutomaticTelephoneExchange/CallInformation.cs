using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.AutomaticTelephoneExchange
{
    public class CallInformation
    {
        public int Id { get; set; }
        public int MyNumber { get; set; }
        public int TargetNumber { get; set; }
        public DateTime Date { get; set; }
        public int Cost { get; set; }

    }
}
