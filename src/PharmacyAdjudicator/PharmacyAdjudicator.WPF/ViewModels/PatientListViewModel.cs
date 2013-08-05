using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Csla.Xaml;
using CslaContrib.Caliburn.Micro;
using System.ComponentModel;

namespace PharmacyAdjudicator.WPF.ViewModels
{
    public class PatientListViewModel : ViewModel<Library.Core.PatientList>
    {
        public PatientListViewModel()
        {
            ManageObjectLifetime = true;
            if (Caliburn.Micro.Execute.InDesignMode == true)
            {
                DoRefresh("GetAll");
                //DoRefresh("GetAll");
                //Model = Library.Core.PatientList.GetAll();
            }
			else
			{
                DoRefresh("GetAll");
				//DoRefresh("GetAll");
			}
        }

        public new void DoSave()
        {
            base.DoSave();
        }

        public new void DoCancel()
        {
            base.DoCancel();
        }
    }
}
