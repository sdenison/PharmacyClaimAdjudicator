using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PharmacyAdjudicator.Library.Core;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using PharmacyAdjudicator.ModernUI.Interface;
using System.Windows;
using System.ComponentModel.Composition;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientViewModel : Conductor<PatientEditViewModel>.Collection.AllActive, IHandle<PatientEditViewModelClosingMessage>
    {
        private IDialog _dialogManager;
        private readonly IWindowManager _windowManager;
        private IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public PatientViewModel(IDialog dialogManager, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _dialogManager = dialogManager;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        /// <summary>
        /// Allows the conductor to gracefully close a PatientEditViewModel. 
        /// </summary>
        public void Handle(PatientEditViewModelClosingMessage closingMessage)
        {
            this.Items.Remove(closingMessage.PatientEditCslaViewModel);
        }

        #region "Patient Search"
        private static PatientSearchCriteria _patientSearchCriteria = new PatientSearchCriteria();
        public PatientSearchCriteria PatientSearchCriteria
        {
            get { return _patientSearchCriteria; }
            set { _patientSearchCriteria = value; }
        }

        private PatientList _searchResults;
        public PatientList SearchResults
        {
            get { return _searchResults; }
            set 
            { 
                _searchResults = value; 
                NotifyOfPropertyChange(() => SearchResults);
                NotifyOfPropertyChange(() => CanShowSearchResults);
            }
        }

        public Boolean CanShowSearchResults
        {
            get 
            { 
                if (_searchResults == null)
                    return false;
                else 
                    return _searchResults.Count > 0; 
            }
            private set { }
        }

        public async void FindPatients()
        {
            if ((string.IsNullOrWhiteSpace(_patientSearchCriteria.PatientFirstName)) && (string.IsNullOrWhiteSpace(_patientSearchCriteria.PatientLastName))
                && (string.IsNullOrWhiteSpace(_patientSearchCriteria.GroupId)) && (string.IsNullOrWhiteSpace(_patientSearchCriteria.CardholderId)))
            {
                _dialogManager.ShowMessage("Please enter search criteria", "Search Criteria Missing", MessageBoxButton.OK);
            }
            else
            {
                this.IsFindingPatients = true;
                var patientSearchResutls = await PatientList.GetBySearchObjectAsync(this.PatientSearchCriteria);
                this.IsFindingPatients = false;
                if (patientSearchResutls.Count == 0)
                {
                    _dialogManager.ShowMessage("No patients found for search criteria.", "No Records Found", MessageBoxButton.OK);
                }
                this.SearchResults = patientSearchResutls;
            }
        }

        private bool _isFindingPatients = false;
        public bool IsFindingPatients
        {
            get { return _isFindingPatients; }
            private set { _isFindingPatients = value; NotifyOfPropertyChange(() => IsFindingPatients); }
        }

        /// <summary>
        /// Creates a view model from Patient object.
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public async Task ShowPatient(Library.Core.Patient.PatientEdit patient)
        //public void ShowPatient(Library.Core.Patient patient)
        {
            //Tries to find an existing PatientEditViewModel to show.
            //If it fails to find an existing one it will build it and show it.
            var existingPatientEditViewModel = this.Items.FirstOrDefault(p => p.Id.ToString() == patient.PatientId.ToString());
            if (existingPatientEditViewModel == null)
            {
                this.IsOpeningPatientWindow = true;
                var patientViewModel = await PatientEditViewModel.BuildViewModelAsync(patient.PatientId, _eventAggregator, _dialogManager);
                ActivateItem(patientViewModel);
                this.IsOpeningPatientWindow = false;
            }
            else
            {
                existingPatientEditViewModel.Focus();
            }
        }

        /// <summary>
        /// Closes the view model
        /// </summary>
        /// <param name="item"></param>
        public void DeactivateItem(PatientEditViewModel item)
        {
            this.DeactivateItem(item, true);
        }

        /// <summary>
        /// Shows a view model that already exists.
        /// </summary>
        /// <param name="patientVm"></param>
        public void ShowPatientViewModel(PatientEditViewModel patientVm)
        {
            patientVm.Focus();
        }

        public override void ActivateItem(PatientEditViewModel item)
        {
            _windowManager.ShowWindow(item);
            base.ActivateItem(item);
        }

        public bool CanAddPatient
        {
            get
            {
                return (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.CreateObject, typeof(Library.Core.Patient.PatientEdit)));
            }
        }

        public async Task AddPatient()
        {
            this.IsOpeningPatientWindow = true;
            var patient = await Library.Core.Patient.PatientEdit.NewPatientAsync();
            var patientViewModel = new PatientEditViewModel(patient, _eventAggregator, _dialogManager);
            ActivateItem(patientViewModel);
            this.IsOpeningPatientWindow = false;
        }


        private bool _isOpeningPatientWindow = false;
        public bool IsOpeningPatientWindow
        {
            get { return _isOpeningPatientWindow; }
            set { _isOpeningPatientWindow = value; NotifyOfPropertyChange(() => IsOpeningPatientWindow); }
        }

        #endregion
    }
}
