namespace MemberManager.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string LessonName { get; set; }
        public string Instructor { get; set; }
        public int Quota { get; set; }
    }
}
