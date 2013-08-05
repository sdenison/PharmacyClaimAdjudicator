using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class DurSegment
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
        /// DUR segment consists of a list of DUR containers.
        /// This is a bit of a departure from the way other
        /// segments treat counter fields.
        /// </summary>
        public List<DurContainer> DurContainers { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static DurSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new DurSegment(fields);
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
        public DurSegment(string[] fields)
        {
            DurContainer currentDur = null;
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
                        this.DurContainers = new List<DurContainer>();
                        break;
                    case "7E":
                        currentDur = new DurContainer();
                        currentDur.DurPpsCodeCounter = int.Parse(ncpdpFieldValue);
                        this.DurContainers.Add(currentDur);
                        break;
                    case "E4":
                        currentDur.ReasonForServiceCode = ncpdpFieldValue;
                        break;
                    case "E5":
                        currentDur.ProfessionalServiceCode = ncpdpFieldValue;
                        break;
                    case "E6":
                        currentDur.ResultOfServiceCode = ncpdpFieldValue;
                        break;
                    case "8E":
                        currentDur.DurPpsLevelOfEffort = ncpdpFieldValue;
                        break;
                    case "J9":
                        currentDur.DurCoAgentIdQualifier = ncpdpFieldValue;
                        break;
                    case "H6":
                        currentDur.DurCoAgentId = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }

        public class DurContainer
        {
            /// <summary>
            /// DUR/PPS Code Counter
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 473-7E</para>
            /// <para>Counter number for each DUR/PPS set/logical grouping.</para>
            /// </remarks>
            [Required]
            [NcpdpFieldAttribute("473-7E")]
            public int DurPpsCodeCounter { get; set; }

            /// <summary>
            /// Reason For Service 
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 439-E4</para>
            /// <para>
            /// Code identifying the type of utilization conflict detected by the 
            /// prescriber or the pharmacist or the reason for the pharmacist’s 
            /// professional service.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("439-E4")]
            public string ReasonForServiceCode { get; set; }

            /// <summary>
            /// Professional Service Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 440-E5</para>
            /// <para>
            /// Code identifying pharmacist intervention when a conflict code has 
            /// been identified or service has been rendered.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("440-E5")]
            public string ProfessionalServiceCode { get; set; }

            /// <summary>
            /// Result of Service Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 441-E6</para> 
            /// <para>
            /// Action taken by a pharmacist or prescriber in response to a 
            /// conflict or the result of a pharmacist’s professional service.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("441-E6")]
            public string ResultOfServiceCode { get; set; }

            /// <summary>
            /// DUR/PPS Level of Effort
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 474-8E</para> 
            /// <para>
            /// Code indicating the level of effort as determined by the 
            /// complexity of decision-making or resources utilized by a 
            /// pharmacist to perform a professional service.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("474-8E")]
            public string DurPpsLevelOfEffort { get; set; }

            /// <summary>
            /// DUR Co-Agent ID Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 475-J9</para>
            /// <para>
            /// Code qualifying the value in ‘DUR Co-Agent ID’ (476-H6).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("475-J9")]
            public string DurCoAgentIdQualifier { get; set; }

            /// <summary>
            /// DUR Co-Agent ID 
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 476-H6</para>
            /// <para>
            /// Identifies the co-existing agent contributing to the DUR event 
            /// (drug or disease conflicting with the prescribed drug or prompting 
            /// pharmacist professional service).
            /// </para>
            /// </remarks>
            public string DurCoAgentId { get; set; }
        }
    }
}
