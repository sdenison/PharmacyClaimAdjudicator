using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class PharmacyProviderSegment
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
        /// Provider ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 465-EY</para>
        /// <para>Code qualifying the 'Provider ID (444-E9).</para> 
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("465-EY")]
        public string ProviderIdQualifier { get; set; }

        /// <summary>
        /// Provider ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 444-E9</para>
        /// <para>
        /// Unique ID assigned to the person responsible for the dispensing 
        /// of the prescription or provision of the service.
        /// </para>
        /// </remarks>
        [MaxLength(15)]
        [NcpdpFieldAttribute("444-E9")]
        public string ProviderId { get; set; }


        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static PharmacyProviderSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new PharmacyProviderSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public PharmacyProviderSegment(string[] fields)
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
                    case "EY":
                        this.ProviderIdQualifier = ncpdpFieldValue;
                        break;
                    case "E9":
                        this.ProviderId = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
