using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Dal
{
    public class PatientDto
    {
        public DateTime? DateOfBirth { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public long PatientId { get; set; }
        public string Gender { get; set; }
        public string CardholderId { get; set; }
        public DateTime LastChangedDateTime { get; set; }
    }
}
