using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StringReaderCatalog.Test
{
    [TestClass]
    public class JsonStringReaderTest
    {
        #region Save then Load test

        [TestMethod]
        public async Task SaveLoadTestAsync0()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 0));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync1()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 1));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync2()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 2));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync3()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 3));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync4()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 4));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync5()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 5));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync6()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 6));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync7()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 7));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync8()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 8));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync9()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 9));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync10()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 10));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync11()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, true, 11));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync12()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(false, false, 0));
        }

        [TestMethod]
        public async Task SaveLoadTestAsync13()
        {
            Assert.IsTrue(await SaveLoadTestBaseAsync(true, false, 0));
        }

        #endregion


        #region Base

        private async Task<bool> SaveLoadTestBaseAsync(bool canEmitBom, bool canAcceptBom, int selector)
        {
            var filePath = Path.GetTempFileName();

            try
            {
                await JsonStringReaderTest.SaveAsync(filePath, canEmitBom, selector);

                return await JsonStringReaderTest.LoadAsync(filePath, canAcceptBom);
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
                .Select(x => new DataBag { Id = x, Name = String.Format("Name{0}", x) })
                .ToArray();
        }

        #endregion
    }
}