using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StringReaderCatalog
{
    public class StringReader
    {
        public static string ReadFile(string filePath, int selector = 0)
        {
            switch (selector)
            {
                /* BOM will be removed. */
                case 0:
                    // File.ReadAllText
                    return File.ReadAllText(filePath);

                case 1:
                    // File.ReadAllText with encoding
                    return File.ReadAllText(filePath, Encoding.UTF8);

                case 2:
                    // StreamReader constructor then ReadToEnd
                    using (var sr = new StreamReader(filePath))
                        return sr.ReadToEnd();

                case 3:
                    // StreamReader constructor with encoding then ReadToEnd
                    using (var sr = new StreamReader(filePath, Encoding.UTF8))
                        return sr.ReadToEnd();

                case 4:
                    // StreamReader by File.OpenText then ReadToEnd
                    using (var sr = File.OpenText(filePath))
                        return sr.ReadToEnd();

                case 5:
                    // StreamReader by FileInfo.OpenText then ReadToEnd
                    using (var sr = new FileInfo(filePath).OpenText())
                        return sr.ReadToEnd();

                case 6:
                    // FileStream constructor and StreamReader constructor then ReadToEnd
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs))
                        return sr.ReadToEnd();

                case 7:
                    // FileStream constructor and StreamReader constructor with encoding then ReadToEnd
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                        return sr.ReadToEnd();

                case 8:
                    // FileStream by File.OpenRead and StreamReader constructor then ReadToEnd
                    using (var fs = File.OpenRead(filePath))
                    using (var sr = new StreamReader(fs))
                        return sr.ReadToEnd();

                case 9:
                    // FileStream by FileInfo.OpenRead and StreamReader constructor then ReadToEnd
                    using (var fs = new FileInfo(filePath).OpenRead())
                    using (var sr = new StreamReader(fs))
                        return sr.ReadToEnd();

                /* BOM will remain as unreadable character. */
                case 10:
                    // File.ReadAllBytes then Encoding.GetString
                    {
                        var buff = File.ReadAllBytes(filePath);
                        return Encoding.UTF8.GetString(buff);
                    }

                case 11:
                    // FileStream constructor then Read then Encoding.GetString
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var buff = new Byte[fs.Length];
                        fs.Read(buff, 0, buff.Length);
                        return Encoding.UTF8.GetString(buff);
                    }

                default:
                    throw new ArgumentOutOfRangeException("selector");
            }
        }

        public static async Task<string> ReadFileAsync(string filePath, int selector = 0)
        {
            switch (selector)
            {
                /* BOM will be removed. */
                case 0:
                    // StreamReader constructor then ReadToEndAsync
                    using (var sr = new StreamReader(filePath))
                        return await sr.ReadToEndAsync();

                case 1:
                    // StreamReader constructor with encoding then ReadToEndAsync
                    using (var sr = new StreamReader(filePath, Encoding.UTF8))
                        return await sr.ReadToEndAsync();

                case 2:
                    // StreamReader by File.OpenText then ReadToEndAsync
                    using (var sr = File.OpenText(filePath))
                        return await sr.ReadToEndAsync();

                case 3:
                    // FileStream constructor and StreamReader constructor then ReadToEndAsync
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs))
                        return await sr.ReadToEndAsync();

                case 4:
                    // FileStream constructor and StreamReader constructor with Encoding then ReadToEndAsync
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                        return await sr.ReadToEndAsync();

                case 5:
                    // FileStream by File.OpenRead and StreamReader constructor then ReadToEnd
                    using (var fs = File.OpenRead(filePath))
                    using (var sr = new StreamReader(fs))
                        return await sr.ReadToEndAsync();

                case 6:
                    // FileAddition.ReadAllTextAsync
                    return await FileAddition.ReadAllTextAsync(filePath);

                case 7:
                    // FileAddition.ReadAllTextAsync with encoding
                    return await FileAddition.ReadAllTextAsync(filePath, Encoding.UTF8);

                case 8:
                    // FileAddition.ReadAllTextAsync with encoding and CancellationToken
                    using (var cts = new CancellationTokenSource())
                    {
                        return await FileAddition.ReadAllTextAsync(filePath, Encoding.UTF8, cts.Token);
                    }

                /* BOM will remain as unreadable character. */
                case 9:
                    // FileStream constructor then ReadAsync then Encoding.GetString
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var bytes = new Byte[fs.Length];
                        await fs.ReadAsync(bytes, 0, bytes.Length);
                        return Encoding.UTF8.GetString(bytes);
                    }

                default:
                    throw new ArgumentOutOfRangeException("selector");
            }
        }

        public static string ReadBytes(byte[] source, int selector = 0)
        {
            switch (selector)
            {
                /* BOM will be removed. */
                case 0:
                    // MemoryStream constructor and StreamReader constructor then ReadToEnd
                    using (var ms = new MemoryStream(source))
                    using (var sr = new StreamReader(ms))
                        return sr.ReadToEnd();

                case 1:
                    // MemoryStream constructor and StreamReader constructor with encoding then ReadToEnd
                    using (var ms = new MemoryStream(source))
                    using (var sr = new StreamReader(ms, Encoding.UTF8))
                        return sr.ReadToEnd();

                case 2:
                    // StreamReader constructor then MemoryStream.Write then ReadToEnd
                    using (var ms = new MemoryStream())
                    using (var sr = new StreamReader(ms))
                    {
                        ms.Write(source, 0, source.Length);
                        ms.Seek(0, SeekOrigin.Begin);
                        return sr.ReadToEnd();
                    }

                case 3:
                    // StreamReader constructor with Encoding then MemoryStream.Write then ReadToEnd 
                    using (var ms = new MemoryStream())
                    using (var sr = new StreamReader(ms, Encoding.UTF8))
                    {
                        ms.Write(source, 0, source.Length);
                        ms.Seek(0, SeekOrigin.Begin);
                        return sr.ReadToEnd();
                    }

                case 4:
                    // StreamReader constructor then BinaryWriter.Write then ReadToEnd
                    using (var ms = new MemoryStream())
                    using (var bw = new BinaryWriter(ms))
                    using (var sr = new StreamReader(ms))
                    {
                        bw.Write(source);
                        ms.Seek(0, SeekOrigin.Begin);
                        return sr.ReadToEnd();
                    }

                case 5:
                    // MemoryStream constructor then ToArray then UnicodeStringReader.Read
                    using (var ms = new MemoryStream(source))
                    {
                        return UnicodeStringReader.Read(ms.ToArray());
                    }

                /* BOM will remain as unreadable character. */
                case 6:
                    // Encoding.GetString
                    return Encoding.UTF8.GetString(source);

                case 7:
                    // MemoryStream constructor then ToArray then Encoding.GetString
                    using (var ms = new MemoryStream(source))
                    {
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }

                default:
                    throw new ArgumentOutOfRangeException("selector");
            }
        }

        public static async Task<string> ReadBytesAsync(byte[] source, int selector = 0)
        {
            switch (selector)
            {
                /* BOM will be removed. */
                case 0:
                    // MemoryStream constructor and StreamReader constructor then ReadToEndAsync
                    using (var ms = new MemoryStream(source))
                    using (var sr = new StreamReader(ms))
                        return await sr.ReadToEndAsync();

                case 1:
                    // MemoryStream constructor and StreamReader constructor with encoding then ReadToEndAsync
                    using (var ms = new MemoryStream(source))
                    using (var sr = new StreamReader(ms, Encoding.UTF8))
                        return await sr.ReadToEndAsync();

                case 2:
                    // StreamReader constructor then MemoryStream.WriteAsync then ReadToEndAsync
                    using (var ms = new MemoryStream())
                    using (var sr = new StreamReader(ms))
                    {
                        await ms.WriteAsync(source, 0, source.Length);
                        ms.Seek(0, SeekOrigin.Begin);
                        return await sr.ReadToEndAsync();
                    }

                case 3:
                    // StreamReader constructor then BinaryWriter.Write then ReadToEndAsync
                    using (var ms = new MemoryStream())
                    using (var bw = new BinaryWriter(ms))
                    using (var sr = new StreamReader(ms))
                    {
                        bw.Write(source);
                        ms.Seek(0, SeekOrigin.Begin);
                        return await sr.ReadToEndAsync();
                    }

                case 4:
                    // MemoryStream constructor then ReadAsync then UnicodeStringReader.Read
                    using (var ms = new MemoryStream(source))
                    {
                        var buff = new Byte[ms.Length];
                        await ms.ReadAsync(buff, 0, buff.Length);
                        return UnicodeStringReader.Read(buff);
                    }

                /* BOM will remain as unreadable character. */
                case 5:
                    // MemoryStream constructor then ReadAsync then Encoding.GetString
                    using (var ms = new MemoryStream(source))
                    {
                        var buff = new Byte[ms.Length];
                        await ms.ReadAsync(buff, 0, buff.Length);
                        return Encoding.UTF8.GetString(buff);
                    }

                case 6:
                    // MemoryStream WriteAsync then ToArray then Encoding.GetString
                    using (var ms = new MemoryStream())
                    {
                        await ms.WriteAsync(source, 0, source.Length);
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }

                default:
                    throw new ArgumentOutOfRangeException("selector");
            }
        }
    }
}