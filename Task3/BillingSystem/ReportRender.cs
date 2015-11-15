using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Interfaces;

namespace Task3.BillingSystem
{
    public class ReportRender : IReportRender
    {
        public ReportRender()
        {

        }
        public void Render(Report report)
        {
            foreach(var record in report.GetRecords())
            {
                Console.WriteLine("Call: {0} | Date: {1} | Duration: {2} | Cost: {3} | Telephone number: {4}",
                    record.CallType, record.Date, record.Time, record.Cost, record.Number);
            }
        }
    }
}
