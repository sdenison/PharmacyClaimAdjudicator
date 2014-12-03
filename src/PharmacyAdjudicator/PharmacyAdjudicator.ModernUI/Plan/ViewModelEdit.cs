using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla.Xaml;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class ViewModelEdit<T> : ViewModel<T> where T : Csla.Core.ITrackStatus
    {

        public void Cancel()
        {
            base.DoCancel();
        }

        public void Save()
        {
            if (Model != null)
            {
                if (Model.IsSavable)
                {
                    //Bxf.Shell.Instance.ShowStatus(new Bxf.Status { IsBusy = true, Text = "Saving..." });
                    base.BeginSave();
                }
                else
                {
                    //Bxf.Shell.Instance.ShowError("Object can not be saved", "Save error");
                }
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
        }
    }
}
