using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class GroupList : ReadOnlyListBase<GroupList, GroupEdit>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(GroupList), "Role");
        }

        #endregion

        #region Factory Methods

        public static GroupList GetReadOnlyList(string filter)
        {
            return DataPortal.Fetch<GroupList>(filter);
        }

        private GroupList()
        { /* require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(GroupSearchCriteria criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                IQueryable<DataAccess.GroupDetail> groupData = (from g in ctx.DbContext.GroupDetail
                                                                where g.Retraction == false
                                                                && !ctx.DbContext.GroupDetail.Any(g2 => g2.Retraction == true && g2.OriginalFactRecordId == g.RecordId)
                                                                orderby g.GroupId, g.Name
                                                                select g);
                if (!string.IsNullOrWhiteSpace(criteria.Name))
                    groupData = groupData.Where(g => g.Name.StartsWith(criteria.Name.Trim()));
                if (!string.IsNullOrWhiteSpace(criteria.GroupId))
                    groupData = groupData.Where(g => g.GroupId.StartsWith(criteria.GroupId.Trim()));
            
                foreach (var g in groupData)
                    Add(DataPortal.FetchChild<GroupEdit>(g));

            }
            //object objectData = null;
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
        #endregion
    }
}
