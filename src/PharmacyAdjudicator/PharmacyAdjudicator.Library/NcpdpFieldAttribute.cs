using System;

namespace PharmacyAdjudicator.Library
{
    /// <summary>
    /// Hods the property's NCPDP Field value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NcpdpFieldAttribute : System.Attribute
    {
        public string NcpdpFieldName { get; set; }

        public NcpdpFieldAttribute(string ncpdpFieldName)
        {
            this.NcpdpFieldName = ncpdpFieldName;
        }

        public string ToNcpdpPrefix()
        {
            if ((string.IsNullOrEmpty(this.NcpdpFieldName)) || (this.NcpdpFieldName.Length < 2))
                return "";
            return this.NcpdpFieldName.Substring(this.NcpdpFieldName.Length - 2);
        }
    }
}
