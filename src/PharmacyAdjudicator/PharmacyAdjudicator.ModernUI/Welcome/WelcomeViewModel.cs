using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Welcome
{

    [Export]
    public class WelcomeViewModel : Screen
    {
        private string _WelcomeMessage = "Welcome to the pharmacy claim adjudicator.  You can use this interface to keep patient records up to date and setup plans";
        
        public string WelcomeMessage
        {
            get { return _WelcomeMessage; }
            private set { } 
        }
    }
}
