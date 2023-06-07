namespace FetchJiraLogic.dto;

public class Author
{
    public string self { get; set; }
    public string accountId { get; set; }
    public string emailAddress { get; set; }

    public string username
    {
        get
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return null;
            }

            return emailAddress.Split('@')[0].ToLower();
        }
    }

    public AvatarUrls avatarUrls { get; set; }
    public string displayName { get; set; }
    public bool active { get; set; }
    public string timeZone { get; set; }
    public string accountType { get; set; }
}