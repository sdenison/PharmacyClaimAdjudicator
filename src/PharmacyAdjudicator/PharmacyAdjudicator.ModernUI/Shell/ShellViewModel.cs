using System;
using System.ComponentModel.Composition;
using System.Linq;
using FirstFloor.ModernUI.Presentation;
using Caliburn.Micro;
using Csla;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Input;

namespace PharmacyAdjudicator.ModernUI.Shell
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, 
        IShellViewModel, 
        IHandle<EventMessages.LoginChangedMessage>,
        IHandle<EventMessages.DisplayViewModelMessage>,
        IHandle<EventMessages.NavigateGoBackMessage>,
        IHandle<EventMessages.PatientSearchResultsMessage>
    {
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator events) : this()
        {
            events.Subscribe(this);
        }

        public void Handle(EventMessages.LoginChangedMessage message)
        {
            UpdateMenu();
            _titleLinks[1].DisplayName = message.Message;
        }

        public string ContentSource { get; set; }

        public IConductActiveItem ConductorArea { get; set; }
        public IWindowManager wm { get; set; }

        public void Handle(EventMessages.NavigateGoBackMessage message)
        {
            var navService = AppBootstrapper.GetInstance<Interface.INavigationService>();
            navService.GoBack();
        }

        public void Handle(EventMessages.DisplayViewModelMessage message)
        {
            var navService = AppBootstrapper.GetInstance<Interface.INavigationService>();
            navService.Navigate<Welcome.WelcomeViewModel>();
        }

        public void Handle(EventMessages.PatientSearchResultsMessage message)
        {
            var navService = AppBootstrapper.GetInstance<Interface.INavigationService>();
            navService.Navigate<Patient.PatientFindAndEditViewModel>(message.PatientSearchResults);
        }

        public LinkGroupCollection MenuLinkGroups { get; set; }
        private LinkCollection _titleLinks;
        public LinkCollection TitleLinks
        {
            get {
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

        private LinkGroup PatientLinkGroup()
        {
            var patientLinkGroup = new LinkGroup { DisplayName = "Patient" };
            patientLinkGroup.Links.Add(new Link { DisplayName = "Search", Source = new Uri("/Patient/PatientSearchView.xaml", UriKind.Relative) });
            patientLinkGroup.Links.Add(new Link { DisplayName = "Edit", Source = new Uri("/Patient/PatientEditView.xaml", UriKind.Relative) });
            return patientLinkGroup;
        }

        private void UpdateMenu()
        {
            if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Welcome")))
            {
                var welcomeLinkGroup = new LinkGroup { DisplayName = "Welcome" };
                var welcomeLink = new Link { DisplayName = "Welcome", Source = new Uri("/Welcome/WelcomeView.xaml", UriKind.Relative) };
                welcomeLinkGroup.Links.Add(welcomeLink);
                this.MenuLinkGroups.Add(welcomeLinkGroup);
                if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(Library.Core.Patient)))
                    this.MenuLinkGroups.Add(PatientLinkGroup());
            }
            else
            {
                if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(Library.Core.Patient)))
                    if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                        this.MenuLinkGroups.Add(PatientLinkGroup());
                else
                    if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                        this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Patient")));
            }

            if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Settings")))
            {
                var settingsLinkGroup = new LinkGroup { DisplayName = "Settings", GroupName = "settings" };
                var settingsLink = new Link { DisplayName = "Software", Source = new Uri("/Pages/Settings.xaml", UriKind.Relative) };
                settingsLinkGroup.Links.Add(settingsLink);

                this.MenuLinkGroups.Add(settingsLinkGroup);
            }

            if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Login")))
            {
                var loginLinkGroup = new LinkGroup { DisplayName = "Login", GroupName = "login" };
                var loginLink = new Link { DisplayName = "Login", Source = new Uri("/Login/LoginView.xaml", UriKind.Relative) };
                loginLinkGroup.Links.Add(loginLink);

                this.MenuLinkGroups.Add(loginLinkGroup);
            }


            if (!TitleLinks.Any(t => t.DisplayName.Equals("Settings")))
            {
                var settingsTitleLink = new Link { DisplayName = "Settings", Source = new Uri("/Pages/Settings.xaml", UriKind.Relative) };
                this._titleLinks.Add(settingsTitleLink);
            }

            if ((!TitleLinks.Any(t => t.DisplayName.Equals("Login")) && (!TitleLinks.Any(t => t.DisplayName.StartsWith("Hello")))))
            {
                var loginTitleLink = new Link { DisplayName = "Login", Source = new Uri("/Login/LoginView.xaml", UriKind.Relative) };
                this._titleLinks.Add(loginTitleLink);
            }
        }

    }
}
