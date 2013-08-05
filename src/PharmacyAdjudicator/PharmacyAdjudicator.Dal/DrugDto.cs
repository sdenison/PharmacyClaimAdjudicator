using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Dal
{
    public class DrugDto
    {
        public string Ndc { get; set; }
        public string BrandName { get; set; }
        public string Upn { get; set; }
        public string VaClass { get; set; }
        public string PkgType { get; set; }
    }
}
