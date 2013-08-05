using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace PharmacyAdjudicator.Library
{
    public class IncomingTransmissionException : Exception 
    {
        public IncomingTransmissionException(string message)
            : base(message)
        {
        }

        public IncomingTransmissionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IncomingTransmissionException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
