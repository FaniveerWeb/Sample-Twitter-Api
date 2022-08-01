using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwitterApiDotNet.Models;

namespace TwitterApiDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public TwitterDbContext TwitterDbContext { get; }
        public ILogger Logger { get; }

        public EmployeeController(TwitterDbContext twitterDbContext, ILogger<EmployeeController> logger)
        {
            TwitterDbContext = twitterDbContext;
            Logger = logger;
        }

        [HttpGet("{id}")]
        public Employee GetEmployeeByID(int id)
        {
            Logger.LogInformation("Get the employee information");
            return TwitterDbContext.Employees.SingleOrDefault(e => e.EmployeeID == id);
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            TwitterDbContext.Employees.Add(employee);
            TwitterDbContext.SaveChanges();
            return Ok(employee);
        }

    }
}