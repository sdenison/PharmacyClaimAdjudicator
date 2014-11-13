using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
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
    }
}
