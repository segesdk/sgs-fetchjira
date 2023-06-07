using FetchJiraLogic.dto;
using Newtonsoft.Json;
using System.Globalization;

namespace FetchJiraLogic
{
    public class FetchJira
    {
        public static List<WorklogResult> FetchJiraData(string jiraRestApiKey, DateTime sinceDateTime, string[] usernames)
        {
            var jiraRestApi = new JiraRestApi(jiraRestApiKey);

            var sinceUnixTime = Time.ConvertToUnixTimestampMs(sinceDateTime).ToString(CultureInfo.InvariantCulture);

            // Get updated worklog ids
            List<UpdatedWorkLogValue> workLogIds = jiraRestApi.GetUpdatedWorkLogIds(sinceUnixTime);

            // Get updated worklogs 
            List<WorkLogs> worklogs = jiraRestApi.GetWorkLogs(workLogIds.Select(x => x.worklogId).ToList());

            // Filter app worklogs
            var filteredWorkLogs = worklogs.Where(x => x.author.accountType != "app").ToList();

            //if (!string.IsNullOrEmpty(username))
            //{
            //    filteredWorkLogs = filteredWorkLogs.Where(x => x.author.username != null && x.author.username.Equals(username)).ToList();
            //}

            if (!(usernames.Length == 0))
            {
                filteredWorkLogs = filteredWorkLogs.Where(x => x.author.username != null && usernames.Contains(x.author.username)).ToList();
            }

            // Get issues
            List<Issue> issues = jiraRestApi.GetIssues(filteredWorkLogs.Select(x => x.issueId).ToList());

            //File.WriteAllText(@"c:\Users\perh\source\worklogids.json", JsonConvert.SerializeObject(workLogIds));
            //File.WriteAllText(@"c:\Users\perh\source\filteredworklogs.json", JsonConvert.SerializeObject(filteredWorkLogs));
            //File.WriteAllText(@"c:\Users\perh\source\issues.json", JsonConvert.SerializeObject(issues));

            var workLogResults = new List<WorklogResult>();
            var createDateTime = DateTime.Now;

            filteredWorkLogs.ForEach(worklog =>
            {
                try
                {
                    var issue = issues.First(i => i.id.Equals(worklog.issueId));

                    var wl = new WorklogResult
                    {
                        id = worklog.id,
                        email = worklog.author.emailAddress.ToLower(),
                        username = worklog.author.emailAddress.Split('@')[0],
                        timeSpentSeconds = worklog.timeSpentSeconds,
                        issueId = issue.id,
                        issueKey = issue.key,
                        // https://segesinnovation.atlassian.net/browse/IGN-284
                        started = worklog.started + TimeSpan.FromHours(2),
                        caseNumber = issue.fields.customfield_10034?.value,
                        taskNumber = issue.fields.customfield_10034?.child?.value,
                        summary = issue.fields.summary,
                        labels = string.Join(",", issue.fields.labels),
                        projectkey = issue.fields.project.key,
                        projectname = issue.fields.project.name,
                        parentissuekey = issue.fields.parent?.key,
                        parentissuetypename = issue.fields.parent?.fields.issuetype.name,
                        parentissuesummary = issue.fields.parent?.fields.summary,
                        createdatetime = createDateTime
                    };

                    workLogResults.Add(wl);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error parsing worklog id {worklog.id} {e.Message}");
                    // this._logger.LogError($"Error parsing worklog id {worklog.id}", e);
                    // Continue    
                }

            });

            // Get deleted worklog ids
            List<UpdatedWorkLogValue> workLogIdsdeleted = jiraRestApi.GetDeletedWorkLogIds(sinceUnixTime);

            foreach (var worklog in workLogIdsdeleted)
            {
                var wl = new WorklogResult
                {
                    id = worklog.worklogId.ToString()
                };
                workLogResults.Add(wl);
            }
            
            return workLogResults;
        }
    }
}