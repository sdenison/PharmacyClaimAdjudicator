using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CslaContrib.Caliburn.Micro;
using PharmacyAdjudicator.Library.Core.Plan;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class PlanListItemViewModel : ScreenWithModel<PlanEdit>
    {
        public PlanListItemViewModel(PlanEdit plan)
        {
            Model = plan;
        }
    }
}
