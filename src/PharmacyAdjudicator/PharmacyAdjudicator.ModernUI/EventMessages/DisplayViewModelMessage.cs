using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.EventMessages
{
    public class DisplayViewModelMessage
    {
        public object Requestor { get; set; }
        public object ViewModel { get; set; }
    }
}
