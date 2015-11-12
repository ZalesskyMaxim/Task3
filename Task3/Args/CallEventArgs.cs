using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallEventArgs : EventArgs
    {
        public readonly int telephoneNumber;
        public readonly int targetTelephoneNumber;

        public CallEventArgs(int number, int target)
        {
            telephoneNumber = number;
            targetTelephoneNumber = target;
        }
    }
}
