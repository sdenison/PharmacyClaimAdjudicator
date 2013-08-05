using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Serialization;
using Csla.Security;
using System.Web.Security;

namespace PharmacyAdjudicator.Library.Security
{
    [Serializable]
    public class PAIdentity : CslaIdentityBase<PAIdentity>
    {
        public static void GetPAIdentity(string username, string password, EventHandler<DataPortalResult<PAIdentity>> callback)
        {
            DataPortal.BeginFetch<PAIdentity>(new UsernameCriteria(username, password), callback);
        }

#if !SILVERLIGHT && !NETFX_CORE && !WINDOWS_PHONE
        public static PAIdentity GetPAIdentity(string username, string password)
        {
            return DataPortal.Fetch<PAIdentity>(new UsernameCriteria(username, password));
        }

        internal static PAIdentity GetPAIdentity(string username)
        {
            return DataPortal.Fetch<PAIdentity>(username);
        }

        private void DataPortal_Fetch(string username, string password)
        {
            try
            {
                if (Membership.ValidateUser(username, password))
                {
                    var user = Membership.GetUser(username);
                    base.Name = username;
                    base.IsAuthenticated = true;
                    base.AuthenticationType = "Membership";
                    //Just testing
                    //TODO: Add real user management here
                    base.Roles = new Csla.Core.MobileList<string>();
                    base.Roles.Add("Manager");
                }
                else
                {
                    base.Name = string.Empty;
                    base.IsAuthenticated = false;
                    base.AuthenticationType = string.Empty;
                    base.Roles = new Csla.Core.MobileList<string>();
                }
            }
            catch (Exception ex)
            {
                throw new DataNotFoundException("username = " + username, ex);
            }
        }
#endif
    }
}
