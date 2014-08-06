using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel;
using CslaContrib.Caliburn.Micro;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientSearchViewModel : ScreenWithModel<Library.Core.PatientSearchCriteria>, INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Interface.IDialog _dialogManager;
        private readonly Interface.INavigationService _navigationService;
        private readonly IWindowManager _windowManager;
        private readonly IHaveWindowsForType _openWindows;
        private LinkCollection _patientListLinks;
        public LinkCollection PatientListLinks
        {
            get
            {
                return _patientListLinks;
            }
            set
            {
                _patientListLinks = value;
                NotifyOfPropertyChange("PatientListLinks");
            }
        }

        public string SelectedPatientFirstName
        {
            get
            {
                return _selectedPatient == null ? "name not set" : _selectedPatient.FirstName;
            }
        }

        private Library.Core.Patient _selectedPatient;
        public Library.Core.Patient SelectedPatient 
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;
                NotifyOfPropertyChange("SelectedPatient");
                NotifyOfPropertyChange("SelectedPatientFirstName");
            }
        }

        private ObservableCollection<Library.Core.Patient> _results;
        public ObservableCollection<Library.Core.Patient> Results
        {
            get { return _results; }
            set { _results = value; NotifyOfPropertyChange("Results"); }
        }

        //private Library.Core.PatientList _results;
        //public Library.Core.PatientList Results
        //{
        //    get { return _results; }
        //    set { _results = value; NotifyOfPropertyChange("Results"); }
        //}

        [ImportingConstructor]
        public PatientSearchViewModel(IEventAggregator eventAggregator, Interface.IDialog dialogManager, Interface.INavigationService navigationService, IWindowManager windowManager, IHaveWindowsForType openWindows)
        {
            _eventAggregator = eventAggregator;
            _dialogManager = dialogManager;
            _navigationService = navigationService;
            _windowManager = windowManager;
            _openWindows = openWindows;
            this.Model = new Library.Core.PatientSearchCriteria();
            var patients = Library.Core.PatientList.NewPatientList();
            this.PatientListLinks = ConvertPatientList(patients);
        }

        private LinkCollection ConvertPatientList(Library.Core.PatientList patients)
        {
            var links = new LinkCollection();
            var resultNumber = 0;
            foreach (var patient in patients)
            {
                links.Add(new Link() { DisplayName = patient.FirstName + " " + patient.LastName + " " + patient.DateOfBirth.Value.ToString("MM/dd/yyyy"), Source = new Uri(resultNumber.ToString(), UriKind.Relative) });
                resultNumber++;
            }
            return links;
        }

        public async void Search()
        {
            if ((string.IsNullOrWhiteSpace(Model.PatientFirstName)) && (string.IsNullOrWhiteSpace(Model.PatientLastName))
                && (string.IsNullOrWhiteSpace(Model.GroupId)) && (string.IsNullOrWhiteSpace(Model.CardholderId)))
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                _dialogManager.ShowMessage("Please enter search criteria", "Search Criteria Missing", btn);
            }
            else
            {
                this.IsBusy = true;
                var patientSearchResutls = await Library.Core.PatientList.GetBySearchObjectAsync(this.Model);
                if (patientSearchResutls.Count == 0)
                {
                    MessageBoxButton btn = MessageBoxButton.OK;
                    _dialogManager.ShowMessage("No patients found for search criteria.", "No Records Found", btn);
                    NotifyOfPropertyChange(() => this.CanShowResults);
                }
                this.Results = patientSearchResutls;
                NotifyOfPropertyChange(() => this.CanShowResults);
                this.IsBusy = false;
            }
        }

        public async Task ShowPatient(Library.Core.Patient patient)
        {
            this.IsBusy = true;
            var patientViewModel = await PatientEditViewModel.BuildViewModelAsync(patient.PatientId, _eventAggregator);
            _windowManager.ShowWindow(patientViewModel);
            this.IsBusy = false;
        }

        public bool CanShowResults
        {
            get
            {
                if ((_results != null) && (_results.Count > 0))
                    return true;
                return false;
            }
            private set { }
        }
    }
}
