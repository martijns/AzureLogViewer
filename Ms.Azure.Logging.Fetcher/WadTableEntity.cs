///
/// Copyright (c) 2013, Martijn Stolk
/// This work is available under the Creative Commons Attribution 3.0 Unported (CC BY 3.0) license:
/// http://creativecommons.org/licenses/by/3.0/
///
using System;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Globalization;

namespace Ms.Azure.Logging.Fetcher
{
    /// <summary>
    /// The table entity compatible with the current Windows Azure Diagnostics format, so it can use the same
    /// table to log to allowing the use of any 3rd party applications that use this format.
    /// </summary>
    public class WadTableEntity : TableEntity
    {
        public WadTableEntity()
        {
            var now = DateTime.UtcNow;
            PartitionKey = now.Ticks.ToString("D19");
            RowKey = string.Format("{0:dd HH:mm:ss.fff}-{1}", now, Guid.NewGuid());
            Properties = new Dictionary<string, string>();
        }

        public long EventTickCount { get { return Properties.ContainsKey("EventTickCount") ? long.Parse(Properties["EventTickCount"], CultureInfo.InvariantCulture) : 0L; } set { Properties["EventTickCount"] = value.ToString(); } }
        public string DeploymentId { get { return Properties.ContainsKey("DeploymentId") ? Properties["DeploymentId"] : null; } set { Properties["DeploymentId"] = value.ToString(); } }
        public string Role { get { return Properties.ContainsKey("Role") ? Properties["Role"] : null; } set { Properties["Role"] = value.ToString(); } }
        public string RoleInstance { get { return Properties.ContainsKey("RoleInstance") ? Properties["RoleInstance"] : null; } set { Properties["RoleInstance"] = value.ToString(); } }
        public string Level { get { return Properties.ContainsKey("Level") ? Properties["Level"] : null; } set { Properties["Level"] = value.ToString(); } }
        public string EventId { get { return Properties.ContainsKey("EventId") ? Properties["EventId"] : null; } set { Properties["EventId"] = value.ToString(); } }
        public string Pid { get { return Properties.ContainsKey("Pid") ? Properties["Pid"] : null; } set { Properties["Pid"] = value.ToString(); } }
        public string Tid { get { return Properties.ContainsKey("Tid") ? Properties["Tid"] : null; } set { Properties["Tid"] = value.ToString(); } }
        public string Message { get { return Properties.ContainsKey("Message") ? GetMessage() : null; } set { Properties["Message"] = value.ToString(); } }

        private string GetMessage()
        {
            var message = Properties["Message"];

            if (message != null)
            {
                var substring = message.Substring(message.Length - 2, 1);

                if (substring.Contains("\n"))
                {
                    var remove = message.Remove(message.Length - 2, 1);
                    return remove;
                }
            }

            return message;
        }

        public Dictionary<string, string> Properties { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);
            foreach (var property in properties)
            {
                var key = property.Key;
                var val = property.Value.PropertyAsObject;
                var valStr = val is IConvertible ? ((IConvertible)val).ToString(CultureInfo.InvariantCulture) : val.ToString();

                if (key.Equals("TIMESTAMP"))
                    key = "Timestamp";

                if (Properties.ContainsKey(key))
                    Properties[key] = valStr;
                else
                    Properties.Add(key, valStr);
            }
        }
    }
}
