namespace week7_TODOAPP.Models
{
    public class UpdateTodoRequest
    {
        public string TaskName { get; set; }

        public DateTime TaskDate { get; set; }

        public string TaskStatus { get; set; }
    }
}
