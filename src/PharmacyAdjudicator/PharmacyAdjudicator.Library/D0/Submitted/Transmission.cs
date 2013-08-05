using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyAdjudicator.Library.Utils;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class Transmission
    {
        public TransactionHeaderSegment TransactionHeader { get; private set; } 
        public bool IsCompound { get; private set; }
        public bool IsPartD { get; private set; }

        private TransactionTypeEnum _transactionType;
        public TransactionTypeEnum TransactionType
        {
            get
            {
                return _transactionType;
            }
            private set
            {
                _transactionType = value;
            }
        }

        private PatientSegment _patient;
        public PatientSegment Patient
        {
            get
            {
                return _patient;
            }
            private set
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
            private set
            {
                _insurance = value;
            } 
        }

        /// <summary>
        /// Prior Authorization Request Segment
        /// </summary>
        /// <remarks>
        /// This only makes sense for Prior Authorization Reversals because there might not be a claim to apply the reversal to.
        /// </remarks>
        private PriorAuthorizationRequestSegment _priorAuth;
        public PriorAuthorizationRequestSegment PriorAuth
        {
            get
            {
                return _priorAuth;
            }
            set
            {
                _priorAuth = value;
            }
        }
        
        //moving these inside the claim
        //private PharmacyProviderSegment _pharmacy;
        //private PrescriberSegment _prescriber;

        private ClaimBillingList _claims;
        public ClaimBillingList Claims
        {
            get
            {
                if ((IsCompound) && (_claims.Count > 1))
                    throw new Exception("Claims with compound ingredients cannot have more than one transaction per transmission.");
                if ((IsPartD) && (_claims.Count > 1))
                    throw new Exception("Part D transmissisons cannot have more than one claim because of TrOOP calculations.");
                return _claims;
            }
            private set
            {
                _claims = value;
            }
        }

        public Transmission(string s)
        {
            //Strip out group separator.  It's not necessary and causes problems.
            int groupIndex = s.IndexOf(NcpdpString.GroupSeparator);
            if (groupIndex != -1)
                s = s.Remove(groupIndex, 1);

            string[] segments = s.Split(NcpdpString.SegmentSeparator);
            this.TransactionHeader = new TransactionHeaderSegment(segments[0]); 
            ClaimBilling currentClaim = null;

            for (int currentSegment = 1; currentSegment < segments.Count(); currentSegment++)
            {
                string[] fields = segments[currentSegment].Split(NcpdpString.FieldSeparator);
                //have to look at the second item for the field.  The leading separator comes before the field.
                switch (fields[1])
                {
                    case "AM01":
                        //Patient Segment
                        //Cannot repeat Patient Segment
                        if (_patient != null)
                            throw new InvalidIncomingLineException("Patient Segment already exists line = " + segments[currentSegment]);
                        _patient = new PatientSegment(fields);
                        break;
                    case "AM02":
                        //Pharmacy Segment
                        if (currentClaim.PharmacyProvider != null)
                            throw new InvalidIncomingLineException("Pharmacy Segment already exists.  Line = " + segments[currentSegment]);
                        currentClaim.PharmacyProvider = new PharmacyProviderSegment(fields);
                        break;
                    case "AM03":
                        //Prescriber Segment
                        if (currentClaim.Prescriber != null)
                            throw new InvalidIncomingLineException("Prescriber Segment already exists.  Line = " + segments[currentSegment]);
                        currentClaim.Prescriber = new PrescriberSegment(fields);
                        break;
                    case "AM04":
                        //Insurance Segment
                        //Cannot repeat Insurance Segment
                        if (_insurance != null)
                            throw new InvalidIncomingLineException("Insurance Segment already exists" + segments[currentSegment]);
                        _insurance = new InsuranceSegment(fields);
                        break;
                    case "AM05":
                        //Coordination of Benefits/Other Payemnts Segment
                        if (currentClaim.CooridinationOfBenefits == null)
                            currentClaim.CooridinationOfBenefits = new CoordinationOfBenefitsSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Coordination of benefits segment already exists for claim.  " + segments[currentSegment]);
                        break;
                    case "AM06":
                        //Worker's Compensation
                        if (currentClaim.WorkersCompensation == null)
                            currentClaim.WorkersCompensation = new WorkersCompensationSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Worker's compenstaion segment already exists for claim.  " + segments[currentSegment]);
                        break;
                    case "AM07":
                        //Claim Segment
                        //Four transactions allowed unless patient is Part D or drug is a compound
                        if (_claims == null)
                            _claims = new ClaimBillingList();
                        currentClaim = new ClaimBilling(fields);
                        _claims.Add(currentClaim);
                        break;
                    case "AM08":
                        //DUR/PPS Segment
                        if (currentClaim.Dur == null)
                            currentClaim.Dur = new DurSegment(fields);
                        else
                            throw new InvalidIncomingLineException("DUR segment already exists for claim.  " + segments[currentSegment]);
                        break;
                    case "AM09":
                        //Coupon Segment
                        if (currentClaim.Coupon == null)
                            currentClaim.Coupon = new CouponSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Coupon segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM10":
                        //Compound Segment
                        if (currentClaim.Compound == null)
                            currentClaim.Compound = new CompoundSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Compound segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM11":
                        //Pricing Segment
                        if (currentClaim == null)
                            throw new Exception("currentClaim not yet defined.  Line = " + fields.ToString());
                        currentClaim.Pricing = new PricingSegment(fields);
                        break;
                    case "AM12":
                        //Prior Authorization Segment
                        if (currentClaim == null)
                            //On prior auth reversals the prior auth will apply to the transmission and not a specific claim.
                            _priorAuth = new PriorAuthorizationRequestSegment(fields);
                        else if (currentClaim.PriorAuthorization == null)
                            currentClaim.PriorAuthorization = new PriorAuthorizationRequestSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Prior Authorization Segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM13":
                        //Clinical Segment
                        if (currentClaim.Clinical == null)
                            currentClaim.Clinical = new ClinicalSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Clinical Segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM14":
                        //Additional Documentation Segment
                        if (currentClaim.AdditionalDocumentation == null)
                            currentClaim.AdditionalDocumentation = new AdditionalDocumentationSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Additional Documentation Segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM15":
                        //Facility Segment 
                        if (currentClaim.Facility == null)
                            currentClaim.Facility = new FacilitySegment(fields);
                        else
                            throw new InvalidIncomingLineException("Facility Segment already exists for claim. " + segments[currentSegment]);
                        break;
                    case "AM16":
                        //Narrative Segment
                        if (currentClaim.Narrative == null)
                            currentClaim.Narrative = new NarrativeSegment(fields);
                        else
                            throw new InvalidIncomingLineException("Narrative Segment already exists for claim. " + segments[currentSegment]);
                        break;
                    default:
                        throw new InvalidIncomingLineException("Segment Identification unknown for segment = " + segments[currentSegment]);
                }
            }

            this._transactionType = ParseTransactionType(this.TransactionHeader.TransactionCode);

            if (_transactionType == TransactionTypeEnum.Billing)
            {
                if ((_claims.Count < 1) && (_claims.Count > 4))
                    throw new InvalidIncomingLineException("Transaction count for billing transmission not between 1 and 4 line = " + s);
                if (_claims.Count != this.TransactionHeader.TransactionCount)
                    throw new InvalidIncomingLineException("TransactionCount does not match actual number of transactions line = " + s);
                if (_insurance == null)
                    throw new InvalidIncomingLineException("Missing Insurance Segment line = " + s);
            }

            //Enforces rule about what level the prior auth sholud apply to.
            if (_priorAuth != null)
                if ((_transactionType != TransactionTypeEnum.PriorAuthReversal)&&(_transactionType != TransactionTypeEnum.PriorAuthInquiry))
                    throw new InvalidIncomingLineException("Prior Auth should not apply to Transmission when Transaction Type != PriorAuthReversal or PriorAuthInquiry.  Line = " + s);
        }

        private TransactionTypeEnum ParseTransactionType(string s)
        {
            switch (s)
            {
                case "E1":
                    return TransactionTypeEnum.EligibilityVerification;
                case "B1":
                    return TransactionTypeEnum.Billing;
                case "B2":
                    return TransactionTypeEnum.Reversal;
                case "B3":
                    return TransactionTypeEnum.Rebill;
                case "P1":
                    return TransactionTypeEnum.PriorAuthAndBilling;
                case "P2":
                    return TransactionTypeEnum.PriorAuthReversal;
                case "P3":
                    return TransactionTypeEnum.PriorAuthInquiry;
                case "P4":
                    return TransactionTypeEnum.PriorAuthRequest;
                default:
                    throw new Exception("Transaction type not defied for " + s);
            }
        }

        public enum TransactionTypeEnum
        {
            EligibilityVerification,
            Billing,
            Reversal,
            Rebill,
            PriorAuthAndBilling,
            PriorAuthReversal,
            PriorAuthInquiry,
            PriorAuthRequest
        }

    }
}
