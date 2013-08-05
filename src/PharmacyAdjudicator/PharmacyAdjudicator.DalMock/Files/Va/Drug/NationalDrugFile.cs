using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PharmacyAdjudicator.DalMock.Files.Va.Drug
{
    public class NationalDrugFile : List<NationalDrugFileItem>
    {
        public NationalDrugFile(StreamReader sr, char delimiter)
        {
            var lineNumber = 1;
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                //Skip the first line because it is a header
                if (lineNumber > 1)
                {
                    this.Add(new NationalDrugFileItem(line, delimiter));
                }
                lineNumber++;
            }
        }
        
    }
}
