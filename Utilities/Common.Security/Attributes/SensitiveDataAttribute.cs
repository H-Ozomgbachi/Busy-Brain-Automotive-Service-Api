using System;

namespace Common.Security.Encryption.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class SensitiveDataAttribute : Attribute
    {
    }
}
