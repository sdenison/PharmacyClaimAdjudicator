using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Enums
{
    public enum TaxExemptIndicator
    {
        NotSpecified, //blank
        PlanIsTaxExempt, //1
        NotTaxExempt, //2
        PatientIsTaxExempt, //3
        PlanAndPatientAreTaxExempt //4
    }

    public static class TaxExemptConverter
    {
        public static TaxExemptIndicator Parse(string taxExemptStatus)
        {
            switch (taxExemptStatus)
            {
                case "":
                    return TaxExemptIndicator.NotSpecified;
                case " ":
                    return TaxExemptIndicator.NotSpecified;
                case "1":
                    return TaxExemptIndicator.PlanIsTaxExempt;
                case "2":
                    return TaxExemptIndicator.NotTaxExempt;
                case "3":
                    return TaxExemptIndicator.PatientIsTaxExempt;
                case "4":
                    return TaxExemptIndicator.PlanAndPatientAreTaxExempt;
                default:
                    throw new ArgumentException("No taxExemptIndicator = " + taxExemptStatus);
            }
        }

        public static string ToString(TaxExemptIndicator taxExemptIndicator)
        {
            switch (taxExemptIndicator)
            {
                case TaxExemptIndicator.NotSpecified:
                    return " ";
                case TaxExemptIndicator.PlanIsTaxExempt:
                    return "1";
                case TaxExemptIndicator.NotTaxExempt:
                    return "2";
                case TaxExemptIndicator.PatientIsTaxExempt:
                    return "3";
                case TaxExemptIndicator.PlanAndPatientAreTaxExempt:
                    return "4";
                default:
                    throw new ArgumentException("No TaxExemptIndicator to string conversion.  TaxExemptIndicator " + taxExemptIndicator.ToString());
            }
        }
    }
}
