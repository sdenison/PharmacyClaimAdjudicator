using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    [Export]
    public class PlanWorkspaceViewModel : Screen
    {
        private IDialog _dialogManager;
        //private readonly IWindowManager _windowManager;
        private IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public PlanWorkspaceViewModel(IDialog dialogManager, IEventAggregator eventAggregator)
        {
            _dialogManager = dialogManager;
            //_windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            Plans = Library.Core.Plan.PlanList.GetAll();
            NotifyOfPropertyChange(() => FilteredPlans);
            _planLoader = new PlanLoader(this);
        }

        public Library.Core.Plan.PlanList Plans
        {
            get;
            private set;
        }

        public IList<Library.Core.Plan.PlanEdit> FilteredPlans 
        { 
            get
            {
                if (string.IsNullOrEmpty(PlanFilter))
                    return Plans;
                else
                    return Plans.Where(p => p.Name.Contains(PlanFilter)).ToList();
            }
            private set { }
        }

        private string _planfilter;
        public string PlanFilter 
        { 
            get
            {
                return _planfilter;
            }
            set
            {
                _planfilter = value;
                NotifyOfPropertyChange(() => FilteredPlans);
            }
        }

        public LinkCollection PlanLinks
        {
            get
            {
                var lc = new LinkCollection();
                foreach (var plan in FilteredPlans)
                    lc.Add(new Link() { DisplayName = plan.PlanId, Source = new Uri(plan.PlanId, UriKind.Relative) });
                return lc;
            }
        }

        private PlanLoader _planLoader;
        public PlanLoader PlanLoader
        {
            get { return _planLoader; }
        }

        public void AddPlan()
        {
            var newPlan = Library.Core.Plan.PlanEdit.NewPlan("Replace this Plan ID");
            this.Plans.Add(newPlan);
            //this.Plans.AddNew();

        }
    }
}
