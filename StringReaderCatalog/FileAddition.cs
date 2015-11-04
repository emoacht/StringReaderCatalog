using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringReaderCatalog
{
	/// <summary>
	/// Additional method for <see cref="Syste.IO.File"/>
	/// </summary>
	public static class FileAddition
	{
		public const int defaultCopyBufferSize = 81920; // 80KiB is actual default buffer size in System.IO.File class.

		#region Read

		/// <summary>
		/// Read all text from a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Source file path</param>
		/// <returns>Contents of source file</returns>
		public static async Task<string> ReadAllTextAsync(string filePath)
		{
			using (var sr = new StreamReader(filePath))
				return await sr.ReadToEndAsync();
		}

		/// <summary>
		/// Read all text from a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Source file path</param>
		/// <param name="encoding">Character encoding</param>
		/// <returns>Contents of source file</returns>
		public static async Task<string> ReadAllTextAsync(string filePath, Encoding encoding)
		{
			using (var sr = new StreamReader(filePath, encoding))
				return await sr.ReadToEndAsync();
		}

		/// <summary>
		/// Read all text from a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Source file path</param>
		/// <param name="encoding">Character encoding</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>Contents of source file</returns>
		public static async Task<string> ReadAllTextAsync(string filePath, Encoding encoding, CancellationToken cancellationToken)
		{
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var ms = new MemoryStream())
			using (var sr = new StreamReader(ms, encoding))
			{
				await fs.CopyToAsync(ms, defaultCopyBufferSize, cancellationToken).ConfigureAwait(false);
				ms.Seek(0, SeekOrigin.Begin);
				return await sr.ReadToEndAsync();
			}
		}

		#endregion

		#region Write

		/// <summary>
		/// Write all text to a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Target file path</param>
		/// <param name="contents">Contents being written to target file</param>
		/// <returns></returns>
		/// <remarks>No BOM will be emitted.</remarks>
		public static async Task WriteAllTextAsync(string filePath, string contents)
		{
			using (var sw = new StreamWriter(filePath))
			{
				await sw.WriteAsync(contents).ConfigureAwait(false);
				await sw.FlushAsync();
			}
		}

		/// <summary>
		/// Write all text to a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Target file path</param>
		/// <param name="contents">Contents being written to target file</param>
		/// <param name="append">Whether contents to be appended</param>
		/// <param name="encoding">Character encoding</param>
		/// <returns></returns>
		/// <remarks>BOM will be emitted if encoding is Unicode.</remarks>
		public static async Task WriteAllTextAsync(string filePath, string contents, bool append, Encoding encoding)
		{
			using (var sw = new StreamWriter(filePath, append, encoding))
			{
				await sw.WriteAsync(contents).ConfigureAwait(false);
				await sw.FlushAsync();
			}
		}

		/// <summary>
		/// Write all text to a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Target file path</param>
		/// <param name="contents">Contents being written to target file</param>
		/// <param name="append">Whether contents to be appended</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns></returns>
		/// <remarks>No BOM will be emitted.</remarks>
		public static async Task WriteAllTextAsync(string filePath, string contents, bool append, CancellationToken cancellationToken)
		{
			var fileMode = append ? FileMode.Append : FileMode.Create;

			using (var fs = new FileStream(filePath, fileMode, FileAccess.Write, FileShare.ReadWrite))
			using (var sw = new StreamWriter(fs))
			{
				await sw.WriteAsync(contents).ConfigureAwait(false);
				await sw.FlushAsync().ConfigureAwait(false);
				await fs.FlushAsync(cancellationToken);
			}
		}

		/// <summary>
		/// Write all text to a specified file asynchronously.
		/// </summary>
		/// <param name="filePath">Target file path</param>
		/// <param name="contents">Contents being written to target file</param>
		/// <param name="append">Whether contents to be appended</param>
		/// <param name="encoding">Character encoding</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns></returns>
		/// <remarks>BOM will be emitted if encoding is Unicode.</remarks>
		public static async Task WriteAllTextAsync(string filePath, string contents, bool append, Encoding encoding, CancellationToken cancellationToken)
		{
			var fileMode = append ? FileMode.Append : FileMode.Create;

			using (var fs = new FileStream(filePath, fileMode, FileAccess.Write, FileShare.ReadWrite))
			using (var sw = new StreamWriter(fs, encoding))
			{
				await sw.WriteAsync(contents).ConfigureAwait(false);
				await sw.FlushAsync().ConfigureAwait(false);
				await fs.FlushAsync(cancellationToken);
			}
		}

		#endregion
	}
}