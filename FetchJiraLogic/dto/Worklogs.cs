using System;

namespace FetchJiraLogic.dto
{
    // Issue myDeserializedClass = JsonConvert.DeserializeObject<List<Issue>>(myJsonResponse);

    public class WorkLogs
    {
        public string self { get; set; }
        public Author author { get; set; }
        public UpdateAuthor updateAuthor { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public DateTime started { get; set; }
        public string timeSpent { get; set; }
        public int timeSpentSeconds { get; set; }
        public string id { get; set; }
        public string issueId { get; set; }
    }
}
