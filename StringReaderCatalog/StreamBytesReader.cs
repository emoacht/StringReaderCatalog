using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StringReaderCatalog
{
	public class StreamBytesReader
	{
		public const int defaultBufferSize = 65536; // 64KiB

		public static async Task<byte[]> ReadBytesAsync(string filePath, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
		{
			return await ReadBytesAsync(filePath, defaultBufferSize, progress, cancellationToken);
		}

		public static async Task<byte[]> ReadBytesAsync(string filePath, int bufferSize, IProgress<StreamProgress> progress, CancellationToken cancellationToken)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				var buffer = new byte[bufferSize];
				var bufferTotal = new byte[fs.Length];

				while (fs.Position < fs.Length)
				{
					var readPosition = fs.Position;
					var readCount = await fs.ReadAsync(buffer, 0, buffer.Length, cancellationToken);

					Array.Copy(buffer, 0L, bufferTotal, readPosition, (long)readCount); // Buffer.BlockCopy method accepts only int parameters.

					if (progress != null)
						progress.Report(new StreamProgress(fs.Position, fs.Length));
				}

				return bufferTotal;
			}
		}
	}
}