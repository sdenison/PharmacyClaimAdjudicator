using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Utils
{
    public static class Overpunch
    {
        private static char[] _positiveDigit = { '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };
        private static char[] _negativeDigit = { '}', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };

        public static string Format(int value)
        {
            int abs;
            string text;
            abs = Math.Abs(value);

            char overpunchChar;
            if (value < 0)
                overpunchChar = (char)_negativeDigit.GetValue((value % 10) * -1); 
            else
                overpunchChar = (char)_positiveDigit.GetValue(value % 10);
            text = abs.ToString();
            return text.Substring(0, text.Length - 1) + overpunchChar;
        }

        public static int Parse(string s) 
        {
            int sign = 1;
            int lastDigit = 0;
            char lastChar = s.ToCharArray()[s.Length - 1];
            int negativeIndex;
            int positiveIndex;

            negativeIndex = Array.IndexOf(_negativeDigit, lastChar);
            positiveIndex = Array.IndexOf(_positiveDigit, lastChar);
            
            //if no index was found for either then the nubmer is not overpunched
            if ((negativeIndex == -1) && (positiveIndex == -1))
            {
                throw new InvalidIncomingLineException("Overpunch number in wrong format.");
            }

            if (negativeIndex >= 0)
            {
                sign = -1;
                lastDigit = negativeIndex;
            }
            else
            {
                sign = 1;
                lastDigit = positiveIndex;
            }
            return (int.Parse(s.Substring(0, s.Length - 1).Trim()) * (int)10 + (int)lastDigit) * (int)sign;
        }

        public static decimal ParseToCurrency(string s)
        {
            return (decimal)Parse(s) / 100;
        }
    }
}
