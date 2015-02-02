using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Utils
{
    public static class ToProperCaseExtension
    {
        public static string ToProperCase(this string theString)
        {
            //If there are 0 or 1 characters, just return the string.
            if (theString == null) return theString;
            if (theString.Length < 2) return theString.ToUpper();

            //Start with the first character.
            string result = theString.Substring(0, 1).ToUpper();

            //Add the characters one at a time.
            //Add a space where the characters is upper case.
            for (int i=1; i < theString.Length; i++)
            {
                if (char.IsUpper(theString[i]))
                    result += " ";
                result += theString[i];
            }
            return result;
        }
    }
}
