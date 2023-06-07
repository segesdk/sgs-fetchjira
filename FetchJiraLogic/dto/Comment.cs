using System.Collections.Generic;

namespace FetchJiraLogic.dto;

public class Comment
{
    public List<object> comments { get; set; }
    public string self { get; set; }
    public int maxResults { get; set; }
    public int total { get; set; }
    public int startAt { get; set; }
    public string type { get; set; }
    public int version { get; set; }
    public List<object> content { get; set; }
}