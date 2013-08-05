using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class CouponSegment
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
        /// Coupon Type
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 485-KE</para>
        /// <para>Code indicating the type of coupon being used.</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("485-KE")]
        public string CouponType { get; set; }

        /// <summary>
        /// Coupon Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 486-ME</para>
        /// <para>Unique serial number assigned to the prescription coupons.</para>
        /// </remarks>
        [Required]
        [MaxLength(15)]
        [NcpdpFieldAttribute("486-ME")]
        public string CouponNumber { get; set; }

        /// <summary>
        /// Coupon Value Amount
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 487-NE</para>
        /// <para>Value of the coupon.</para>
        /// </remarks>
        [NcpdpFieldAttribute("487-NE")]
        public decimal CouponValueAmount { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties
        /// </summary>
        /// <param name="s">NCPPD string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static CouponSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new CouponSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public CouponSegment(string[] fields)
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
                    case "KE":
                        this.CouponType = ncpdpFieldValue;
                        break;
                    case "ME":
                        this.CouponNumber = ncpdpFieldValue;
                        break;
                    case "NE":
                        this.CouponValueAmount = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
