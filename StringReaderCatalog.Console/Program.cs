using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringReaderCatalog.Test;

namespace StringReaderCatalog.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
				return;

			var command = args[0].ToLower();
			var restArgs = args.Skip(1).ToArray();

			if (command.StartsWith("read"))
			{
				Read(restArgs);
			}
			else if (command.StartsWith("load"))
			{
				JsonLoad(restArgs);

				//var loadTask = JsonLoadAsync(restArgs);
			}
			else if (command.StartsWith("save"))
			{
				var saveTask = JsonSaveAsync(restArgs);
			}
		}

		static void Read(string[] args)
		{
			if (args.Length < 2)
				return;

			var inputPath = args[0];
			var outputPath = args[1];

			int selector = 0;
			if (3 <= args.Length)
				selector = int.Parse(args[2]);

			try
			{
				var imageString = StringReader.ReadFile(inputPath, selector);

				var imageBytes = Convert.FromBase64String(imageString);

				using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
				{
					var image = Image.FromStream(ms, true);
					image.Save(outputPath, ImageFormat.Png);
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

		static void JsonLoad(string[] args)
		{
			var filePath = args[0];
			var canAcceptBom = (2 <= args.Length);

			try
			{
				System.Console.WriteLine(JsonStringReaderTest.Load(filePath, canAcceptBom));
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

		static async Task JsonLoadAsync(string[] args)
		{
			var filePath = args[0];
			var canAcceptBom = (2 <= args.Length);

			try
			{
				System.Console.WriteLine(await JsonStringReaderTest.LoadAsync(filePath, canAcceptBom));
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

		static async Task JsonSaveAsync(string[] args)
		{
			var filePath = args[0];
			var canEmitBom = (2 <= args.Length);

			int selector = 0;
			if (canEmitBom)
			{
				int buff;
				if (int.TryParse(args[1], out buff))
				{
					selector = buff;
					System.Console.WriteLine("Selector: {0}", selector);
				}
			}

			try
			{
				await JsonStringReaderTest.SaveAsync(filePath, canEmitBom, selector);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}
	}
}