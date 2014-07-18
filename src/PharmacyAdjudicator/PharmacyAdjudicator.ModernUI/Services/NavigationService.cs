using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using PharmacyAdjudicator.ModernUI.Interface;
using Newtonsoft.Json;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace PharmacyAdjudicator.ModernUI.Services
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// The view model routing.
        /// </summary>
        private static readonly Dictionary<Type, string> viewModelRouting
            = new Dictionary<Type, string>
            {
                { typeof(Patient.PatientEditViewModel), "/Patient/PatientEditView.xaml" },
                { typeof(Welcome.WelcomeViewModel), "/Welcome/WelcomeView.xaml" }
                //{ typeof(Patient.PatientFindAndEditViewModel), "/Patient/PatientFindAndEditView.xaml" }
            };

        //private static readonly Dictionary<Type, Type> modelToViewModelRouting = new Dictionary<Type, Type>
        //{
        //    { typeof(Library.Core.Patient), typeof(Patient.PatientEditViewModel) },
        //};

        private ObservableCollection<ModernWindow> _openPatients = new ObservableCollection<ModernWindow>();
        public ObservableCollection<ModernWindow> OpenPatients()
        {
            return _openPatients;
        }
        public ModernWindow SelectedItem { get; set; }

        public void ShowWindow(ModernWindow window)
        {
            window.Show();
        }

        ModernFrame mainFrame;

        public NavigationService()
        {
            EnsureMainFrame();
        }

        private void EnsureMainFrame()
        {
            if (mainFrame == null)
            {
                var f = Application.Current.MainWindow;
                mainFrame = GetDescendantFromName(f, "ContentFrame") as ModernFrame;
            }
        }

        /// <summary>
        /// Gets the name of the descendant from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>Gets a descendant FrameworkElement based on its name.A descendant FrameworkElement with the specified name or null if not found.</returns>
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
                return null;

            FrameworkElement fe;

            for (int i = 0; i < count; i++)
            {
                fe = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (fe != null)
                {
                    if (fe.Name == name)
                        return fe;

                    fe = GetDescendantFromName(fe, name);
                    if (fe != null)
                        return fe;
                }
            }

            return null;
        }

        /// <summary>
        /// Navigates the specified parameter.
        /// </summary>
        /// <typeparam name="T">ViewModel type</typeparam>
        /// <param name="parameter">The parameter.</param>
        public void Navigate<T>(object parameter = null)
        {
            EnsureMainFrame();

            var navParameter = string.Empty;
            if (parameter != null)
            {
                navParameter = "?param=" + JsonConvert.SerializeObject(parameter);
            }

            if (viewModelRouting.ContainsKey(typeof(T)))
            {
                Uri newUrl = new Uri(viewModelRouting[typeof(T)] + navParameter, UriKind.Relative);
                Uri oldUrl = mainFrame.Source;

                //Not sure what to use here.  The mainFrame.Navigate method doesn't update the menu links.
                //mainFrame.Navigate(oldUrl, newUrl, NavigationType.New);
                mainFrame.Source = newUrl;
            }
        }

        public void OpenIndependentWindow<T>(IScreen vm)
        {
            var uri = new Uri(viewModelRouting[typeof(T)], UriKind.Relative);
            var content = Application.LoadComponent(uri);
            if (content is DependencyObject)
            {
                Caliburn.Micro.ViewModelBinder.Bind(vm, content as DependencyObject, null);
            }
            var wnd = new ModernWindow
            {
                Style = (System.Windows.Style)App.Current.Resources["EmptyWindow"],
                Content = content,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            _openPatients.Add(wnd);
            wnd.Show();
        }


        /// <summary>
        /// Invokes the go back.
        /// </summary>
        public void GoBack()
        {
            NavigationCommands.BrowseBack.Execute(null, null);
        }
    }
}
