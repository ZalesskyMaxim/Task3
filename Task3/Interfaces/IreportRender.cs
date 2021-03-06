﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.BillingSystem;
using Task3.Enums;

namespace Task3.Interfaces
{
    public interface IReportRender
    {
        void Render(Report report);
        IEnumerable<ReportRecord> SortCalls(Report report, TypeSort sortType);
    }
}
