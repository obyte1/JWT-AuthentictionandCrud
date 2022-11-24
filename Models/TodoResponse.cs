namespace week7_TODOAPP.Models
{
    public class TodoResponse
    {
        public List<TodoModel> Todo { get; set; } = new List<TodoModel>();
        public int Pages { get; set; } 
        public int CurrentPage { get; set; }

    }
}
