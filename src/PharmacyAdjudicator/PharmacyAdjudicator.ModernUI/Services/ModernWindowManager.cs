using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Caliburn.Micro;
using System.Windows;
using System.Windows.Data;
using FirstFloor.ModernUI.Windows.Controls;
using System.ComponentModel;
using System.Windows.Navigation;

using PharmacyAdjudicator.ModernUI.Interface;
using System.ComponentModel.Composition;

namespace PharmacyAdjudicator.ModernUI.Services
{
    [Export(typeof(IHaveWindowsForType))]
    [Export(typeof(IWindowManager))]
    public class ModernWindowManager : WindowManager, IWindowManager, IHaveWindowsForType, INotifyPropertyChanged
    {
        private static Dictionary<Type, Dictionary<string, ModernWindow>> _viewModelDictionary = new Dictionary<Type, Dictionary<string, ModernWindow>>();
        public Dictionary<string, ModernWindow> GetWindowsForType (Type type)
        {
            if (_viewModelDictionary.ContainsKey(type))
                return _viewModelDictionary[type];
            else
                return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        
        public override bool? ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            return CreateWindow(rootModel, true, context, settings).ShowDialog();
        }

        //public void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null)
        //{
        //    throw new NotImplementedException();
        //}

        public override void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            NavigationWindow navWindow = null;

            var application = Application.Current;
            if (application != null && application.MainWindow != null)
            {
                navWindow = application.MainWindow as NavigationWindow;
            }

            if (navWindow != null)
            {
                var window = CreatePage(rootModel, context, settings);
                navWindow.Navigate(window);
            }
            else
            {
                CreateWindow(rootModel, false, context, settings).Show();
            }
        }

        /// <summary>
        /// Creates a window.
        /// </summary>
        /// <param name="rootModel">The view model.</param>
        /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
        /// <param name="context">The view context.</param>
        /// <param name="settings">The optional popup settings.</param>
        /// <returns>The window.</returns>
        new protected ModernWindow CreateWindow(object rootModel, bool isDialog, object context, IDictionary<string, object> settings)
        {
            var view = EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, null, context), isDialog);
            ViewModelBinder.Bind(rootModel, view, context);

            var haveDisplayName = rootModel as IHaveDisplayName;
            if (haveDisplayName != null && !ConventionManager.HasBinding(view, Window.TitleProperty))
            {
                var binding = new Binding("DisplayName") { Mode = BindingMode.TwoWay };
                view.SetBinding(Window.TitleProperty, binding);
            }

            ApplySettings(view, settings);

            new WindowConductor(rootModel, view);

            return view;
        }

        /// <summary>
        /// Makes sure the view is a window or is wrapped by one.
        /// </summary>
        /// <param name="model">The view model.</param>
        /// <param name="view">The view.</param>
        /// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
        /// <returns>The window.</returns>
        new protected ModernWindow EnsureWindow(object model, object view, bool isDialog)
        {
            var window = view as ModernWindow;

            if (window == null)
            {
                window = new ModernWindow
                {
                    Style = (System.Windows.Style)App.Current.Resources["EmptyWindow"],
                    Content = view,
                    SizeToContent = SizeToContent.WidthAndHeight
                };

                window.SetValue(View.IsGeneratedProperty, true);

                var owner = InferOwnerOf(window);
                if (owner != null)
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                }
                else
                {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }
            else
            {
                var owner = InferOwnerOf(window);
                if (owner != null && isDialog)
                {
                    window.Owner = owner;
                }
            }

            return window;
        }

        bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings)
        {
            if (settings != null)
            {
                var type = target.GetType();

                foreach (var pair in settings)
                {
                    var propertyInfo = type.GetProperty(pair.Key);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(target, pair.Value, null);
                    }
                }

                return true;
            }

            return false;
        }

        class WindowConductor
        {
            bool deactivatingFromView;
            bool deactivateFromViewModel;
            bool actuallyClosing;
            readonly Window view;
            readonly object model;

            public WindowConductor(object model, Window view)
            {
                this.model = model;
                this.view = view;

                var activatable = model as IActivate;
                if (activatable != null)
                {
                    activatable.Activate();
                }

                var deactivatable = model as IDeactivate;
                if (deactivatable != null)
                {
                    view.Closed += Closed;
                    deactivatable.Deactivated += Deactivated;
                }

                var guard = model as IGuardClose;
                if (guard != null)
                {
                    view.Closing += Closing;
                }
            }

            void Closed(object sender, EventArgs e)
            {
                view.Closed -= Closed;
                view.Closing -= Closing;

                if (deactivateFromViewModel)
                {
                    return;
                }

                var deactivatable = (IDeactivate)model;

                deactivatingFromView = true;
                deactivatable.Deactivate(true);
                deactivatingFromView = false;
            }

            void Deactivated(object sender, DeactivationEventArgs e)
            {
                if (!e.WasClosed)
                {
                    return;
                }

                ((IDeactivate)model).Deactivated -= Deactivated;

                if (deactivatingFromView)
                {
                    return;
                }

                deactivateFromViewModel = true;
                actuallyClosing = true;
                view.Close();
                actuallyClosing = false;
                deactivateFromViewModel = false;
            }

            void Closing(object sender, CancelEventArgs e)
            {
                if (e.Cancel)
                {
                    return;
                }

                var guard = (IGuardClose)model;

                if (actuallyClosing)
                {
                    actuallyClosing = false;
                    return;
                }

                bool runningAsync = false, shouldEnd = false;

                guard.CanClose(canClose =>
                {
                    Execute.OnUIThread(() =>
                    {
                        if (runningAsync && canClose)
                        {
                            actuallyClosing = true;
                            view.Close();
                        }
                        else
                        {
                            e.Cancel = !canClose;
                        }

                        shouldEnd = true;
                    });
                });

                if (shouldEnd)
                {
                    return;
                }

                runningAsync = e.Cancel = true;
            }
        }

        public void RemoveWindow(Type type, string id)
        {
            if (_viewModelDictionary.ContainsKey(type))
            {
                var windows = _viewModelDictionary[type];
                windows.Remove(id);
            }
        }
    }
}
