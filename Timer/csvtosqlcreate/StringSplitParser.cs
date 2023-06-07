using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvToSqlCreate
{
    class StringSplitParser : ICsvParser
    {
        public string FilePath { get; set; }
        public char Delimiter { get; set; }
        public bool QuotedFields { get; set; }
        public ushort EncodingNumber { get; set; } = 1252;

        //string CultureInfoName = CultureInfo.CurrentCulture.Name;


        public IEnumerable<string[]> GetCsvRows(uint Skip = 0, uint Take = int.MaxValue) // 10000000)
        {
            uint linecount = 0;

            using (StreamReader reader = new StreamReader(FilePath, Encoding.GetEncoding(EncodingNumber), true))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    linecount += 1;

                    if (linecount > Skip)
                    {
                        if (QuotedFields)
                        {
                            var sp = line.Split(Delimiter).Select(s => s.Trim('\"')).ToArray();
                            yield return sp;
                        }
                        else
                        {
                            yield return line.Split(Delimiter).ToArray();
                        }
                    }

                    if (linecount >= Skip + Take)
                        break;
                }
            }
        }
    }
}
