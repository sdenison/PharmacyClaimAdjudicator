using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    public class ClinicalSegment
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
        /// Diagnosis Code Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 491-VE</para>
        /// <para>Count of diagnosis occurrences</para>
        /// </remarks>
        [NcpdpFieldAttribute("491-VE")]
        public int DiagnosisCodeCount { get; set; }

        /// <summary>
        /// List of Diagnoses in the Clinical Segment
        /// </summary>
        public List<DiagnosisContainer> Diagnoses { get; set; }

        /// <summary>
        /// Parses incoming NCPDP string into properties.
        /// </summary>
        /// <param name="s">NCPDP string</param>
        /// <param name="delimiter">Field separator</param>
        /// <returns></returns>
        public static ClinicalSegment Parse(string s, char delimiter)
        {
            string[] fields = s.Split(delimiter);
            if (fields.Length > 1)
            {
                return new ClinicalSegment(fields);
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
        public ClinicalSegment(string[] fields)
        {
            DiagnosisContainer currentDiagnosis = null;
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
                    case "VE":
                        this.DiagnosisCodeCount = int.Parse(ncpdpFieldValue);
                        this.Diagnoses = new List<DiagnosisContainer>();
                        break;
                    case "WE":
                        currentDiagnosis = new DiagnosisContainer();
                        currentDiagnosis.DiagnosisCodeQualifier = ncpdpFieldValue;
                        this.Diagnoses.Add(currentDiagnosis);
                        break;
                    case "DO":
                        currentDiagnosis.DiagnosisCode = ncpdpFieldValue;
                        break;
                    case "XE":
                        currentDiagnosis.ClinicalInformationCounter = int.Parse(ncpdpFieldValue);
                        break;
                    case "ZE":
                        currentDiagnosis.MeasurementDate = ncpdpFieldValue;
                        break;
                    case "H1":
                        currentDiagnosis.MeasurementTime = ncpdpFieldValue;
                        break;
                    case "H2":
                        currentDiagnosis.MeasurementDimension = ncpdpFieldValue;
                        break;
                    case "H3":
                        currentDiagnosis.MeasurementUnit = ncpdpFieldValue;
                        break;
                    case "H4":
                        currentDiagnosis.MeasurementValue = ncpdpFieldValue;
                        break;
                    default:
                        break;
                }
            }
        }

        public class DiagnosisContainer
        {
            /// <summary>
            /// Diagnosis Code Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 492-WE</para>
            /// <para>Code qualifying the 'Diagnosis Code' (424-DO).</para>
            /// </remarks>
            [NcpdpFieldAttribute("492-WE")]
            public string DiagnosisCodeQualifier { get; set; }

            /// <summary>
            /// Diagnosis Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 424-DO</para>
            /// <para>Code identifying the diagnosis of the patient.</para>
            /// </remarks>
            [NcpdpFieldAttribute("424-DO")]
            public string DiagnosisCode { get; set; }

            /// <summary>
            /// Clinical Information Counter
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 493-XE</para>
            /// <para>
            /// Counter number of clinical information measurement set/logical 
            /// grouping.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("493-XE")]
            public int ClinicalInformationCounter { get; set; }

            /// <summary>
            /// Measurement Date
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 494-ZE</para>
            /// <para>Date clinical information was collected or measured.</para>
            /// </remarks>
            [NcpdpFieldAttribute("494-ZE")]
            public string MeasurementDate { get; set; }

            /// <summary>
            /// Measurement Time
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 495-H1</para>
            /// <para>Time clinical information was collected or measured.</para>
            /// </remarks>
            [NcpdpFieldAttribute("495-H1")]
            public string MeasurementTime { get; set; }

            /// <summary>
            /// Measurement Dimension
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 496-H2</para>
            /// <para>
            /// Code indicating the clinical domain of the observed value in 
            /// ‘Measurement Value’ (499-H4).
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("496-H2")] 
            public string MeasurementDimension { get; set; }

            /// <summary>
            /// Measurement Unit
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 497-H3</para>
            /// <para>
            /// Code indicating the metric or English units used with the clinical 
            /// information.
            /// </para>
            /// </remarks>
            [NcpdpFieldAttribute("497-H3")]
            public string MeasurementUnit { get; set; }

            /// <summary>
            /// Measurement Value
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 499-H4</para>
            /// <para>Actual value of clinical information.</para>
            /// </remarks>
            [NcpdpFieldAttribute("499-H4")]
            public string MeasurementValue { get; set; }
        }
    }
}
