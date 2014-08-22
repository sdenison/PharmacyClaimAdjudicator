using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI
{
    [Export]
    public class LoginCommand : CommandBase
    {
        [Import(typeof(IWindowManager))]
        public IWindowManager WindowManager { get; set; }

        //[ImportingConstructor]
        //public LoginCommand(IWindowManager windowManager)
        //{
        //    var x = "stop here";
        //    this.WindowManager = windowManager;
        //}

        protected override void OnExecute(object parameter)
        {

            var loginVM = new Login.LoginViewModel();
            WindowManager.ShowDialog(loginVM);
        //               public override void ActivateItem(PatientEditCslaViewModel item)
        //{
        //    _windowManager.ShowWindow(item);
        //    base.ActivateItem(item);
        //}
        }
    }
}
