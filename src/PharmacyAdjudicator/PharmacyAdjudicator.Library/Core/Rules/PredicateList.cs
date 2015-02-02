using System;
using System.Collections.Generic;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    [Serializable]
    public class PredicateList : BusinessListBase<PredicateList, IPredicate>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(PredicateList), "Role");
        }

        #endregion

        #region Factory Methods

        public static PredicateList NewPredicateList(AtomGroup parent)
        {
            return DataPortal.Create<PredicateList>(parent);
        }

        private PredicateList()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access
        
        private void Child_Fetch(AtomGroup parent)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var predicateDataList =  from ag in ctx.DbContext.AtomGroupItem
                                         where ag.AtomGroupId == parent.AtomGroupId
                                         && ag.Retraction == false
                                         && !ctx.DbContext.AtomGroupItem.Any(agi2 => agi2.Retraction == true && agi2.OriginalFactRecordId == ag.RecordId)
                                         orderby ag.RecordId
                                         select ag;
                foreach (var predicateData in predicateDataList)
                {
                    if (predicateData.AtomId != null)
                    {
                        this.Add(DataPortal.FetchChild<Atom>(predicateData.AtomId));
                    }
                    else
                    {
                        this.Add(DataPortal.FetchChild<AtomGroup>(predicateData.ContainedAtomGroupId));  
                    }
                }
            }
            RaiseListChangedEvents = rlce;
        }

        protected override void DataPortal_Update()
        {
            foreach (var item in this)
            {
                item.Save();
            }
        }

        protected void Child_Update(AtomGroup parent)
        {
            //Takes care of saving AtomGroups and Atoms
            //Takes care of the record in AtomGroupItem
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var atomGroupItems = (from agi in ctx.DbContext.AtomGroupItem
                                      where agi.AtomGroupId == parent.AtomGroupId
                                      && agi.Retraction == false
                                      && !ctx.DbContext.AtomGroupItem.Any(agi2 => agi2.Retraction == true && agi2.OriginalFactRecordId == agi.RecordId)
                                      select agi).ToList();
                foreach (var item in this.DeletedList)
                {
                    var atom = item as Atom;
                    if (atom != null)
                    {
                        var atomData = atomGroupItems.FirstOrDefault(agi => agi.AtomId == atom.AtomId);
                        if (atomData != null)
                        {
                            var atomGroupItemRetraction = new DataAccess.AtomGroupItem();
                            atomGroupItemRetraction.OriginalFactRecordId = atomData.RecordId;
                            atomGroupItemRetraction.Retraction = true;
                            atomGroupItemRetraction.RecordId = Utils.GuidHelper.GenerateComb();
                            atomGroupItemRetraction.AtomGroupId = parent.AtomGroupId;
                            atomGroupItemRetraction.AtomId = atom.AtomId;
                            atomGroupItemRetraction.Priority = 0;
                            atomGroupItemRetraction.RecordCreatedDateTime = DateTime.Now;
                            atomGroupItemRetraction.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                            ctx.DbContext.AtomGroupItem.Add(atomGroupItemRetraction);
                        }
                    }

                    var atomGroup = item as AtomGroup;
                    if (atomGroup != null)
                    {
                        var atomData = atomGroupItems.FirstOrDefault(agi => agi.ContainedAtomGroupId == atomGroup.AtomGroupId);
                        if (atomData != null)
                        { 
                            var atomGroupItemRetraction = new DataAccess.AtomGroupItem();
                            atomGroupItemRetraction.OriginalFactRecordId = atomData.RecordId;
                            atomGroupItemRetraction.Retraction = true;
                            atomGroupItemRetraction.RecordId = Utils.GuidHelper.GenerateComb();
                            atomGroupItemRetraction.AtomGroupId = atomData.AtomGroupId;
                            atomGroupItemRetraction.ContainedAtomGroupId = atomGroup.AtomGroupId;
                            atomGroupItemRetraction.Priority = 0;
                            atomGroupItemRetraction.RecordCreatedDateTime = DateTime.Now;
                            atomGroupItemRetraction.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                            ctx.DbContext.AtomGroupItem.Add(atomGroupItemRetraction);
                        }
                    }
                }
                base.Child_Update();
                DeletedList.Clear();
                foreach (var item in this)
                {
                    var atom = item as Atom;
                    if (atom != null)
                    {
                        var atomData = atomGroupItems.FirstOrDefault(agi => agi.AtomId == atom.AtomId);
                        if (atomData == null)
                        {
                            atomData = new DataAccess.AtomGroupItem();
                            atomData.RecordId = Utils.GuidHelper.GenerateComb();
                            atomData.AtomGroupId = parent.AtomGroupId;
                            atomData.AtomId = atom.AtomId;
                            atomData.Priority = 0;
                            atomData.Retraction = false;
                            atomData.RecordCreatedDateTime = DateTime.Now;
                            atomData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                            ctx.DbContext.AtomGroupItem.Add(atomData);
                        }
                    }
                    var atomGroup = item as AtomGroup;
                    if (atomGroup != null)
                    {
                        var atomData = atomGroupItems.FirstOrDefault(agi => agi.ContainedAtomGroupId == atomGroup.AtomGroupId);
                        if (atomData == null)
                        {
                            atomData = new DataAccess.AtomGroupItem();
                            atomData.RecordId = Utils.GuidHelper.GenerateComb();
                            atomData.AtomGroupId = parent.AtomGroupId;
                            atomData.ContainedAtomGroupId = atomGroup.AtomGroupId;
                            atomData.Priority = 0;
                            atomData.Retraction = false;
                            atomData.RecordCreatedDateTime = DateTime.Now;
                            atomData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                            ctx.DbContext.AtomGroupItem.Add(atomData);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
