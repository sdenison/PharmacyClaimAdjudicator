using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
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
    [Export]
    public class PlanListViewModel : ScreenWithModel<PlanList> //, IContentLoader //Conductor<PlanEditViewModel>.Collection.OneActive, IContentLoader// ScreenWithModel<PlanList> /// Conductor<PlanEditViewModel>.Collection.OneActive
    {
        private IDialog _dialogManager;

        [ImportingConstructor]
        public PlanListViewModel(IDialog dialogManager)
        {
            //_planList = PlanList.GetAll();
            Model = PlanList.GetAll(); // _planList;
            Plans = new BindableCollection<PlanListItemViewModel>();
            foreach (var plan in Model)
                Plans.Add(new PlanListItemViewModel(plan));
            //Model = _planList;
            _dialogManager = dialogManager;
            _selectedPlan = Plans.FirstOrDefault();
            //_planList.CollectionChanged +=  OnPlanListChanged;
        }

        public BindableCollection<PlanListItemViewModel> Plans
        {
            get;
            private set;
        }

        private PlanListItemViewModel _selectedPlan;
        public PlanListItemViewModel SelectedPlan
        {
            get 
            { 
                return _selectedPlan; 
            }
            set
            {
                _selectedPlan = value;
                NotifyOfPropertyChange(() => SelectedPlan);
                NotifyOfPropertyChange(() => SelectedPlanEdit);
            }
        }

        public PlanEditViewModel SelectedPlanEdit
        {
            get
            {
                return new PlanEditViewModel(_selectedPlan.Model);
            }
            private set { }
        }

        

        //public PlanEditViewModel SelectedPlan
        //{
        //    get;
        //    set;
        //}

        //public BindableCollection<PlanEditViewModel> Plans { get; set; }

        //Task<object> IContentLoader.LoadContentAsync(Uri uri, System.Threading.CancellationToken cancellationToken)
        //{
        //    //throw new NotImplementedException();
        //    return new Task(new Action(SelectedPlan));// Task<SelectedPlan>;
        //}


    }
}
