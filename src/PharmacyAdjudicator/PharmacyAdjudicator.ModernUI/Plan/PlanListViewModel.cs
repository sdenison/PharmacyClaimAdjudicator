using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Csla.Core;
using Csla.Server;
using CslaContrib.Caliburn.Micro;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using PharmacyAdjudicator.Library.Core.Plan;
using PharmacyAdjudicator.ModernUI.Interface;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    [Export]
    public class PlanListViewModel : Screen //ScreenWithModel<PlanList> //ViewModelEdit<PlanList>  //ScreenWithModel<PlanList> //, IContentLoader //Conductor<PlanEditViewModel>.Collection.OneActive, IContentLoader// ScreenWithModel<PlanList> /// Conductor<PlanEditViewModel>.Collection.OneActive
    {
        private IDialog _dialogManager;

        [ImportingConstructor]
        public PlanListViewModel(IDialog dialogManager) : base()
        {
            _dialogManager = dialogManager;
            //var task = PlanList.GetAllAsync();
            //var awaiter = task.GetAwaiter();
            //IsBusy = true;
            //awaiter.OnCompleted(() =>
            //    {
            //        if (task.Exception != null)
            //        {
            //            IsBusy = false;
            //            _dialogManager.ShowMessage(task.Exception.Message, "Could not load plans", System.Windows.MessageBoxButton.OK);
            //        }
            //        else
            //        {
            //            this.Model = task.Result;
            //            IsBusy = false;
            //        }
            //    });

            GetPlans();
        }

        public async void GetPlans()
        {
            IsBusy = true;
            Model = await PlanList.GetAllAsync();
            IsBusy = false;
        }

        //protected override void OnActivate()
        //{
        //    base.OnActivate();
        //}

        protected override async void OnInitialize()
        {
            base.OnInitialize();
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value; 
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        private PlanList _model;
        public PlanList Model
        {
            get { return _model; }
            set 
            {
                if (_model != null)
                { 
                    _model.ChildChanged -= ModelChildChanged;
                    _model.CollectionChanged -= ModelCollectionChanged;
                }
                _model = value;
                if (value != null)
                {
                    value.ChildChanged += ModelChildChanged;
                    value.CollectionChanged += ModelCollectionChanged;
                }
                NotifyOfPropertyChange(() => this.Model);
                NotifyOfPropertyChange(() => this.CanSave);
            }
        }

        private void ModelChildChanged(object sender, ChildChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => this.CanSave);
        }

        private void ModelCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => this.CanSave);
        }

        public bool CanSave
        {
            get 
            { 
                return Model != null && Model.IsSavable; 
            }
        }

        public async Task Save()
        {
            //Saves the currently selected plan ID so we can re-select the plan after saving.
            string currentlySelectedPlanId = "";
            if (SelectedPlan != null)
                currentlySelectedPlanId = SelectedPlan.PlanId;

            IsBusy = true;
            try
            {
                Model = await Model.SaveAsync();
                //If there was a plan selected when the saved then try to select that plan again.
                //Otherwise just use the first item as the selected plan.
                if (string.IsNullOrEmpty(currentlySelectedPlanId))
                    SelectedPlan = Model[0];
                else
                    SelectedPlan = Model.Where(p => p.PlanId == currentlySelectedPlanId).FirstOrDefault();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                var message = ex.GetBaseException().Message;
                _dialogManager.ShowMessage(message, "ERROR", System.Windows.MessageBoxButton.OK);
            }
        }

        private PlanEdit _selectedPlan;
        public PlanEdit SelectedPlan
        {
            get
            {
                return _selectedPlan;
            }
            set
            {
                if ((_selectedPlan == null) || (!_selectedPlan.Equals(value)))
                {
                    _selectedPlan = value;
                    NotifyOfPropertyChange(() => SelectedPlan);
                    NotifyOfPropertyChange(() => SelectedPlanEdit);
                }
            }
        }

        public PlanEditViewModel SelectedPlanEdit
        {
            get
            {
                return new PlanEditViewModel(_selectedPlan);
            }
            private set { }
        }

        public void AddPlan()
        {
            SelectedPlan = Model.AddNew();
        }
    }
}
