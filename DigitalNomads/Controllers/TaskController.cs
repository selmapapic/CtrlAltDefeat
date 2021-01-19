using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DigitalNomads.Data;
using DigitalNomads.Models.Task;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using DigitalNomads.Models.Account;
using DigitalNomads.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalNomads.Controllers
{
    public class TaskController : Controller
    {
        private CtrlAltDefeatDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
  
        public TaskController(CtrlAltDefeatDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> AllTasks()
        {
            TaskList lists = await GetAllTasksByIdAsync();

            return View(lists);
        }

        public async Task<int> GetAccountIdByUserIdAsync(string UserId)
        {
            AccountGetDetailsRes res = await _dbContext.Accounts
                .Where(a => a.UserId == UserId)
                .Select(a => new AccountGetDetailsRes
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Lat = a.Lat,
                    Long = a.Long

                }).FirstOrDefaultAsync();

            return res.Id;
        }

        public async Task<TaskList> GetAllTasksByIdAsync ()
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //int accId = await GetAccountIdByUserIdAsync(userId);

            //IList<TaskRes> res = await _dbContext.Tasks
            //    .Where(t => t.UserId == accId)
            //    .Select(t => new TaskRes
            //    {
            //        Id = t.Id,
            //        Title = t.Title,
            //        IsFinished = t.IsFinished,
            //        Deadline = t.Deadline,
            //        Description = t.Description,
            //        UserId = t.UserId
            //    }).ToListAsync();

            IList<TaskRes> finished = await GetAllFinishedTasksByIdAsync();
            IList<TaskRes> unfinished = await GetAllUnfinishedTasksByIdAsync();

            return new TaskList {
                FinishedTasks = finished,
                UnfinishedTasks = unfinished
            };
        }

        public async Task<IList<TaskRes>> GetAllFinishedTasksByIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int accId = await GetAccountIdByUserIdAsync(userId);

            IList<TaskRes> res = await _dbContext.Tasks
                .Where(t => t.UserId == accId && t.IsFinished == true)
                .Select(t => new TaskRes
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsFinished = t.IsFinished,
                    Deadline = t.Deadline,
                    Description = t.Description,
                    UserId = t.UserId
                }).ToListAsync();

            return res;
        }

        public async Task<IList<TaskRes>> GetAllUnfinishedTasksByIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int accId = await GetAccountIdByUserIdAsync(userId);

            IList<TaskRes> res = await _dbContext.Tasks
                .Where(t => t.UserId == accId && t.IsFinished == false)
                .Select(t => new TaskRes
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsFinished = t.IsFinished,
                    Deadline = t.Deadline,
                    Description = t.Description,
                    UserId = t.UserId
                }).ToListAsync();

            var sorted = res.OrderBy(t => t.Deadline).ToList();

            return sorted;
        }

        public async Task DeleteTaskAsync(int Id)
        {
            Duty task = await _dbContext.Tasks
                .Where(t => t.Id == Id)
                .FirstOrDefaultAsync();

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTaskAsync(Task task)
        {
            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IActionResult> ChangeTaskType (TaskRes task)
        {
            Duty taskDb = await _dbContext.Tasks
                .Where(t => t.Id == task.Id)
                .FirstOrDefaultAsync();


            taskDb.IsFinished = true;

            _dbContext.Tasks.Update(taskDb);
            _dbContext.SaveChanges();

            TaskList lists = await GetAllTasksByIdAsync();

            return View("AllTasks", lists);
        }

        public async Task<IActionResult> AddNewTask (string Title, string Description, DateTime DeadlineDate, DateTime DeadlineTime, int UserId)
        {
            DateTime deadline = new DateTime(DeadlineDate.Year, DeadlineDate.Month, DeadlineDate.Day, DeadlineTime.Hour, DeadlineTime.Minute, DeadlineTime.Second);
            Duty newTask = new Duty();
            newTask.Title = Title;
            newTask.IsFinished = false;
            newTask.Deadline = deadline;
            newTask.Description = Description;
            newTask.UserId = UserId;

            _dbContext.Add(newTask);
            _dbContext.SaveChanges();

            TaskList lists = await GetAllTasksByIdAsync();

            return View("AllTasks", lists);
        }

        public async Task<IActionResult> RemoveTask(TaskRes task)
        {
            Duty taskDb = await _dbContext.Tasks
                .Where(t => t.Id == task.Id)
                .FirstOrDefaultAsync();

            _dbContext.Tasks.Remove(taskDb);
            _dbContext.SaveChanges();

            TaskList lists = await GetAllTasksByIdAsync();

            return View("AllTasks", lists);
        }
    }
}
