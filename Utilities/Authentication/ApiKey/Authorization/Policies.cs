using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api.Configuration.Authorization
{
    public static class Policies
    {
        public const string OnlyCustomers = nameof(OnlyCustomers);
        public const string OnlyAdmin = nameof(OnlyAdmin);
    }
}
