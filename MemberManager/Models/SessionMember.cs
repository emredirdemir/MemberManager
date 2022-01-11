namespace MemberManager.Models
{
    public class SessionMember
    {
        public int SessionId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
