using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Enums
{
    public enum Gender
    {
        [Description("Not Set")]
        NotSet = 0,
        Male = 1,
        Female = 2
    }
}
