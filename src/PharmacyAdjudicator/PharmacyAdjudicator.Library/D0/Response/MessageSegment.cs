using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class MessageSegment
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
        /// Message
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 504-F4</para>
        /// <para>Free form message.</para>
        /// </remarks>
        [Required]
        [MaxLength(200)]
        [NcpdpFieldAttribute("504-F4")]
        public string Message { get; set; }

        public MessageSegment()
        {
            this.SegmentIdentification = "20";
            this.Message = string.Empty;
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.Message, this.Message));

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }

            return returnValue.ToString();
        }
    }
}
