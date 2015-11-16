using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.AutomaticTelephoneExchange;
using Task3.Enums;
using Task3.Interfaces;

namespace Task3.BillingSystem
{
    public class BillingSystem : IBillingSystem
    {
        private IStorage<CallInformation> _storage;
        //private IList<CallInformation> _callList;
        public BillingSystem(IStorage<CallInformation> storage)
        {
            _storage = storage;
        }

        public Report GetReport(int telephoneNumber)
        {
            var calls = _storage.GetInfoList().
                Where(x => x.MyNumber == telephoneNumber || x.TargetNumber == telephoneNumber).
                ToList();
            var report = new Report(); 

            foreach(var call in calls)
            {
                CallType callType;
                int number;
                if(call.MyNumber == telephoneNumber)
                {
                    callType = CallType.OutgoingCall;
                    number = call.TargetNumber;
                }
                else
                {
                    callType = CallType.IncomingCall;
                    number = call.MyNumber;
                }
                var record = new ReportRecord(callType, number, call.BeginCall, new DateTime((call.EndCall - call.BeginCall).Ticks), call.Cost); // TimeSpan.FromTicks((call.EndCall - call.BeginCall).Ticks) .TotalMinutes  
                report.AddRecord(record);
            }
            return report;
        }

    }
}
