using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class AdditionalDocumentationSegment
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
        /// Additional Documentation Type ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 369-2Q</para> 
        /// <para>Unique identifier for the data being submitted.</para>
        /// </remarks>
        [MaxLength(3)]
        [NcpdpFieldAttribute("369-2Q")]
        public string AdditionalDocumentationTypeId { get; set; }

        /// <summary>
        /// Request Period Begin Date
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 374-2V</para>
        /// <para>The beginning date of need.</para>
        /// </remarks>
        [NcpdpFieldAttribute("374-2V")]
        public DateTime? RequestPeriodBeginDate { get; set; }

        /// <summary>
        /// Request Period Recert/Revised Date
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 375-2W</para>
        /// <para>
        /// The efective date of the revision or re-certification provided by 
        /// the certifying physician.
        /// </para> 
        /// </remarks>
        [NcpdpFieldAttribute("375-2W")]
        public DateTime? RequestPeriodRecertRevisedDate { get; set; }

        /// <summary>
        /// Request Status
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 373-2U</para>
        /// <para>Code identifying type of request.</para>
        /// </remarks>
        [MaxLength(1)]
        [NcpdpFieldAttribute("373-2U")]
        public string RequestStatus { get; set; }

        /// <summary>
        /// Length of Need Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 371-2S</para>
        /// <para>Code qualifying the length of need.</para>
        /// </remarks>
        [MaxLength(2)]
        [NcpdpFieldAttribute("371-2S")]
        public string LengthOfNeedQualifier { get; set; }

        /// <summary>
        /// Length of Need
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 370-2R</para>
        /// <para>
        /// Length of time the physician expects the patient to require use of 
        /// the ordered item.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("370-2R")]
        public int? LengthOfNeed { get; set; }

        /// <summary>
        /// Prescriber/Supplier Date Signed
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 372-2T</para>
        /// <para>
        /// The date the form was completed and signed by the ordering physician.
        /// </para>
        /// </remarks>
        [NcpdpFieldAttribute("372-2T")] 
        public DateTime? PrescriberSupplierDateSigned { get; set; }

        /// <summary>
        /// Supporting Documentation
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 376-2X</para>
        /// <para>Free text message.</para>
        /// </remarks>
        [NcpdpFieldAttribute("376-2X")]
        public string SupportingDocumentation { get; set; }

        /// <summary>
        /// Question Number/Letter Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 377-2Z</para> 
        /// <para>Count of Question Number/Letter occurrences.</para>
        /// </remarks>
        [NcpdpFieldAttribute("377-2Z")]
        public int QuestionCount { get; set; }

        /// <summary>
        /// List os repeating questions
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static AdditionalDocumentationSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new AdditionalDocumentationSegment(fields);
            }
            else
            {
                throw new InvalidIncomingLineException("line = " + s);
            }
        }

        public AdditionalDocumentationSegment(string[] fields)
        {
            Question currentQuestion = null;
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
                    case "2Q":
                        this.AdditionalDocumentationTypeId = ncpdpFieldValue;
                        break;
                    case "2V":
                        this.RequestPeriodBeginDate = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "2W":
                        this.RequestPeriodRecertRevisedDate = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "2U":
                        this.RequestStatus = ncpdpFieldValue;
                        break;
                    case "2S":
                        this.LengthOfNeedQualifier = ncpdpFieldValue;
                        break;
                    case "2R":
                        this.LengthOfNeed = int.Parse(ncpdpFieldValue);
                        break;
                    case "2T":
                        this.PrescriberSupplierDateSigned = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "2X":
                        this.SupportingDocumentation = ncpdpFieldValue;
                        break;
                    case "2Z":
                        this.QuestionCount = int.Parse(ncpdpFieldValue);
                        break;
                    case "4B":
                        if (this.Questions == null)
                            this.Questions = new List<Question>();
                        currentQuestion = new Question();
                        currentQuestion.QuestionNumberLetter = ncpdpFieldValue;
                        this.Questions.Add(currentQuestion);
                        break;
                    case "4D":
                        currentQuestion.QuestionPercentResponse = decimal.Parse(ncpdpFieldValue) / 100;
                        break;
                    case "4G":
                        currentQuestion.QuestionDateResponse = DateTime.ParseExact(ncpdpFieldValue, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "4H":
                        currentQuestion.QuestionDollarAmountResponse = Utils.Overpunch.ParseToCurrency(ncpdpFieldValue);
                        break;
                    case "4J":
                        currentQuestion.QuestionNumericResponse = int.Parse(ncpdpFieldValue);
                        break;
                    case "4K":
                        currentQuestion.QuestionAlphanumericResponse = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
            if (this.QuestionCount != this.Questions.Count)
                throw new InvalidIncomingLineException("Question Count does not equal number of questions.  Line = " + fields.ToString());
        }

        public class Question
        {
            /// <summary>
            /// Question Number/Letter
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 378-4B</para> 
            /// <para>
            /// Identifies the question number/letter that the question response 
            /// applies to (part of the question information).
            /// </para>
            /// </remarks>
            [MaxLength(3)]
            [NcpdpFieldAttribute("378-4B")]
            public string QuestionNumberLetter { get; set; }

            /// <summary>
            /// Question Percent Response
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 379-4D</para>
            /// <para>
            /// Percent response to a question (part of the question information).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("379-4D")]
            public decimal? QuestionPercentResponse { get; set; }

            /// <summary>
            /// Question Date Response
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 380-4G</para>
            /// <para>
            /// Date response to a question (part of the question information).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("380-4G")]
            public DateTime? QuestionDateResponse { get; set; }

            /// <summary>
            /// Question Dollar Amount Response
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 381-4H</para>
            /// <para>
            /// Dollar Amount response to a question (part of the question 
            /// information).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("381-4H")]
            public decimal? QuestionDollarAmountResponse { get; set; }

            /// <summary>
            /// Question Numeric Response
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 382-4J</para>
            /// <para>
            /// Numeric response to a question (part of the question information).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("382-4J")]
            public int? QuestionNumericResponse { get; set; }

            /// <summary>
            /// Question Alphanumeric Response
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 383-4K</para>
            /// <para>Alphanumeric response to a question (part of the question information).</para>
            /// </remarks>
            [MaxLength(30)]
            [NcpdpFieldAttribute("343-4K")]
            public string QuestionAlphanumericResponse { get; set; }
        }
    }
}
