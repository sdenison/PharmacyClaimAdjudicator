using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Rules
{

    /// <summary>
    /// Helper class to extract rule types from Transaction class
    /// </summary>
    public static class RuleTypes
    {
        public static List<string> GetInferrableProperties()
        {
            return GetPropertiesOfType(typeof(InferrableAttribute));
        }

        public static List<string> GetFactProperties()
        {
            return GetPropertiesOfType(typeof(FactAttribute));
        }

        private static List<string> GetPropertiesOfType(Type type)
        {
            var returnValue = new List<string>();
            List<PropertyInfo> properties = new List<PropertyInfo>(typeof(Transaction).GetProperties());
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, type)) 
                    returnValue.Add(property.Name);
            }
            returnValue.Sort();
            return returnValue;
        }

        public static List<string> GetFactProperties(Type type)
        {
            var returnValue = new List<string>();
            List<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(FactAttribute)))
                    returnValue.Add(property.Name);
            }
            returnValue.Sort();
            return returnValue;
        }

        public static List<string> GetTypes()
        {
            var returnValue = new List<string>();
            returnValue.Add("Transaction");
            List<PropertyInfo> properties = new List<PropertyInfo>(typeof(Transaction).GetProperties());
            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(ComplexFactAttribute)))
                    returnValue.Add(property.Name);
            }
            returnValue.Sort();
            return returnValue;
        }
    }
}
