using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows;

namespace PharmacyAdjudicator.ModernUI.Services
{
    public class DialogService : Interface.IDialog
    {
        public MessageBoxResult ShowMessage(string message, string title, System.Windows.MessageBoxButton btn)
        {
            return FirstFloor.ModernUI.Windows.Controls.ModernDialog.ShowMessage(message, title, btn); //"Could not login in.  Bad username/password combination.", "Login failed", btn);
        }
    }
}
