using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class ClaimBilling
    {
        [Required]
        public ClaimSegment Claim { get; set; }
        [Required]
        public PricingSegment Pricing { get; set; }
        public PharmacyProviderSegment PharmacyProvider { get; set; }
        public PrescriberSegment Prescriber { get; set; }
        public CoordinationOfBenefitsSegment CooridinationOfBenefits { get; set; }
        public WorkersCompensationSegment WorkersCompensation { get; set; }
        public DurSegment Dur { get; set; }
        public CouponSegment Coupon { get; set; }
        public CompoundSegment Compound { get; set; }
        public PriorAuthorizationRequestSegment PriorAuthorization { get; set; }
        public ClinicalSegment Clinical { get; set; }
        public AdditionalDocumentationSegment AdditionalDocumentation { get; set; }
        public FacilitySegment Facility { get; set; }
        public NarrativeSegment Narrative { get; set; }

        public ClaimBilling(string[] fields)
        {
            this.Claim = new ClaimSegment(fields);
        }
    }
}
