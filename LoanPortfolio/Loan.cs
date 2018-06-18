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
        public decimal PrincipalBorrowed { get; set; }
        public decimal MinPmtAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyInterestRate { get { return InterestRate / NumMonthsInYear; } }
        IDictionary<string, IList<Payment>> PaymentScenarios { get; set; }

        public Loan(LoanInput loanInput)
        {
            //TODO: automapper for basic things
            Description = loanInput.Description;
            FirstPmtDate = loanInput.FirstPmtDate;
            PrincipalBorrowed = loanInput.Principal;
            MinPmtAmount = loanInput.PmtAmount;
            InterestRate = loanInput.InterestRate;
            PaymentScenarios = new Dictionary<string, IList<Payment>>();

            decimal amtPaid;
            do
            {
                amtPaid = MakePayment(MinPmtAmount, "Minumum Payments");
            } while (amtPaid > 0);
        }

        public bool CheckCompletion(string scenarioName)
        {
            //get the list of payments (create first if necessary)
            if (!PaymentScenarios.ContainsKey(scenarioName))
            {
                throw new ArgumentOutOfRangeException();
            }
            var payments = PaymentScenarios[scenarioName];
            return payments.Min(p => p.TotalPrincipal) == 0;
        }
        public decimal MakePayment(decimal maxPmtAmount, string scenarioName)
        {
            //get the list of payments (create first if necessary)
            if (!PaymentScenarios.ContainsKey(scenarioName))
            {
                PaymentScenarios.Add(scenarioName, new List<Payment>());
            }
            var payments = PaymentScenarios[scenarioName];

            //get the last payment or a pseudo "zero-ith" payment if none yet exist
            var lastPayment =
                payments.OrderBy(p => p.PmtNumber).LastOrDefault() ??
                new Payment()
                {
                    PmtNumber = 0,
                    CurrentPrincipal = 0,
                    CurrentInterest = 0,
                    TotalPrincipal = PrincipalBorrowed,
                    TotalPayment = 0,
                    TotalInterest = 0
                };

            //decide whether to make another payment
            if (lastPayment.TotalPrincipal == 0)
            {
                return 0;
            }
            if (lastPayment.TotalPrincipal < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            //create tne new payment and calculate interest
            var payment = new Payment()
            {
                CurrentInterest = lastPayment.TotalPrincipal * MonthlyInterestRate,
                PmtNumber = lastPayment.PmtNumber + 1
            };

            //pay as much as possible (or all) remaining principal
            if (maxPmtAmount < lastPayment.TotalPrincipal + payment.CurrentInterest)
            {
                payment.CurrentPrincipal = maxPmtAmount - payment.CurrentInterest;
            }
            else
            {
                payment.CurrentPrincipal = lastPayment.TotalPrincipal;
            }

            //update running totals
            payment.TotalInterest = lastPayment.TotalInterest + payment.CurrentInterest;
            payment.TotalPrincipal = lastPayment.TotalPrincipal - payment.CurrentPrincipal;
            payment.TotalPayment = lastPayment.TotalPayment + payment.CurrentPayment;

            //add to list of payments
            payments.Add(payment);

            return payment.CurrentPayment;
        }
    }
}
