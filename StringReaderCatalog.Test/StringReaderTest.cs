using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringReaderCatalog.Test
{
	[TestFixture]
	public class StringReaderTest
	{
		[TestCase(0, TestName = "ReadFile0", Result = true)]
		[TestCase(1, TestName = "ReadFile1", Result = true)]
		[TestCase(2, TestName = "ReadFile2", Result = true)]
		[TestCase(3, TestName = "ReadFile3", Result = true)]
		[TestCase(4, TestName = "ReadFile4", Result = true)]
		[TestCase(5, TestName = "ReadFile5", Result = true)]
		[TestCase(6, TestName = "ReadFile6", Result = true)]
		[TestCase(7, TestName = "ReadFile7", Result = true)]
		[TestCase(8, TestName = "ReadFile8", Result = true)]
		[TestCase(9, TestName = "ReadFile9", Result = true)]
		[TestCase(10, TestName = "ReadFile10", Result = false, ExpectedException = typeof(FormatException))]
		[TestCase(11, TestName = "ReadFile11", Result = false, ExpectedException = typeof(FormatException))]
		public bool ReadFileTest(int selector)
		{
			using (var tf = new TestFile(Encoding.UTF8))
			{
				var imageString = StringReader.ReadFile(tf.FilePath, selector);

				var imageBytes = Convert.FromBase64String(imageString);

				return (tf.FileSize == imageBytes.Length)
					&& imageBytes.SequenceEqual(tf.FileBytes);
			}
		}

		[TestCase(0, TestName = "ReadFileAsync0", Result = true)]
		[TestCase(1, TestName = "ReadFileAsync1", Result = true)]
		[TestCase(2, TestName = "ReadFileAsync2", Result = true)]
		[TestCase(3, TestName = "ReadFileAsync3", Result = true)]
		[TestCase(4, TestName = "ReadFileAsync4", Result = true)]
		[TestCase(5, TestName = "ReadFileAsync5", Result = true)]
		[TestCase(6, TestName = "ReadFileAsync6", Result = true)]
		[TestCase(7, TestName = "ReadFileAsync7", Result = true)]
		[TestCase(8, TestName = "ReadFileAsync8", Result = true)]
		[TestCase(9, TestName = "ReadFileAsync9", Result = false, ExpectedException = typeof(FormatException))]
		public async Task<bool> ReadFileAsyncTest(int selector)
		{
			using (var tf = new TestFile(Encoding.UTF8))
			{
				var imageString = await StringReader.ReadFileAsync(tf.FilePath, selector);

				var imageBytes = Convert.FromBase64String(imageString);

				return (tf.FileSize == imageBytes.Length)
					&& imageBytes.SequenceEqual(tf.FileBytes);
			}
		}

		[TestCase(0, TestName = "ReadBytes0", Result = true)]
		[TestCase(1, TestName = "ReadBytes1", Result = true)]
		[TestCase(2, TestName = "ReadBytes2", Result = true)]
		[TestCase(3, TestName = "ReadBytes3", Result = true)]
		[TestCase(4, TestName = "ReadBytes4", Result = true)]
		[TestCase(5, TestName = "ReadBytes5", Result = true)]
		[TestCase(6, TestName = "ReadBytes6", Result = true)]
		[TestCase(7, TestName = "ReadBytes7", Result = false, ExpectedException = typeof(FormatException))]
		[TestCase(8, TestName = "ReadBytes8", Result = false, ExpectedException = typeof(FormatException))]
		[TestCase(9, TestName = "ReadBytes9", Result = false, ExpectedException = typeof(FormatException))]
		public bool ReadBytesTest(int selector)
		{
			using (var tf = new TestFile(Encoding.UTF8))
			{
				var sourceBytes = File.ReadAllBytes(tf.FilePath);

				var imageString = StringReader.ReadBytes(sourceBytes, selector);

				var imageBytes = Convert.FromBase64String(imageString);

				return (tf.FileSize == imageBytes.Length)
					&& imageBytes.SequenceEqual(tf.FileBytes);
			}
		}

		[TestCase(0, TestName = "ReadBytesAsync0", Result = true)]
		[TestCase(1, TestName = "ReadBytesAsync1", Result = true)]
		[TestCase(2, TestName = "ReadBytesAsync2", Result = true)]
		[TestCase(3, TestName = "ReadBytesAsync3", Result = true)]
		[TestCase(4, TestName = "ReadBytesAsync4", Result = true)]
		[TestCase(5, TestName = "ReadBytesAsync5", Result = false, ExpectedException = typeof(FormatException))]
		[TestCase(6, TestName = "ReadBytesAsync6", Result = false, ExpectedException = typeof(FormatException))]
		[TestCase(7, TestName = "ReadBytesAsync7", Result = false, ExpectedException = typeof(FormatException))]
		public async Task<bool> ReadBytesAsyncTest(int selector)
		{
			using (var tf = new TestFile(Encoding.UTF8))
			{
				using (var fs = new FileStream(tf.FilePath, FileMode.Open, FileAccess.Read))
				using (var ms = new MemoryStream())
				{
					await fs.CopyToAsync(ms);
					ms.Seek(0, SeekOrigin.Begin);

					var imageString = await StringReader.ReadBytesAsync(ms.ToArray(), selector);

					var imageBytes = Convert.FromBase64String(imageString);

					return (tf.FileSize == imageBytes.Length)
						&& imageBytes.SequenceEqual(tf.FileBytes);
				}
			}
		}
	}
}