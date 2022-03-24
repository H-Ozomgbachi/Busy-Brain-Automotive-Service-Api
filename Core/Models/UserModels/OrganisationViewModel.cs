using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Models
{
    public class OrganisationViewModel
    {
        public int ID { get; set; }
        public Guid UniqueID { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ReportEmail { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public string AccountType { get; set; }
    }
}
