using System;
using System.ComponentModel.Composition;
using FirstFloor.ModernUI.Presentation;
using Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Shell
{
    public class ShellViewModel : IShellViewModel, IHandle<EventMessages.LoginChangedMessage> //Conductor<IScreen>.Collection.OneActive //IShellViewModel
    {
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator events) : this()
        {
            events.Subscribe(this);
        }

        public void Handle(EventMessages.LoginChangedMessage message)
        {
            _titleLinks[1].DisplayName = message.Message;
        }

        public LinkGroupCollection MenuLinkGroups { get; set; }
        private bool _wasAuthenticated = false;
        private LinkCollection _titleLinks;
        public LinkCollection TitleLinks
        {
            get
            {
                return _titleLinks;
            }
            private set
            {
                _titleLinks = value;
            }
        }


        public ShellViewModel()
        {
            this.MenuLinkGroups = new LinkGroupCollection();
            this.TitleLinks = new LinkCollection();
            
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            this.MenuLinkGroups.Clear();

            var welcomeLinkGroup = new LinkGroup { DisplayName = "Welcome" };
            var welcomeLink = new Link { DisplayName = "Welcome", Source = new Uri("/Welcome/WelcomeView.xaml", UriKind.Relative) };
            welcomeLinkGroup.Links.Add(welcomeLink);

            this.MenuLinkGroups.Add(welcomeLinkGroup);

            var settingsLinkGroup = new LinkGroup { DisplayName = "Settings", GroupName = "settings" };
            var settingsLink = new Link { DisplayName = "Software", Source = new Uri("/Pages/Settings.xaml", UriKind.Relative) };
            settingsLinkGroup.Links.Add(settingsLink);

            this.MenuLinkGroups.Add(settingsLinkGroup);

            var loginLinkGroup = new LinkGroup { DisplayName = "Login", GroupName = "login" };
            var loginLink = new Link { DisplayName = "Login", Source = new Uri("/Login/LoginView.xaml", UriKind.Relative) };
            loginLinkGroup.Links.Add(loginLink);

            this.MenuLinkGroups.Add(loginLinkGroup);

            //this._titleLinks.Clear();

            var settingsTitleLink = new Link { DisplayName = "Settings", Source = new Uri("/Pages/Settings.xaml", UriKind.Relative) };
            this._titleLinks.Add(settingsTitleLink);

            //User.UserViewModel user = new User.UserViewModel();
            var loginTitleLink = new Link { DisplayName = "Login", Source = new Uri("/Login/LoginView.xaml", UriKind.Relative) };
            //var loginTitleLink = new Link { DisplayName = user.UserName, Source = new Uri("/Login/LoginView.xaml", UriKind.Relative) };
            this._titleLinks.Add(loginTitleLink);
        }

    }
}
