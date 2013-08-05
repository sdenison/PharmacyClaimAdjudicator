using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PharmacyAdjudicator.DalMock.Files.Va.Drug
{
    /// <summary>
    /// Represents a line in the NDF file from the VA
    /// </summary>
    public class NationalDrugFileItem
    {
        public string Ndc1 { get; private set; }
        public string Ndc2 { get; private set; }
        public string Ndc3 { get; private set; }
        public string NdfNdc { get; private set; }
        public string Upn { get; private set; }
        public DateTime? IDateNdc { get; private set; }
        public string TradeName { get; private set; }
        public string VaProductName { get; private set; }
        public DateTime? IDateVap { get; private set; }
        public string ProductNu { get; private set; }
        public string FeeDer { get; private set; } //17 digit number
        public string GenericName { get; private set; }
        public decimal PkgSize { get; private set; }
        public string PkgType { get; private set; }
        public string VaClass { get; private set; }
        public string Manufacturer { get; private set; }
        public string StandardMedRoute { get; private set; }
        public string Strength { get; private set; }
        public string Units { get; private set; }
        public string DoseForm { get; private set; }
        public string NfName { get; private set; }
        public string Csfs { get; private set; }
        public string RxOtc { get; private set; }
        public string NfIndicator { get; private set; }
        public string VaPrn { get; private set; }
        public string DispUnit { get; private set; }
        public string Id { get; private set; }
        public string Mark { get; private set; }

        /// <summary>
        /// Set as private to force use of constructor with arguments
        /// </summary>
        private NationalDrugFileItem()
        {
        }

        /// <summary>
        /// Creates a Drug File Item 
        /// </summary>
        /// <param name="rowInFile">Row from the file</param>
        /// <param name="delimiter">Delimiter used in the file</param>
        public NationalDrugFileItem(string rowInFile, char delimiter)
        {
            string[] rowItems = rowInFile.Split(delimiter);
            if (rowItems.Count() == 28)
            {
                this.Ndc1 = rowItems[(int)FieldPosition.Ndc1].Trim();
                this.Ndc2 = rowItems[(int)FieldPosition.Ndc2].Trim();
                this.Ndc3 = rowItems[(int)FieldPosition.Ndc3].Trim();
                this.NdfNdc = rowItems[(int)FieldPosition.NdfNdc].Trim();
                this.Upn = rowItems[(int)FieldPosition.Upn].Trim();
                DateTime readDate;
                if (DateTime.TryParse(rowItems[(int)FieldPosition.IDateNdc].Trim(), out readDate))
                    this.IDateNdc= readDate;
                else
                    this.IDateNdc = null;
                this.TradeName = rowItems[(int)FieldPosition.TradeName].Trim();
                this.VaProductName = rowItems[(int)FieldPosition.VaProductName].Trim();
                if (DateTime.TryParse(rowItems[(int)FieldPosition.IDateVap].Trim(), out readDate))
                    this.IDateVap = readDate;
                else
                    this.IDateVap = null;
                this.ProductNu = rowItems[(int)FieldPosition.ProductNu].Trim();
                this.FeeDer = rowItems[(int)FieldPosition.FeeDer].Trim();
                this.GenericName = rowItems[(int)FieldPosition.Genericname].Trim();
                this.PkgSize = Decimal.Parse(rowItems[(int)FieldPosition.PkgSize].Trim());
                this.VaClass = rowItems[(int)FieldPosition.PkgType].Trim();
                this.Manufacturer = rowItems[(int)FieldPosition.Manufacturer].Trim();
                this.StandardMedRoute = rowItems[(int)FieldPosition.StandardMedRoute].Trim();
                this.Strength = rowItems[(int)FieldPosition.Strength].Trim();
                this.Units = rowItems[(int)FieldPosition.Units].Trim();
                this.DoseForm = rowItems[(int)FieldPosition.DoseForm].Trim();
                this.NfName = rowItems[(int)FieldPosition.NfName].Trim();
                this.Csfs = rowItems[(int)FieldPosition.Csfs].Trim();
                this.RxOtc = rowItems[(int)FieldPosition.RxOtc].Trim();
                this.NfIndicator = rowItems[(int)FieldPosition.NfIndicator].Trim();
                this.VaPrn = rowItems[(int)FieldPosition.VaPrn].Trim();
                this.DispUnit = rowItems[(int)FieldPosition.DispUnit].Trim();
                this.Id = rowItems[(int)FieldPosition.Id].Trim();
                this.Mark = rowItems[(int)FieldPosition.Mark].Trim();
            }
            else
            {
                throw new BadLineFormatException("rowInFile = " + rowInFile);
            }

        }

        /// <summary>
        /// Enum that holds the index for each field
        /// </summary>
        enum FieldPosition
        {
            Ndc1 = 0,
            Ndc2 = 1,
            Ndc3 = 2,
            NdfNdc = 3,
            Upn = 4,
            IDateNdc = 5,
            TradeName = 6,
            VaProductName = 7,
            IDateVap = 8,
            ProductNu = 9,
            FeeDer = 10,
            Genericname = 11,
            PkgSize = 12,
            PkgType = 13,
            VaClass = 14,
            Manufacturer = 15,
            StandardMedRoute = 16,
            Strength = 17,
            Units = 18,
            DoseForm = 19,
            NfName = 20,
            Csfs = 21,
            RxOtc = 22,
            NfIndicator = 23,
            VaPrn = 24,
            DispUnit = 25,
            Id = 26,
            Mark = 27
        }
    }
}
