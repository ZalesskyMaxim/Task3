using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Args
{
    public interface ICallingEventArgs
    {
        int TelephoneNumber { get; }
        int TargetTelephoneNumber { get; }
        int? Id { get; }
    }
}
