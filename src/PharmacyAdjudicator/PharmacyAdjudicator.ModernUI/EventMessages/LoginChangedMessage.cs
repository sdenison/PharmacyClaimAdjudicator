using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.EventMessages
{
    public class LoginChangedMessage
    {
        public string Message { get; set; }
        public LoginChangedMessage(string message)
        {
            this.Message = message;
        }
    }
}
