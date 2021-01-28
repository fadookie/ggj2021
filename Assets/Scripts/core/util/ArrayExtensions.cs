using System.Collections.Generic;
using System.Text;

namespace com.eliotlash.core.util {
	static class ArrayExtensions
	{
		public static string toString<T>(this IEnumerable<T> elements)
		{
			StringBuilder sb = new StringBuilder("[");
			foreach (var element in elements)
			{
				sb.AppendFormat("{0},", element);
			}
			sb.Remove(sb.Length - 1, 1);
			sb.Append("]");
			return sb.ToString();
		}
	}
}
