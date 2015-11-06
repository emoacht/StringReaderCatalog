using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringReaderCatalog.Trial
{
	public class FileStreamReaderTrial
	{
		private static readonly int[] bufferSizes = { 64, 128, 256, 512, }; // Buffer sizes in KiB

		// This method may cause an OutOfMemoryException if run on 32bit process.
		// To avoid the exception, uncheck "Prefer 32-bit" in the build settings.
		public static async Task ReadAsync(string filePath)
		{
			if (!File.Exists(filePath))
				return;

			foreach (var bufferSize in bufferSizes)
			{
				Console.WriteLine($"Start {bufferSize}KiB");

				var sw = new Stopwatch();
				sw.Start();

				double oldValue = 0;

				var progress = new Progress<StreamProgress>(x => ShowPercentage(ref oldValue, x.Percentage));

				await FileStreamReader.ReadBytesAsync(filePath, bufferSize * 1024, progress, CancellationToken.None);

				sw.Stop();

				Console.WriteLine($"Complete {bufferSize}KiB {sw.Elapsed.TotalSeconds:f3}sec");
			}
		}

		private static void ShowPercentage(ref double oldValue, double newValue)
		{
			newValue = Math.Round(newValue * 100D) / 100D;

			if (oldValue + 0.099 < newValue)
			{
				oldValue = newValue;
				Console.WriteLine($"{newValue:f2}");
			}
		}
	}
}
