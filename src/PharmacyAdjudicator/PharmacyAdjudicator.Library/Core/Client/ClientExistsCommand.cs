using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Client
{
    [Serializable]
    public class ClientExistsCommand : CommandBase<ClientExistsCommand>
    {
        #region Factory Methods

        public static bool Execute(string clientId)
        {
            ClientExistsCommand cmd = new ClientExistsCommand() { ClientId = clientId };
            cmd = DataPortal.Execute<ClientExistsCommand>(cmd);
            return cmd.Result;
        }

        //private ClientExistsCommand(string clientId)
        //{
        //    this.ClientId = clientId;
        //}

        private ClientExistsCommand()
        { /* require use of factory methods */ }

        #endregion

        #region Client-side Code

        public static readonly PropertyInfo<string> ClientIdProperty = RegisterProperty<string>(c => c.ClientId);
        public string ClientId
        {
            get { return ReadProperty(ClientIdProperty); }
            set { LoadProperty(ClientIdProperty, value); }
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(p => p.Result);
        public bool Result
        {
            get { return ReadProperty(ResultProperty); }
            set { LoadProperty(ResultProperty, value); }
        }

        #endregion

        #region Server-side Code

        protected override void DataPortal_Execute()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                this.Result = ctx.DbContext.ClientDetail.Any(c => c.ClientId == this.ClientId);
            }
        }

        #endregion
    }
}
