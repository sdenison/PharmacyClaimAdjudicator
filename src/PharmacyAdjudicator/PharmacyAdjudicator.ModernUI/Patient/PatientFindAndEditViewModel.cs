using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using CslaContrib.Caliburn.Micro;
using Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientFindAndEditViewModel //: Conductor<  // ConScreenWithModel<Library.Core.PatientList>//, IContentLoader 
    {
        //public LinkCollection Links { get; set; }
        //public Uri SelectedSource { get; set; }

        //public PatientFindAndEditViewModel(Library.Core.PatientList patients)
        //{
        //    this.Model = patients;
        //    if (this.Links == null)
        //        this.Links = new LinkCollection();

        //    this.Links = new LinkCollection(from p in Model
        //                                    orderby p.PatientId
        //                                    select new Link
        //                                    {
        //                                        DisplayName = p.FirstName + " " + p.LastName,
        //                                        Source = new Uri("/Patient/PatientEditView?PatientId=" + p.PatientId, UriKind.Relative)
        //                                    });
        //    //SelectedSource = this.Links.FirstOrDefault();
        //}


        //public Task<object> LoadContentAsync(Uri uri, System.Threading.CancellationToken cancellationToken)
        //{
        //    //var patientId = 
        //    var result = this.Model.Select(p => p.PatientId.ToString() == uri.ToString()).FirstOrDefault();
        //    return Task<>(0);
        //}
    }
}
