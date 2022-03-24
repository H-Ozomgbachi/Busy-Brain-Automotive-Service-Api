using MediatR;
using System;

namespace Common.Core.CQRS.Commands.Organisation
{
    public class CreateOrganisationCommand : IRequest<int>
    {        
        public Guid UniqueID { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ReportEmail { get; set; }
        public string Phone { get; set; }
        public Guid CreatedBy { get; set; }
        public string AccountType { get; set; }
    }
}
