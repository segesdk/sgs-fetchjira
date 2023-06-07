using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchJiraLogic.dto
{
    // Issue myDeserializedClass = JsonConvert.DeserializeObject<Issue>(myJsonResponse);

    //public class Author
    //{
    //    public string self { get; set; }
    //    public string accountId { get; set; }
    //    public string emailAddress { get; set; }
    //    public AvatarUrls avatarUrls { get; set; }
    //    public string displayName { get; set; }
    //    public bool active { get; set; }
    //    public string timeZone { get; set; }
    //    public string accountType { get; set; }
    //}

    //public class AvatarUrls
    //{
    //    public string _48x48 { get; set; }
    //    public string _24x24 { get; set; }
    //    public string _16x16 { get; set; }
    //    public string _32x32 { get; set; }
    //}

    public class Issue
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }

    //public class UpdateAuthor
    //{
    //    public string self { get; set; }
    //    public string accountId { get; set; }
    //    public string emailAddress { get; set; }
    //    public AvatarUrls avatarUrls { get; set; }
    //    public string displayName { get; set; }
    //    public bool active { get; set; }
    //    public string timeZone { get; set; }
    //    public string accountType { get; set; }
    //}

    public class Worklog
    {
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public List<Worklog> worklogs { get; set; }
    }

    public class Worklog2
    {
        public string self { get; set; }
        public Author author { get; set; }
        public UpdateAuthor updateAuthor { get; set; }
        public Comment comment { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public DateTime started { get; set; }
        public string timeSpent { get; set; }
        public int timeSpentSeconds { get; set; }
        public string id { get; set; }
        public string issueId { get; set; }
    }
}
