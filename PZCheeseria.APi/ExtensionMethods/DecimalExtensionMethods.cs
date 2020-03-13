using System;
using System.Globalization;

namespace PZCheeseria.Api.ExtensionMethods
{
    public static class DecimalExtensionMethods
    {
        public static string FormatAsAustralianDollar(this decimal value)
        {
            return value.ToString("C", CultureInfo.CreateSpecificCulture("en-AU"));
        }
    }
}