using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Group
{
    [Serializable]
    public class GroupSearchCriteria : CriteriaBase<GroupSearchCriteria>
    {
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return ReadProperty(NameProperty); }
            set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
        public string GroupId
        {
            get { return ReadProperty(GroupIdProperty); }
            set { LoadProperty(GroupIdProperty, value); }
        }

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        public string ClientId
        {
            get { return ReadProperty(ClientIdProperty); }
            set { LoadProperty(ClientIdProperty, value); }
        }
    }
}
