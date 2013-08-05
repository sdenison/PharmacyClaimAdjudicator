using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class PatientSegment
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
        /// Patient First Name
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 310-CA</para>
        /// <para>Individual first name.</para>
        /// </remarks>
        [NcpdpFieldAttribute("310-CA")]
        [MaxLength(35)]
        public string PatientFirstName { get; set; }

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
        public string PatientLastName { get; set; }

        /// <summary>
        /// Date Of Birth
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 304-C4</para>
        /// <para>Date of birth of patient</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("304-C4")]
        public DateTime? DateOfBirth { get; set; }

        public PatientSegment()
        {
            this.SegmentIdentification = "29";
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PatientFirstName, this.PatientFirstName));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.PatientLastName, this.PatientLastName));
            if (this.DateOfBirth != null)
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.DateOfBirth, this.DateOfBirth.Value.ToString("yyyyMMdd")));

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
