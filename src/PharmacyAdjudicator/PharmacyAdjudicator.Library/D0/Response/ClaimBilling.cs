using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class ClaimBilling
    {
        [Required]
        public StatusSegment Status { get; set; }
        [Required]
        public ClaimSegment Claim { get; set; }

        //Required when the status is accepted
        public PricingSegment Pricing { get; set; }

        public CoordinationOfBenefitsSegment CooridinationOfBenefits { get; set; }
        public DurSegment Dur { get; set; }

        public ClaimBilling()
        {
        }

        public ClaimBilling(Core.Transaction transaction)
        {
            this.Status = new StatusSegment(transaction);
            this.Claim = new ClaimSegment(transaction);

            if ((transaction.ResponseStatus == Core.Enums.ResponseStatus.Approved) ||
                (transaction.ResponseStatus == Core.Enums.ResponseStatus.Paid))
            {
                this.Pricing = new PricingSegment(transaction);
            }
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();
            returnValue.Append(this.Status.ToNcpdpString());
            returnValue.Append(this.Claim.ToNcpdpString());
            returnValue.Append(this.Pricing.ToNcpdpString());
            if (this.CooridinationOfBenefits != null)
                returnValue.Append(this.CooridinationOfBenefits.ToNcpdpString());
            if (this.Dur != null)
                returnValue.Append(this.Dur.ToNcpdpString());

            return returnValue.ToString();
        }
    }
}
