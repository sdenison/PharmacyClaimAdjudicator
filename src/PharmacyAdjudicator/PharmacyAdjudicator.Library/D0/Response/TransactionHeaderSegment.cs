using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Response
{
    public class TransactionHeaderSegment
    {
        /// <summary>
        /// Version Number
        /// </summary>
        /// <remarks>
        /// NCPDP 102-A2
        /// Code Uniquely identifying the transmission syntax and corresponding Data Dictionary.
        /// </remarks>
        [Required]
        [MaxLength(2)]
        public string VersionNumber { get; set; }

        /// <summary>
        /// Transaction Code
        /// </summary>
        /// <remarks>
        /// NCPDP 103-A3
        /// Code identifying the type of transaction.
        /// </remarks>
        [Required]
        public string TransactionCode { get; set; }

        /// <summary>
        /// Transaction Count
        /// </summary>
        /// <remarks>
        /// NCPDP 109-A9
        /// Count of transactions in the transmission.
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("109-A9")]
        public int TransactionCount { get; set; }

        /// <summary>
        /// Header Response Status
        /// </summary>
        /// <remarks>
        /// NCPDP 501-F1
        /// Code Indicating the status of the transmission.
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("501-F1")]
        public string HeaderResponseStatus { get; set; }

        /// <summary>
        /// Service Provider ID Qualifier
        /// </summary>
        /// <remarks>
        /// NCPDP 202-B2
        /// Code qualifying the 'Service Provider ID'.
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("202-B2")]
        public string ServiceProviderIdQualifier { get; set; }

        /// <summary>
        /// Service Provider ID
        /// </summary>
        /// <remarks>
        /// NCPDP 201-B1
        /// ID assigned to a pharmacy or provider
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("201-B1")]
        public string ServiceProviderId { get; set; }

        /// <summary>
        /// Date of Service
        /// </summary>
        /// <remarks>
        /// NCPDP 401-D1
        /// Identifies date the prescription was filled or
        /// professional service rendered or subsequent
        /// payer began coverage following Part A
        /// expiration in long-term care setting only.
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("401-D1")]
        public DateTime DateOfService { get; set; }

        public TransactionHeaderSegment()
        {
        }

        /// <summary>
        /// Parses a string into a TransactionHeaderSegment
        /// </summary>
        /// <param name="s">String value to parse</param>
        /// <returns>A new TransactionHeaderSegmnet</returns>
        public TransactionHeaderSegment Parse(string s)
        {
            if (s.Length != 31)
                throw new InvalidResponseException("TransactionHeaderSegment = " + s);
            TransactionHeaderSegment ths = new TransactionHeaderSegment();
            this.VersionNumber = s.Substring(6, 2);
            this.TransactionCode = s.Substring(8, 2);
            this.TransactionCount = int.Parse(s.Substring(20, 1));
            this.ServiceProviderIdQualifier = s.Substring(21, 2);
            this.ServiceProviderId = s.Substring(23, 16).Trim();
            this.DateOfService = DateTime.ParseExact(s.Substring(39, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            return ths;
        }

        public TransactionHeaderSegment(Submitted.TransactionHeaderSegment submittedThs)
        {
            this.VersionNumber = submittedThs.VersionNumber;
            this.TransactionCode = submittedThs.TransactionCode;
            this.TransactionCount = submittedThs.TransactionCount;
            this.ServiceProviderIdQualifier = submittedThs.ServiceProviderIdQualifier;
            this.ServiceProviderId = submittedThs.ServiceProviderId;
            this.DateOfService = submittedThs.DateOfService;
        }

        public string ToNcpdpString()
        {
            StringBuilder returnValue = new StringBuilder();
            returnValue.Append(this.VersionNumber);
            returnValue.Append(this.TransactionCode);
            returnValue.Append(this.TransactionCount);
            returnValue.Append(this.HeaderResponseStatus);
            returnValue.Append(this.ServiceProviderIdQualifier);
            returnValue.Append(FormatText(this.ServiceProviderId, 15, ' ', false));
            returnValue.Append(this.DateOfService.ToString("yyyyMMdd"));
            return returnValue.ToString();
        }

        private string FormatText(string s, int width, char paddingChar, bool padLeft)
        {
            if (s.Length > width)
                return s.Substring(0, width);
            else
                if (padLeft)
                    return s.PadLeft(width, paddingChar);
                else
                    return s.PadRight(width, paddingChar);
        }
    }
}
