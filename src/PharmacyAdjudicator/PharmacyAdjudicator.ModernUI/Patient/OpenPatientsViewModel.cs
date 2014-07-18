using Caliburn.Micro;
using PharmacyAdjudicator.ModernUI.Interface;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    public class OpenPatientsViewModel : Screen
    {
        private INavigationService _navigationService;

        [ImportingConstructor]
        public OpenPatientsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<ModernWindow> PatientWindows
        {
            set { }
            get
            {
                return _navigationService.OpenPatients();
            }
        }

        new public void Refresh()
        {
            NotifyOfPropertyChange(() => this.PatientWindows);
        }

        public ModernWindow SelectedPatientWindow
        {
            get;
            set;
        }

        public void ShowWindow(ModernWindow window)
        {
            _navigationService.ShowWindow(window);
        }
    }
}
