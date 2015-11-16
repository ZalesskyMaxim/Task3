using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;
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
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    record.CallType, record.Date, record.Time.ToString("mm:ss"), record.Cost, record.Number);
            }
        }
        public IEnumerable<ReportRecord> SortCalls(Report report, TypeSort sortType)
        {
            var rep = report.GetRecords();
            switch (sortType)
            {
                case TypeSort.SortByCallType: 
                    return rep = rep.
                        OrderBy(x => x.CallType).
                        ToList(); 

                case TypeSort.SortByDate:
                    return rep = rep.
                        OrderBy(x => x.Date).
                        ToList(); 

                case TypeSort.SortByCost:
                    return rep = rep
                        .OrderBy(x => x.Cost)
                        .ToList();

                case TypeSort.SortByNumber:
                    return rep = rep.
                        OrderBy(x => x.Number).
                        ToList(); 

                default:
                    return rep;
            }
        }
    }
}
