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

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientSearchViewModel : ScreenWithModel<Library.Core.PatientSearchCriteria>, INotifyPropertyChanged
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Interface.IDialog _dialogManager;
        private PatientContentLoader _patientContentLoader;
        public PatientContentLoader PatientContentLoader 
        {
            get
            {
                return _patientContentLoader;
            }
            set
            {
                _patientContentLoader = value;
                NotifyOfPropertyChange("PatientContentLoader");
            }
        }

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

        [ImportingConstructor]
        public PatientSearchViewModel(IEventAggregator eventAggregator, Interface.IDialog dialogManager)
        {
            //this.IsBusy = true;

            
            _eventAggregator = eventAggregator;
            _dialogManager = dialogManager;

            this.Model = new Library.Core.PatientSearchCriteria();
            //var patients = Library.Core.PatientList.GetAll();
            //_results = new ObservableCollection<Library.Core.Patient>(patients);
            //this.PatientListLinks = ConvertPatientList(patients);
            //_patientContentLoader = new PatientContentLoader(patients);

            var patients = Library.Core.PatientList.NewPatientList();
            _results = new ObservableCollection<Library.Core.Patient>(patients);
            this.PatientListLinks = ConvertPatientList(patients);
            _patientContentLoader = new PatientContentLoader(patients);
            //this.PatientListLinks = 




            //this.IsBusy = false;
            //GetDefaultPatients();
            //_initializingTask = GetDefaultPatients();
            //Task.WaitAll(_initializingTask);
            //_results = new ObservableCollection<Library.Core.Patient>(_defaultPatients);
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
                }
                _results.Clear();
                patientSearchResutls.ToList().ForEach(p => _results.Add(p));
                this.PatientListLinks = ConvertPatientList(patientSearchResutls);
                this.PatientContentLoader = new PatientContentLoader(patientSearchResutls);
                this.IsBusy = false;
            }
        }
    }
}
