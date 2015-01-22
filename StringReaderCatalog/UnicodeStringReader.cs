using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StringReaderCatalog
{
    public class UnicodeStringReader
    {
        private static readonly Encoding[] unicodeEncodings =
        {
            Encoding.UTF8, // UTF-8
            Encoding.Unicode, // UTF-16 Little Endian
            Encoding.BigEndianUnicode, // UTF-16 Big Endian
            Encoding.UTF32, // UTF-32 Little Endian
            new UTF32Encoding(true, false) // UTF-32 Big Endian
        };

        public static string Read(byte[] source)
        {
            foreach (var encoding in unicodeEncodings)
            {
                string outcome;
                if (TryDetectRead(source, out outcome, encoding))
                    return outcome;
            }

            throw new NotSupportedException("Character encoding must be Unicode.");
        }

        private static bool TryDetectRead(byte[] source, out string outcome, Encoding encoding)
        {
            var preamble = encoding.GetPreamble();
            if (!preamble.Any() || !preamble.SequenceEqual(source.Take(preamble.Length)))
            {
                outcome = null;
                return false;
            }

            Debug.WriteLine("Encoding: {0}", encoding);

            outcome = encoding.GetString(source.Skip(preamble.Length).ToArray());
            return true;
        }

        public static Encoding DetectUnicodeEncoding(byte[] source)
        {
            foreach (var encoding in unicodeEncodings)
            {
                var preamble = encoding.GetPreamble();
                if (preamble.SequenceEqual(source.Take(preamble.Length)))
                    return encoding;
            }

            return null;
        }
    }
}