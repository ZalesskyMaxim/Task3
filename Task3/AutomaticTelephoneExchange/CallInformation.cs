using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.AutomaticTelephoneExchange
{
    public class CallInformation
    {
        public Guid Id { get; set; }
        public int MyNumber { get; set; }
        public int TargetNumber { get; set; }
        public DateTime BeginCall { get; set; }
        public DateTime EndCall { get; set; }
        public int Cost { get; set; }

        public CallInformation(int myNumber, int targetNumber, DateTime beginCall)
        {
            Id = Guid.NewGuid();
            MyNumber = myNumber;
            TargetNumber = targetNumber;
            BeginCall = beginCall;
        }

    }
}
