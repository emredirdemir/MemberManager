namespace MemberManagement.entity
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string MailAddress { get; set; }
    }
}
