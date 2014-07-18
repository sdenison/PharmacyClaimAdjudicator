using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace PharmacyAdjudicator.ModernUI.Interface
{
    public interface INavigationService
    {
        void Navigate<T>(object parameter = null);
        void OpenIndependentWindow<T>(IScreen vm);
        void GoBack();
        ObservableCollection<ModernWindow> OpenPatients();
        void ShowWindow(ModernWindow window);
    }
}
