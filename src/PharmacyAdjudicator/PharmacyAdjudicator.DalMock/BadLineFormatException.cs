using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

namespace PharmacyAdjudicator.DalMock
{
    [Serializable]
    public class BadLineFormatException : Exception
    {
        public BadLineFormatException(string message)
            : base(message)
        { }

        public BadLineFormatException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected BadLineFormatException(
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
