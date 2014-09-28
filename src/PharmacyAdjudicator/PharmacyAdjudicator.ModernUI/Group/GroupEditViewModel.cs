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

        public IEnumerable<GroupEditState> EditStates
        {
            get
            {
                yield return GroupEditState.Details;
                yield return GroupEditState.ClientAssignments;
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

        internal GroupEditViewModel(Library.Core.Group.GroupEdit existingGroup, IEventAggregator eventAggregator, IDialog dialog)
        {
            _eventAggregator = eventAggregator;
            _dialog = dialog;
            this.Model = existingGroup;
            this.State = GroupEditState.Details;
            this.DisplayName = "Group Display: " + Model.GroupId;
        }

        async public static Task<GroupEditViewModel> BuildViewModelAsync(string clientId, string groupId, IEventAggregator eventAggregator, IDialog dialog)
        {
            var groupModel = await Library.Core.Group.GroupEdit.GetByClientIdGroupIdAsync(clientId, groupId);
            return new GroupEditViewModel(groupModel, eventAggregator, dialog);
        }

        public async Task RefreshAsync()
        {
            if (Model.IsNew)
                return;
            this.IsBusy = true;
            var currentGroupId = Model.GroupId;
            var currentClientId = Model.ClientId;
            Model = await Library.Core.Group.GroupEdit.GetByClientIdGroupIdAsync(currentGroupId, currentClientId);
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

        public ClientAssignment SelectedClientAssignment { get; set; }

        public void AddClientAssignment()
        {
            this.Model.ClientAssignments.AddNewAssignment();
            SelectedClientAssignment = this.Model.ClientAssignments[this.Model.ClientAssignments.Count - 1];
        }
    }

    public enum GroupEditState
    {
        Details,
        ClientAssignments
    }
}
