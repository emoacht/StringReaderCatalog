using System.Runtime.Serialization;

namespace StringReaderCatalog.Test
{
	[DataContract]
	public class DataBag
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }
	}
}