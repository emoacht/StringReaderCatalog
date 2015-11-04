using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace StringReaderCatalog.Test
{
	public sealed class TestFile : IDisposable
	{
		public Encoding FileEncoding { get; private set; }
		public string FilePath { get; private set; }
		public byte[] FileBytes { get; private set; }

		public int FileSize
		{
			get { return (FileBytes != null) ? FileBytes.Length : 0; }
		}

		public TestFile(Encoding encoding)
		{
			FileEncoding = encoding;
			FilePath = Path.GetTempFileName();

			CreateTestFile();
		}

		private void CreateTestFile()
		{
			if ((FileEncoding == null) || string.IsNullOrEmpty(FilePath))
				return;

			var imageBitmap = Properties.Resources.cockpit;
			FileBytes = (byte[])new ImageConverter().ConvertTo(imageBitmap, typeof(byte[]));
			var imageString = Convert.ToBase64String(FileBytes);

			File.WriteAllText(FilePath, imageString, FileEncoding);
		}

		private void DeleteTestFile()
		{
			if (string.IsNullOrEmpty(FilePath))
				return;

			File.Delete(FilePath);
		}

		#region Dispose

		private bool disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				// Free any other managed objects here.
				FileBytes = null;
			}

			// Free any unmanaged objects here.
			DeleteTestFile();

			disposed = true;
		}

		~TestFile()
		{
			Dispose(false);
		}

		#endregion
	}
}