using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;
using Csla.Xaml;

namespace PharmacyAdjudicator.WPF.ViewModels
{
    public class AtomViewModel : ScreenWithModel<Library.Core.Rules.Atom> // ViewModel<Library.Core.Rules.Atom>
    {
        protected override void OnActivate()
        {
            //base.Model = Library.Core.Rules.Atom.GetByAtomId(1);
            base.OnActivate();
        }
    }
}
