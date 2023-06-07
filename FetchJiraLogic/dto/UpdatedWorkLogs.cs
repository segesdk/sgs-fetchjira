using System;
using System.Collections.Generic;

namespace FetchJiraLogic.dto
{
    // Issue myDeserializedClass = JsonConvert.DeserializeObject<Issue>(myJsonResponse);
    public class UpdatedWorkLogs
    {
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public List<UpdatedWorkLogValue> values { get; set; }
        // ReSharper disable once InconsistentNaming
        public long since { get; set; }
        // ReSharper disable once InconsistentNaming
        public long until { get; set; }
        // ReSharper disable once InconsistentNaming
        public string self { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool lastPage { get; set; }

        // ReSharper disable once InconsistentNaming
        public string nextPage { get; set; }
    }
}
