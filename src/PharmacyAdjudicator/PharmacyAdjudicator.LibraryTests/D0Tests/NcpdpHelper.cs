using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.LibraryTests.D0Tests
{
    public class NcpdpHelper
    {
        public static string FromNcpdpToHumanReadable(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('0', 'Ø');
            sb.Replace(Library.Utils.NcpdpString.SegmentSeparator.ToString(), "<1E>");
            sb.Replace(Library.Utils.NcpdpString.GroupSeparator.ToString(), "<1D>");
            sb.Replace(Library.Utils.NcpdpString.FieldSeparator.ToString(), "<1C>");
            return sb.ToString();
        }

        public static string FromHumanReadableToNcpdp(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('Ø', '0');
            sb.Replace("<1E>", Library.Utils.NcpdpString.SegmentSeparator.ToString());
            sb.Replace("<1D>", Library.Utils.NcpdpString.GroupSeparator.ToString());
            sb.Replace("<1C>", Library.Utils.NcpdpString.FieldSeparator.ToString());
            return sb.ToString();
        }
    }
}
