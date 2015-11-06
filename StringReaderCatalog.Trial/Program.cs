using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringReaderCatalog.Trial
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
				return;

			var command = args[0].ToLower();

			if (command.Equals("stringread"))
			{
				if (args.Length < 3)
					return;

				var inputPath = args[1];
				var outputPath = args[2];

				int selector = (args.Length < 4)
					? 0
					: FindInteger(args[3]) ?? 0;

				StringReaderTrial.Read(inputPath, outputPath, selector);
			}
			if (command.Equals("streamread"))
			{
				var filePath = args[1];

				FileStreamReaderTrial.ReadAsync(filePath).Wait();
			}
			else if (command.Equals("jsonload"))
			{
				var filePath = args[1];
				var canAcceptBom = (args.Length >= 3);

				//JsonStringReaderTrial.Load(filePath, canAcceptBom);

				JsonStringReaderTrial.LoadAsync(filePath, canAcceptBom).Wait();
			}
			else if (command.Equals("jsonsave"))
			{
				var filePath = args[1];
				var canEmitBom = (args.Length >= 3);

				int selector = !canEmitBom
					? 0
					: FindInteger(args[2]) ?? 0;

				JsonStringReaderTrial.SaveAsync(filePath, canEmitBom, selector).Wait();
			}
			else if (command.Equals("jsonsaveload"))
			{
				var filePath = args[1];
				var canEmitBom = (3 <= args.Length);

				int selector = !canEmitBom
					? 0
					: FindInteger(args[2]) ?? 0;

				JsonStringReaderTrial.SaveLoadAsync(canEmitBom, selector).Wait();
			}

			Console.ReadKey();
		}

		static int? FindInteger(string source)
		{
			int buff;
			if (int.TryParse(source, out buff))
				return buff;

			return null;
		}
	}
}