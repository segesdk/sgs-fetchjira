using NConsoler;
using System;
using System.Diagnostics;
using System.IO;

namespace CsvToSqlCreate
{
    public enum Parser
    {
        Simple, Complex
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                var args1 = new string[] {

                //@"c:\Users\perh\source\Arbejdsfiler\PERH\powershellsqlcsv\authors.csv","dbo.test","/d:,","/c","/q"
                //@"c:\Users\perh\source\Arbejdsfiler\PERH\powershellsqlcsv\authors.csv","dbo.test","/d:,","/c"
                //@"c:\Users\perh\source\Arbejdsfiler\PERH\powershellsqlcsv\authors.csv","dbo.test","/d:,"
                //@"c:\Users\perh\source\Arbejdsfiler\PERH\powershellsqlcsv\authors.csv","dbo.test","/d:,","/O:test.sql"
                //@"c:\Users\perh\source\WorkInProgress\DivPs\test.dat","src.test",
                @"c:\Users\perh\OneDrive - SEGES P S\WorkInProgress\DivPs\PsDatabaseModule\Customers.csv",
                    //"/T:src.test",
                    //@"/O:C:\Users\perh\source\WorkInProgress\PigCoin\InitialData.sql",
                    //"/D:;",
                    //"/n", 
                    "/q",
                    "/c",
                    "/s"
                };

                Consolery.Run(typeof(Program), args1);
                Console.ReadLine();
            }
            else
            {
                Consolery.Run();
            }
        }

        [Action("Analyze a complete CSV file and generate Sql Server CREATE TABLE statements")]
        public static void Main2(
            [Required(Description = "CSV file path")] string FilePath,
            [Optional("sample","T",Description = "Sql table name (default is [sample])")] string TableName,
            [Optional(",", "D", Description = "Delimiter (default is ,)")] string Delimiter,
            [Optional(null, "O", Description = "Output Sql file path")] string OutfilePath,
            [Optional(false, "c", Description = "Use complex parser switch")] bool ComplexParser,
            [Optional(false, "q", Description = "Fields have quotes switch")] bool QuotedFields,
            [Optional(false, "n", Description = "Keep original column names switch")] bool OriginalColumnNames,
            [Optional(false, "s", Description = "Suppress analysis switch")] bool SuppressAnalysis
        // TODO: Encoding, Culture, Suppress analysis-summary
        // Parameters as class
        )
        {
            Console.WriteLine($"Analyzing {FilePath}");

            var csvtosql = new ParseCsvToSql(ComplexParser);

            csvtosql.FilePath = FilePath;
            csvtosql.Delimiter = Delimiter[0];
            csvtosql.QuotedFields = QuotedFields;
            csvtosql.SqlTableName = TableName;
            csvtosql.OriginalColumnNames = OriginalColumnNames;
            csvtosql.SuppressAnalysis = SuppressAnalysis;

            var sql = csvtosql.GenerateSql();

            if (OutfilePath == null)
            {
                Console.WriteLine(sql);
            }
            else
            {
                File.WriteAllText(OutfilePath, sql);
                Console.WriteLine($"Created {OutfilePath}");
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
