using FileHelpers;
using System;
using System.Globalization;

namespace LoanPortfolio
{
    [DelimitedRecord(",")]
    [IgnoreFirst]
    class LoanInput
    {
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public string Description { get; set; }
        [FieldConverter(ConverterKind.Date, "d")]
        public DateTime FirstPmtDate { get; set; }
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        [FieldConverter(typeof(CurrencyConverter))]
        public decimal Principal { get; set; }
        [FieldConverter(typeof(CurrencyConverter))]
        [FieldQuoted(QuoteMode.OptionalForBoth)]
        public decimal PmtAmount { get; set; }
        [FieldConverter(typeof(PercentageConverter))]
        public decimal InterestRate { get; set; }
    }
}

//https://stackoverflow.com/questions/25283510/ignore-dollar-currency-sign-in-decimal-field-with-filehelpers-library
public class CurrencyConverter : ConverterBase
{
    private NumberFormatInfo nfi = new NumberFormatInfo();

    public CurrencyConverter()
    {
        nfi.NegativeSign = "-";
        nfi.CurrencyDecimalSeparator = ".";
        nfi.CurrencyGroupSeparator = ",";
        nfi.CurrencySymbol = "$";
    }

    public override object StringToField(string from)
    {
        return decimal.Parse(from, NumberStyles.Currency, nfi);
    }
}
public class PercentageConverter : ConverterBase
{
    public override object StringToField(string from)
    {
        var percentageStr = from.TrimEnd(new char[] { '%' });
        var percentageDec = decimal.Parse(percentageStr);
        return percentageDec / 100;
    }

    //public override string FieldToString(object fieldValue)
    //{
    //    //return ((decimal)fieldValue).ToString("#.##").Replace(".", "");
    //}

}