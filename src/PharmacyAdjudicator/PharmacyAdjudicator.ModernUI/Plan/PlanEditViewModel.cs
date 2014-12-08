using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;
using PharmacyAdjudicator.Library.Core.Plan;
using PharmacyAdjudicator.Library.Core.Rules;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class PlanEditViewModel : ScreenWithModel<PlanEdit> // Screen 
    {
        //public Library.Core.Plan.PlanEdit Model { get; set; }
        public PlanEditViewModel(Library.Core.Plan.PlanEdit model)
        {
            this.Model = model;
        }

        private Library.Core.Rules.Rule _selectedRule;
        public Library.Core.Rules.Rule SelectedRule
        {
            get
            {
                return _selectedRule;
            }
            set
            {
                _selectedRule = value;
                NotifyOfPropertyChange(() => SelectedRule);
            }
        }

        public void AddAtomGroup(AtomGroup atomGroup)
        {
            //Determine if the new AtomGroup should have a logical operator of And or Or.
            NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator logicalOperator;
            if (atomGroup.LogicalOperator == NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And)
                logicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.Or;
            else
                logicalOperator = NxBRE.InferenceEngine.Rules.AtomGroup.LogicalOperator.And;

            //Add AtomGroup to AtomGroup passed in as parameter
            //var atomGroupToAdd = AtomGroup.NewAtomGroup();
            //atomGroupToAdd.LogicalOperator = logicalOperator;
            //atomGroup.AddPredicate(atomGroupToAdd);


            atomGroup.AddAtomGroup(logicalOperator);
            NotifyOfPropertyChange(() => Model);
        }

        public void AddAtom(AtomGroup atomGroup)
        {
            //atomGroup.AddPredicate(Atom.NewAtom());
            atomGroup.AddAtom();

            var x = Model.AssignedRules[4].IsDirty;

            //_selectedRule.Implications[0].Body.AddPredicate(Atom.NewAtom());
            //Model.Implications[0].AtomGroup.Members[0]
            NotifyOfPropertyChange(() => Model);
        }
    }
}
