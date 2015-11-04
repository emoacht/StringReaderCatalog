
namespace StringReaderCatalog
{
	public class StreamProgress
	{
		public long Position { get; }
		public long Length { get; }

		public double Percentage =>
			(Length != 0L) ? (double)Position / (double)Length : 0D;

		public StreamProgress(long position, long length)
		{
			this.Position = position;
			this.Length = length;
		}
	}
}