using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3.BillingSystem
{
    public class Report
    {
        private IList<ReportRecord> _listRecords;

        public Report()
        {
            _listRecords = new List<ReportRecord>();
        }

        public void AddRecord(ReportRecord record)
        {
            _listRecords.Add(record);
        }
        public IList<ReportRecord> GetRecords()
        {
            return _listRecords;
        }
    }
}
