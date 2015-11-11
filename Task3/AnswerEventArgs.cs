using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3
{
    public class AnswerEventArgs : EventArgs
    {
        public readonly int telephoneNumber;
        public readonly int targetTelephoneNumber;
        public CallState StateInCall;
        public AnswerEventArgs(int number, int target, CallState state)
        {
            telephoneNumber = number;
            targetTelephoneNumber = target;
            StateInCall = state;
        }


    }
}
