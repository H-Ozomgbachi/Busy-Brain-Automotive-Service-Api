using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserOrganisationSetEvent : IEvent
    {
        public int? OrganisationId { get; set; }
    }
}
