using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.LogMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEfController : ControllerBase
    {
        private readonly LogDbContext _dbContext;

        public LogEfController(LogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var logs = await _dbContext.Logs.ToListAsync();
            return new JsonResult(logs);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Log log)
        {
            _dbContext.Add(log);
            await _dbContext.SaveChangesAsync();
            return new JsonResult(log.Id);
        }
    }
}
