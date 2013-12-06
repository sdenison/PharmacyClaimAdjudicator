using System;
using System.Security.Principal;
using Csla.Security;
using Csla.Serialization;

namespace PharmacyAdjudicator.Library.Security
{
    [Serializable]
    public class PAPrincipal : CslaPrincipal
    {
        public PAPrincipal()
        {
        }

        protected PAPrincipal(IIdentity identity)
            : base(identity)
        {
        }

        public static async System.Threading.Tasks.Task LoginAsync(string username, string password)
        {
            try
            {
                var identity = await PAIdentity.GetPAIdentityAsync(username, password);
                SetPrincipal(identity);
            }
            catch
            {
                Logout();
            }
        }

#if !SILVERLIGHT && !NETFX_CORE
        public static bool Login(string username, string password)
        {
            var identity = PAIdentity.GetPAIdentity(username, password);
            return SetPrincipal(identity);
        }

        public static bool Load(string username)
        {
            var identity = PAIdentity.GetPAIdentity(username);
            return SetPrincipal(identity);
        }
#endif

        public static bool SetPrincipal(IIdentity identity)
        {
            if (identity.IsAuthenticated)
            {
                PAPrincipal principal = new PAPrincipal(identity);
                Csla.ApplicationContext.User = principal;
            }
            else
            {
                Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
            }
            OnNewUser();
            return identity.IsAuthenticated;
        }

        public static void Logout()
        {
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
            OnNewUser();
        }

        public static event Action NewUser;
        private static void OnNewUser()
        {
            if (NewUser != null)
                NewUser();
        }

    }
}
