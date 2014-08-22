using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace PharmacyAdjudicator.ModernUI.Welcome
{

    [Export]
    public class WelcomeViewModel : Screen
    {
        //[Import]
        //public IWindowManager WindowManager { get; set; }
        //[Import]
        //public IEventAggregator EventAggregator { get; set; }

        private IEventAggregator _eventAggregator;

        public ICommand LoginCommand { get; set; }

        [ImportingConstructor]
        public WelcomeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            LoginCommand = new RelayCommand(o => _eventAggregator.PublishOnUIThread(new EventMessages.LoginMessage()));
        }

        private string _WelcomeMessage = "Welcome to the pharmacy claim adjudicator.  You can use this interface to keep patient records up to date and setup plans";
        
        public string WelcomeMessage
        {
            get { return _WelcomeMessage; }
            private set { } 
        }

        //public void Login()
        //{
        //    EventAggregator.PublishOnUIThread(new EventMessages.LoginMessage());
        //}

    }
}
