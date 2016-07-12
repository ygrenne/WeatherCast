using System.Collections.Generic;
using System.Linq;

namespace Weather.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this IDictionary<string, string> dictionary)
        {
            return dictionary.Aggregate("", (current, dict) => current + $"{dict.Key}={dict.Value}&").TrimEnd('&');
        }
    }
}