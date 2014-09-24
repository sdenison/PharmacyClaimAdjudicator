using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Xaml;
using Caliburn.Micro;

using PharmacyAdjudicator.Library;
using FirstFloor.ModernUI.Windows;
using System.Windows;
using FirstFloor.ModernUI.Presentation;
using CslaContrib.Caliburn.Micro;
using PharmacyAdjudicator.ModernUI.Interface;
using System.ComponentModel;
using System.Windows.Data;
using PharmacyAdjudicator.Library.Core.Patient;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    /// <summary>
    /// This ViewModel takes care of saving, refreshing and undoing patient data.
    /// </summary>
    public class PatientEditViewModel : ScreenWithModel<Library.Core.Patient.PatientEdit>, IHaveEditStates
    {
        /// <summary>
        /// Allows us to publish events.
        /// </summary>
        private IEventAggregator _eventAggregator;
        private IDialog _dialog;

        /// <summary>
        /// Sets the EditState.  Caliburn uses this to switch between multiple Views for this ViewModel
        /// </summary>
        private PatientEditState _state;
        public PatientEditState State 
        {
            get { return _state; }
            set 
            {
                if (_state != value)
                {
                    _state = value; 
                    NotifyOfPropertyChange(() => this.State); 
                }
            }
        }

        /// <summary>
        /// Used to set edit state of data fields
        /// </summary>
        public bool IsReadOnly
        {
            get { return !CanEditObject; }
            private set { }
        }

        /// <summary>
        /// This is where we get the Tabs
        /// </summary>
        public IEnumerable<PatientEditState> EditStates
        {
            get
            {
                yield return PatientEditState.Details;
                yield return PatientEditState.Addresses;
            }
        }

        /// <summary>
        /// Gives all values of the Gender Enum for use in combobox
        /// </summary>
        public IEnumerable<Library.Core.Enums.Gender> GenderValues
        {
            get { return Enum.GetValues(typeof(Library.Core.Enums.Gender)).Cast<Library.Core.Enums.Gender>(); }
        }

        /// <summary>
        /// Unique ID of the view model
        /// </summary>
        public object Id
        {
            get { return Model.PatientId; }
        }

        /// <summary>
        /// Public constructor
        /// </summary> 
        /// <param name="existingPatient"></param>
        /// <param name="eventAggregator"></param> 
        private PatientEditViewModel(Library.Core.Patient.PatientEdit existingPatient, IEventAggregator eventAggregator, IDialog dialog)
        {
            _eventAggregator = eventAggregator;
            _dialog = dialog;
            this.Model = existingPatient;
            this.State = PatientEditState.Details;
            this.DisplayName = "Pateint Display: " + Model.FullName;
            ConventionManager.Singularize = original =>
            {
                if (original.EndsWith("Addresses"))
                    return original.Replace("Addresses", "Address");
                return original.TrimEnd('s');
            };
        }

        /// <summary>
        /// Builds the ViewModel asynchronously
        /// </summary>
        /// <param name="patientId">Patient ID for the patient</param>
        /// <param name="eventAggregator">Event aggregator that lets us notify the rest of the system when we're closing</param>
        /// <returns></returns>
        async public static Task<PatientEditViewModel> BuildViewModelAsync(long patientId, IEventAggregator eventAggregator, IDialog dialog)
        {
            var patientModel = await Library.Core.Patient.PatientEdit.GetByPatientIdAsync(patientId);
            return new PatientEditViewModel(patientModel, eventAggregator, dialog);
        }  

        /// <summary>
        /// Pulls the patient data from the database and re-displays it
        /// </summary>
        /// <returns></returns>
        public async Task RefreshAsync()
        {
            this.IsBusy = true;
            var currentPatientId = Model.PatientId;
            Model = null;
            Model = await Library.Core.Patient.PatientEdit.GetByPatientIdAsync(currentPatientId);
            this.IsBusy = false;
            base.Refresh();
        }

        /// <summary>
        /// Undoes the current changes and returns the patient data to what it was when we originally pulled it.
        /// </summary>
        public void Undo()
        {
            Model.CancelEdit();
            Model.BeginEdit();
            //NotifyOfPropertyChange(() => this.PatientAddresses);
        }

        /// <summary>
        /// Used to let the Conductor know we are closing.
        /// </summary>
        /// <param name="close"></param>
        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.PublishOnCurrentThread(new PatientEditViewModelClosingMessage() { PatientEditCslaViewModel  = this });
            base.OnDeactivate(close);
        }

        /// <summary>
        /// Set's the focus of the window.  Used by the Conductor.
        /// </summary>
        public void Focus()
        {
            var window = GetView() as Window;
            if (window != null) window.Activate();
        }

        public void RemoveAddress()
        {
            Model.PatientAddresses.Remove(SelectedPatientAddress);
        }

        /// <summary>
        /// Bound to the ListBox.SelectedItem property
        /// </summary>
        public PatientAddress SelectedPatientAddress { get; set; }

        /// <summary>
        /// Before adding the address to the list we check what other address types already exist in the list.
        /// </summary>
        public void AddAddress()
        {
            var addressTypes = Enum.GetValues(typeof(Library.Core.Enums.AddressType)).Cast<Library.Core.Enums.AddressType>();
            var addressToAdd = PatientAddress.NewAddress(Model.PatientId);
            var allAddressTypesAreInList = true;
            foreach (var addressType in addressTypes)
            {
                if (!Model.PatientAddresses.ContainsAddressType(addressType))
                {
                    addressToAdd.AddressType = addressType;
                    allAddressTypesAreInList = false;
                    break;
                }
            }
            if (allAddressTypesAreInList)
            {
                _dialog.ShowMessage("All address types have already been added.", "Could not add address", MessageBoxButton.OK);
            }
            else
            {
                Model.PatientAddresses.Add(addressToAdd);
                this.SelectedPatientAddress = addressToAdd;
            }
        }
    }
}
