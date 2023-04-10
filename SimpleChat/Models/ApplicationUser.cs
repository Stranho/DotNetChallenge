using Microsoft.AspNetCore.Identity;

namespace SimpleChat.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public virtual ICollection<PrivateMessage> ChatMessagesFromUsers { get; set; }
        public virtual ICollection<PrivateMessage> ChatMessagesToUsers { get; set; }
        public ApplicationUser()
        {
            ChatMessagesFromUsers = new HashSet<PrivateMessage>();
            ChatMessagesToUsers = new HashSet<PrivateMessage>();
        }
    }
}
