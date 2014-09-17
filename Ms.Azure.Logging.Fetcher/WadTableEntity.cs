///
/// Copyright (c) 2013, Martijn Stolk
/// This work is available under the Creative Commons Attribution 3.0 Unported (CC BY 3.0) license:
/// http://creativecommons.org/licenses/by/3.0/
///
using System;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System.Collections.Generic;

namespace Ms.Azure.Logging.Fetcher
{
    /// <summary>
    /// The table entity compatible with the current Windows Azure Diagnostics format, so it can use the same
    /// table to log to allowing the use of any 3rd party applications that use this format.
    /// </summary>
    public class WadTableEntity : TableServiceEntity
    {
        public WadTableEntity()
        {
            var now = DateTime.UtcNow;
            PartitionKey = string.Format("{0:yyyy-MM-dd}", now);
            RowKey = string.Format("{0:dd HH:mm:ss.fff}-{1}", now, Guid.NewGuid());
            Properties = new Dictionary<string, string>();
        }

        public long EventTickCount { get; set; }
        public string DeploymentId { get; set; }
        public string Role { get; set; }
        public string RoleInstance { get; set; }
        public string Level { get; set; }
        public string EventId { get; set; }
        public string Pid { get; set; }
        public string Tid { get; set; }
        public string Message { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
