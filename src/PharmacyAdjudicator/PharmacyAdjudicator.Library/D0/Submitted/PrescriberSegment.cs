using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class PrescriberSegment
    {
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
        /// Prescriber ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 466-EZ</para>
        /// <para>Code qualifying the 'Prescriber ID' (411-DB).</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("466-EZ")]
        public string PrescriberIdQualifier { get; set; }

        /// <summary>
        /// Prescriber ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 411-DB</para>
        /// <para>ID assigned to the prescriber.</para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("411-DB")]
        public string PrescriberId { get; set; }

        /// <summary>
        /// Prescriber Last Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 427-DR</para>
        /// <para>Individual last name</para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("427-DR")]
        public string PrescriberLastName { get; set; }

        /// <summary>
        /// Prescriber Phone Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 498-PM</para>
        /// <para>Ten-digit phone number of the prescriber.</para>
        /// </remarks>
        [MaxLength(10)]
        [NcpdpFieldAttribute("498-PM")]
        public string PrescriberPhoneNumber { get; set; }

        /// <summary>
        /// Primary Care Provider ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 468-2E</para>
        /// <para>Code qualifying the 'Primary Care Provider ID' (421-DL).</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("468-2E")]
        public string PrimaryCareProviderIdQualifier { get; set; }

        /// <summary>
        /// Primary Care Provider ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 421-DL</para>
        /// <para>
        /// ID assigned to the primary care provider.  Used when the 
        /// patient is referred to a secondary care provider.
        /// </para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("421-DL")]
        public string PrimaryCareProviderId { get; set; } 

        /// <summary>
        /// Primary Care Provider Last Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 470-4A</para>
        /// <para>Individual last name</para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("470-4A")]
        public string PrimaryCareProviderLastName { get; set; }

        /// <summary>
        /// Prescriber First Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 364-2J</para>
        /// <para>Individual first name.</para>
        /// </remarks>
        [MaxLength(12)]
        [NcpdpFieldAttribute("364-2J")]
        public string PrescriberFirstName { get; set; }

        /// <summary>
        /// Prescriber Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 365-2K</para>
        /// <para>Free form text for prescriber address information.</para>
        /// </remarks>
        [MaxLength(30)]
        [NcpdpFieldAttribute("365-2K")]
        public string PrescriberStreetAddress { get; set; }

        /// <summary>
        /// Prescriber City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 366-2M</para>
        /// <para>Free form text for prescriber city name.</para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("366-2M")]
        public string PrescriberCityAddress { get; set; }

        /// <summary>
        /// Prescriber State/Province Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 367-2N</para>
        /// <para>
        /// Standard state/province code as defined by appropriate government 
        /// agency.
        /// </para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("367-2N")]
        public string PrescriberStateProvinceAddress { get; set; }

        /// <summary>
        /// Prescriber Zip/Postal Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 368-2P</para>
        /// <para>
        /// Code defining international postal zone excluding punctuation and 
        /// blanks.
        /// </para>
        /// </remarks>
        public string PrescriberZipPostalCode { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static PrescriberSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new PrescriberSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public PrescriberSegment(string[] fields)
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
                    case "EZ":
                        this.PrescriberIdQualifier = ncpdpFieldValue;
                        break;
                    case "DB":
                        this.PrescriberId = ncpdpFieldValue;
                        break;
                    case "DR":
                        this.PrescriberLastName = ncpdpFieldValue;
                        break;
                    case "PM":
                        this.PrescriberPhoneNumber = ncpdpFieldValue;
                        break;
                    case "2E":
                        this.PrimaryCareProviderIdQualifier = ncpdpFieldValue;
                        break;
                    case "DL":
                        this.PrimaryCareProviderId = ncpdpFieldValue;
                        break;
                    case "4E":
                        this.PrimaryCareProviderLastName = ncpdpFieldValue;
                        break;
                    case "2J":
                        this.PrescriberFirstName = ncpdpFieldValue;
                        break;
                    case "2K":
                        this.PrescriberStreetAddress = ncpdpFieldValue;
                        break;
                    case "2M":
                        this.PrescriberCityAddress = ncpdpFieldValue;
                        break;
                    case "2N":
                        this.PrescriberStateProvinceAddress = ncpdpFieldValue;
                        break;
                    case "2P":
                        this.PrescriberZipPostalCode = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
