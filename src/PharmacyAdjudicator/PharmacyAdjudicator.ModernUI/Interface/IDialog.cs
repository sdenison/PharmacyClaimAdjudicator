using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Composition;

namespace PharmacyAdjudicator.ModernUI.Interface
{
    [InheritedExport]
    public interface IDialog
    {
        MessageBoxResult ShowMessage(string message, string title, MessageBoxButton btn);
    }
}
