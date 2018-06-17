using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPortfolio
{
    class Loan
    {
        const int NumMonthsInYear = 12;

        public string Description { get; set; }
        public DateTime FirstPmtDate { get; set; }
        public decimal Principal { get; set; }
        public decimal PmtAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyInterestRate { get { return InterestRate / NumMonthsInYear; } }
        public decimal TotalPrincipalPaid { get { return Payments.Sum(p => p.Principal); } }

        IList<Payment> Payments { get; set; }

        public Loan(LoanInput loanInput)
        {
            Description = loanInput.Description;
            FirstPmtDate = loanInput.FirstPmtDate;
            Principal = loanInput.Principal;
            PmtAmount = loanInput.PmtAmount;
            InterestRate = loanInput.InterestRate;

            decimal remainingPrincipal = Principal;
            decimal interestPaid = 0;
            int pmtNumber = 0;

            Payments = new List<Payment>();
            do
            {
                pmtNumber++;

                

                var p = new Payment();
                p.PmtNumber = pmtNumber;
                p.Interest = remainingPrincipal * MonthlyInterestRate;


                p.Principal = PmtAmount - p.Interest;
                //if (pmtNumber == 1) { p.Principal += 300; }


                interestPaid += p.Interest;
                remainingPrincipal -= p.Principal;

                Payments.Add(p);


            } while (remainingPrincipal > 0);

        }


    }
}
