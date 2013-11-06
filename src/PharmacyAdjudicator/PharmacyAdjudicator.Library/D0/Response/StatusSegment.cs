using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class StatusSegment //: NcpdpBindable
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
        /// Transaction Response Status
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 112-AN</para>
        /// <para>Code indicating the status of the transaction.</para>
        /// </remarks>
        [Required]
        [NcpdpField("112-AN")]
        public string TransactionResponseStatus { get; set; }

        /// <summary>
        /// Authorization Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 503-F3</para>
        /// <para>
        /// Number assigned by the processor to identify an authorized transaction.
        /// </para>
        /// </remarks>
        [NcpdpField("503-F3")]
        public string AuthorizationNumber { get; set; }

        /// <summary>
        /// Reject Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 510-FA</para>
        /// <para>Count of ‘Reject Code’ (511-FB) occurrences.</para>
        /// </remarks>
        [NcpdpField("510-FA")]
        public int? RejectCount { get; set; }

        /// <summary>
        /// List of rejects
        /// </summary>
        /// <remarks>
        /// Not used in Claim/Billing Encounter
        /// </remarks>
        public List<RejectContainer> Rejects { get; set; }

        /// <summary>
        /// Additional Message Information Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 130-UF</para>
        /// <para>
        /// Count of the ‘Additional Message Information’ (526-FQ) occurrences 
        /// that follow.
        /// </para>
        /// </remarks>
        [NcpdpField("130-UF")]
        public int? AdditionalMessageInformationCount { get; set; }

        public List<AdditionalMessageContainer> AdditionalMessages { get; set; }

        /// <summary>
        /// Approved Message Code Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 547-5F</para>
        /// <para>Count of the 'Approved Message Code' (548-6F) occurrences.</para>
        /// </remarks>
        [NcpdpField("547-5F")]
        public int? ApprovedMessageCodeCount { get; set; }

        /// <summary>
        /// List of Approved Messages
        /// </summary>
        public List<ApprovedMessage> ApprovedMessages { get; set; }

        /// <summary>
        /// Help Desk Phone Number Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 549-7F</para>
        /// <para>
        /// Code qualifying the phone number in the 'Help Desk Phone Number' (550-8F).
        /// </para>
        /// </remarks>
        [NcpdpField("549-7F")] 
        public string HelpDeskPhoneNumberQualifier { get; set; }

        /// <summary>
        /// Help Desk Phone Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 550-8F</para>
        /// <para>Ten digit phone number of the help desk.</para>
        /// </remarks>
        [MaxLength(18)]
        [NcpdpField("550-8F")]
        public string HelpDeskPhoneNumber { get; set; }

        /// <summary>
        /// Transaction Reference Number 
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 880-K5</para>
        /// <para>
        /// A reference number assigned by the provider to each of the data 
        /// records in the batch or real-time transactions. The purpose of this 
        /// number is to facilitate the process of matching the transaction 
        /// response to the transaction. The transaction reference number 
        /// assigned should be returned in the response.
        /// </para> 
        /// </remarks>
        [NcpdpField("880-K5")]
        public string TransactionReferenceNumber { get; set; }

        /// <summary>
        /// Internal Control Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 993-A7</para>
        /// <para>
        /// Number assigned by the processor to identify an adjudicated claim 
        /// when supplied in payer-to-payer coordination of benefits only.
        /// </para>
        /// </remarks>
        [NcpdpField("993-A7")]
        public string InternalControlNumber { get; set; } 

        /// <summary>
        /// URL
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 987-MA</para>
        /// <para>The web page address.</para>
        /// </remarks>
        [NcpdpField("987-MA")]
        public string Url { get; set; }


        public StatusSegment()
        {
            this.SegmentIdentification = "21";
        }

        public StatusSegment(Core.Transaction transaction)
        {
            this.SegmentIdentification = "21";
            this.AuthorizationNumber = transaction.AuthorizationNumber;
            this.TransactionResponseStatus = Core.Enums.ResponseStatusConverter.ToString(transaction.ResponseStatus);
            
            this.HelpDeskPhoneNumberQualifier = "03";
            this.HelpDeskPhoneNumber = "8305158129";
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();

            //Append properties to returnValue.
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.TransactionResponseStatus, this.TransactionResponseStatus));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.AuthorizationNumber, this.AuthorizationNumber));
            if (this.RejectCount > 0)
            {
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.RejectCount, this.RejectCount.ToString()));
                foreach (var reject in this.Rejects)
                    returnValue.Append(reject.ToNcpdpString());
            }

            if (this.AdditionalMessageInformationCount > 0)
            {
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.AdditionalMessageInformationCount, this.AdditionalMessageInformationCount.ToString()));
                foreach (var message in AdditionalMessages)
                {
                    returnValue.Append(message.ToNcpdpString());
                }
            }

            if (this.ApprovedMessageCodeCount > 0)
            {
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.ApprovedMessageCodeCount, this.ApprovedMessageCodeCount.ToString()));
                foreach (var message in this.ApprovedMessages)
                    returnValue.Append(message.ToNcpdpString());
            }

            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.HelpDeskPhoneNumberQualifier, this.HelpDeskPhoneNumberQualifier));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.HelpDeskPhoneNumber, this.HelpDeskPhoneNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.TransactionReferenceNumber, this.TransactionReferenceNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.InternalControlNumber, this.InternalControlNumber));
            returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.Url, this.Url));

            //Adds segment separator and identifier to beginning if the segment has data.
            if (returnValue.Length > 0)
            {
                returnValue.Insert(0, Utils.NcpdpString.ToNcpdpFieldString(() => this.SegmentIdentification, this.SegmentIdentification));
                returnValue.Insert(0, Utils.NcpdpString.SegmentSeparator);
            }

            return returnValue.ToString();
        }

        public class RejectContainer
        {
            /// <summary>
            /// Reject Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 511-FB</para>
            /// <para>Code including the error encountered.</para>
            /// </remarks>
            [Required]
            [NcpdpField("511-FB")]
            public string RejectCode { get; set; }

            /// <summary>
            /// Reject Field Occurrence Indicator
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 546-4F</para>
            /// <para>
            /// Identifies the counter number or occurrence of the field that 
            /// is being rejected. Used to indicate rejects for repeating fields.
            /// </para>
            /// </remarks>
            [Required]
            [NcpdpField("546-4F")]
            public int? RejectFieldOccurrenceIndicator { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();
                //Append properties to returnValue.
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.RejectCode, this.RejectCode));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.RejectFieldOccurrenceIndicator, this.RejectFieldOccurrenceIndicator.ToString()));
                return returnValue.ToString();
            }
        }

        public class ApprovedMessage
        {
            /// <summary>
            /// Approved Messsage Code
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 548-6F</para>
            /// <para>
            /// Message code, on an approved claim/service, communicating the 
            /// need for an additional follow-up.
            /// </para>
            /// </remarks>
            [NcpdpField("548-6F")]
            public string ApprovedMessageCode { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();
                //Append properties to returnValue.
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.ApprovedMessageCode, this.ApprovedMessageCode));
                return returnValue.ToString();
            }
        }

        public class AdditionalMessageContainer
        {
            /// <summary>
            /// Additional Message Information Qualifier
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 132-UH</para>
            /// <para>
            /// Format qualifier of the ‘Additional Message Information’ (526-FQ) 
            /// that follows. Each value may occur only once per transaction and 
            /// values must be ordered sequentially (numeric characters precede 
            /// alpha characters, i.e., 0-9, A-Z).
            /// </para>
            /// </remarks>
            [Required]
            [NcpdpField("132-UH")]
            public string AdditionalMessageInformationQualifier { get; set; }

            /// <summary>
            /// Additional Message Information
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 526-FQ</para>
            /// <para>Free text message.</para>
            /// </remarks>
            [Required]
            [NcpdpField("526-FQ")]
            public string AdditionalMessageInformation { get; set; }

            /// <summary>
            /// Additional Message Information Continuity
            /// </summary>
            /// <remarks>
            /// <para>NCPDP 131-UG</para>
            /// <para>
            /// Indicates continuity of the text found in the current repetition 
            /// of ‘Additional Message Information’ (526-FQ) with the text found 
            /// in the next repetition that follows.
            /// </para>
            /// </remarks>
            [NcpdpField("131-UG")]
            public string AdditionalMessageInformationContinuity { get; set; }

            public string ToNcpdpString()
            {
                StringBuilder returnValue = new StringBuilder();
                //Append properties to returnValue.
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.AdditionalMessageInformationQualifier, this.AdditionalMessageInformationQualifier));
                returnValue.Append(Utils.NcpdpString.ToNcpdpFieldString(() => this.AdditionalMessageInformation, this.AdditionalMessageInformation));
                return returnValue.ToString();
            }
        } 
    }
}
