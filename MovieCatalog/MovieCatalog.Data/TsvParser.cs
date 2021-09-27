using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace MovieCatalog.Data
{
    public class TsvParser
    {
        public static IEnumerable<IReadOnlyDictionary<string, string>> ParseTsv(string filePath)
        {
            using var reader = new StreamReader(new GZipStream(File.OpenRead(filePath), CompressionMode.Decompress));

            var headers = reader.ReadLine().Split('\t');
            while (reader.ReadLine() is var line && !string.IsNullOrWhiteSpace(line))
            {
                yield return new Dictionary<string, string>(line.Split('\t').Select((item, index) => new KeyValuePair<string, string>(headers[index], item != "\\N" ? item : null)));
            }
        }
    }
}
