﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.RequestResponseLogMaster
{
    public class RequestResponseLog
    {
        public int Id { get; set; }
        [JsonPropertyName("insert_date")]
        public DateTime InsertDate { get; set; }
        public string HttpVerb { get; set; }
        public string User { get; set; }
        public string RequestHost { get; set; }
        public string RequestPath { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }
    }
}
