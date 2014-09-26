using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class GroupExistsCommand : CommandBase<GroupExistsCommand>
    {
        #region Factory Methods

        public static bool Execute(string clientId, string groupId)
        {
            GroupExistsCommand cmd = new GroupExistsCommand() { ClientId = clientId, GroupId = groupId };
            cmd = DataPortal.Execute<GroupExistsCommand>(cmd);
            return cmd.Result;
        }

        private GroupExistsCommand()
        { /* require use of factory methods */ }

        #endregion

        #region Client-side Code

        public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
        public string GroupId
        {
            get { return ReadProperty(GroupIdProperty); }
            set { LoadProperty(GroupIdProperty, value); }
        }

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        public string ClientId
        {
            get { return ReadProperty(ClientIdProperty); }
            set { LoadProperty(ClientIdProperty, value); }
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(p => p.Result);
        public bool Result
        {
            get { return ReadProperty(ResultProperty); }
            set { LoadProperty(ResultProperty, value); }
        }

        #endregion

        #region Server-side Code

        protected override void DataPortal_Execute()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Checks if there are any current Group records attached to active clients
                //Tables are joined by GroupDetail -> Group -> ClientGroup -> Client -> ClientDetail
                Result = (from gd in ctx.DbContext.GroupDetail
                         join g in ctx.DbContext.Group on gd.GroupInternalId equals g.GroupInternalId
                         join cg in ctx.DbContext.ClientGroup on g.GroupInternalId equals cg.GroupInternalId
                         join c in ctx.DbContext.Client on cg.ClientInternalId equals c.ClientInternalId
                         join cd in ctx.DbContext.ClientDetail on c.ClientInternalId equals cd.ClientInternalId
                         where gd.GroupId == this.GroupId
                         && gd.Retraction == false
                         && !ctx.DbContext.GroupDetail.Any(gd2 => gd2.Retraction == true && gd2.OriginalFactRecordId == gd.RecordId)
                         && cg.Retraction == false
                         && !ctx.DbContext.ClientGroup.Any(cg2 => cg2.Retraction == true && cg2.OriginalFactRecordId == cg.RecordId)
                         && cd.Retraction == false
                         && !ctx.DbContext.ClientGroup.Any(cd2 => cd2.Retraction == true && cd2.OriginalFactRecordId == cd.RecordId)
                         && cd.ClientId == this.ClientId
                         select gd).Any();
            }
        }

        #endregion
    }
}
