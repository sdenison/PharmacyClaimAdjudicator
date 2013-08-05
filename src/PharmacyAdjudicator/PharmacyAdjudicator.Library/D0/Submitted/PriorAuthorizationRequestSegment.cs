using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{ 
    public class PriorAuthorizationRequestSegment {
        //All NCPDP fields start with 498 in Prior Authorization Request Segment.  
        //Usually they're unique.

        /// <summary>
        /// Segment Identification
        /// </summary>
        /// <remarks>
        /// <para>>NCPDP 111-AM</para>
        /// <para>Identifies the segment in the request and/or response</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Request Type
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PA</para>
        /// <para>
        /// Code identifying type of prior authorization request.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PA")] 
        public string RequestType { get; set; }

        /// <summary>
        /// Request Period Date-Begin
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PB</para>
        /// <para>Beginning date for a prior authorization request.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PB")]
        public string RequestPeriodDateBegin { get; set; }

        /// <summary>
        /// Request Period Date-End
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PC</para>
        /// <para>Ending date for a prior authorization request.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PC")]
        public string RequestPeriodDateEnd { get; set; }

        /// <summary>
        /// Basis of Request
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PD</para>
        /// <para>Code describing the reason for prior authorization request.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PD")]
        public string BasisOfRequest { get; set; }

        /// <summary>
        /// Authorized Representative First Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PE</para>
        /// <para>First name of the patient’s authorized representative.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PE")]
        public string AuthorizedRepresentativeFirstName { get; set; }

        /// <summary>
        /// Authorized Representative Last Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PF</para>
        /// <para>Last name of the patient’s authorized representative.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PF")]
        public string AuthorizedRepresentativeLastName { get; set; }

        /// <summary>
        /// Authorized Representative Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PG</para>
        /// <para>Free-form text for address information.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PG")]
        public string AuthorizedRepresentativeStreetAddress { get; set; }

        /// <summary>
        /// Authorized Representative City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PH</para> 
        /// <para>Free-form text for city name.</para>
        /// </remarks> 
        [NcpdpFieldAttribute("498-PH")]
        public string AuthorizedRepresentativeCityAddress { get; set; }

        /// <summary>
        /// Authorized Representative State/Province Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PJ</para>
        /// <para>
        /// Standard State/Province code as defined by appropriate government 
        /// agency.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PJ")]
        public string AuthorizedRepresentativeStateAddress { get; set; }

        /// <summary>
        /// Authorized Representative Zip/Postal Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PK</para>
        /// <para>
        /// Code defining international postal zone excluding punctuation and 
        /// blanks (zip code for US).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PK")]
        public string AuthorizedRepresentativeZipPostalCode { get; set; }

        /// <summary>
        /// Prior Authorization Number-Assigned
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PY</para>
        /// <para>
        /// Unique number identifying the prior authorization assigned by the 
        /// processor.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PY")]
        public string PriorAuthorizationNumberAssigned { get; set; }

        /// <summary>
        /// Authorization Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 503-F3</para>
        /// <para>
        /// Number assigned by the processor to identify an authorized transaction.
        /// </para>
        /// <para>
        /// This is the same Authorization Number that is returned in a Response Status Segment.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("503-F3")]
        public string AuthorizationNumber { get; set; }

        /// <summary>
        /// Prior Authorization Supporting Documentation
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PP</para>
        /// <para>Free text message.</para>
        /// </remarks>
        [NcpdpFieldAttribute("498-PP")]
        public string PriorAuthorizationSupportingDocumentation { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static PriorAuthorizationRequestSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new PriorAuthorizationRequestSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public PriorAuthorizationRequestSegment(string[] fields)
        {
            foreach (string field in fields)
            {
                //Skips blank fields
                if (string.IsNullOrEmpty(field))
                    continue;
                string ncpdpField = field.Substring(0, 2).ToUpper();
                string ncpdpFieldValue = field.Substring(2).Trim();
                switch (ncpdpField)
                {
                    case "AM":
                        if (string.IsNullOrEmpty(this.SegmentIdentification) == false)
                            throw new InvalidIncomingLineException("Segment Identification already set.  Line is probably missing a segment separator.  " + fields.ToString());
                        this.SegmentIdentification = ncpdpFieldValue;
                        break;
                    case "PA":
                        this.RequestType = ncpdpFieldValue;
                        break;
                    case "PB":
                        this.RequestPeriodDateBegin = ncpdpFieldValue;
                        break;
                    case "PC":
                        this.RequestPeriodDateEnd = ncpdpFieldValue;
                        break;
                    case "PD":
                        this.BasisOfRequest = ncpdpFieldValue;
                        break;
                    case "PE":
                        this.AuthorizedRepresentativeFirstName = ncpdpFieldValue;
                        break;
                    case "PF":
                        this.AuthorizedRepresentativeLastName = ncpdpFieldValue;
                        break;
                    case "PG":
                        this.AuthorizedRepresentativeStreetAddress = ncpdpFieldValue;
                        break;
                    case "PH":
                        this.AuthorizedRepresentativeCityAddress = ncpdpFieldValue;
                        break;
                    case "PJ":
                        this.AuthorizedRepresentativeStateAddress = ncpdpFieldValue;
                        break;
                    case "PK":
                        this.AuthorizedRepresentativeZipPostalCode = ncpdpFieldValue;
                        break;
                    case "PY":
                        this.PriorAuthorizationNumberAssigned = ncpdpFieldValue;
                        break;
                    case "F3":
                        this.AuthorizationNumber = ncpdpFieldValue;
                        break;
                    case "PP":
                        this.PriorAuthorizationSupportingDocumentation = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
