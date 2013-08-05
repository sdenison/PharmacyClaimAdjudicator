using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyAdjudicator.Dal;

namespace PharmacyAdjudicator.DalMock
{
    public class PatientDal : IPatientDal
    {
        public List<PatientDto> FetchAll()
        {
            var result = from p in MockDb.Patients
                         select new PatientDto
                         {
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             DateOfBirth = p.DateOfBirth,
                             PatientId = p.PatientId,
                             CardholderId = p.CardholderId,
                             Gender = p.Gender,
                             LastChangedDateTime = p.LastChangedDateTime
                         };
            return result.ToList();
        }

        public List<PatientDto> FetchByLastName(string nameFilter)
        {
            var result = from p in MockDb.Patients
                         where p.LastName.Contains(nameFilter)
                         select new PatientDto
                         {
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             DateOfBirth = p.DateOfBirth,
                             PatientId = p.PatientId,
                             CardholderId = p.CardholderId,
                             Gender = p.Gender,
                             LastChangedDateTime = p.LastChangedDateTime
                         };
            return result.ToList();
        }

        public PatientDto Fetch(long patientId)
        {
            var result = (from p in MockDb.Patients
                          where p.PatientId == patientId
                          select new PatientDto
                          {
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             DateOfBirth = p.DateOfBirth,
                             PatientId = p.PatientId,
                             CardholderId = p.CardholderId,
                             Gender = p.Gender,
                             LastChangedDateTime = p.LastChangedDateTime
                          }).FirstOrDefault();
            if (result == null)
                throw new DataNotFoundException("Patient = " + patientId);
            return result;
        }

        public bool Exists(long patientId)
        {
            var result = (from p in MockDb.Patients
                          where p.PatientId == patientId
                          select p.PatientId).Count() > 0;
            return result;
        }

        public void Insert(PatientDto item)
        {
            item.PatientId = MockDb.Patients.Max(p => p.PatientId) + 1;
            item.LastChangedDateTime = MockDb.GetTimeStamp();
            var newItem = new MockDbTypes.PatientData
            {
                PatientId = item.PatientId,
                FirstName = item.FirstName, 
                LastName = item.LastName,
                DateOfBirth = item.DateOfBirth,
                CardholderId = item.CardholderId,
                Gender = item.Gender,
                LastChangedDateTime = item.LastChangedDateTime
            };
            MockDb.Patients.Add(newItem);
        }

        public void Update(PatientDto item)
        {
            var data = (from p in MockDb.Patients
                        where p.PatientId == item.PatientId
                        select p).FirstOrDefault();
            if (data == null)
                throw new DataNotFoundException("PatientId = " + item.ToString());
            if (!data.LastChangedDateTime.Equals(item.LastChangedDateTime))
                throw new ConcurrencyException("PatientId = " + item.ToString());

            item.LastChangedDateTime = MockDb.GetTimeStamp();

            data.FirstName = item.FirstName;
            data.LastName = item.LastName;
            data.DateOfBirth = item.DateOfBirth;
            data.Gender = item.Gender;
            data.CardholderId = item.CardholderId;
        }

        public void Delete(long patientId)
        {
            var data = (from p in MockDb.Patients
                        where p.PatientId == patientId
                        select p).FirstOrDefault();
            if (data != null)
                MockDb.Patients.Remove(data);
        }


        public List<PatientDto> FetchByInsurance(string processorControlNumber, string cardholderId)
        {
            throw new NotImplementedException();
        }
    }
}
