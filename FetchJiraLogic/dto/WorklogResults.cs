using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchJiraLogic.dto
{
    public class WorklogResults
    {
        public List<WorklogResult> worklogResults { set; get; }
    }

    public class WorklogResult
    {
        public string username { get; set; }
        public string email { get; set; }
        public int timeSpentSeconds { get; set; }
        public string issueId { get; set; }
        public string issueKey { get; set; }
        public DateTime started { get; set; }
        public string id { get; set; }
        public string caseNumber { get; set; }
        public string taskNumber { get; set; }
        public string summary { get; set; }

        public string labels { get; set; }
        public string projectkey { get; set; }
        public string projectname { get; set; }
        public string parentissuekey { get; set; }
        public string parentissuetypename { get; set; }
        public string parentissuesummary { get; set; }

        public DateTime createdatetime { get; set;  }
    }
}
