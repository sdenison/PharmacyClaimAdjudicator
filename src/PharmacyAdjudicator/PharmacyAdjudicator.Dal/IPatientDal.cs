using System;
using System.Collections.Generic;

namespace PharmacyAdjudicator.Dal
{
    public interface IPatientDal
    {
        List<PatientDto> FetchAll();
        List<PatientDto> FetchByLastName(string nameFilter);
        List<PatientDto> FetchByInsurance(string processorControlNumber, string cardholderId);
        PatientDto Fetch(long patientId);
        bool Exists(long patientId);
        void Insert(PatientDto item);
        void Update(PatientDto item);
        void Delete(long patientId);
    }
}
