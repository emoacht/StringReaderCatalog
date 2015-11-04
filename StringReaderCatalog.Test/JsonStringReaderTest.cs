using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringReaderCatalog.Test
{
	[TestFixture]
	public class JsonStringReaderTest
	{
		[TestCase(true, true, 0, TestName = "SaveLoadAsync0", Result = true)]
		[TestCase(true, true, 1, TestName = "SaveLoadAsync1", Result = true)]
		[TestCase(true, true, 2, TestName = "SaveLoadAsync2", Result = true)]
		[TestCase(true, true, 3, TestName = "SaveLoadAsync3", Result = true)]
		[TestCase(true, true, 4, TestName = "SaveLoadAsync4", Result = true)]
		[TestCase(true, true, 5, TestName = "SaveLoadAsync5", Result = true)]
		[TestCase(true, true, 6, TestName = "SaveLoadAsync6", Result = true)]
		[TestCase(true, true, 7, TestName = "SaveLoadAsync7", Result = true)]
		[TestCase(true, true, 8, TestName = "SaveLoadAsync8", Result = true)]
		[TestCase(true, true, 9, TestName = "SaveLoadAsync9", Result = true)]
		[TestCase(true, true, 10, TestName = "SaveLoadAsync10", Result = true)]
		[TestCase(true, true, 11, TestName = "SaveLoadAsync11", Result = true)]
		[TestCase(false, false, 0, TestName = "SaveLoadAsync12", Result = true)]
		[TestCase(true, false, 0, TestName = "SaveLoadAsync13", Result = false, ExpectedException = typeof(SerializationException))]
		public async Task<bool> SaveLoadAsyncTest(bool canEmitBom, bool canAcceptBom, int selector)
		{
			var filePath = Path.GetTempFileName();

			try
			{
				await SaveAsync(filePath, canEmitBom, selector);

				return await LoadAsync(filePath, canAcceptBom);
			}
			finally
			{
				File.Delete(filePath);
			}
		}

		public static bool Load(string filePath, bool canAcceptBom)
		{
			var bags = !canAcceptBom
				? JsonStringReader.LoadNoBom<DataBag[]>(filePath)
				: JsonStringReader.LoadRegardlessBom<DataBag[]>(filePath);

			return bags.SequenceEqual(GetSampleBags(3), new DataBagEqualityComparer());
		}

		public static async Task<bool> LoadAsync(string filePath, bool canAcceptBom)
		{
			var bags = !canAcceptBom
				? await JsonStringReader.LoadNoBomAsync<DataBag[]>(filePath)
				: await JsonStringReader.LoadRegardlessBomAsync<DataBag[]>(filePath);

			return bags.SequenceEqual(GetSampleBags(3), new DataBagEqualityComparer());
		}

		public static async Task SaveAsync(string filePath, bool canEmitBom, int selector)
		{
			if (!canEmitBom)
				await JsonStringReader.SaveNoBomAsync(filePath, GetSampleBags(3));
			else
				await JsonStringReader.SaveSelectableBomAsync(filePath, GetSampleBags(3), selector);
		}

		private static DataBag[] GetSampleBags(int count)
		{
			return Enumerable.Range(100, count)
				.Select(x => new DataBag { Id = x, Name = $"Name{x}" })
				.ToArray();
		}
	}
}