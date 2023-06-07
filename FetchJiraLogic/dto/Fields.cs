using System;
using System.Collections.Generic;

namespace FetchJiraLogic.dto;

public class Fields
{
    public DateTime statuscategorychangedate { get; set; }
    public Issuetype issuetype { get; set; }
    public Parent parent { get; set; }
    public int timespent { get; set; }
    public Project project { get; set; }
    public List<FixVersion> fixVersions { get; set; }
    public int aggregatetimespent { get; set; }
    public Resolution resolution { get; set; }
    public object customfield_10036 { get; set; }
    public object customfield_10037 { get; set; }
    public List<object> customfield_10029 { get; set; }
    //public DateTime resolutiondate { get; set; }
    public int workratio { get; set; }
    public object lastViewed { get; set; }
    public Issuerestriction issuerestriction { get; set; }
    public Watches watches { get; set; }
    public DateTime created { get; set; }
    public object customfield_10020 { get; set; }
    public object customfield_10021 { get; set; }
    public object customfield_10022 { get; set; }
    public Priority priority { get; set; }
    public object customfield_10023 { get; set; }
    public object customfield_10024 { get; set; }
    public string customfield_10025 { get; set; }
    public List<object> labels { get; set; }
    public object customfield_10026 { get; set; }
    public object customfield_10016 { get; set; }
    public object customfield_10017 { get; set; }
    public Customfield10018 customfield_10018 { get; set; }
    public string customfield_10019 { get; set; }
    public object aggregatetimeoriginalestimate { get; set; }
    public object timeestimate { get; set; }
    public List<object> versions { get; set; }
    public List<object> issuelinks { get; set; }
    public Assignee assignee { get; set; }
    public DateTime updated { get; set; }
    public Status status { get; set; }
    public List<object> components { get; set; }
    public object timeoriginalestimate { get; set; }
    public object customfield_10052 { get; set; }
    public object description { get; set; }
    public object customfield_10010 { get; set; }
    public string customfield_10014 { get; set; }
    public object customfield_10015 { get; set; }
    public Timetracking timetracking { get; set; }
    public object security { get; set; }
    public object customfield_10008 { get; set; }
    public object customfield_10009 { get; set; }
    public object aggregatetimeestimate { get; set; }
    public List<object> attachment { get; set; }
    public string summary { get; set; }
    public Creator creator { get; set; }
    public List<object> subtasks { get; set; }
    public object customfield_10041 { get; set; }
    public Reporter reporter { get; set; }
    public Aggregateprogress aggregateprogress { get; set; }
    public string customfield_10000 { get; set; }
    public object customfield_10001 { get; set; }
    public object customfield_10002 { get; set; }
    public object customfield_10003 { get; set; }
    public object customfield_10004 { get; set; }
    public Customfield10034 customfield_10034 { get; set; }

    public object customfield_10038 { get; set; }
    public object customfield_10039 { get; set; }
    public object duedate { get; set; }
    public Progress progress { get; set; }
    public Comment comment { get; set; }
    public Votes votes { get; set; }
    public Worklog worklog { get; set; }
}