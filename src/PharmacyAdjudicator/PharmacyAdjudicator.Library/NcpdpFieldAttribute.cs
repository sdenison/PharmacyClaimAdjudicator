using System;
using System.Reflection;

namespace PharmacyAdjudicator.Library
{
    /// <summary>
    /// Holds the property's NCPDP Field value
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

        public object FindPropertyInObject(Object objectToFindPropertyIn)
        {
            foreach (PropertyInfo propertyInfo in objectToFindPropertyIn.GetType().GetProperties())
            {
                if (Attribute.IsDefined(propertyInfo, typeof(NcpdpFieldAttribute)))
                {
                    if (propertyInfo.GetValue(objectToFindPropertyIn) != null)
                    {
                        var ncpdpdFieldValue = (NcpdpFieldAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NcpdpFieldAttribute));
                        if (ncpdpdFieldValue.NcpdpFieldName.Equals(this.NcpdpFieldName))
                            return propertyInfo.GetValue(objectToFindPropertyIn);
                    }
                }
                else
                {
                    //Search the children for the attribute if not found in this object.
                    return FindPropertyInObject(propertyInfo.GetValue(objectToFindPropertyIn));
                }
            }
            return null;
        }
    }
}
