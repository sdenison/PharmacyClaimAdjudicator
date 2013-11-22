using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;

namespace PharmacyAdjudicator.WPF.ViewModels
{
    //[Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen> //IShell
    {
        public ShellViewModel()
        {

        }

        public void ShowAtomView()
        {
            ActivateItem(new AtomViewModel());
        }

    }
}
