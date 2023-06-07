using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace CsvToSqlCreate
{
    class TextFieldParserVB : ICsvParser
    {
        public string FilePath { get; set; }
        public char Delimiter { get; set; }
        public bool QuotedFields { get; set; }
        public ushort EncodingNumber { get; set; } = 1252;
        //string CultureInfoName = CultureInfo.CurrentCulture.Name;

        public IEnumerable<string[]> GetCsvRows(uint Skip = 0, uint Take = int.MaxValue) // 10000000)
        {
            uint linecount = 0;
            var parser = new TextFieldParser(FilePath, Encoding.GetEncoding(EncodingNumber), true);
            parser.SetDelimiters(Delimiter.ToString());
            parser.HasFieldsEnclosedInQuotes = QuotedFields;

            while (!parser.EndOfData)
            {
                linecount += 1;
                string[] line;
                try
                {
                    line = parser.ReadFields();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(parser.ErrorLine);
                    var s = parser.ErrorLine.Split(Delimiter);
                    Console.WriteLine(s.Count());

                    throw ex;
                }

                if (linecount > Skip)
                    yield return line;

                if (linecount >= Skip + Take)
                    break;
            }
        }

    }
}

