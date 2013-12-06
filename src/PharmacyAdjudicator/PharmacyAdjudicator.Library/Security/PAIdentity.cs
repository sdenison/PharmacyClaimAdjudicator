using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static async Task<PAIdentity> GetPAIdentityAsync(string username, string password)
        {
            return await DataPortal.FetchAsync<PAIdentity>(new UsernameCriteria(username, password));
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

        private void DataPortal_Fetch(UsernameCriteria criteria)
        {
            var username = criteria.Username.ToLower();
            var password = criteria.Password.ToLower();
            try
            {
                base.IsAuthenticated = false;
                //Faking logins until AspNet.Identity is finished or good identity management system is found.
                if (username.Equals("sam") || username.Equals("manager") || username.Equals("admin"))
                {
                    if (password.Equals("password"))
                    {
                        base.Name = username;
                        base.IsAuthenticated = true;
                        base.AuthenticationType = "Custom";

                        base.Roles = new Csla.Core.MobileList<string>();

                        switch (username)
                        {
                            case "sam":
                                base.Roles.Add("User");
                                break;
                            case "manager":
                                base.Roles.Add("Admin");
                                base.Roles.Add("Manager");
                                break;
                            case "admin":
                                base.Roles.Add("Admin");
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (base.IsAuthenticated == false)
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
