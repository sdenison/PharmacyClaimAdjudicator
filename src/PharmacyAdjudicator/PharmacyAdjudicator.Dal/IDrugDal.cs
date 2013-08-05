using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyAdjudicator.Dal
{
    public interface IDrugDal
    {
        List<DrugDto> FetchAll();
        List<DrugDto> FetchByBrandName(string brandName);
        DrugDto FetchByNdc(string ndc);
        bool Exists(string ndc);
        void Insert(DrugDto item);
        void Update(DrugDto item);
        void Delete(string ndc);
    }
}
