using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Args
{
    public class EndCallEventArgs : EventArgs, ICallingEventArgs
    {
        public Guid Id { get; private set; }
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        //public EndCallEventArgs(Guid id, int number, int target)
        //{
        //    Id = id;
        //    TelephoneNumber = number;
        //    TargetTelephoneNumber = target;
        //}
        public EndCallEventArgs(Guid id, int number)
        {
            Id = id;
            TelephoneNumber = number;
        }
    }
}
