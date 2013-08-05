using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PharmacyAdjudicator.DalMock.MockDbTypes;

namespace PharmacyAdjudicator.DalMock
{
    public class MockDb
    {
        public static List<PatientData> Patients { get; private set; }
        public static List<DrugData> Drugs { get; private set; }

        static MockDb()
        {
            Patients = GeneratePatientMock();
            Drugs = GenerateDrugMock();
        }

        public static void Refresh()
        {
            Patients = GeneratePatientMock();
            Drugs = GenerateDrugMock();
        }

        private static List<DrugData> GenerateDrugMock()
        {
            List<DrugData> Drugs = new List<DrugData>();
            Drugs.Add(new DrugData() { Ndc = "000904107060", BrandName = "METOCLOPRAMIDE HCL 10MG TAB", Upn = "", VaClass = "AU300" });
            Drugs.Add(new DrugData() { Ndc = "000004904701", BrandName = "MENADIOL NA DIPHOSPHATE 10MG/ML INJ", Upn = "", VaClass = "VT701" });
            Drugs.Add(new DrugData() { Ndc = "999999981500", BrandName = "BRIEF,FITTED LARGE EXTRA ABSORBENT", Upn = "", VaClass = "XA399" });
            Drugs.Add(new DrugData() { Ndc = "000003125278", BrandName = "WAFER,DURAHESIVE W/CONVEX-IT C#1252-78", Upn = "125278", VaClass = "XA604" });
            Drugs.Add(new DrugData() { Ndc = "000003401916", BrandName = "IRRIGATION SET,VISI-FLOW C#4019-16", Upn = "401916", VaClass = "XA607" }); 
            return Drugs;
        }

        private static List<PatientData> GeneratePatientMock()
        {
            return new List<PatientData>
            {
                new PatientData { PatientId = 20, FirstName = "Eric", LastName = "Bloom", DateOfBirth = new DateTime(1944, 12, 01), Gender = "0", CardholderId = "111222333", LastChangedDateTime = GetTimeStamp() },
                new PatientData { PatientId = 21, FirstName = "Richard", LastName = "Hell", DateOfBirth = new DateTime(1949, 10, 02), Gender = "1", CardholderId = "222333444555", LastChangedDateTime = GetTimeStamp() },
                new PatientData { PatientId = 22, FirstName = "Joe", LastName = "Strummer", DateOfBirth = new DateTime(1952, 08, 21), Gender = "", CardholderId = "222333444", LastChangedDateTime = GetTimeStamp() },
                new PatientData { PatientId = 55, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1954, 02, 25), Gender = "1", CardholderId = "333444555b", LastChangedDateTime = GetTimeStamp() },
                new PatientData { PatientId = 1, FirstName = "Bobby", LastName = "Bob", DateOfBirth = new DateTime(1975, 02, 19), Gender = "1", CardholderId = "123456789", LastChangedDateTime = GetTimeStamp() },

                //Example from guide
                new PatientData { PatientId = 60, FirstName = "Joseph", LastName = "Smith", DateOfBirth = new DateTime(1962, 06, 15), Gender = "1", CardholderId = "987654321", LastChangedDateTime = GetTimeStamp() },
                //Dad
                new PatientData { PatientId = 61, FirstName = "Joseph", LastName = "Smith", DateOfBirth = new DateTime(1932, 09, 15), Gender = "1", CardholderId = "987654321", LastChangedDateTime = GetTimeStamp() },
                //Twin Sister
                new PatientData { PatientId = 62, FirstName = "Bertha", LastName = "Smith", DateOfBirth = new DateTime(1962, 06, 15), Gender = "2", CardholderId = "987654321", LastChangedDateTime = GetTimeStamp() },
            };
        }

        public static DateTime GetTimeStamp()
        {
            return DateTime.Now;
        }

    }
}
