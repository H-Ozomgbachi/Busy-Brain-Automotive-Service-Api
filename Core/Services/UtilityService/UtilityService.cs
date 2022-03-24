using Common.Contracts.Exceptions.Types;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IConfiguration _config;

        public UtilityService(IConfiguration config)
        {
            _config = config;
        }

        public string FormatString(string rawString)
        {
            char[] letters = rawString.ToCharArray();
             
            List<string> final = new List<string>();

            for (int i = 0; i < letters.Length; i++)
            {
                var refined = letters[i].ToString().Trim().ToLower();
                if (i == 0)
                {
                    final.Add(refined.ToUpper());
                }
                else
                {
                    final.Add(refined);
                }
            }
            return string.Join(string.Empty, final);
        }

        public string MergeIntListToString(IEnumerable<int> values)
        {
            return string.Join(',', values);
        }
    }
}
