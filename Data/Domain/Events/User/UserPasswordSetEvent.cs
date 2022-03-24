using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.Domain.Events
{
    public class UserPasswordSetEvent : IEvent
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public UserPasswordSetEvent(byte[] passwordHash, byte[] passwordSalt)
        {
            this.PasswordSalt = passwordSalt;
            this.PasswordHash = passwordHash;
        }
    }
}
