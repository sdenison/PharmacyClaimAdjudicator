using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.Interface
{
    public interface INavigationService
    {
        void Navigate<T>(object parameter = null);
        void GoBack();
    }
}
