using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using PharmacyAdjudicator.Library.Core.Plan;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    //public class PlanListViewModel : Conductor<PlanEditViewModel>.Collection.OneActive, IContentLoader// ScreenWithModel<PlanList> /// Conductor<PlanEditViewModel>.Collection.OneActive
    //{
    //    //private PlanList _planList;
    //    //private IDialog _dialogManager;

    //    //public PlanListViewModel(IDialog dialogManager)
    //    //{
    //    //    _planList = PlanList.GetAll();
    //    //    _dialogManager = dialogManager;
    //    //    _planList.CollectionChanged += OnPlanListChanged;
    //    //}

    //    //public delegate void OnPlanListChanged(object sender, NotifyCollectionChangedEventArgs e)
    //    //{
    //    //    NotifyOfPropertyChange(() => PlanLinks);
    //    //}

    //    //public LinkCollection PlanLinks
    //    //{
    //    //    get 
    //    //    { 
    //    //        var lc = new LinkCollection();
    //    //        //foreach (var plan in FilteredPlans)
    //    //        //    lc.Add(new Link() { DisplayName = plan.PlanId, Source = new Uri(plan.PlanId, UriKind.Relative) });
    //    //        return lc;
    //    //    }
    //    //}

    //    //private void 

    //    //        //        var lc = new LinkCollection();
    //    //        //foreach (var plan in FilteredPlans)
    //    //        //    lc.Add(new Link() { DisplayName = plan.PlanId, Source = new Uri(plan.PlanId, UriKind.Relative) });
    //    //        //return lc;
    //    //public IContentLoader ContentLoader
    //    //{
    //    //    get { return this; }
    //    //    private set { }
    //    //}

    //    //public PlanEditViewModel SelectedPlan { get; set; }

    //    ////public BindableCollection<PlanEditViewModel> Plans { get; set; }

    //    //Task<object> IContentLoader.LoadContentAsync(Uri uri, System.Threading.CancellationToken cancellationToken)
    //    //{
    //    //    //throw new NotImplementedException();
    //    //    return this.ActiveItem;
    //    //}


    //}
}
