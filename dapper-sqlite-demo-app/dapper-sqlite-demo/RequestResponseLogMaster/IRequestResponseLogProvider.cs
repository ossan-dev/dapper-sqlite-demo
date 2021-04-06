using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.RequestResponseLogMaster
{
    public interface IRequestResponseLogProvider
    {
        Task<IEnumerable<RequestResponseLog>> Get();
    }
}
