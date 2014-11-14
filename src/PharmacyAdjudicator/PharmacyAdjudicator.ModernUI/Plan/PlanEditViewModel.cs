using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class PlanEditViewModel : Screen 
    {
        public Library.Core.Plan.PlanEdit Model { get; set; }
        public PlanEditViewModel(Library.Core.Plan.PlanEdit model)
        {
            this.Model = model;
        }
    }
}
