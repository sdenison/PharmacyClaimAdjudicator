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

        public object FindPropertyInObject(object objectToFindPropertyIn)
        {
            if (objectToFindPropertyIn == null)
                return null;
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
                else if (Attribute.IsDefined(propertyInfo, typeof(NcpdpLoopAttribute)))
                {
                    //Right now, don't do anything.  Just want lists to not be processed as regular properties.
                }
                else if (!(object.ReferenceEquals(propertyInfo.DeclaringType, objectToFindPropertyIn.GetType())))
                {
                    //Should be inherited members that we want to ignore.
                    var x = "break here";
                }
                else
                {
                    //We don't want to send the property for evaluation of it's a primitive
                    if ((!objectToFindPropertyIn.GetType().IsPrimitive) && (!(objectToFindPropertyIn.GetType() == typeof(string))))
                    {
                        //Only return if that NCPDP value was found in the property
                        var returnValue = FindPropertyInObject(propertyInfo.GetValue(objectToFindPropertyIn));
                        if (returnValue != null)
                            return returnValue;
                    }
                }
            }
            return null;
        }
    }
}
