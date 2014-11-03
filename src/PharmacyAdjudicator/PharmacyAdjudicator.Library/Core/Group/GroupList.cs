using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async static Task<GroupList> GetByCriteriaAsync(GroupSearchCriteria criteria)
        {
            return await DataPortal.FetchAsync<GroupList>(criteria);
        }

        public static GroupList GetByCriteria(GroupSearchCriteria criteria)
        {
            return DataPortal.Fetch<GroupList>(criteria);
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
                IQueryable<DataAccess.GroupDetail> groupData = (from gd in ctx.DbContext.GroupDetail
                                                                join g in ctx.DbContext.Group on gd.GroupInternalId equals g.GroupInternalId
                                                                join cg in ctx.DbContext.ClientGroup on g.GroupInternalId equals cg.GroupInternalId
                                                                join c in ctx.DbContext.Client on cg.ClientInternalId equals c.ClientInternalId
                                                                join cd in ctx.DbContext.ClientDetail on c.ClientInternalId equals cd.ClientInternalId
                                                                where gd.Retraction == false
                                                                && !ctx.DbContext.GroupDetail.Any(gd2 => gd2.Retraction == true && gd2.OriginalFactRecordId == gd.RecordId)
                                                                && cg.Retraction == false
                                                                && !ctx.DbContext.ClientGroup.Any(cg2 => cg2.Retraction == true && cg2.OriginalFactRecordId == cg.RecordId)
                                                                && cd.Retraction == false
                                                                && !ctx.DbContext.ClientDetail.Any(cd2 => cd2.Retraction == true && cd2.OriginalFactRecordId == cd.RecordId)
                                                                && cd.ClientId == criteria.ClientId
                                                                orderby gd.GroupId, gd.Name
                                                                select gd);
                if (!string.IsNullOrWhiteSpace(criteria.Name))
                    groupData = groupData.Where(g => g.Name.StartsWith(criteria.Name.ToUpper()));
                if (!string.IsNullOrWhiteSpace(criteria.GroupId))
                    groupData = groupData.Where(g => g.GroupId.StartsWith(criteria.GroupId.ToUpper()));
            
                foreach (var g in groupData)
                    Add(DataPortal.FetchChild<GroupEdit>(g, criteria.ClientId));

            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
        #endregion
    }
}
