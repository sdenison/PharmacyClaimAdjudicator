using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Caliburn.Micro;
using Csla.Xaml;
using System.Windows.Controls;
using System.Windows;

namespace PharmacyAdjudicator.WPF.ViewModels
{
    public class PatientViewModel : ViewModel<Library.Core.Patient> //PropertyChangedBase
    {
        public PatientViewModel()
        {
            ManageObjectLifetime = true;
            if (Caliburn.Micro.Execute.InDesignMode == true)
            {
                DoRefresh("GetPatient", 22);
                //BeginRefresh(Library.Core.Patient.GetByPatientId);

            }
        }

        public PatientViewModel(Library.Core.Patient patient)
        {
            ManageObjectLifetime = false;
            Model = patient;
        }

        public new Library.Core.Patient DoSave()
        {
            return base.DoSave();
        }

    }
}
