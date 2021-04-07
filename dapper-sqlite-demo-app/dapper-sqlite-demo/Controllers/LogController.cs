using dapper_sqlite_demo.LogMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogProvider _logProvider;
        private readonly ILogRepo _logRepo;

        public LogController(ILogProvider logProvider, ILogRepo logRepo)
        {
            _logProvider = logProvider;
            _logRepo = logRepo;
        }

        // GET api/requestResponseLog
        [HttpGet]
        public async Task<IEnumerable<Log>> Get()
        {
            return await _logProvider.Get();
        }

        // POST api/requestResponseLog
        [HttpPost]
        public async Task Post([FromBody] Log log)
        {
            await _logRepo.Create(log);
        }
    }
}
