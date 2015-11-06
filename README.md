String Reader Catalog
=====================

A catalog of methods to read string by C#, especially focusing on handling of Unicode BOM

#BOM

 * The BOM (byte order mark) is 2-4 bytes of data which can be inserted at the head of actual string data in order to indicate the type of Unicode encoding.
  A concrete BOM can be obtained by [System.Text.Encoding.GetPreamble](https://msdn.microsoft.com/en-us/library/system.text.encoding.getpreamble.aspx) method.
 * [System.IO.StreamReader](https://msdn.microsoft.com/en-us/library/system.io.streamreader.aspx) class has private [DetectEncoding](http://referencesource.microsoft.com/#mscorlib/system/io/streamreader.cs,ea5187ae9c79350e) method and so can handle BOM correctly.
 * [System.IO.File.ReadAllText](https://msdn.microsoft.com/en-us/library/system.io.file.readalltext.aspx) method calls StreamReader class through private [InternalReadAllText](http://referencesource.microsoft.com/#mscorlib/system/io/file.cs,c193e57831aa94a9) method and so can handle BOM.
 * [System.Text.Encoding.GetString](https://msdn.microsoft.com/en-us/library/system.text.encoding.getstring.aspx) method doesn't remove BOM even if the source data contains BOM.
  It produces unreadable characters from BOM at the head of string.
 