using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class TodoTaskController : Controller
{
    private readonly ApplicationDbContext _db;

    public TodoTaskController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        //var objTodoTaskList=_db.Tasks.ToList(); //instead we can use the below line
        //IEnumerable<TodoTask> objTodoTaskList = _db.Tasks;
        var objTodoTaskList = _db.Tasks.Where(t => !t.IsCompleted).ToList();
        return View(objTodoTaskList);
    }


    //Create GET
    public IActionResult Create()
    {
        //var objTodoTaskList=_db.Tasks.ToList(); //instead we can use the below line

        return View();
    }


    //Create POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TodoTask obj)
    {
        if (obj.TaskName == obj.Description.ToString())
        {
            ModelState.AddModelError("Description", "The Task Name and Description cannot be same");
        }
        //var objTodoTaskList=_db.Tasks.ToList(); //instead we can use the below line
        if (ModelState.IsValid)
        {
            _db.Tasks.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Task created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);

    }

    //Edit GET
    public IActionResult Edit(int? id)

    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var objTodoTaskList=_db.Tasks.ToList(); //instead we can use the below line
        var taskFromDb = _db.Tasks.Find(id);
        //var taskFromDbFirst=_db.Tasks.FirstOrDefault(u=>u.Id==id);
        //var taskFromDbSingle=_db.Tasks.SingleOrDefault(u=>u.Id==id);

        if (taskFromDb == null)
        {
            return NotFound();
        }
        return View(taskFromDb);
    }


    //Edit POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(TodoTask obj)
    {
        if (obj.TaskName == obj.Description.ToString())
        {
            ModelState.AddModelError("Description", "The Task Name and Description cannot be same");
        }
        //var objTodoTaskList=_db.Tasks.ToList(); //instead we can use the below line
        if (ModelState.IsValid)
        {
            _db.Tasks.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Task Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);

    }


    // DELETE GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var taskFromDb = _db.Tasks.Find(id);
        if (taskFromDb == null)
        {
            return NotFound();
        }
        return View(taskFromDb);
    }
    //DELETE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Tasks.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Tasks.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Task Deleted Successfully";
        return RedirectToAction("Index");
    }

    ////////
    //History GET
    public IActionResult MarkAsDone(int id)
    {
        var task = _db.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }

        task.IsCompleted = true;
        task.DoneDateTime = DateTime.Now;
        _db.Tasks.Update(task);
        _db.SaveChanges();

        TempData["success"] = "Task marked as done successfully!";
        return RedirectToAction("Index");
    }

    //History POST
    public IActionResult History()
    {
        var completedTasks = _db.Tasks.Where(t => t.IsCompleted).ToList();
        return View(completedTasks);
    }

    //////
}
