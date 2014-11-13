using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Enums
{
    public enum ResponseStatus
    {
        Approved,
        Benefit,
        Captured,
        DuplicateOfPaid,
        PaDeferred,
        Paid,
        DuplicateOfCapture,
        Rejected,
        DuplicateOfApproved
    }

    public static class ResponseStatusConverter
    {
        public static ResponseStatus Parse(string responseStatus)
        {
            int intValue;
            if (int.TryParse(responseStatus, out intValue))
                return (ResponseStatus)intValue;

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
                    return (ResponseStatus) Enum.Parse(typeof(ResponseStatus), responseStatus);
            }
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
    }
}
