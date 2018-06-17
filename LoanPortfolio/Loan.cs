using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPortfolio
{
    class Loan
    {
        public string Description { get; set; }
        public DateTime FirstPmtDate { get; set; }
        public decimal Principal { get; set; }
        public decimal PmtAmount { get; set; }
        public decimal InterestRate { get; set; }

    }
}
