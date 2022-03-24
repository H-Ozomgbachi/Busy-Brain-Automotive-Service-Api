using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Contracts.v1.Organisation
{
    public class OrganisationPayload
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ReportEmail { get; set; }
        public string Phone { get; set; }
        public string AccountType { get; set; }
    }
}
