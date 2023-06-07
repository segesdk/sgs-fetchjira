using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace CsvToSqlCreate
{
    public class ParseCsvToSql
    {
        // Object parameters

        public string FilePath;
        public char Delimiter = ',';
        public uint MaxLinesToAnalyze = int.MaxValue;

        public string SqlTableName = "TEST";

        public string CultureInfoName = CultureInfo.CurrentCulture.Name;
        public ushort EncodingNumber = 1252;

        public bool OriginalColumnNames = false;
        public bool VarcharOnly = false;

        public bool ComplexParser = false;
        public bool QuotedFields = false;
        public bool SuppressAnalysis = false;


        public ParseCsvToSql()
        {
        }

        public ParseCsvToSql(bool ComplexParser)
        {
            this.ComplexParser = ComplexParser;
        }

        public string GenerateSql()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(CultureInfoName);

            ICsvParser csvparser;

            if (ComplexParser)
                csvparser = new TextFieldParserVB();
            else
                csvparser = new StringSplitParser();

            csvparser.FilePath = FilePath;
            csvparser.Delimiter = Delimiter;
            csvparser.QuotedFields = QuotedFields;
            csvparser.EncodingNumber = EncodingNumber;

            var fieldformats = UpdateCsvFieldHeaders(csvparser);

            var sql = GenerateTableDefinition(csvparser, fieldformats, SqlTableName, VarcharOnly);

            return sql;
        }

        private List<FieldFormat> UpdateCsvFieldHeaders(ICsvParser csvparser)
        {
            var columns = csvparser.GetCsvRows(Take: 1).First().Select(s => s.Trim('\"')).ToList();

            if (!OriginalColumnNames)
            {
                for (int i = 0; i < columns.Count(); i++)
                {
                    foreach (var letter in ColumnNameReplacementMap.Keys)
                    {
                        columns[i] = columns[i].Replace(letter, ColumnNameReplacementMap[letter]);
                    }
                }
            }

            var fieldFormats = new List<FieldFormat>();
            columns.ForEach(e => fieldFormats.Add(new FieldFormat() { Name = e }));

            return fieldFormats;
        }

        static readonly Dictionary<string, string> ColumnNameReplacementMap = new Dictionary<string, string>
        {
            { "æ", "ae" },
            { "Æ", "AE" },
            { "ø", "oe" },
            { "Ø", "OE" },
            { "å", "aa" },
            { "Å", "AA" },
            { " ", "_" },
            { "@", "_" },
            { "$", "_" },
            { "*", "_" },
            { ".", "_" },
            { "-", "_" },
            { "!", "_" },
            { "[", "_" },
            { "]", "_" },
            { "'", "_" },
            { "\"", "_" }
        };

        private string GenerateTableDefinition(ICsvParser csvparser, List<FieldFormat> fieldformats, string tableName, bool varcharonly)
        {
            var linecount = 0;
            var sw = Stopwatch.StartNew();

            int n = 0;
            string[] s;

            try
            {
                foreach (var row in csvparser.GetCsvRows(Skip: 1, Take: MaxLinesToAnalyze))
                {
                    s = row;
                    linecount += 1;
                    var rowcolumns = row.ToList();
                    for (int i = 0; i < rowcolumns.Count(); i++)
                    {
                        n = i;
                        fieldformats[i].Parse(rowcolumns[i], linecount);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception on row {linecount} {n}");
                throw ex;
            }

            var sb = new StringBuilder();

            var first = true;

            sb.AppendLine("/*");
            sb.AppendLine("Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
            sb.AppendLine($"From: [{FilePath}]");
            sb.AppendLine($"Delimiter: [{Delimiter}] QuotedFields: [{QuotedFields}] Complex parser: [{ComplexParser}] OriginalColumnNames: [{OriginalColumnNames}]");
            sb.AppendLine($"Encoding: [{EncodingNumber}] Culture: [{CultureInfoName}]");

            sb.AppendLine(string.Format("Analyzed {0} rows in {1} ms", linecount, sw.ElapsedMilliseconds));

            if (!SuppressAnalysis)
            {
                sb.AppendLine();
                foreach (var fieldformat in fieldformats)
                {
                    if (fieldformat.StringFormat == FieldFormat.Format.Double || fieldformat.StringFormat == FieldFormat.Format.Int)
                    {
                        sb.AppendLine(
                            string.Format("{0,-20}: {1}, HasNull: {2}, MinValue: {3}, MaxValue: {4}", fieldformat.Name,
                                fieldformat.StringFormat,
                                fieldformat.HasNull, fieldformat.MinValue, fieldformat.MaxValue));
                    }
                    else
                    {
                        sb.AppendLine(
                            string.Format("{0,-20}: {1}, HasNull: {2}, MinLength: {3}, MaxLength: {4}", fieldformat.Name,
                                fieldformat.StringFormat,
                                fieldformat.HasNull, fieldformat.MinLength, fieldformat.MaxLength));
                    }

                }
            }
            sb.AppendLine("*/");
            sb.AppendLine("");

            // Set [] for schema & table
            var iSchemaSep = tableName.IndexOf('.');
            var tableSchemaName = iSchemaSep < 0 ? $"[{tableName}]"
                : $"[{tableName.Substring(0, iSchemaSep).Trim('[', ']')}].[{tableName.Substring(iSchemaSep + 1, tableName.Length - iSchemaSep - 1).Trim('[', ']')}]";

            sb.AppendLine(string.Format($"IF OBJECT_ID(N'{tableSchemaName}', 'U') IS NOT NULL DROP TABLE {tableSchemaName};"));
            sb.AppendLine(string.Format($"CREATE TABLE {tableSchemaName} ("));

            foreach (var fieldformat in fieldformats)
            {
                if (first)
                    first = false;
                else
                    sb.AppendLine(",");

                sb.Append(string.Format($"\t[{fieldformat.Name}]"));

                if (varcharonly)
                {
                    int l = 200;
                    if (fieldformat.StringFormat == FieldFormat.Format.String || fieldformat.StringFormat == FieldFormat.Format.Unknown)
                    {
                        if (fieldformat.MaxLength > 100)
                        {
                            l = ((fieldformat.MaxLength / 100) * 100) + l;
                        }
                    }
                    sb.Append(string.Format(" [varchar]({0})", l));
                }
                else
                {
                    if (fieldformat.StringFormat == FieldFormat.Format.DateTime)
                        sb.Append(fieldformat.HasTime ? " [datetime]" : " [date]");

                    if (fieldformat.StringFormat == FieldFormat.Format.Int)
                    {
                        if (fieldformat.MinValue >= 0 && fieldformat.MaxValue <= 255)
                            sb.Append(" [tinyint]");
                        else if (fieldformat.MinValue >= short.MinValue && fieldformat.MaxValue <= short.MaxValue)
                            sb.Append(" [smallint]");
                        else if (fieldformat.MinValue >= int.MinValue && fieldformat.MaxValue <= int.MaxValue)
                            sb.Append(" [int]");
                        else
                            sb.Append(" [bigint]");
                    }

                    if (fieldformat.StringFormat == FieldFormat.Format.Double)
                        sb.Append(" [real]"); // Decimal?

                    if (fieldformat.StringFormat == FieldFormat.Format.String || fieldformat.StringFormat == FieldFormat.Format.Unknown)
                    {
                        if (fieldformat.MaxLength > 10)
                        {
                            var l = (int)(fieldformat.MaxLength * 1.10); // Increase 10%
                            l = ((l / 10) + 1) * 10; // Round up
                            sb.Append(string.Format(" [varchar]({0})", l));
                        }
                        else
                        {
                            var l = fieldformat.MaxLength == 0 ? 1 : fieldformat.MaxLength;
                            sb.Append(string.Format(" [char]({0})", l));
                        }
                    }

                    if (!fieldformat.HasNull) sb.Append(" NOT");
                    sb.Append(" NULL");
                }
            }
            sb.AppendLine("");
            sb.AppendLine(");");

            return sb.ToString();
        }
    }
}