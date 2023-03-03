using System.IO;

namespace TTS_LIB
{
    public static class Utils
    {
        private const int speech_synthesis_limit = 5000 - 10;
        public static void WriteToFile(string path, byte[] data)
        {
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(data);
                }
            }

        }


        /// <summary>
        /// split a very long string into smaller strings 
        /// Had better split by periods
        /// </summary>
        /// <param name="long_string"></param>
        /// <returns></returns>
        public static IList<string> SplitLongString(string long_string)
        {
            var length = long_string.Length;
            var ls = new List<string>();

            int start = 0;
            int end = Math.Min(speech_synthesis_limit - 1, length - 1);

            if (length < speech_synthesis_limit)
            {
                return new List<string> { long_string };
            }

            while (true)
            {
                var period_index = long_string.LastIndexOf('.', end);
                if (period_index >= start)
                {
                    end = period_index;
                }
                ls.Add(long_string.Substring(start, end - start + 1));
                start = end + 1;
                end = start + speech_synthesis_limit - 1;
                if (end >= length)
                {
                    ls.Add(long_string.Substring(start));
                    break;
                }
            }


            return ls;
        }
    }
}
