using System;

namespace ImageAnalyzer.SpecialClasses
{
    class DigitConverter
    {
        public static double Convert2to10(string digit)
        {
            double result = 0;

            string[] digits = digit.Split('.');
            for (int i = 0; i < digits[0].Length; i++)
            {
                result += digits[0][i] * Math.Pow(2, digits[0].Length - i - 1);
            }

            if (digits[1].Length > 0)
            {
                for (int i = 0; i < digits[1].Length; i++)
                {
                    result += digits[1][i] * Math.Pow(2, -(i + 1));
                }
            }

            return result == 0 ? 1 : result;
        }
    }
}
