using System.Collections.Generic;

namespace CsvToSqlCreate
{
    interface ICsvParser
    {
        string FilePath { get; set; }
        char Delimiter { get; set; }
        bool QuotedFields { get; set; }
        ushort EncodingNumber { get; set; }

        IEnumerable<string[]> GetCsvRows(uint Skip = 0, uint Take = 2147483647);
    }
}