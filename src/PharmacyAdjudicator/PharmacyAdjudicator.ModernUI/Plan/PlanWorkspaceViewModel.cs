using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            //Plans = new BindableCollection<PlanEditViewModel>();
            //foreach (var plan in Library.Core.Plan.PlanList.GetAll())
            //    Plans.Add(new PlanEditViewModel(plan));

            //NotifyOfPropertyChange(() => FilteredPlans);
            Plans.CollectionChanged += new NotifyCollectionChangedEventHandler(PlanList_CollectionChanged);
            Plans.ChildChanged += new EventHandler<Csla.Core.ChildChangedEventArgs>(PlanList_ItemChanged);
            _planLoader = new PlanLoader(this);
        }

        private void PlanList_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            //var x = this.PlanLinks;
            NotifyOfPropertyChange(() => PlanLinks);
        }

        private void SyncPlansToLinks()
        {
            if (_planLinks == null)
                _planLinks = new LinkCollection();
            foreach(var plan in Plans)
            {
                var pl = _planLinks.FirstOrDefault(p => p.Source.Equals(plan.PlanInternalId.ToString()));//.DisplayName.Equals())
                if (pl == null)
                    _planLinks.Add(new Link() { Source = new Uri(plan.PlanInternalId.ToString(), UriKind.Relative), DisplayName = plan.PlanId });
                else
                {
                    if (!pl.DisplayName.Equals(plan.PlanId))
                    {
                        pl.DisplayName = plan.PlanId;
                    }
                }
            }
            List<Link> linksToDelete = new List<Link>();
            foreach (var link in _planLinks)
            {
                var plan = Plans.FirstOrDefault(p => p.PlanInternalId.ToString().Equals(link.Source.ToString()));
                if (plan == null)
                    linksToDelete.Add(link);
            }
            //var linksToDelete2 = _planLinks.All(pl => !Plans.Any(p => p.PlanInternalId.ToString().Equals(pl.Source.OriginalString)));
            linksToDelete.All(link => _planLinks.Remove(link));
        }

        private void PlanList_ItemChanged(Object sender, Csla.Core.ChildChangedEventArgs e)
        {
            //var x = this.PlanLinks;
            NotifyOfPropertyChange(() => PlanLinks);
        }

        public Library.Core.Plan.PlanList Plans
        {
            get;
            private set;
        }

        public Library.Core.Plan.PlanEdit SelectedPlan
        {
            get;
            set;
        }

        //public BindableCollection<PlanEditViewModel> Plans
        //{
        //    get;
        //    private set;
        //}

        //public PlanListViewModel Plans
        //{
        //    get;
        //    private set;
        //}

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
                //NotifyOfPropertyChange(() => FilteredPlans);
            }
        }

        private LinkCollection _planLinks;
        public LinkCollection PlanLinks
        {
            get
            {
                SyncPlansToLinks();
                return _planLinks;
                //var lc = new LinkCollection();
                //foreach (var plan in FilteredPlans)
                //    lc.Add(new Link() { DisplayName = plan.PlanId, Source = new Uri(plan.PlanId, UriKind.Relative) });
                //return lc;
            }
        }

        private PlanLoader _planLoader;
        public PlanLoader PlanLoader
        {
            get { return _planLoader; }
        }

        public void AddPlan()
        {
            //var newPlan = Library.Core.Plan.PlanEdit.NewPlan("Replace this Plan ID");
            //var newPlan = this.Plans.AddNew();
            //NotifyOfPropertyChange(() => this.Plans);
            //NotifyOfPropertyChange(() => this.PlanLinks);

            //this.Plans.Add(newPlan);
            this.Plans.AddNew();
        }

        public void Save()
        {

        }
    }
}
