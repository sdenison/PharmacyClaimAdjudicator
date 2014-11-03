using Caliburn.Micro;
using Csla.Threading;
using PharmacyAdjudicator.ModernUI.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PharmacyAdjudicator.Library.Core.Group;
using System.Windows;

namespace PharmacyAdjudicator.ModernUI.Group
{
    [Export]
    public class GroupWorkspaceViewModel : Conductor<GroupEditViewModel>.Collection.AllActive, IHandle<GroupEditViewModelClosingMessage>
    {

        private IDialog _dialogManager;
        private readonly IWindowManager _windowManager;
        private IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public GroupWorkspaceViewModel(IDialog dialogManager, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _dialogManager = dialogManager;
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            LoadPropertyData();
        }

        /// <summary>
        /// Load data for combo boxes in a background worker.
        /// </summary>
        private void LoadPropertyData()
        {
            //this.IsBusy = true;
            //this.BusyMessage = "Retrieving data...";
            //var worker = new BackgroundWorker();
            //worker.DoWork += (o, ea) =>
            //{
            //    this.Clients = Library.Core.Client.ClientInfoList.GetAllClients();
            //};
            //worker.RunWorkerCompleted += (o, ea) =>
            //{
            //    this.IsBusy = false;
            //    this.BusyMessage = "";
            //    NotifyOfPropertyChange(() => this.Clients);
            //};
            //worker.RunWorkerAsync();
        }

        #region "Group Search"
        private static GroupSearchCriteria _groupSearchCriteria = new GroupSearchCriteria();
        public GroupSearchCriteria GroupSearchCriteria
        {
            get { return _groupSearchCriteria; }
            set { _groupSearchCriteria = value; }
        }

        private GroupList _searchResults;
        public GroupList SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                NotifyOfPropertyChange(() => SearchResults);
                NotifyOfPropertyChange(() => CanShowSearchResults);
            }
        }

        public Boolean CanShowSearchResults
        {
            get
            {
                if (_searchResults == null)
                    return false;
                else
                    return _searchResults.Count > 0;
            }
            private set { }
        }

        //public async void FindGroups()
        public void FindGroups()
        {
            if (string.IsNullOrEmpty(SelectedClientId))
            {
                _dialogManager.ShowMessage("Please select a Client from the dropdown list.", "Search Criteria Missing", MessageBoxButton.OK);
                return;
            }
            _groupSearchCriteria.ClientId = SelectedClientId;
            this.IsSearching = true;
            //var groupSearchResults = await GroupList.GetByCriteriaAsync(this.GroupSearchCriteria);
            var groupSearchResults = GroupList.GetByCriteria(this.GroupSearchCriteria);
            this.IsSearching = false;
            if (groupSearchResults.Count == 0)
            {
                _dialogManager.ShowMessage("No groups found for search criteria.", "No Records Found", MessageBoxButton.OK);
            }
            this.SearchResults = groupSearchResults;
        }

        private bool _isSearching = false;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { _isSearching = value; NotifyOfPropertyChange(() => IsSearching); }
        }
        #endregion

        public void Handle(GroupEditViewModelClosingMessage closingMessage)
        {
            this.Items.Remove(closingMessage.GroupEditViewModel);
        }

        public async Task AddGroup()
        {
            if (string.IsNullOrEmpty(this.SelectedClientId)) // == null)
            {
                _dialogManager.ShowMessage("Please select a client from the dropdown box.", "Could not add group", System.Windows.MessageBoxButton.OK);
                return;
            }
            IsBusy = true;
            BusyMessage = "Adding group...";
            var group = await Library.Core.Group.GroupEdit.NewGroupAsync(this.SelectedClientId, "");
            var groupViewModel = new GroupEditViewModel(group, _eventAggregator, _dialogManager);
            ActivateItem(groupViewModel);
            IsBusy = false;
            BusyMessage = "";
        }

        public async Task ShowGroup(Library.Core.Group.GroupEdit group)
        {
            var existingGroupEditViewModel = this.Items.FirstOrDefault(g => g.Id.ToString() == group.GroupId.ToString());
            if (existingGroupEditViewModel == null)
            {
                this.IsBusy = true;
                var groupViewModel = await GroupEditViewModel.BuildViewModelAsync(group.ClientId, group.GroupId, _eventAggregator, _dialogManager);
                ActivateItem(groupViewModel);
                this.IsBusy = false;
            }
            else
            {
                existingGroupEditViewModel.Focus();
            }
        }

        public void ShowGroupViewModel(GroupEditViewModel groupVm)
        {
            groupVm.Focus();
        }

        public override void ActivateItem(GroupEditViewModel item)
        {
            _windowManager.ShowWindow(item);
            base.ActivateItem(item);
        }

        public void DeactivateItem(GroupEditViewModel item)
        {
            this.DeactivateItem(item, true);
        }

        private List<string> _clients = null;
        public IEnumerable<string> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new List<string>();
                    foreach (var client in Library.Core.Client.ClientInfoList.GetAllClients())
                        _clients.Add(client.ClientId);
                }
                return _clients;
            }
        }

        public string SelectedClientId { get; set; }

        private bool _isBusy;
        public bool IsBusy 
        {
            get { return _isBusy; }
            set 
            { 
                if (value != _isBusy)
                {
                    _isBusy = value;
                    NotifyOfPropertyChange(() => IsBusy);
                }
            } 
        }

        private string _busyMessge;
        public string BusyMessage
        {
            get { return _busyMessge; }
            set 
            { 
                if (string.IsNullOrEmpty(_busyMessge) || !_busyMessge.Equals(value))
                {
                    _busyMessge = value; 
                    NotifyOfPropertyChange(() => BusyMessage); }
                }
        }
    }
}
