using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class GroupEdit : BusinessBase<GroupEdit>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        //[Required]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
        //[Required]
        public string GroupId
        {
            get { return GetProperty(GroupIdProperty); }
            set { SetProperty(GroupIdProperty, value); }
        }


        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        /// <summary>
        /// Must be assigned to one client
        /// </summary>
        [Required]
        public string ClientId
        {
            get { return GetProperty(ClientIdProperty); }
            private set { SetProperty(ClientIdProperty, value); }
        }

        public static readonly PropertyInfo<ClientAssignmentList> ClientAssignmentsProperty = RegisterProperty<ClientAssignmentList>(c => c.ClientAssignments, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public ClientAssignmentList ClientAssignments
        {
            get
            {
                if (!FieldManager.FieldExists(ClientAssignmentsProperty))
                    LoadProperty(ClientAssignmentsProperty, DataPortal.FetchChild<ClientAssignmentList>(this.GroupInternalId));
                    //LoadProperty(ClientAssignmentsProperty, DataPortal.FetchChild<ClientAssignmentList>(this.ClientId, this.GroupId));
                return GetProperty(ClientAssignmentsProperty);
            }
            private set
            {
                SetProperty(ClientAssignmentsProperty, value);
                OnPropertyChanged(ClientAssignmentsProperty);
            }
        }

        private Guid _GroupInternalId;
        internal Guid GroupInternalId
        {
            get { return _GroupInternalId; }
            set { _GroupInternalId = value; }
        }

        private Guid _RecordId;
        private Guid RecordId
        {
            get { return _RecordId; }
            set { _RecordId = value; }
        }

        public static bool Exists(string clientId, string groupId)
        {
            return GroupExistsCommand.Execute(clientId, groupId);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
            //BusinessRules.AddRule(new OnlyUniqueAddressTypesAllowedInAddressList(PatientAddressesProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(NameProperty, "Name is required."));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MinLength(NameProperty, 5, "Must be at least five characters long."));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(GroupIdProperty, "Group ID is requried."));
            BusinessRules.AddRule(new ClientAssignmentsCannotOverlap(ClientAssignmentsProperty));
            base.AddBusinessRules();
        }

        private static void AddObjectAuthorizationRules()
        {
            //Requires that the user be in the RuleManager role to create, edit or delete a Group object
            Csla.Rules.BusinessRules.AddRule(typeof(GroupEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(GroupEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(GroupEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(GroupEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
        {
            base.OnChildChanged(e);
            BusinessRules.CheckRules(GroupEdit.ClientAssignmentsProperty);
        }

        #endregion

        #region Factory Methods

        public static GroupEdit GetByClientIdGroupId(string clientId, string groupId)
        {
            return DataPortal.Fetch<GroupEdit>(new CriteriaByClientIdGroupId() { ClientId = clientId, GroupId = groupId }); 
        } 

        public static async Task<GroupEdit> GetByClientIdGroupIdAsync(string clientId, string groupId)
        {
            return await DataPortal.FetchAsync<GroupEdit>(new CriteriaByClientIdGroupId() { ClientId = clientId, GroupId = groupId });
        }

        public static GroupEdit NewGroup(string clientId, string groupId)
        {
            return DataPortal.Create<GroupEdit>(new CriteriaByClientIdGroupId() { ClientId = clientId, GroupId = groupId });
        }

        //public static GroupEdit NewGroup()
        //{
        //    return DataPortal.Create<GroupEdit>();
        //}

        public static async Task<GroupEdit> NewGroupAsync(string clientId, string groupId)
        {
            return await DataPortal.CreateAsync<GroupEdit>(new CriteriaByClientIdGroupId() { ClientId = clientId, GroupId = groupId });
        }

        //public static async Task<GroupEdit> NewGroupAsync()
        //{
        //    return await DataPortal.CreateAsync<GroupEdit>();
        //}

        //public static void DeleteBy(int id)
        //{
        //    DataPortal.Delete<GroupEdit>(id);
        //}

        private GroupEdit()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        [Serializable]
        protected class CriteriaByClientIdGroupId : CriteriaBase<CriteriaByClientIdGroupId>
        {
            public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
            public string ClientId
            {
                get { return ReadProperty(ClientIdProperty); }
                set { LoadProperty(ClientIdProperty, value); }
            }

            public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
            public string GroupId
            {
                get { return ReadProperty(GroupIdProperty); }
                set { LoadProperty(GroupIdProperty, value); }
            }
        }

        [RunLocal]
        protected void DataPortal_Create(CriteriaByClientIdGroupId criteria)
        {
            //Make sure the client exists
            if (!Client.ClientEdit.Exists(criteria.ClientId))
                throw new DataNotFoundException("Client must exist when creating group.");
            //No need to go to the database yet since all our Id's are Guids.
            this.ClientId = criteria.ClientId;
            this.GroupId = criteria.GroupId;
            this.GroupInternalId = Utils.GuidHelper.GenerateComb();
            //Assigns a client to the Group with default effective and expiration dates
            var clientAssignments = this.ClientAssignments;
            clientAssignments.Add(ClientAssignment.NewAssignment(this.ClientId, DateTime.Now, new DateTime(9999, 12, 31)));
            base.DataPortal_Create();
        }

        //[RunLocal]
        //protected void DataPortal_Create(CriteriaByClientIdGroupId criteria)
        //{
        //    //Make sure the client exists
        //    this.ClientId = criteria.ClientId;
        //    this.GroupId = criteria.GroupId;
        //    this.GroupInternalId = Utils.GuidHelper.GenerateComb();
        //    base.DataPortal_Create();
        //}

        private void DataPortal_Fetch(CriteriaByClientIdGroupId criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var groupData = (from gd in ctx.DbContext.GroupDetail
                                 join g in ctx.DbContext.Group on gd.GroupInternalId equals g.GroupInternalId
                                 join cg in ctx.DbContext.ClientGroup on g.GroupInternalId equals cg.GroupInternalId
                                 join c in ctx.DbContext.Client on cg.ClientInternalId equals c.ClientInternalId
                                 join cd in ctx.DbContext.ClientDetail on c.ClientInternalId equals cd.ClientInternalId
                                 where gd.GroupId == criteria.GroupId
                                 && gd.Retraction == false
                                 && !ctx.DbContext.GroupDetail.Any(gd2 => gd2.Retraction == true && gd2.OriginalFactRecordId == gd.RecordId)
                                 && cg.Retraction == false
                                 && !ctx.DbContext.ClientGroup.Any(cg2 => cg2.Retraction == true && cg2.OriginalFactRecordId == cg.RecordId)
                                 && cd.Retraction == false
                                 && !ctx.DbContext.ClientDetail.Any(cd2 => cd2.Retraction == true && cd2.OriginalFactRecordId == cd.RecordId)
                                 && cd.ClientId == criteria.ClientId
                                 select gd).Distinct().ToList();
                if (groupData.Count == 0)
                    throw new DataNotFoundException("ClientId = " + criteria.ClientId + "; GroupId = " + criteria.GroupId);
                if (groupData.Count > 1)
                    throw new DataNotUniqueException("ClientId = " + criteria.ClientId + "; GroupId = " + criteria.GroupId);
                using (BypassPropertyChecks)
                {
                    PopulateByRow(groupData[0], criteria.ClientId);
                }
            }
        }

        protected override void DataPortal_Insert()
        {
            var client = Client.ClientInfo.GetByClientId(this.ClientId);
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Creates record in Group table
                DataAccess.Group newGroup = new DataAccess.Group();
                this.GroupInternalId = Utils.GuidHelper.GenerateComb();
                newGroup.GroupInternalId = this.GroupInternalId;
                newGroup.RecordCreatedDateTime = DateTime.Now;
                newGroup.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Group.Add(newGroup);

                //Creates record in GroupDetail
                AssertNewFact();

                ////Assigns a client to the Group with default effective and expiration dates
                //var clientAssignments = this.ClientAssignments;
                //clientAssignments.Add(ClientAssignment.NewAssignment(this.ClientId, DateTime.Now, new DateTime(9999, 12, 31)));

                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                if (this.IsSelfDirty)
                {
                    RetractFact();
                    this.RecordId = Utils.GuidHelper.GenerateComb();
                    AssertNewFact();
                }
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            RetractFact();
        }

        private void RetractFact()
        {
            var originalRecordId = this.RecordId;
            var groupData = CreateNewEntity();
            groupData.Retraction = true;
            groupData.OriginalFactRecordId = originalRecordId;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.GroupDetail.Add(groupData);
            }
        }

        private DataAccess.GroupDetail AssertNewFact()
        {
            var groupData = CreateNewEntity();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.GroupDetail.Add(groupData);
            }
            return groupData;
        }

        private void Child_Fetch(DataAccess.GroupDetail groupData, string clientId)
        {
            using (BypassPropertyChecks)
                PopulateByRow(groupData, clientId);
                //PopulateByRow(patientData);
        }

        private DataAccess.GroupDetail CreateNewEntity()
        {
            var groupDetail = new DataAccess.GroupDetail();
            groupDetail.RecordId = Utils.GuidHelper.GenerateComb();
            groupDetail.GroupId = this.GroupId.ToUpper();
            groupDetail.GroupInternalId = this.GroupInternalId;
            groupDetail.Name = this.Name.ToUpper();
            groupDetail.Retraction = false;
            groupDetail.RecordCreatedDateTime = DateTime.Now;
            groupDetail.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return groupDetail;
        }

        //ClientID is used to identify the group so it must be passed in.
        private void PopulateByRow (DataAccess.GroupDetail groupDetailData, string clientId)
        {
            this.RecordId = groupDetailData.RecordId;
            this.GroupId = groupDetailData.GroupId;
            this.GroupInternalId = groupDetailData.GroupInternalId;
            this.Name = groupDetailData.Name;
            this.ClientId = clientId;
        }

        #endregion
    }
}
