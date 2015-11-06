using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringReaderCatalog.Trial
{
	internal class StringReaderTrial
	{
		public static void Read(string inputPath, string outputPath, int selector)
		{
			try
			{
				var imageString = StringReader.ReadFile(inputPath, selector);

				var imageBytes = Convert.FromBase64String(imageString);

				using (var ms = new MemoryStream(imageBytes))
				{
					var image = Image.FromStream(ms, true);
					image.Save(outputPath, ImageFormat.Png);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}