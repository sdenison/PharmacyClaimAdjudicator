using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.Interface
{
    public interface IHaveWindowsForType
    {
        Dictionary<string, ModernWindow> GetWindowsForType(Type type);
        void RemoveWindow(Type type, string id);
    }
}
