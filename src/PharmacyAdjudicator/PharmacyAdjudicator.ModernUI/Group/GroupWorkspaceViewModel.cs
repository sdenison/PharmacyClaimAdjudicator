using Caliburn.Micro;
using Csla.Threading;
using PharmacyAdjudicator.ModernUI.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Handle(GroupEditViewModelClosingMessage closingMessage)
        {
            this.Items.Remove(closingMessage.GroupEditViewModel);
        }

        public async Task AddGroup()
        //public void AddGroup()
        {
            if (string.IsNullOrEmpty(this.SelectedClientId)) // == null)
            {
                _dialogManager.ShowMessage("Please select a client from the dropdown box.", "Could not add group", System.Windows.MessageBoxButton.OK);
                return;
            }
            IsBusy = true;
            BusyMessage = "Adding group...";
            //var group = await Library.Core.Group.GroupEdit.NewGroupAsync(this.SelectedClient.ClientId, "");
            var group = await Library.Core.Group.GroupEdit.NewGroupAsync(this.SelectedClientId, "");
            var groupViewModel = new GroupEditViewModel(group, _eventAggregator, _dialogManager);
            ActivateItem(groupViewModel);
            this.IsBusy = false;
        }

        public override void ActivateItem(GroupEditViewModel item)
        {
            _windowManager.ShowWindow(item);
            base.ActivateItem(item);
        }

        //private Library.Core.Client.ClientInfoList _clients;
        //public Library.Core.Client.ClientInfoList Clients
        //{
        //    get
        //    {
        //        return _clients;
        //    }
        //    set
        //    {
        //        _clients = value;
        //        NotifyOfPropertyChange(() => Clients);
        //    }
        //}

        //public Library.Core.Client.ClientInfo SelectedClient { get; set; }
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
