using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class ClientAssignmentList : BusinessListBase<ClientAssignmentList, ClientAssignment>
    {
        public void AddNewAssignment()
        {
            var currentLastItem = this.Items[this.Count - 1];
            if (currentLastItem.ExpirationDate.Equals(new DateTime(9999, 12, 31)))
                throw new Exception("Cannot add ClientAssignment to Group when current expiration date is set to default.");
            var newEffectiveDate = currentLastItem.ExpirationDate.AddDays(1);
            //var defaultClientId = currentLastItem.Client.ClientId;
            var defaultClientId = currentLastItem.ClientId;
            var newAssignment = ClientAssignment.NewAssignment(defaultClientId, newEffectiveDate, new DateTime(9999, 12, 31));
            Add(newAssignment);
        }
        #region Factory Methods

        internal static ClientAssignmentList NewAssignmentList()
        {
            return DataPortal.CreateChild<ClientAssignmentList>();
        }

        internal static ClientAssignmentList GetByGroupInternalId(string clientId, string groupId)
        {
            return DataPortal.FetchChild<ClientAssignmentList>(clientId, groupId);
        }

        private ClientAssignmentList()
        { }

        #endregion

        #region Data Access

        private void Child_Fetch(string clientId, string groupId)
        {
            RaiseListChangedEvents = false;

            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var clientGroups = (from gd in ctx.DbContext.GroupDetail
                                 join g in ctx.DbContext.Group on gd.GroupInternalId equals g.GroupInternalId
                                 join cg in ctx.DbContext.ClientGroup on g.GroupInternalId equals cg.GroupInternalId
                                 join c in ctx.DbContext.Client on cg.ClientInternalId equals c.ClientInternalId
                                 join cd in ctx.DbContext.ClientDetail on c.ClientInternalId equals cd.ClientInternalId
                                 where gd.GroupId == groupId
                                 && gd.Retraction == false
                                 && !ctx.DbContext.GroupDetail.Any(gd2 => gd2.Retraction == true && gd2.OriginalFactRecordId == gd.RecordId)
                                 && cg.Retraction == false
                                 && !ctx.DbContext.ClientGroup.Any(cg2 => cg2.Retraction == true && cg2.OriginalFactRecordId == cg.RecordId)
                                 && cd.Retraction == false
                                 && !ctx.DbContext.ClientDetail.Any(cd2 => cd2.Retraction == true && cd2.OriginalFactRecordId == cd.RecordId)
                                 && cd.ClientId == clientId
                                 orderby cg.EffectiveDate 
                                 select new ClientAssignmentDto 
                                 {
                                     RecordId = cg.RecordId,
                                     ClientInternalId = cg.ClientInternalId,
                                     ClientId = cd.ClientId,
                                     GroupInternalId = cg.GroupInternalId,
                                     EffectiveDate = cg.EffectiveDate,
                                     ExpirationDate = cg.ExpirationDate
                                 });
                foreach (var clientGroup in clientGroups)
                    Add(DataPortal.FetchChild<ClientAssignment>(clientGroup));
            }

            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
