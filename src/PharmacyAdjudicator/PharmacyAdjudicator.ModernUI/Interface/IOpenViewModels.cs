using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.Interface
{
    public interface IOpenViewModels
    {
        Dictionary<Type, List<object>> OpenViewModels { get; set;}
    }
}
