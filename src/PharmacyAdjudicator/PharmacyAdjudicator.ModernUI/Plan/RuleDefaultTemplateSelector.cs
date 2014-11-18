﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyAdjudicator.ModernUI.Plan
{
    public class RuleDefaultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BooleanTemplate { get; set; }
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate EnumTemplate { get; set; }
        public DataTemplate MoneyTemplate { get; set; }
        public DataTemplate BasisOfReimbursementTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Library.Core.Rules.Rule)
            {
                var ruleItem = (Library.Core.Rules.Rule)item;
                if (ruleItem.ClrType().Equals(typeof(Boolean)))
                    return BooleanTemplate;
                if (ruleItem.ClrType().Equals(typeof(string)))
                    return StringTemplate;
                if (ruleItem.ClrType().Equals(typeof(decimal)))
                    return MoneyTemplate;
                if (ruleItem.ClrType().Equals(typeof(Library.Core.Enums.BasisOfReimbursement)))
                    return BasisOfReimbursementTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
