using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class Transmission
    {
        public TransactionHeaderSegment TransactionHeader { get; set; }
        public MessageSegment Message { get; set; }
        public List<ClaimBilling> Claims { get; set; }

        private PatientSegment _patient;
        public PatientSegment Patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }

        private InsuranceSegment _insurance;
        public InsuranceSegment Insurance
        {
            get
            {
                return _insurance;
            }
            set
            {
                _insurance = value;
            }
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(TransactionHeader.ToNcpdpString());
            if (this.Message != null)
                returnValue.Append(this.Message.ToNcpdpString());
            foreach (var claim in this.Claims)
            {
                returnValue.Append(claim.ToNcpdpString());
            }

            return returnValue.ToString();
            
            //returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.TransactionResponseStatus, this.TransactionResponseStatus));
            //returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.AuthorizationNumber, this.AuthorizationNumber));
            if (returnValue.Length > 0)
            {
                //returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }

            return returnValue.ToString();
        }
        
    }
}
