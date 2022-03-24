using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Contracts.v1.Account
{
    public class ResetPasswordModel
    {
        public Guid ResetToken { get; set; }
        public Guid UserGuid { get; set; }
        public string ExpiryDate { get; set; }
        public string NewPassword { get; set; }
    }
}
