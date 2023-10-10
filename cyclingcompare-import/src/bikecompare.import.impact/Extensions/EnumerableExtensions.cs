using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Extensions
{
    public static class EnumerableExtensions
    {		
		public static Dictionary<string, string> ToSafeDictionary<T>(this IEnumerable<T> items, Func<T, string> keyselector, Func<T, string> elemSelector)
		{
			var result = new Dictionary<string, string>();

			foreach (var element in items)
			{
				var key = keyselector(element);
				while (result.ContainsKey(key))
				{
					key = $" {key}";
				}
				result.Add(key, elemSelector(element));
			}
			return result;
		}
		
	}
}
