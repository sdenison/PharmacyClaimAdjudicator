using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using CslaContrib.Caliburn.Micro;
using Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientSearchResultsViewModel : ScreenWithModel<Library.Core.PatientList>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Interface.IDialog _dialogManager;

        [ImportingConstructor]
        public PatientSearchResultsViewModel(IEventAggregator eventAggregator, Interface.IDialog dialogManager)
        {
            //this.IsBusy = true;

            
            _eventAggregator = eventAggregator;
            _dialogManager = dialogManager;

            //this.Model = ;
            //var patients = Library.Core.PatientList.GetAll();
            //_results = new ObservableCollection<Library.Core.Patient>(patients);
            //this.PatientListLinks = ConvertPatientList(patients);
            //_patientContentLoader = new PatientContentLoader(patients);

            //var patients = Library.Core.PatientList.NewPatientList();
            //_results = new ObservableCollection<Library.Core.Patient>(patients);
            //this.PatientListLinks = ConvertPatientList(patients);
            //_patientContentLoader = new PatientContentLoader(patients);
            //this.PatientListLinks = 




            //this.IsBusy = false;
            //GetDefaultPatients();
            //_initializingTask = GetDefaultPatients();
            //Task.WaitAll(_initializingTask);
            //_results = new ObservableCollection<Library.Core.Patient>(_defaultPatients);
        }
    }
}
