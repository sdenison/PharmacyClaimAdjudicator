using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library
{
    /// <summary>
    /// Used to denote a property representing a repeating group of fileds
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NcpdpLoopAttribute : Attribute 
    {
        public string NcpdpFieldName { get; set; }

        public NcpdpLoopAttribute(string ncpdpFieldName)
        {
            this.NcpdpFieldName = ncpdpFieldName;
        }
    }
}
