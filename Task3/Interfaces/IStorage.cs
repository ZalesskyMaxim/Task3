using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneExchange;

namespace Task3.Interfaces
{
    public interface IStorage<T>
    {
        IList<T> GetInfoList();
    }
}
