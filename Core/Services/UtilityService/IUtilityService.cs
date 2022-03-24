using System.Collections.Generic;

namespace Common.Core.Services
{
    public interface IUtilityService
    {
        string FormatString(string rawString);
        string MergeIntListToString(IEnumerable<int> values);
    }
}
