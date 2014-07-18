using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;
using FirstFloor.ModernUI.Windows.Controls;
using Csla.Xaml;
using Csla;
using System.Threading;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientEditViewModel : ScreenWithModel<Library.Core.Patient>
    {
        [ImportingConstructor]
        public PatientEditViewModel()
        {
        }

        private PatientEditViewModel(Library.Core.Patient existingPatient)
        {
            this.Model = existingPatient;
            RefreshStatus();
        }

        //static async method that behave like a constructor       
        async public static Task<PatientEditViewModel> BuildViewModelAsync(int patientId)
        {
            var patientModel = await Library.Core.Patient.GetByPatientIdAsync(patientId);
            return new PatientEditViewModel(patientModel);
        }  

        //Supplies values for gender ComboBoxes 
        public IEnumerable<Library.Core.Enums.Gender> GenderValues
        {
            get
            {
                return Enum.GetValues(typeof(Library.Core.Enums.Gender)).Cast<Library.Core.Enums.Gender>();
            }
        }

        public bool IsReadOnly
        {
            get { return !this.CanEditObject; }
            private set { }
        }

        new public async Task Refresh()
        {
            this.IsBusy = true;
            ChangeStatus("Refreshing...");
            this.LastChangedUser = "";
            var currentPatientId = this.Model.PatientId;
            this.Model = null;
            this.Model = await Library.Core.Patient.GetByPatientIdAsync(currentPatientId);
            RefreshStatus();
            this.IsBusy = false;
        }

        new public async Task SaveAsync()
        {
            this.IsBusy = true;
            ChangeStatus("Saving...");
            await base.SaveAsync();
            RefreshStatus();
            this.IsBusy = false;
        }

        private void RefreshStatus()
        {
            this.Status = "Last updated " + this.Model.LastChangedDateTime.ToString("MM/dd/yyyy hh:mm:ss");
            this.LastChangedUser = "Changed by " + this.Model.LastChangedUserName;
        }

        private void ChangeStatus(string status)
        {
            this.Status = status;
            this.LastChangedUser = "";
        }

        public void Undo()
        {
            this.Model.CancelEdit();
            this.Model.BeginEdit();
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set 
            { 
                _status = value; 
                NotifyOfPropertyChange(() => this.Status); 
            }
        }

        private string _lastChangedUser;
        public string LastChangedUser
        {
            get { return _lastChangedUser; }
            set
            {
                _lastChangedUser = value;
                NotifyOfPropertyChange(() => this.LastChangedUser);
            }
        }

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                _lastUpdated = Model.LastChangedDateTime;

            }
        }


    }
}    
