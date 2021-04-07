using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.LogMaster
{
    public interface ILogProvider
    {
        Task<IEnumerable<Log>> Get();
    }
}
