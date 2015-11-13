using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;

namespace Task3.Args
{
    public class CallEventArgs : EventArgs, ICallingEventArgs
    {
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        public int? Id { get; private set; }
        public CallEventArgs(int number, int target)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
        }
        public CallEventArgs(int number, int target, int id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            Id = id;
        }
    }
}
