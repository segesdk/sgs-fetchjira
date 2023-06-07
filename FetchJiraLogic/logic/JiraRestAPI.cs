#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using FetchJiraLogic.dto;

namespace FetchJiraLogic
{
    public class JiraRestApi
    {
        private readonly string _jiraApiKey;

        public JiraRestApi(string  jiraApiKey)
        {
            _jiraApiKey = jiraApiKey;
        }

        public List<UpdatedWorkLogValue> GetUpdatedWorkLogIds(string since)
        {
            var client = CreateRestClient();
            var nextPage = $"worklog/updated?since={since}";


            var result = new List<UpdatedWorkLogValue>();
            bool lastPage;
            do
            {
                var request = new RestRequest(nextPage);
                var response = client.Get(request);
                var updatedWorkLogs = JsonConvert.DeserializeObject<UpdatedWorkLogs>(response.Content);
                result.AddRange(updatedWorkLogs.values);

                lastPage = updatedWorkLogs.lastPage;
                if (!lastPage)
                {
                    nextPage = updatedWorkLogs.nextPage;
                }
                

            } while (!lastPage); 

            return result;
        }

        public List<UpdatedWorkLogValue> GetDeletedWorkLogIds(string since)
        {
            var client = CreateRestClient();
            var nextPage = $"worklog/deleted?since={since}";


            var result = new List<UpdatedWorkLogValue>();
            bool lastPage;
            do
            {
                var request = new RestRequest(nextPage);
                var response = client.Get(request);
                var updatedWorkLogs = JsonConvert.DeserializeObject<UpdatedWorkLogs>(response.Content);
                result.AddRange(updatedWorkLogs.values);

                lastPage = updatedWorkLogs.lastPage;
                if (!lastPage)
                {
                    nextPage = updatedWorkLogs.nextPage;
                }


            } while (!lastPage);

            return result;
        }


        private RestClient CreateRestClient()
        {
            var client = new RestClient("https://segesinnovation.atlassian.net/rest/api/3/");
            client.Authenticator = new HttpBasicAuthenticator("jirabot@seges.dk", _jiraApiKey);
            client.UseDefaultSerializers();
            return client;
        }

        public List<WorkLogs> GetWorkLogs(List<int> worklogIds)
        {
           
            List<WorkLogs> workLogs = new List<WorkLogs>();
            int page = 0;
            int pageSize = 500;
            do
            {
                var request = new RestRequest("worklog/list", Method.Post);
                var idList = new
                {
                    ids = worklogIds.Page<int>(pageSize, page)
                };

                request.AddBody(idList);
                var client = CreateRestClient();
                var response = client.Post(request);
                List<WorkLogs>? result = JsonConvert.DeserializeObject<List<WorkLogs>>(response.Content);
                workLogs = workLogs.Concat(result).ToList();
                page++;

            } while (worklogIds.Page<int>(pageSize, page).Any());

           
            return workLogs;

        }

        public List<Issue> GetIssues(List<string> issueIds)
        {
            var client = CreateRestClient();
            var result = new List<Issue>();

            Parallel.ForEach(issueIds, issueId =>
            {
                var request = new RestRequest($"issue/{issueId}");
                var response = client.Get(request);
                var issue = JsonConvert.DeserializeObject<Issue>(response.Content);
                result.Add(issue);
            });
            
            return result;
        }
    }
}
