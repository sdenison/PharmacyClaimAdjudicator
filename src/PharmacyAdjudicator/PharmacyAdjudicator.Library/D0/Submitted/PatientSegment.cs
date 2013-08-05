using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class PatientSegment
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
        public string SegmentIdentification { get; private set; }

        /// <summary>
        /// Patient ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 331-CX</para>
        /// <para>Patient ID Qualifier</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("331-CX")]
        public string PatientIdQualifier { get; private set; }

        /// <summary>
        /// Patient ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 332-CY</para>
        /// <para>ID assigned to the patient</para>
        /// </remarks>
        [Required]
        [MaxLength(20)]
        [NcpdpFieldAttribute("332-CY")]
        public string PatientId { get; private set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 304-C4</para>
        /// <para>Date of birth of patient</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("304-C4")]
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Patient Gender Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 305-C5</para>
        /// <para>Code indicating the gender of the individual.</para>
        /// <list type="table">
        ///     <listheader>
        ///         <term>Code</term> 
        ///         <description>Value</description>
        ///     </listheader>
        /// <item>
        ///     <term>0</term>
        ///     <description>Not Specified</description>
        /// </item>    
        /// <item>
        ///     <term>1</term>
        ///     <description>Male</description>
        /// </item>    
        /// <item>
        ///     <term>2</term>
        ///     <description>Female</description>
        /// </item>    
        /// </list>
        /// </remarks>
        [NcpdpFieldAttribute("305-C5")]
        public string PatientGenderCode { get; private set; }

        /// <summary>
        /// Patient First Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 310-CA</para>
        /// <para>Individual first name.</para>
        /// </remarks>
        [NcpdpFieldAttribute("310-CA")]
        [MaxLength(35)]
        public string PatientFirstName { get; private set; }

        /// <summary>
        /// Patient Last Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 311-CB</para>
        /// <para>Individual last name.</para>
        /// </remarks>
        [NcpdpFieldAttribute("311-CB")]
        [MaxLength(35)]
        [Required]
        public string PatientLastName { get; private set; }

        /// <summary>
        /// Patient Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 322-CM</para>
        /// <para>Free-form text for address information.</para>
        /// </remarks>
        [NcpdpFieldAttribute("322-CM")]
        [MaxLength(30)]
        public string PatientStreetAddress { get; private set; }

        /// <summary>
        /// Patient City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 323-CN</para>
        /// <para>Free-form text for city name.</para>
        /// </remarks>
        [NcpdpFieldAttribute("323-CN")]
        [MaxLength(20)]
        public string PatientCityAddress { get; private set; }

        /// <summary>
        /// Patient State
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 324-CO</para>
        /// <para>Standard State/Provice Code as defined by appropriate government agency.</para>
        /// </remarks>
        [NcpdpFieldAttribute("324-CO")]
        [MaxLength(2)]
        public string PatientState { get; private set; }

        /// <summary>
        /// Patient Zip
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 325-CP</para>
        /// <para>Code defining international postal zone excluding punctuation 
        /// and blanks (zip code for US).</para>
        /// </remarks>
        [NcpdpFieldAttribute("325-CP")]
        [MaxLength(15)]
        public string PatientZip { get; private set; }

        /// <summary>
        /// Patient Phone Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 326-CQ</para>
        /// <para>Ten-digit phone number of patient</para>
        /// </remarks>
        [NcpdpFieldAttribute("326-CQ")]
        [MaxLength(10)]
        public string PatientPhoneNumber { get; private set; }

        /// <summary>
        /// Place of Service
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 307-C7</para>
        /// <para>Code identifyng the place where a drug or service is 
        /// dispensed or administered.</para>
        /// </remarks>
        [NcpdpFieldAttribute("307-C7")]
        [MaxLength(2)]
        public string PlaceOfService { get; private set; }

        /// <summary>
        /// Employer ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 333-CZ</para>
        /// <para>ID assigned to employer.</para>
        /// </remarks>
        [NcpdpFieldAttribute("333-C7")]
        [MaxLength(15)]
        public string EmployerId { get; private set; }

        /// <summary>
        /// Smoker Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 334-1C</para>
        /// <para>Code indicating the patient as a smoker or non-smoker.</para>
        /// </remarks>
        [NcpdpFieldAttribute("334-1C")]
        [MaxLength(1)]
        public string SmokerCode { get; private set; }

        /// <summary>
        /// Pregnancy Indicator
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 335-2C</para> 
        /// <para>Code indicating the patient as pregnant or non-pregnant.</para>
        /// </remarks>
        [NcpdpFieldAttribute("335-2C")]
        [MaxLength(1)]
        public string PregnancyIndicator { get; private set; }

        /// <summary>
        /// Patient Email Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 350-HN</para>
        /// <para>The E-Mail address of the patient (memeber).</para>
        /// </remarks>
        [NcpdpFieldAttribute("350-HN")]
        [MaxLength(80)]
        public string PatientEmailAddress { get; private set; }

        /// <summary>
        /// Pateint Residence
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 384-4X</para>
        /// <para>Code identifying the patient's place of residence</para> 
        /// </remarks>
        [NcpdpFieldAttribute("384-4X")]
        [MaxLength(2)]
        public string PatientResidence { get; private set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static PatientSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new PatientSegment(fields);
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
        public PatientSegment(string[] fields)
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
                    case "CX":
                        this.PatientIdQualifier = ncpdpFieldValue;
                        break;
                    case "CY":
                        this.PatientId = ncpdpFieldValue;
                        break;
                    case "C4":
                        this.DateOfBirth = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "C5":
                        this.PatientGenderCode = ncpdpFieldValue;
                        break;
                    case "CA":
                        this.PatientFirstName = ncpdpFieldValue;
                        break;
                    case "CB":
                        this.PatientLastName = ncpdpFieldValue;
                        break;
                    case "CM":
                        this.PatientStreetAddress = ncpdpFieldValue;
                        break;
                    case "CN":
                        this.PatientCityAddress = ncpdpFieldValue;
                        break;
                    case "CO":
                        this.PatientState = ncpdpFieldValue;
                        break;
                    case "CP":
                        this.PatientZip = ncpdpFieldValue;
                        break;
                    case "CQ":
                        this.PatientPhoneNumber = ncpdpFieldValue;
                        break;
                    case "C7":
                        this.PlaceOfService = ncpdpFieldValue;
                        break;
                    case "CZ":
                        this.EmployerId = ncpdpFieldValue;
                        break;
                    case "1C":
                        this.SmokerCode = ncpdpFieldValue;
                        break;
                    case "2C":
                        this.PregnancyIndicator = ncpdpFieldValue;
                        break;
                    case "HN":
                        this.PatientEmailAddress = ncpdpFieldValue;
                        break;
                    case "4X":
                        this.PatientResidence = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
