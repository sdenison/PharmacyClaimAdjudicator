using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Navigation;

namespace PharmacyAdjudicator.ModernUI.Welcome
{

    [Export]
    public class WelcomeViewModel : Screen
    {
        private string _welcomeMessage = "Welcome to the pharmacy claim adjudicator.  You can use this interface to keep patient records up to date and setup plans";
        public string WelcomeMessage 
        { 
            get { return _welcomeMessage; } 
            private set { throw new NotImplementedException(); } 
        }

        public WelcomeViewModel()
        {
        }
    }
}
