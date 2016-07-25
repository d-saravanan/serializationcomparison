using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace GzipSample
{
    public static class CompressionWrapper
    {
        public static string Compress(string input)
        {
            using (var outStream = new MemoryStream())
            {
                using (var tinyStream = new GZipStream(outStream, CompressionMode.Compress))
                using (var mStream = new MemoryStream(Encoding.UTF8.GetBytes(input)))
                    mStream.CopyTo(tinyStream);

                return Convert.ToBase64String(outStream.ToArray());
            }
        }

        public static string DeCompress(string input)
        {
            using (var inStream = new MemoryStream(Convert.FromBase64String(input)))
            using (var bigStream = new GZipStream(inStream, CompressionMode.Decompress))
            using (var bigStreamOut = new MemoryStream())
            {
                bigStream.CopyTo(bigStreamOut);
                return Encoding.UTF8.GetString(bigStreamOut.ToArray());
            }
        }
    }
}
