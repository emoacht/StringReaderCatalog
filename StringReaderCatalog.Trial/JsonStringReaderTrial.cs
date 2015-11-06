using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringReaderCatalog.Test;

namespace StringReaderCatalog.Trial
{
	internal class JsonStringReaderTrial
	{
		public static void Load(string filePath, bool canAcceptBom)
		{
			try
			{
				var result = JsonStringReaderTest.Load(filePath, canAcceptBom);
				Console.WriteLine(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public static async Task LoadAsync(string filePath, bool canAcceptBom)
		{
			try
			{
				var result = await JsonStringReaderTest.LoadAsync(filePath, canAcceptBom);
				Console.WriteLine(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public static async Task SaveAsync(string filePath, bool canEmitBom, int selector)
		{
			try
			{
				await JsonStringReaderTest.SaveAsync(filePath, canEmitBom, selector);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		public static async Task SaveLoadAsync(bool canEmitBom, int selector)
		{
			var filePath = Path.GetTempFileName();

			try
			{
				await JsonStringReaderTest.SaveAsync(filePath, canEmitBom, selector);

				var result = await JsonStringReaderTest.LoadAsync(filePath, true);
				Console.WriteLine(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}