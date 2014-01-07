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
        }
        
    }
}
