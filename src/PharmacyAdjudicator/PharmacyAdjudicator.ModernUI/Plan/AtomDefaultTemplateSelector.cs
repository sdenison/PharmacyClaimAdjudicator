using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class AtomDefaultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AtomBooleanTemplate { get; set; }
        public DataTemplate AtomStringTemplate { get; set; }
        public DataTemplate EnumTemplate { get; set; }
        public DataTemplate AtomMoneyTemplate { get; set; }
        public DataTemplate BasisOfReimbursementTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Library.Core.Rules.Atom)
            {
                var atomItem = (Library.Core.Rules.Atom)item;
                if (atomItem.ClrType.Equals(typeof(Boolean)))
                    return AtomBooleanTemplate;
                if (atomItem.ClrType.Equals(typeof(string)))
                    return AtomStringTemplate;
                if (atomItem.ClrType.Equals(typeof(decimal)))
                    return AtomMoneyTemplate;
                if (atomItem.ClrType.Equals(typeof(Library.Core.Enums.BasisOfReimbursement)))
                    return BasisOfReimbursementTemplate;
            }
            return base.SelectTemplate(item, container);

            //if (item is Library.Core.Rules.Rule)
            //{
            //    var ruleItem = (Library.Core.Rules.Rule)item;
            //    if (ruleItem.ClrType().Equals(typeof(Boolean)))
            //        return BooleanTemplate;
            //    if (ruleItem.ClrType().Equals(typeof(string)))
            //        return StringTemplate;
            //    if (ruleItem.ClrType().Equals(typeof(decimal)))
            //        return MoneyTemplate;
            //    if (ruleItem.ClrType().Equals(typeof(Library.Core.Enums.BasisOfReimbursement)))
            //        return BasisOfReimbursementTemplate;
            //}
            //return base.SelectTemplate(item, container);
        }
    }
}
