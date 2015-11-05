String Reader Catalog
=====================

A catalog of methods to read string by C#, especially focusing on handling of Unicode BOM

##Note

 * The types of BOM are described in [System.Text.Encoding.GetPreamble](https://msdn.microsoft.com/en-us/library/system.text.encoding.getpreamble.aspx) method.
 * [System.IO.StreamReader](https://msdn.microsoft.com/en-us/library/system.io.streamreader.aspx) class has private [DetectEncoding](http://referencesource.microsoft.com/#mscorlib/system/io/streamreader.cs,ea5187ae9c79350e) method and so can correctly handle BOM.
 * [System.Text.Encoding.GetString](https://msdn.microsoft.com/en-us/library/system.text.encoding.getstring.aspx) method doesn't remove BOM even if the source bytes contain BOM.
 * [System.IO.File.ReadAllText](https://msdn.microsoft.com/en-us/library/system.io.file.readalltext.aspx) method calls StreamReader class through private [InternalReadAllText](http://referencesource.microsoft.com/#mscorlib/system/io/file.cs,c193e57831aa94a9) method.
