using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace LoanPortfolio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Loan Portfilio Application");

            //get incoming loan objects
            const string fileName = @"C:\Users\vince\Downloads\Bills Google Sheet - Loan Portfolio.csv";
            Console.WriteLine($"Using {fileName}");

            //convert to complete loan with all details
            var engine = new FileHelperEngine<LoanInput>();
            var result = engine.ReadFile(fileName);
            var loansList = result.Select(il => new Loan(il));

            //if this is an enumerable, it's a QUERY
            //changes made within the foreach loop don't stay saved
            var loansListByRate = loansList.OrderByDescending(l => l.InterestRate).ToList();

            decimal extraMoney = 100;
            var scenarioName = "$100";

            do
            {
                foreach (var l in loansListByRate)
                {
                    var amountToPay = l.MinPmtAmount + extraMoney;
                    var amountPaid = l.MakePayment(amountToPay, scenarioName);
                }
            } while (loansListByRate.Where(l => l.CheckCompletion(scenarioName) == false).Count() > 0);


            //show results

        }

    }
}
