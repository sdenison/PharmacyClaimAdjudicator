using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Enums
{
    //NcpdpField : 522-FM
    public enum BasisOfReimbursement
    {
        NotSpecified = 0,
        IngredientCostPaid = 1,
        IngredientCostReducedToAwp = 2,
        IngredientCostReudcedToAwpLessPercentPricing = 3,
        UsualAndCustomary = 4,
        LowerOfIngredientCostPlusFeesVsUsualAndCustomary = 5,
        MacPricingIngredientCostPaid = 6,
        MacPricingIngredientCostReducedToMac = 7,
        ContractPricing = 8,
        AcquisitionPricing = 9,
        AverageSalesPrice = 10,
        AverageManufacturerPrice = 11,
        Three40B = 12,
        WholesaleAcquisitionCost = 13,
        OtherPayerPatientResponsibilityAmount = 14,
        PatientPayAmount = 15,
        CouponPayment = 16,
        SpecialPatientReimbursement = 17,
        DirectPrice = 18,
        StateFeeSchedule = 19
    }
}
