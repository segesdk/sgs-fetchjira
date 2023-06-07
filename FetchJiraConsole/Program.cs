using FetchJiraLogic;
using FetchJiraLogic.dto;
using static System.Net.Mime.MediaTypeNames;

namespace FetchJiraConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var jiraRestApiKey = "****";
            string[] usernames = { "perh", "mbjerre", "mac", "jblo", "mcm", "erk" };
            //string[] usernames = { "perh" };

            var sinceDateTime = new DateTime(2023, 01, 01, 0, 0, 0).ToUniversalTime();
            //var sinceDateTime = DateTime.UtcNow.AddDays(-1);
            //var sinceDateTime = DateTime.UtcNow.AddDays(-1);
            //var sinceDateTime = DateTime.UtcNow.AddHours(-1);

            List<WorklogResult> j = FetchJira.FetchJiraData(jiraRestApiKey, sinceDateTime, usernames);

            var lh = new ListHelp<WorklogResult>(j);

            //var s = lh.CsvLinesAsString;

            var filepath = @$"c:\Users\perh\source\timer-{sinceDateTime.ToString("yyyyMMddHHmmss")}-{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.csv";
            Console.WriteLine(filepath);
            
            lh.CsvLinesToFile(filepath);

            Console.WriteLine(j.Count());
        }
    }
}