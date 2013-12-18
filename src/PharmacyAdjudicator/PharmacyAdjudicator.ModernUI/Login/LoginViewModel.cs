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

        [ImportingConstructor]
        public LoginViewModel(IEventAggregator eventAggregator, Interface.IDialog dialogManager)
        {
            _eventAggregator = eventAggregator;
            _dialogManager = dialogManager;
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; } 
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }
        public LoginViewModel()
        {
        }
        
        public async void LoginUser()
        {
            await PharmacyAdjudicator.Library.Security.PAPrincipal.LoginAsync(Username, Password);
            if (Csla.ApplicationContext.User.Identity.IsAuthenticated)
            {
                MessageBoxButton btn = MessageBoxButton.OK;
                var result = _dialogManager.ShowMessage("Welcome, " + Csla.ApplicationContext.User.Identity.Name + "!", "Login succeded.", btn);
                //Sets the TitleLink in the main window.
                _eventAggregator.Publish(new EventMessages.LoginChangedMessage("Hello, " + Csla.ApplicationContext.User.Identity.Name));
                //Navigates to appropriate view model after Login is successful.
                _eventAggregator.Publish(new EventMessages.DisplayViewModelMessage() { Requestor = this, ViewModel = AppBootstrapper.GetInstance<Welcome.WelcomeViewModel>() });
            }
            else
            {
                MessageBoxButton btn = MessageBoxButton.OK; 
                var result = _dialogManager.ShowMessage("Could not login in.  Bad username/password combination.", "Login failed", btn);
                _eventAggregator.Publish(new EventMessages.LoginChangedMessage("Login"));
            }
        }

        public void LogoutUser()
        {
            PharmacyAdjudicator.Library.Security.PAPrincipal.Logout();
            _eventAggregator.Publish(new EventMessages.LoginChangedMessage("Login"));
        }
    }
}
