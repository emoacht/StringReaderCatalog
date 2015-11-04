using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringReaderCatalog
{
	public class JsonStringReader
	{
		#region Load

		public static T LoadNoBom<T>(string filePath)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				// If BOM is contained, System.Runtime.Serialization.SerializationException will be thrown.
				return (T)serializer.ReadObject(fs);
			}
		}

		public static async Task<T> LoadNoBomAsync<T>(string filePath)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (var ms = new MemoryStream())
			{
				await fs.CopyToAsync(ms).ConfigureAwait(false);
				ms.Seek(0, SeekOrigin.Begin);

				// If BOM is contained, System.Runtime.Serialization.SerializationException will be thrown.
				return (T)serializer.ReadObject(ms);
			}
		}

		public static T LoadRegardlessBom<T>(string filePath)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			try
			{
				using (var sr = new StreamReader(filePath))
				using (var ms = new MemoryStream())
				using (var sw = new StreamWriter(ms))
				{
					var inputString = sr.ReadToEnd();

					sw.Write(inputString);
					sw.Flush();
					ms.Seek(0, SeekOrigin.Begin);

					return (T)serializer.ReadObject(ms);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return default(T);
			}
		}

		public static async Task<T> LoadRegardlessBomAsync<T>(string filePath)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			try
			{
				using (var sr = new StreamReader(filePath))
				using (var ms = new MemoryStream())
				using (var sw = new StreamWriter(ms))
				{
					var inputString = await sr.ReadToEndAsync().ConfigureAwait(false);

					await sw.WriteAsync(inputString).ConfigureAwait(false);
					await sw.FlushAsync();
					ms.Seek(0, SeekOrigin.Begin);

					return (T)serializer.ReadObject(ms);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return default(T);
			}
		}

		#endregion

		#region Save

		public static async Task SaveNoBomAsync<T>(string filePath, T source)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				serializer.WriteObject(fs, source);

				/* No BOM will be emitted. */
				await fs.FlushAsync();
			}
		}

		public static async Task SaveBomAsync<T>(string filePath, T source)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
			using (var ms = new MemoryStream())
			using (var sr = new StreamReader(ms))
			{
				serializer.WriteObject(ms, source);
				ms.Seek(0, SeekOrigin.Begin);

				var outputString = await sr.ReadToEndAsync().ConfigureAwait(false);

				/* BOM will be emitted. */
				await sw.WriteAsync(outputString).ConfigureAwait(false);
				await sw.FlushAsync();
			}
		}

		public static async Task SaveSelectableBomAsync<T>(string filePath, T source, int selector = 0)
		{
			var serializer = new DataContractJsonSerializer(typeof(T));

			using (var ms = new MemoryStream())
			{
				serializer.WriteObject(ms, source);

				var outputString = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
				var outputBytes = ms.ToArray();

				switch (selector)
				{
					/* BOM will be emitted. */
					case 0:
						// StreamWriter constructor with encoding
						using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
						{
							await sw.WriteAsync(outputString);
							await sw.FlushAsync();
						}
						break;

					case 1:
						// StreamWriter constructor with encoding that encoderShouldEmitUTF8Identifier is true
						using (var sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
						{
							await sw.WriteAsync(outputString);
							await sw.FlushAsync();
						}
						break;

					case 2:
						// File.WriteAllText with encoding
						await Task.Run(() => File.WriteAllText(filePath, outputString, Encoding.UTF8));
						break;

					case 3:
						// FileAddition.WriteAllTextAsync with encoding
						await FileAddition.WriteAllTextAsync(filePath, outputString, false, Encoding.UTF8);
						break;

					case 4:
						// FileAddition.WriteAllTextAsync with encoding and cancellation token
						using (var cts = new CancellationTokenSource())
						{
							await FileAddition.WriteAllTextAsync(filePath, outputString, false, Encoding.UTF8, cts.Token);
						}
						break;

					/* No BOM will be emitted. */
					case 5:
						// StreamWriter constructor without encoding
						using (var sw = new StreamWriter(filePath, false))
						{
							await sw.WriteAsync(outputString);
							await sw.FlushAsync();
						}
						break;

					case 6:
						// StreamWriter constructor with encoding that encoderShouldEmitUTF8Identifier is false
						using (var sw = new StreamWriter(filePath, false, new UTF8Encoding(false)))
						{
							await sw.WriteAsync(outputString);
							await sw.FlushAsync();
						}
						break;

					case 7:
						// StreamWriter constructor by File.CreateText 
						using (var sw = File.CreateText(filePath))
						{
							await sw.WriteAsync(outputString);
							await sw.FlushAsync();
						}
						break;

					case 8:
						// File.WriteAllText without encoding
						await Task.Run(() => File.WriteAllText(filePath, outputString));
						break;

					case 9:
						// File.WriteAllBytes
						await Task.Run(() => File.WriteAllBytes(filePath, outputBytes));
						break;

					case 10:
						// FileAddition.WriteAllTextAsync without encoding
						await FileAddition.WriteAllTextAsync(filePath, outputString);
						break;

					case 11:
						// FileAddition.WriteAllTextAsync without encoding but with cancellation token
						using (var cts = new CancellationTokenSource())
						{
							await FileAddition.WriteAllTextAsync(filePath, outputString, false, cts.Token);
						}
						break;

					default:
						throw new ArgumentOutOfRangeException(nameof(selector));
				}
			}
		}

		#endregion
	}
}