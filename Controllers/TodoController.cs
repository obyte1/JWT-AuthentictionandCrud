using JWT_Practice.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using week7_TODOAPP.Models;

namespace week7_TODOAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TodoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodo()
        {
            return Ok(await dbContext.todoModels.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTodoById([FromRoute] Guid id)
        {
            var todolis = await dbContext.todoModels.FindAsync(id);

            if(todolis == null)
            {
                return NotFound();
            }
            return Ok(todolis);
        }


        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addTodoRequest)
        {
            var todoModel = new TodoModel()
            {
                Id = Guid.NewGuid(),
                TaskName = addTodoRequest.TaskName,
                TaskDate = addTodoRequest.TaskDate,
                TaskStatus = addTodoRequest.TaskStatus
            };

            await dbContext.todoModels.AddAsync(todoModel);
            await dbContext.SaveChangesAsync();
            return Ok(todoModel);  
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateTodoRequest)
        {
            var  todolis = await dbContext.todoModels.FindAsync(id);
            if(todolis != null)
            {
                todolis.TaskName = updateTodoRequest.TaskName; 
                todolis.TaskDate = updateTodoRequest.TaskDate;
                todolis.TaskStatus = updateTodoRequest.TaskStatus;

                await dbContext.SaveChangesAsync();
                
                return Ok(todolis);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todolis = await dbContext.todoModels.FindAsync(id);
            if(todolis != null)
            {
                dbContext.Remove(todolis);
                await dbContext.SaveChangesAsync();
                return Ok(todolis);

            }
            return NotFound();
        }

        //[HttpGet("{search}")]        
        //public async Task<IActionResult> getempname(string search)
        //{
        //    var result = dbContext.todoModels.Where(x => x.TaskName.StartsWith(search) || search == null).ToList();
        //    return Ok(result);
        //}


        [HttpGet("{page}")]
        public async Task<ActionResult<List<TodoModel>>> GetTodos(int page)
        {
            if(dbContext.todoModels == null)
                return NotFound();

            var pageResults = 3f;
            var pageCount = Math.Ceiling(dbContext.todoModels.Count() / pageResults);
            

            var todos = await dbContext.todoModels
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new TodoResponse
            {
                Todo = todos,
                CurrentPage = page,
                Pages = (int)pageCount

            };


            return Ok(response);
        }
    }
}
