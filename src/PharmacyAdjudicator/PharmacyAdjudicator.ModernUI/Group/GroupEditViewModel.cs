using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CslaContrib.Caliburn.Micro;
using PharmacyAdjudicator.ModernUI.Interface;
using Caliburn.Micro;
using System.Windows;
using PharmacyAdjudicator.Library.Core.Group;
using System.ComponentModel;
using System.Collections.Specialized;

namespace PharmacyAdjudicator.ModernUI.Group
{
    public class GroupEditViewModel : ScreenWithModel<Library.Core.Group.GroupEdit>
    {
        private IEventAggregator _eventAggregator;
        private IDialog _dialog;

        /// <summary>
        /// Sets the EditState.  Caliburn uses this to switch between multiple views for this ViewModel.
        /// </summary>
        private GroupEditState _state;
        public GroupEditState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    NotifyOfPropertyChange(() => this.State);
                }
            }
        }

       
        //private Library.Core.Client.ClientInfoList _clients;
        //public Library.Core.Client.ClientInfoList Clients
        //{
        //    get 
        //    { 
        //        return _clients; 
        //    }
        //    private set 
        //    { 
        //        _clients = value; 
        //        NotifyOfPropertyChange(() => Clients); 
        //    }
        //}

        public IEnumerable<GroupEditState> EditStates
        {
            get
            {
                yield return GroupEditState.Details;
                yield return GroupEditState.Clients;
            }
        }

        public bool IsReadOnly
        {
            get { return !CanEditObject; }
            private set { }
        }

        public string Id
        {
            get { return Model.GroupId.ToString();  }
        }

        public void Save()
        {
            //Guards the save if there is another patient with the same first name, last name, dob and cardholder combination.
            if (Model.IsNew)
            {
                if (Library.Core.Group.GroupEdit.Exists(Model.ClientId, Model.GroupId))
                {
                    _dialog.ShowMessage("Another group already exists with the same information.", "Could not add group", MessageBoxButton.OK);
                    return;
                }
            }
            BeginSave();
        }

        protected override void OnSaved()
        {
            //this.SelectedClientAssignment 
            this.Model.BrokenRulesCollection.CollectionChanged += ExtractClientAssignmentRules;
            base.OnSaved();
        }



        internal GroupEditViewModel(Library.Core.Group.GroupEdit existingGroup, IEventAggregator eventAggregator, IDialog dialog)
        {
            _eventAggregator = eventAggregator;
            _dialog = dialog;
            //this.Clients = allClients;
            this.Model = existingGroup;
            this.State = GroupEditState.Details;
            this.DisplayName = "Group Display: " + Model.GroupId;
            this.SelectedClientAssignment = existingGroup.ClientAssignments[0];

            existingGroup.BrokenRulesCollection.CollectionChanged += ExtractClientAssignmentRules;
        }

        private void ExtractClientAssignmentRules(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => BrokenClientAssignmentRules);
        }

        public IEnumerable<Csla.Rules.BrokenRule> BrokenClientAssignmentRules
        {
            get
            { 
                var clientAssignmentBrokenRules = this.Model.BrokenRulesCollection.Where(br => br.Property == "ClientAssignments");
                return clientAssignmentBrokenRules.ToList();
            }

        }

        async public static Task<GroupEditViewModel> BuildViewModelAsync(string clientId, string groupId, IEventAggregator eventAggregator, IDialog dialog)
        {
            var groupModel = await Library.Core.Group.GroupEdit.GetByClientIdGroupIdAsync(clientId, groupId);
            var clients = await Library.Core.Client.ClientInfoList.GetAllClientsAsync();
            //return new GroupEditViewModel(groupModel, clients, eventAggregator, dialog);
            return new GroupEditViewModel(groupModel, eventAggregator, dialog);
        }

        public async Task RefreshAsync()
        {
            if (Model.IsNew)
                return;
            this.IsBusy = true;
            var currentGroupId = Model.GroupId;
            var currentClientId = Model.ClientId;
            Model = await Library.Core.Group.GroupEdit.GetByClientIdGroupIdAsync(currentClientId, currentGroupId);
            this.IsBusy = false;
            base.Refresh();
        }

        public void Undo()
        {
            if (Model.IsNew)
                return;
            Model.CancelEdit();
            Model.BeginEdit();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.PublishOnCurrentThread(new GroupEditViewModelClosingMessage() { GroupEditViewModel = this });
            base.OnDeactivate(close);
        }

        public void Focus()
        {
            var window = GetView() as Window;
            if (window != null) window.Activate();
        }

        private ClientAssignment _selectedClientAssignment;
        public ClientAssignment SelectedClientAssignment
        {
            get { return _selectedClientAssignment; }
            set 
            { 
                _selectedClientAssignment = value; 
                NotifyOfPropertyChange(() => SelectedClientAssignment); 
            }
        }

        public void AddClientAssignment()
        {
            try
            {
                this.Model.ClientAssignments.AddNewAssignment();
                SelectedClientAssignment = this.Model.ClientAssignments[this.Model.ClientAssignments.Count - 1];
            }
            catch (Exception ex)
            {
                _dialog.ShowMessage(ex.GetBaseException().Message, "Could not add assignment", MessageBoxButton.OK);

            }
        }
        public void RemoveClientAssignment()
        {
            if (this.Model.ClientAssignments.Count == 1)
            {
                _dialog.ShowMessage("Group must have at least one assignment to a client.", "Cannot delete client assignment", MessageBoxButton.OK);
                return;
            }
            this.Model.ClientAssignments.Remove(SelectedClientAssignment);
        }
    }

    public enum GroupEditState
    {
        Details,
        Clients
    }
}
