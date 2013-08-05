using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PharmacyAdjudicator.Library.Utils
{
    public class NcpdpString
    {
        /// <summary>
        /// Gets the NCPDP Attribute only
        /// </summary>
        /// <typeparam name="T">Type derived from the lamda expression</typeparam>
        /// <param name="property">Proprety with NcpdpFieldAttribute</param>
        /// <returns></returns>
        public static string GetNcpdpFieldAttribute<T>(Expression<Func<T>> property)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            NcpdpFieldAttribute ncpdpField = (NcpdpFieldAttribute)propertyInfo.GetCustomAttribute(typeof(NcpdpFieldAttribute));
            if (ncpdpField == null)
                return "";
            return FieldSeparator + ncpdpField.ToNcpdpPrefix();
        }

        /// <summary>
        /// Returns the property in NCPDP format
        /// </summary>
        /// <typeparam name="T">Type derived from the lamda expression</typeparam>
        /// <param name="property">Property with NcpdpFieldAttribute</param>
        /// <param name="fieldValue">String represending the field value</param>
        /// <returns></returns>
        public static string ToNcpdpFieldString<T>(Expression<Func<T>> property, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
                return "";
            string fieldAttribute = GetNcpdpFieldAttribute(property);
            if (fieldAttribute == null)
                return "";
            return fieldAttribute + fieldValue;
        }

        /// <summary>
        /// Returns the property in NCPDP format
        /// </summary>
        /// <typeparam name="T">Type derived from the lamda expression</typeparam>
        /// <param name="property">Property with NcpdpFieldAttribute</param>
        /// <param name="fieldValue">String represending the field value</param>
        /// <returns></returns>
        public static string ToNcpdpFieldString<T>(Expression<Func<T>> property, decimal? fieldValue, bool toOverpunch, int decimalPoints)
        {
            //Needs to convert 
            if (fieldValue == null)
                return "";
            string fieldAttribute = GetNcpdpFieldAttribute(property);
            if (fieldAttribute == null)
                return "";
            if (toOverpunch)
            {
                string overpunchedValue;
                int fieldValueInt = (int)((double)fieldValue.Value * Math.Pow(10, decimalPoints));
                overpunchedValue = Overpunch.Format(fieldValueInt);
                return fieldAttribute + overpunchedValue;
            }
            else
            {
                int fieldValueInt = (int)((double)fieldValue.Value * Math.Pow(10,decimalPoints));
                return fieldAttribute + fieldValueInt.ToString();
            }
        }

        public static string ToNcpdpFieldStringFromCurrency<T>(Expression<Func<T>> property, decimal? fieldValue)
        {
            return ToNcpdpFieldString(property, fieldValue, true, 2);
        }

        //Separators used after transmission header
        public const char SegmentSeparator = (char)30; //'1E
        public const char GroupSeparator = (char)29; //1D
        public const char FieldSeparator = (char)28; //1C
    }
}
