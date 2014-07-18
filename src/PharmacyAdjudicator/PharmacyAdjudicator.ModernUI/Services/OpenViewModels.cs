using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.Services
{
    public class OpenViewModels : Interface.IOpenViewModels
    {
        public OpenViewModels()
        {
            openViewModels = new Dictionary<Type, List<object>>();
        }

        private Dictionary<Type, List<object>> openViewModels;
        Dictionary<Type, List<object>> Interface.IOpenViewModels.OpenViewModels
        {
            get
            {
                return openViewModels;
            }
            set
            {
                openViewModels = value;
            }
        }
    }
}
