using dapper_sqlite_demo.RequestResponseLogMaster;
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
    public class RequestResponseLogController : ControllerBase
    {
        private readonly IRequestResponseLogProvider _logProvider;
        private readonly IRequestResponseLogRepo _logRepo;

        public RequestResponseLogController(IRequestResponseLogProvider logProvider, IRequestResponseLogRepo logRepo)
        {
            _logProvider = logProvider;
            _logRepo = logRepo;
        }

        // GET api/requestResponseLog
        [HttpGet]
        public async Task<IEnumerable<RequestResponseLog>> Get()
        {
            return await _logProvider.Get();
        }

        // POST api/requestResponseLog
        [HttpPost]
        public async Task Post([FromBody] RequestResponseLog log)
        {
            await _logRepo.Create(log);
        }
    }
}
