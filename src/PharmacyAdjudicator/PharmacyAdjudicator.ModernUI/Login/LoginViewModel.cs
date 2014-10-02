using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Windows.Controls;
using Caliburn.Micro;
using System.Windows;

namespace PharmacyAdjudicator.ModernUI.Login
{
    [Export]
    public class LoginViewModel : Screen 
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Interface.IDialog _dialogManager;
        private readonly IWindowManager _windowManager;

        [ImportingConstructor]
        public LoginViewModel(IEventAggregator eventAggregator, Interface.IDialog dialogManager, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _dialogManager = dialogManager;
            _windowManager = windowManager;
            this.DisplayName = "Login Window";
        }

        public LoginViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            this.DisplayName = "Login Window";
        }

        public string Username { get; set; }

        //private string _username;
        //public string Username
        //{
        //    get { return _username; }
        //    set { _username = value; } 
        //}

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        public string LoginMessage { get; private set; }

        public LoginViewModel()
        {
        }
        
        public async void LoginUser()
        {
            this.LoginMessage = "";
            this.IsBusy = true;
            await PharmacyAdjudicator.Library.Security.PAPrincipal.LoginAsync(Username, Password);
            if (Csla.ApplicationContext.User.Identity.IsAuthenticated)
            {
                //Do something to initialize database connections.
                var client = await Library.Core.Client.ClientInfoList.GetAllClientsAsync();
                this.IsBusy = false;
                _eventAggregator.PublishOnCurrentThread(new EventMessages.LoginChangedMessage("Hello, " + Csla.ApplicationContext.User.Identity.Name));
                TryClose();
            }
            else
            {
                this.LoginMessage = "Could not log in.  Bad username/password combination.";
                this.NotifyOfPropertyChange("LoginMessage");
                _eventAggregator.PublishOnCurrentThread(new EventMessages.LoginChangedMessage("Login"));
                this.IsBusy = false;
            }
        }

        public void Cancel()
        {
            TryClose();
        }

        public void LogoutUser()
        {
            PharmacyAdjudicator.Library.Security.PAPrincipal.Logout();
            _eventAggregator.PublishOnCurrentThread(new EventMessages.LoginChangedMessage("Login"));
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; NotifyOfPropertyChange(() => IsBusy); }
        }
    }
}
