using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class FacilitySegment
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
        /// Facility ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 336-8C</para>
        /// <para>ID assigned tothe patient's clinic/host party.</para>
        /// </remarks>
        [MaxLength(35)]
        [NcpdpFieldAttribute("336-8C")]
        public string FacilityId { get; set; }

        /// <summary>
        /// Facility Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 385-3Q</para> 
        /// <para>Name identifying the location of the service rendered.</para>
        /// </remarks>
        [NcpdpFieldAttribute("385-3Q")]
        public string FacilityName { get; set; }

        /// <summary>
        /// Facility Street Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 386-3U</para>
        /// <para>Free form text for Facility address information.</para>
        /// </remarks>
        [MaxLength(30)]
        [NcpdpFieldAttribute("386-3U")]
        public string FacilityStreetAddress { get; set; }

        /// <summary>
        /// Facility City Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 388-5J</para>
        /// <para>Free form text for facility city name.</para>
        /// </remarks>
        [MaxLength(20)]
        [NcpdpFieldAttribute("388-5J")] 
        public string FacilityCityAddress { get; set; } 
       
        /// <summary>
        /// Facility State/Province Address
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 387-3V</para>
        /// <para>
        /// Standard state/province code as defined by appropriate government 
        /// agency.
        /// </para>
        /// </remarks>
        [MaxLength(30)]
        [NcpdpFieldAttribute("387-3V")]
        public string FacilityStateProvinceAddress {get; set; }

        /// <summary>
        /// Facility Zip/Postal Zone
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 389-6D</para>
        /// <para>
        /// Code defining international postal zone excluding punctuation 
        /// and blanks.
        /// </para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("389-6D")]
        public string FacilityZipPostalZone { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static FacilitySegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new FacilitySegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public FacilitySegment(string[] fields)
        {
            foreach (string field in fields)
            {
                //Skips blank fields
                if (string.IsNullOrEmpty(field))
                    continue;
                string ncpdpField = field.Substring(0, 2).ToUpper();
                string ncpdpFieldValue = field.Substring(2).Trim();
                switch (field)
                {
                    case "AM":
                        if (string.IsNullOrEmpty(this.SegmentIdentification) == false)
                            throw new InvalidIncomingLineException("Segment Identification already set.  Line is probably missing a segment separator.  " + fields.ToString());
                        this.SegmentIdentification = ncpdpFieldValue;
                        break;
                    case "8C":
                        this.FacilityId = ncpdpFieldValue;
                        break;
                    case "3Q":
                        this.FacilityName = ncpdpFieldValue;
                        break;
                    case "3U":
                        this.FacilityStreetAddress = ncpdpFieldValue;
                        break;
                    case "5J":
                        this.FacilityCityAddress = ncpdpFieldValue;
                        break;
                    case "3V":
                        this.FacilityStateProvinceAddress = ncpdpFieldValue;
                        break;
                    case "6D":
                        this.FacilityZipPostalZone = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }

}
