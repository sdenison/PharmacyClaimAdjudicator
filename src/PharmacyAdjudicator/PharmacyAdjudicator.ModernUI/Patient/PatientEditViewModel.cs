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

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientEditViewModel : ScreenWithModel<Library.Core.Patient>
    {
      
        [ImportingConstructor]
        public PatientEditViewModel()
        {
            //For development
            //this.Model = Library.Core.Patient.GetByPatientId(22);
        }

        public PatientEditViewModel(Library.Core.Patient existingPatient)
        {
            this.Model = existingPatient;
        }

        //Supplies values for gender ComboBoxes 
        public IEnumerable<Library.Core.Enums.Gender> GenderValues
        {
            get
            {
                return Enum.GetValues(typeof(Library.Core.Enums.Gender)).Cast<Library.Core.Enums.Gender>();
            }
        }

        /// <summary>
        /// Creates a new window with the view and totally violates MVVM.
        /// </summary>
        public void NewWindow()
        {
            var pat = Library.Core.Patient.GetByPatientId(this.Model.PatientId);
            var patVM = new PatientEditViewModel(pat);

            var content = Application.LoadComponent(new Uri("/Patient/PatientEditView.xaml", UriKind.Relative));
            if (content is DependencyObject)
            {
                Caliburn.Micro.ViewModelBinder.Bind(patVM, content as DependencyObject, null);
            }

            var wnd = new ModernWindow
            {
                Style = (System.Windows.Style)App.Current.Resources["EmptyWindow"],
                Content = content,
                Width = 480,
                Height = 480
            };
            wnd.Show();
        }
    }
}    
