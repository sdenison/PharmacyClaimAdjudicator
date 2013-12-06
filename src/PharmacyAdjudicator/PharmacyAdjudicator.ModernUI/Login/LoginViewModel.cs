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

        [ImportingConstructor]
        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
                _eventAggregator.Publish(new EventMessages.LoginChangedMessage("Hello, " + Csla.ApplicationContext.User.Identity.Name));
                MessageBoxButton btn = MessageBoxButton.OK;
                var result = FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("Welcome, " + Csla.ApplicationContext.User.Identity.Name + "!", "Login succeded.", btn);
            }
            else
            {
                _eventAggregator.Publish(new EventMessages.LoginChangedMessage("Login"));
                MessageBoxButton btn = MessageBoxButton.OK; 
                var result = FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage("Could not login in.  Bad username/password combination.", "Login failed", btn);
            }
        }
    }
}
