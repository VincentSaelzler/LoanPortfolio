using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanPortfolio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Loan Portfilio Application");

            //get incoming loan objects
            const string inFileName = @"C:\Users\vince\Downloads\Bills Google Sheet - Loan Portfolio.csv";
            const string outFileName = @"C:\Users\vince\Downloads\Loan Payments.csv";

            Console.WriteLine($"Using {inFileName}");

            //convert to complete loan with all details
            var inEngine = new FileHelperEngine<LoanInput>();
            var result = inEngine.ReadFile(inFileName);

            //if this is an enumerable, it's a QUERY
            //changes made within the foreach loop don't stay saved
            var loansList = result.Select(il => new Loan(il)).ToList();
            var loansToPayByRate = loansList.OrderByDescending(l => l.InterestRate).ToList();

            decimal extraMoney = 100;
            var scenarioName = "$100";

            do
            {
                foreach (var l in loansToPayByRate)
                {
                    var amountToPay = l.MinPmtAmount;
                    var loanPriority = loansToPayByRate.IndexOf(l);
                    if (loanPriority == 0)
                    {
                        amountToPay += extraMoney;
                    }
                    var amountPaid = l.MakePayment(amountToPay, scenarioName);
                }
                loansToPayByRate = loansToPayByRate.Where(l => l.CheckCompletion(scenarioName) == false).ToList();
            } while (loansToPayByRate.Count > 0);

            //show results

            IList<PaymentOutput> paymentOutput = new List<PaymentOutput>();

            foreach (var l in loansList)
            {
                foreach (var kvp in l.PaymentScenarios)
                {
                    foreach (var p in kvp.Value)
                    {
                        paymentOutput.Add(new PaymentOutput(l, kvp, p));
                    }
                }
            }

            var outEngine = new FileHelperEngine<PaymentOutput>();
            outEngine.HeaderText = outEngine.GetFileHeader();
            outEngine.WriteFile(outFileName, paymentOutput);
        }

    }
}
