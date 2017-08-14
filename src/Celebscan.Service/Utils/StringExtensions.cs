using System;
using System.Globalization;
using System.Text;

namespace Celebscan.Service.Utils
{
    public static class StringExtensions
    {
        public static string UnicodeNormalize(this string rawValue)
        {
            var decodedValue = rawValue.Normalize(NormalizationForm.FormD);
            var outputBuilder = new StringBuilder();

            for (int index = 0; index < decodedValue.Length; index++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(decodedValue[index]) != UnicodeCategory.NonSpacingMark)
                {
                    outputBuilder.Append(decodedValue[index]);
                }
            }

            return outputBuilder.ToString();
        }
    }
}