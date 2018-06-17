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



            var engine = new FileHelperEngine<LoanInput>();
            var result = engine.ReadFile(fileName);

            IList<LoanInput> loanslist = result.ToList();


            //convert to complete loan with all details

            //show results

        }

    }
}
