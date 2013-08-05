using System;
using System.Configuration;

namespace PharmacyAdjudicator.Dal
{
    public static class DalFactory
    {
        private static Type _dalType;

        public static IDalManager GetManager()
        {
            if (_dalType == null)
            {
                var dalTypeName = ConfigurationManager.AppSettings["DalManagerType"];
                if (!string.IsNullOrEmpty(dalTypeName))
                    _dalType = Type.GetType(dalTypeName);
                else
                    //default to the mock so design time can pull sample data
                    _dalType = Type.GetType("PharmacyAdjudicator.DalMock.DalManager,PharmacyAdjudicator.DalMock");
                    //throw new NullReferenceException("DalManagerType1");
                if (_dalType == null)
                    throw new ArgumentException(string.Format("Type {0} could not be found", dalTypeName));
            }
            return (IDalManager)Activator.CreateInstance(_dalType);
        }
    }
}
