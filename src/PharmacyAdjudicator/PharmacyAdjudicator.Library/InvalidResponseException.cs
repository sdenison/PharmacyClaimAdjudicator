using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException(string message)
            : base(message)
        { }

        public InvalidResponseException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected InvalidResponseException(
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
