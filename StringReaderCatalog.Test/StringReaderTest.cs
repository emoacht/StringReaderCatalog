using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringReaderCatalog.Test
{
    [TestClass]
    public class StringReaderTest
    {
        #region ReadFile test

        [TestMethod]
        public void ReadFileTest0()
        {
            ReadFileTestBase(0);
        }

        [TestMethod]
        public void ReadFileTest1()
        {
            ReadFileTestBase(1);
        }

        [TestMethod]
        public void ReadFileTest2()
        {
            ReadFileTestBase(2);
        }

        [TestMethod]
        public void ReadFileTest3()
        {
            ReadFileTestBase(3);
        }

        [TestMethod]
        public void ReadFileTest4()
        {
            ReadFileTestBase(4);
        }

        [TestMethod]
        public void ReadFileTest5()
        {
            ReadFileTestBase(5);
        }

        [TestMethod]
        public void ReadFileTest6()
        {
            ReadFileTestBase(6);
        }

        [TestMethod]
        public void ReadFileTest7()
        {
            ReadFileTestBase(7);
        }

        [TestMethod]
        public void ReadFileTest8()
        {
            ReadFileTestBase(8);
        }

        [TestMethod]
        public void ReadFileTest9()
        {
            ReadFileTestBase(9);
        }

        [TestMethod]
        public void ReadFileTest10()
        {
            ReadFileTestBase(10);
        }

        [TestMethod]
        public void ReadFileTest11()
        {
            ReadFileTestBase(11);
        }

        #endregion


        #region ReadFileAsync test

        [TestMethod]
        public async Task ReadFileAsyncTest0()
        {
            await ReadFileAsyncTestBase(0);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest1()
        {
            await ReadFileAsyncTestBase(1);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest2()
        {
            await ReadFileAsyncTestBase(2);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest3()
        {
            await ReadFileAsyncTestBase(3);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest4()
        {
            await ReadFileAsyncTestBase(4);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest5()
        {
            await ReadFileAsyncTestBase(5);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest6()
        {
            await ReadFileAsyncTestBase(6);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest7()
        {
            await ReadFileAsyncTestBase(7);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest8()
        {
            await ReadFileAsyncTestBase(8);
        }

        [TestMethod]
        public async Task ReadFileAsyncTest9()
        {
            await ReadFileAsyncTestBase(9);
        }

        #endregion


        #region ReadBytes test

        [TestMethod]
        public void ReadBytesTest0()
        {
            ReadBytesTestBase(0);
        }

        [TestMethod]
        public void ReadBytesTest1()
        {
            ReadBytesTestBase(1);
        }

        [TestMethod]
        public void ReadBytesTest2()
        {
            ReadBytesTestBase(2);
        }

        [TestMethod]
        public void ReadBytesTest3()
        {
            ReadBytesTestBase(3);
        }

        [TestMethod]
        public void ReadBytesTest4()
        {
            ReadBytesTestBase(4);
        }

        [TestMethod]
        public void ReadBytesTest5()
        {
            ReadBytesTestBase(5);
        }

        [TestMethod]
        public void ReadBytesTest6()
        {
            ReadBytesTestBase(6);
        }

        [TestMethod]
        public void ReadBytesTest7()
        {
            ReadBytesTestBase(7);
        }

        #endregion


        #region ReadBytesAsync test

        [TestMethod]
        public async Task ReadBytesAsyncTest0()
        {
            await ReadBytesAsyncTestBase(0);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest1()
        {
            await ReadBytesAsyncTestBase(1);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest2()
        {
            await ReadBytesAsyncTestBase(2);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest3()
        {
            await ReadBytesAsyncTestBase(3);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest4()
        {
            await ReadBytesAsyncTestBase(4);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest5()
        {
            await ReadBytesAsyncTestBase(5);
        }

        [TestMethod]
        public async Task ReadBytesAsyncTest6()
        {
            await ReadBytesAsyncTestBase(6);
        }

        #endregion


        #region Base

        private void ReadFileTestBase(int selector)
        {
            using (var tf = new TestFile(Encoding.UTF8))
            {
                var imageString = StringReader.ReadFile(tf.FilePath, selector);

                var imageBytes = Convert.FromBase64String(imageString);

                Assert.AreEqual<int>(tf.FileSize, imageBytes.Length);
                Assert.IsTrue(imageBytes.SequenceEqual(tf.FileBytes));
            }
        }

        private async Task ReadFileAsyncTestBase(int selector)
        {
            using (var tf = new TestFile(Encoding.UTF8))
            {
                var imageString = await StringReader.ReadFileAsync(tf.FilePath, selector);

                var imageBytes = Convert.FromBase64String(imageString);

                Assert.AreEqual<int>(tf.FileSize, imageBytes.Length);
                Assert.IsTrue(imageBytes.SequenceEqual(tf.FileBytes));
            }
        }

        private void ReadBytesTestBase(int selector)
        {
            using (var tf = new TestFile(Encoding.UTF8))
            {
                var sourceBytes = File.ReadAllBytes(tf.FilePath);

                var imageString = StringReader.ReadBytes(sourceBytes, selector);

                var imageBytes = Convert.FromBase64String(imageString);

                Assert.AreEqual<int>(tf.FileSize, imageBytes.Length);
                Assert.IsTrue(imageBytes.SequenceEqual(tf.FileBytes));
            }
        }

        private async Task ReadBytesAsyncTestBase(int selector)
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

                    Assert.AreEqual<int>(tf.FileSize, imageBytes.Length);
                    Assert.IsTrue(imageBytes.SequenceEqual(tf.FileBytes));
                }
            }
        }

        #endregion
    }
}