using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Enums
{
    public static class EnumConvert
    {
        public static string ToString(BasisOfReimbursement basisOfReimbursement)
        {
            return ((int)basisOfReimbursement).ToString();
        }

        public static BasisOfReimbursement Parse(int basisOfReimbursement)
        {
            return (BasisOfReimbursement)basisOfReimbursement;
        }

        public static string ToString(ResponseStatus responseStatus)
        {
            switch (responseStatus)
            {
                case ResponseStatus.Approved:
                    return "A";
                case ResponseStatus.Benefit:
                    return "B";
                case ResponseStatus.Captured:
                    return "C";
                case ResponseStatus.DuplicateOfPaid:
                    return "D";
                case ResponseStatus.PaDeferred:
                    return "F";
                case ResponseStatus.Paid:
                    return "P";
                case ResponseStatus.DuplicateOfCapture:
                    return "Q";
                case ResponseStatus.Rejected:
                    return "R";
                case ResponseStatus.DuplicateOfApproved:
                    return "S";
                default:
                    throw new ArgumentException("No responseStatus = " + responseStatus.ToString());
            }
        }

        public static ResponseStatus Parse(string responseStatus)
        {
            switch (responseStatus)
            {
                case "A":
                    return ResponseStatus.Approved;
                case "B":
                    return ResponseStatus.Benefit;
                case "C":
                    return ResponseStatus.Captured;
                case "D":
                    return ResponseStatus.DuplicateOfPaid;
                case "F":
                    return ResponseStatus.PaDeferred;
                case "P":
                    return ResponseStatus.Paid;
                case "Q":
                    return ResponseStatus.DuplicateOfCapture;
                case "R":
                    return ResponseStatus.Rejected;
                case "S":
                    return ResponseStatus.DuplicateOfApproved;
                default:
                    throw new ArgumentException("No responseStatus = " + responseStatus);
            }
        }
    }
}
