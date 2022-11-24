namespace week7_TODOAPP.Models
{
    public class TodoModel
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public string TaskStatus { get; set; }
    }
}
