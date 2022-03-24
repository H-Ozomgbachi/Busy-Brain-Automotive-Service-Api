using System;
using System.Collections.Generic;

namespace Common.Security.Encryption.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class SensitiveDataDictionaryAttribute : Attribute
    {
        public readonly IReadOnlyList<object> DictionaryKeys;
        public SensitiveDataDictionaryAttribute(params object[] dictionaryKeys)
        {
            DictionaryKeys = dictionaryKeys;
        }
    }
}
