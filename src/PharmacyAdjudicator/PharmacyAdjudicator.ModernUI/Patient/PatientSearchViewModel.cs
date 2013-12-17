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
            set { _results = value;  }
        }

        [ImportingConstructor]
        public PatientSearchViewModel(IEventAggregator eventAggregator)
        {
            //this.IsBusy = true;
            _eventAggregator = eventAggregator;
            this.Model = new Library.Core.PatientSearchCriteria();
            var patients = Library.Core.PatientList.GetAll();
            _results = new ObservableCollection<Library.Core.Patient>(patients);
            this.PatientListLinks = ConvertPatientList(patients);
            _patientContentLoader = new PatientContentLoader(patients);
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

        //private readonly Task _initializingTask;

        //private Library.Core.PatientList _defaultPatients;

        //public async Task GetDefaultPatients()
        //{
        //    this.IsBusy = true;
        //    //var patients = await Library.Core.PatientList.GetAllAsync();
        //    _defaultPatients = await Library.Core.PatientList.GetAllAsync();
        //    //_results = new ObservableCollection<Library.Core.Patient>(patients);

        //    this.IsBusy = false;
        //    //return Task.Delay(0);
        //}

        public async void Search()
        {
            this.IsBusy = true;
            var patientSearchResutls = await Library.Core.PatientList.GetBySearchObjectAsync(this.Model);
            this.IsBusy = false;
            _results.Clear();
            patientSearchResutls.ToList().ForEach(p => _results.Add(p));
            this.PatientListLinks = ConvertPatientList(patientSearchResutls);
            this.PatientContentLoader = new PatientContentLoader(patientSearchResutls);
            
            //this.Results = new ObservableCollection<Library.Core.Patient>(patientSearchResutls);
            //_eventAggregator.Publish(new EventMessages.PatientSearchResultsMessage() { PatientSearchResults = patientSearchResutls });
        }


        //public event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;


        //private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(caller));
        //    }

        //}
    }

    public class PatientContentLoader : IContentLoader
    {
        private PatientContentLoader()
        {
        }

        private Library.Core.PatientList _patients;
        public PatientContentLoader(Library.Core.PatientList patients)
        {
            _patients = patients;
        }
        
        public Task<object> LoadContentAsync(Uri uri, System.Threading.CancellationToken cancellationToken)
            
        {
            var patientIdentifier = int.Parse(uri.ToString());

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            PatientEditViewModel pew = new PatientEditViewModel(_patients[patientIdentifier]);
            return Task.Factory.StartNew(() => LoadContent(pew), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        public virtual object LoadContent(PatientEditViewModel patientEditVm)
        {
            if (FirstFloor.ModernUI.ModernUIHelper.IsInDesignMode)
            {
                return null;
            }

            var content = Application.LoadComponent(new Uri("/Patient/PatientEditView.xaml", UriKind.Relative));
            if (content is DependencyObject)
            {
                Caliburn.Micro.ViewModelBinder.Bind(patientEditVm, content as DependencyObject, null);
            }
            return content;
        }
    }
}
