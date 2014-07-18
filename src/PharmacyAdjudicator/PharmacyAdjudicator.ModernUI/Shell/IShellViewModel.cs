using System;
using System.ComponentModel.Composition;

namespace PharmacyAdjudicator.ModernUI.Shell
{
    [InheritedExport]
    public interface IShellViewModel
    {
        bool IsBusy { get; set; }
    }
}
