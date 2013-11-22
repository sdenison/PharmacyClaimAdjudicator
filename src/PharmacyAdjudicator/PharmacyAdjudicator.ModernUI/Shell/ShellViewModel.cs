using System;
using FirstFloor.ModernUI.Presentation;

namespace PharmacyAdjudicator.ModernUI.Shell
{
    public class ShellViewModel : NotifyPropertyChanged, IShellViewModel
    {
        public LinkGroupCollection MenuLinkGroups { get; private set; }
        public ShellViewModel()
        {
            this.MenuLinkGroups = new LinkGroupCollection();
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            this.MenuLinkGroups.Clear();

            var welcomeLinkGroup = new LinkGroup { DisplayName = "Welcome", GroupName = "WelcomeGroup" };
            var welcomeLink = new Link { DisplayName = "Welcome", Source = new Uri("/Welcome/WelcomeView.xaml", UriKind.Relative) };
            welcomeLinkGroup.Links.Add(welcomeLink);

            this.MenuLinkGroups.Add(welcomeLinkGroup);

        }

    }
}
