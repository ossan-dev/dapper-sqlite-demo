using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Database
{
    public interface IDatabaseBootstrap
    {
        void Setup();
    }
}
