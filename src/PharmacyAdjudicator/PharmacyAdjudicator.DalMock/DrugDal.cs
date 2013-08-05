using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmacyAdjudicator.Dal;

namespace PharmacyAdjudicator.DalMock
{
    public class DrugDal : IDrugDal
    {
        public List<DrugDto> FetchAll()
        {
            var result = from d in MockDb.Drugs
                         select new DrugDto
                         {
                             Ndc = d.Ndc,
                             BrandName = d.BrandName,
                             Upn = d.Upn,
                             VaClass = d.VaClass,
                             PkgType = d.PkgType
                         };
            return result.ToList();
        }

        public List<DrugDto> FetchByBrandName(string brandName)
        {
            var result = from d in MockDb.Drugs
                         where d.BrandName.StartsWith(brandName)
                         select new DrugDto
                         {
                             Ndc = d.Ndc,
                             BrandName = d.BrandName,
                             Upn = d.Upn,
                             VaClass = d.VaClass,
                             PkgType = d.PkgType 
                         };
            return result.ToList();
        }

        public DrugDto FetchByNdc(string ndc)
        {
            var result = (from d in MockDb.Drugs
                         where d.Ndc.Equals(ndc)
                         select new DrugDto
                         {
                             Ndc = d.Ndc,
                             BrandName = d.BrandName,
                             Upn = d.Upn,
                             VaClass = d.VaClass,
                             PkgType = d.PkgType 
                         }).FirstOrDefault();
            if (result == null)
                throw new DataNotFoundException("Ndc = " + ndc);
            return result;
            
        }

        public bool Exists(string ndc)
        {
            var result = (from d in MockDb.Drugs
                          where d.Ndc == ndc
                          select d.Ndc).Count() > 0;
            return result;
        }

        public void Insert(DrugDto item)
        {
            var newDrug = new MockDbTypes.DrugData
            {
                Ndc = item.Ndc,
                BrandName = item.BrandName,
                Upn = item.Upn,
                VaClass = item.VaClass,
                PkgType = item.PkgType
            };
            MockDb.Drugs.Add(newDrug);
        }

        public void Update(DrugDto item)
        {
            var data = (from d in MockDb.Drugs
                        where d.Ndc == item.Ndc
                        select d).FirstOrDefault();
            if (data == null)
                throw new DataNotFoundException("Ndc = " + item.Ndc);
            data.BrandName = item.BrandName;
            data.Upn = item.Upn;
            data.VaClass = item.VaClass;
            data.PkgType = item.PkgType;
        }

        public void Delete(string ndc)
        {
            var data = (from d in MockDb.Drugs
                        where d.Ndc == ndc
                        select d).FirstOrDefault();
            if (data != null)
                MockDb.Drugs.Remove(data);
        }
    }
}
