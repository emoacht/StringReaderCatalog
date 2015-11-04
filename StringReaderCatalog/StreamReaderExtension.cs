using System.Collections.Generic;
using System.IO;

namespace StringReaderCatalog
{
	/// <summary>
	/// Extension method for <see cref="System.IO.StreamReader"/>
	/// </summary>
	public static class StreamReaderExtension
	{
		/// <summary>
		/// Read lines as IEnumerable.
		/// </summary>
		/// <param name="reader">StreamReader</param>
		/// <returns>Content of lines</returns>
		public static IEnumerable<string> EnumerateLines(this StreamReader reader)
		{
			while (!reader.EndOfStream)
			{
				yield return reader.ReadLine();
			}
		}
	}
}