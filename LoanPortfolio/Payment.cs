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

        public decimal CurrentInterest { get; set; }
        public decimal CurrentPrincipal { get; set; }
        public decimal CurrentPayment { get { return CurrentPrincipal + CurrentInterest; } }

        public decimal TotalPrincipal { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalPayment { get; set; }
    }
}
