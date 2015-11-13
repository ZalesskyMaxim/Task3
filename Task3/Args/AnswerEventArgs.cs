using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.Enums;

namespace Task3.Args
{
    public class AnswerEventArgs : EventArgs, ICallingEventArgs
    {
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        public CallState StateInCall;
        public int? Id { get; private set; }
        public AnswerEventArgs(int number, int target, CallState state)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
        }
        public AnswerEventArgs(int number, int target, CallState state, int id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
            Id = id;
        }


    }
}
