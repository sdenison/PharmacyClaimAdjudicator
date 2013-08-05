using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAdjudicator.Library.D0.Submitted
{
    /// <summary>
    /// Transaction Header Segment data container 
    /// </summary>
    public class TransactionHeaderSegment
    {
        /// <summary>
        /// Bin Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 101-A1</para>
        /// <para>Card Issuer ID or Bank ID Number used for network routing.</para>
        /// </remarks>
        [Required]
        [MaxLength(6)]
        [NcpdpFieldAttribute("101-A1")]
        public string BinNumber { get; set; }

        /// <summary>
        /// Version Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 102-A2</para>
        /// <para>>Code Uniquely identifying the transmission syntax and corresponding Data Dictionary.</para>
        /// </remarks>
        [Required]
        [MaxLength(2)]
        [NcpdpFieldAttribute("102-A2")]
        public string VersionNumber { get; set; }

        /// <summary>
        /// Transaction Code
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 103-A3</para>
        /// <para>Code identifying the type of transaction.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("103-A3")]
        public string TransactionCode { get; set; }

        /// <summary>
        /// Processor Control Number
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 104-A4</para>
        /// <para>Number assigned by the processor.</para>
        /// </remarks>
        [Required]
        [MaxLength(10)]
        [NcpdpFieldAttribute("104-A4")]
        public string ProcessorControlNumber { get; set; }

        /// <summary>
        /// Transaction Count
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 109-A9</para>
        /// <para>Count of transactions in the transmission.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("109-A9")]
        public int TransactionCount { get; set; }

        /// <summary>
        /// Service Provider ID Qualifier
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 202-B2</para>
        /// <para>Code qualifying the 'Service Provider ID'.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("202-B2")]
        public string ServiceProviderIdQualifier { get; set; }

        /// <summary>
        /// Service Provider ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 201-B1</para>
        /// <para>ID assigned to a pharmacy or provider</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("201-B1")]
        public string ServiceProviderId { get; set; }

        /// <summary>
        /// Date of Service
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 401-D1</para>
        /// <para>Identifies date the prescription was filled or
        /// professional service rendered or subsequent
        /// payer began coverage following Part A
        /// expiration in long-term care setting only.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("401-D1")]
        public DateTime DateOfService { get; set; }

        /// <summary>
        /// Software Vendeor ID
        /// </summary>
        /// <remarks>
        /// <para>NCPDP 110-AK</para>
        /// <para>ID assigned by the switch or processor to identify
        /// the software source.</para>
        /// </remarks>
        [Required]
        [NcpdpFieldAttribute("110-AK")]
        public string SoftwareVendorId { get; set; }

        //Require use of factory methods.
        private TransactionHeaderSegment()
        {
        }
        
        /// <summary>
        /// Provides new Transaction Header Segment
        /// </summary>
        /// <param name="s">String from transmission representing the Transaction Header Segment.</param>
        public TransactionHeaderSegment(string s)
        {
            if (s.Length != 56)
                throw new InvalidIncomingLineException("TransactionHeaderSegment = " + s);
            this.BinNumber = s.Substring(0, 6);
            this.VersionNumber = s.Substring(6, 2);
            this.TransactionCode = s.Substring(8, 2);
            this.ProcessorControlNumber = s.Substring(10, 10);
            this.TransactionCount = int.Parse(s.Substring(20, 1));
            this.ServiceProviderIdQualifier = s.Substring(21, 2);
            this.ServiceProviderId = s.Substring(23, 15).Trim();
            this.DateOfService = DateTime.ParseExact(s.Substring(38, 8), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            this.SoftwareVendorId = s.Substring(46, 10).Trim();
        }

        /// <summary>
        /// Parses a string into a TransactionHeaderSegment
        /// </summary>
        /// <param name="s">String value to parse</param>
        /// <returns>A new TransactionHeaderSegmnet</returns>
        public TransactionHeaderSegment Parse(string s)
        {
            TransactionHeaderSegment ths = new TransactionHeaderSegment(s);
            return ths;
        }
    }
}
