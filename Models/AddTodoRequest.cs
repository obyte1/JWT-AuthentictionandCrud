namespace week7_TODOAPP.Models
{
    public class AddTodoRequest
    {
        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public string TaskStatus { get; set; }
    }
}
