using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class WorkersCompensationSegment
    {
        /// <summary>
        /// Segment Identification
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 111-AM</para>
        /// <para>Identifies the segment in the request and/or response</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("111-AM")]
        public string SegmentIdentification { get; set; }

        /// <summary>
        /// Date of Injury
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 434-DY</para>
        /// <para>Date on which the injury occurred.</para>
        /// </remarks>
        [NcpdpFieldAttribute("434-DY")]
        public DateTime DateOfInjury { get; set; }

        /// <summary>
        /// Employer Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 315-CF</para>
        /// <para>Complete name of employer.</para>
        /// </remarks>
        [NcpdpFieldAttribute("315-CF")]
        public string EmployerName { get; set; }

        /// <summary>
        /// Employer Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 318-CG</para>
        /// <para>Free-form text for address information.</para>
        /// </remarks>
        [NcpdpFieldAttribute("318-CG")]
        public string EmployerStreetAddress { get; set; }

        /// <summary>
        /// Employer City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 317-CH</para>
        /// <para>Free-form text for city name.</para>
        /// </remarks>
        [NcpdpFieldAttribute("317-CH")]
        public string EmployerCityAddress { get; set; }

        /// <summary>
        /// Employer State/Province Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 318-CI</para>
        /// <para>
        /// Standard State/Province Code as defined by appropriate government agency.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("318-CI")]
        public string EmployerStateProvinceAddress { get; set; }

        /// <summary>
        /// Employer Zip Postal Zone
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 319-CJ</para>
        /// <para>
        /// Code defining international postal zone excluding punctuation and 
        /// blanks (zip code for US).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("319-CJ")]
        public string EmployerZipPostalZone { get; set; }

        /// <summary>
        /// Employer Phone Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 320-CK</para>
        /// <para>Ten-digit phone number of employer.</para>
        /// </remarks>
        [NcpdpFieldAttribute("320-CK")]
        public string EmployerPhoneNumber { get; set; }

        /// <summary>
        /// Employer Contact Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 321-CL</para>
        /// <para>Employer primary contact.</para>
        /// </remarks>
        [NcpdpFieldAttribute("321-CL")]
        public string EmployerContactName { get; set; }

        /// <summary>
        /// Carrier ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 327-CR</para>
        /// <para>Carrier code assigned in Worker's Compensation Program.</para>
        /// </remarks>
        [NcpdpFieldAttribute("327-CR")]
        public string CarrierId { get; set; }

        /// <summary>
        /// Claim Reference ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 435-DZ</para>
        /// <para>
        /// Identifies the claim number assigned by Worker’s Compensation Program.
        /// </para>
        /// </remarks> 
        [NcpdpFieldAttribute("435-DZ")]
        public string ClaimReferenceId { get; set; }

        /// <summary>
        /// Billing Entity Type Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 117-TR</para>a
        /// <para>
        /// A code that identifies the entity submitting the billing transaction.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("117-TR")]
        public string BillingEntityTypeIndicator { get; set; }

        /// <summary>
        /// Pay To Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 118-TS</para>
        /// <para>Code qualifying the ‘Pay To ID’ (119-TT).</para>
        /// </remarks>
        [NcpdpFieldAttribute("118-TS")]
        public string PayToQualifier { get; set; }

        /// <summary>
        /// Pay To ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 119-TT</para>
        /// <para>
        /// Identifying number of the entity to receive payment for claim.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("119-TT")]
        public string PayToId { get; set; }

        /// <summary>
        /// Pay To Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 120-TU</para>
        /// <para>Name of the entity to receive payment for claim.</para>
        /// </remarks>
        [NcpdpFieldAttribute("120-TU")]
        public string PayToName { get; set; }

        /// <summary>
        /// Pay To Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 121-TV</para>
        /// <para>Street address of the entity to receive payment for claim.</para>
        /// </remarks>
        [NcpdpFieldAttribute("121-TV")]
        public string PayToStreetAddress { get; set; }

        /// <summary>
        /// Pay To City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 122-TW</para>
        /// <para>City of the entity to receive payment for claim.</para>
        /// </remarks>a
        [NcpdpFieldAttribute("122-TW")]
        public string PayToCityAddress { get; set; }

        /// <summary>
        /// Pay To State Province Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 123-TX</para>
        /// <para>
        /// Standard state /province code as defined by appropriate 
        /// government agency.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("123-TX")]
        public string PayToStateProvinceAddress { get; set; }

        /// <summary>
        /// Pay To Zip/Postal Zone
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 124-TY</para>
        /// <para>
        /// Code defining international postal zone excluding punctuation and 
        /// blanks (zip code for US).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("124-TY")]
        public string PayToZipPostalZone { get; set; }

        /// <summary>
        /// Generic Equivalent Product ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 125-TZ</para>
        /// <para>
        /// Code qualifying the ‘Generic Equivalent Product ID’ (126-UA).
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("125-TZ")]
        public string GenericEquivalentProductIdQualifier { get; set; }

        /// <summary>
        /// Generic Equivalent Product ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 126-UA</para>
        /// <para>
        /// Identifies the generic equivalent of the brand product dispensed.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("126-UA")]
        public string GenericEquivalentProductId { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static WorkersCompensationSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new WorkersCompensationSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        /// <summary>
        /// Takes fields and assigns them to properties according to NCPDP rules.
        /// </summary>
        /// <param name="fields">Array of strings representing NCPDP fields.</param>
        public WorkersCompensationSegment(string[] fields)
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
                    case "DY":
                        this.DateOfInjury = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "CF":
                        this.EmployerName = ncpdpFieldValue;
                        break;
                    case "CG":
                        this.EmployerStreetAddress = ncpdpFieldValue;
                        break;
                    case "CH":
                        this.EmployerCityAddress = ncpdpFieldValue;
                        break;
                    case "CI":
                        this.EmployerStateProvinceAddress = ncpdpFieldValue;
                        break;
                    case "CJ":
                        this.EmployerZipPostalZone = ncpdpFieldValue;
                        break;
                    case "CK":
                        this.EmployerPhoneNumber = ncpdpFieldValue;
                        break;
                    case "CL":
                        this.EmployerContactName = ncpdpFieldValue;
                        break;
                    case "CR":
                        this.CarrierId = ncpdpFieldValue;
                        break;
                    case "DZ":
                        this.ClaimReferenceId = ncpdpFieldValue;
                        break;
                    case "TR":
                        this.BillingEntityTypeIndicator = ncpdpFieldValue;
                        break;
                    case "TS":
                        this.PayToQualifier = ncpdpFieldValue;
                        break;
                    case "TT":
                        this.PayToId = ncpdpFieldValue;
                        break;
                    case "TU":
                        this.PayToName = ncpdpFieldValue;
                        break;
                    case "TV":
                        this.PayToStreetAddress = ncpdpFieldValue;
                        break;
                    case "TW":
                        this.PayToCityAddress = ncpdpFieldValue;
                        break;
                    case "TX":
                        this.PayToStateProvinceAddress = ncpdpFieldValue;
                        break;
                    case "TY":
                        this.PayToZipPostalZone = ncpdpFieldValue;
                        break;
                    case "TZ":
                        this.GenericEquivalentProductIdQualifier = ncpdpFieldValue;
                        break;
                    case "UA":
                        this.GenericEquivalentProductId = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
