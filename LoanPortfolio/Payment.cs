using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPortfolio
{
    class Payment
    {
        public int PmtNumber { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPmt { get { return Principal + Interest; } }
    }
}
