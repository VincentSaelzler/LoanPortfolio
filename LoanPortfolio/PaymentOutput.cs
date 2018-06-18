using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanPortfolio
{
    [DelimitedRecord(",")]
    class PaymentOutput
    {
        public string Description { get; set; }
        [FieldConverter(ConverterKind.Date, "d")]
        public DateTime FirstPmtDate { get; set; }
        public decimal PrincipalBorrowed { get; set; }
        public decimal MinPmtAmount { get; set; }
        public decimal InterestRate { get; set; }

        public string PaymentScenarioDescription { get; set; }

        public int PmtNumber { get; set; }
        public decimal CurrentInterest { get; set; }
        public decimal CurrentPrincipal { get; set; }
        public decimal CurrentPayment { get; set; }
        public decimal TotalPrincipal { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalPayment { get; set; }

        public PaymentOutput(Loan loan, KeyValuePair<string, IList<Payment>> kvp, Payment payment)
        {
            Description = loan.Description;
            FirstPmtDate = loan.FirstPmtDate;
            PrincipalBorrowed = loan.PrincipalBorrowed;
            MinPmtAmount = loan.MinPmtAmount;
            InterestRate = loan.InterestRate;

            PaymentScenarioDescription = kvp.Key;

            PmtNumber = payment.PmtNumber;
            CurrentInterest = payment.CurrentInterest;
            CurrentPrincipal = payment.CurrentPrincipal;
            TotalPrincipal = payment.TotalPrincipal;
            TotalInterest = payment.TotalInterest;
            TotalPayment = payment.TotalPayment;

            CurrentPayment = CurrentPrincipal + CurrentInterest;
        }
        public PaymentOutput()
        { }
    }
}
