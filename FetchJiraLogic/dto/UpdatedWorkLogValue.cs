using System.Collections.Generic;

namespace FetchJiraLogic.dto;

public class UpdatedWorkLogValue
{
    // ReSharper disable once InconsistentNaming
    public int worklogId { get; set; }
    // ReSharper disable once InconsistentNaming
    public object updatedTime { get; set; }
    // ReSharper disable once InconsistentNaming
    public List<object> properties { get; set; }
}