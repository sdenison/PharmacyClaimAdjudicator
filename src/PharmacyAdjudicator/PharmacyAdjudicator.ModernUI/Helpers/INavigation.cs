using PharmacyAdjudicator.ModernUI.ViewModels;

namespace PharmacyAdjudicator.ModernUI.Helpers
{
    public interface INavigation
    {
        void NavigateToViewModel<TViewModel>();
        void NavigateToViewModel<TViewModel>(object parameter) where TViewModel : PageViewModelBase;

        void NavigateToViewModelAndRemoveCurrent<TViewModel>();
        void NavigateToViewModelAndRemoveCurrent<TViewModel>(object parameter) where TViewModel : PageViewModelBase;

        void GoBack();

        bool CanGoBack { get; }
    }
}
