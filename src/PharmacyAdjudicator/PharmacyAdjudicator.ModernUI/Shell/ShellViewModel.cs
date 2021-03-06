﻿using System;
using System.ComponentModel.Composition;
using System.Linq;
using FirstFloor.ModernUI.Presentation;
using Caliburn.Micro;
using Csla;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Navigation;
using PharmacyAdjudicator.Library.Core.Patient;
using PharmacyAdjudicator.Library.Core.Group;
using PharmacyAdjudicator.Library.Core.Plan;

namespace PharmacyAdjudicator.ModernUI.Shell
{
    public class ShellViewModel : //Conductor<IScreen>.Collection.OneActive, 
        IShellViewModel, 
        IHandle<EventMessages.LoginChangedMessage>,
        //IHandle<EventMessages.LoginMessage>,
        IHandle<EventMessages.DisplayViewModelMessage>,
        IHandle<EventMessages.NavigateGoBackMessage>//,
        //IHandle<EventMessages.PatientSearchResultsMessage>
    {
        private IWindowManager _windowManager;
        private IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator events, IWindowManager windowManager) : this()
        {
            events.Subscribe(this);
            IsBusy = true;
            _windowManager = windowManager;
            _eventAggregator = events;
            this.LinkNavigator = new DefaultLinkNavigator();
            this.ShowLoginCommand = new RelayCommand(o => ShowLogin(o));
            this.LinkNavigator.Commands.Add(new Uri("cmd://login", UriKind.Absolute), ShowLoginCommand);
        }

        private void ShowLogin(object o)
        {
            _windowManager.ShowDialog(new Login.LoginViewModel(_eventAggregator, _windowManager));
        }

        public RelayCommand ShowLoginCommand { get; private set; }

        public void Handle(EventMessages.LoginChangedMessage message)
        {
            UpdateMenu();
            _titleLinks[1].DisplayName = message.Message;
        }

        public ILinkNavigator LinkNavigator { get; private set; }

        //public void Handle(EventMessages.LoginMessage message)
        //{
        //    var windowManager = AppBootstrapper.GetInstance<IWindowManager>();
        //    //windowManager.ShowDialog();
        //}

        public string ContentSource { get; set; }

        //public IConductActiveItem ConductorArea { get; set; }
        //public IWindowManager wm { get; set; }

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

        public void Handle(EventMessages.PatientEditMessage message)
        {
            
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

        private void UpdatePatientMenu()
        {
            if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(PatientEdit)))
            {
                if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                    this.MenuLinkGroups.Add(PatientLinkGroup());
            }
            else
            {
                if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                    this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Patient")));
            }
        }

        private LinkGroup PatientLinkGroup()
        {
            var patientLinkGroup = new LinkGroup { DisplayName = "Patient" };
            patientLinkGroup.Links.Add(new Link { DisplayName = "Patient Manager", Source = new Uri("/Patient/PatientView.xaml", UriKind.Relative) });
            return patientLinkGroup;
        }

        private void UpdateGroupMenu()
        {
            if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(GroupEdit)))
            {
                if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Group")))
                    this.MenuLinkGroups.Add(GroupLinkGroup());
            }
            else
            {
                if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Group")))
                    this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Group")));
            }
        }

        private LinkGroup GroupLinkGroup()
        {
            var patientLinkGroup = new LinkGroup { DisplayName = "Group" };
            patientLinkGroup.Links.Add(new Link { DisplayName = "Group Manager", Source = new Uri("/Group/GroupWorkspaceView.xaml", UriKind.Relative) });
            return patientLinkGroup;
        }

        private void UpdatePlanMenu()
        {
            if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(PlanEdit)))
            {
                if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Plan")))
                    this.MenuLinkGroups.Add(PlanLinkGroup());
            }
            else
            {
                if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Plan")))
                    this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Plan")));
            }
        }

        private LinkGroup PlanLinkGroup()
        {
            var patientLinkGroup = new LinkGroup { DisplayName = "Plan" };
            //patientLinkGroup.Links.Add(new Link { DisplayName = "Plan Manager", Source = new Uri("/Plan/PlanWorkspaceView.xaml", UriKind.Relative) });
            patientLinkGroup.Links.Add(new Link { DisplayName = "Plan Manager", Source = new Uri("/Plan/PlanListView.xaml", UriKind.Relative) });
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
                if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(PatientEdit)))
                    this.MenuLinkGroups.Add(PatientLinkGroup());
            }
            else
            {
                UpdatePatientMenu();
                UpdateGroupMenu();
                UpdatePlanMenu();
                //if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(PatientEdit)))
                //{
                //    if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                //        this.MenuLinkGroups.Add(PatientLinkGroup());
                //}
                //else
                //{
                //    if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Patient")))
                //        this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Patient")));
                //}
                //if (Csla.Rules.BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(GroupEdit)))
                //{
                //    if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Group")))
                //        this.MenuLinkGroups.Add(GroupLinkGroup());
                //}
                //else
                //{
                //    if (MenuLinkGroups.Any(l => l.DisplayName.Equals("Group")))
                //        this.MenuLinkGroups.Remove(MenuLinkGroups.FirstOrDefault(l => l.DisplayName.Equals("Group")));
                //}
            }

            if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Settings")))
            {
                var settingsLinkGroup = new LinkGroup { DisplayName = "Settings", GroupKey = "settings" };
                var settingsLink = new Link { DisplayName = "Software", Source = new Uri("/Pages/Settings.xaml", UriKind.Relative) };
                settingsLinkGroup.Links.Add(settingsLink);

                this.MenuLinkGroups.Add(settingsLinkGroup);
            }

            if (!MenuLinkGroups.Any(l => l.DisplayName.Equals("Login")))
            {
                var loginLinkGroup = new LinkGroup { DisplayName = "Login", GroupKey = "login" };
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
                var loginTitleLink = new Link { DisplayName = "Login", Source = new Uri("cmd://login", UriKind.Absolute) };
                this._titleLinks.Add(loginTitleLink);
            }
        }

        public bool IsBusy { get; set; }
    }
}
