using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows;

namespace PharmacyAdjudicator.ModernUI.Patient
{
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
            PatientEditViewModel pew;
            if (_patients.Count > 0)
                pew = new PatientEditViewModel(_patients[patientIdentifier]);
            else
                pew = new PatientEditViewModel();
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
