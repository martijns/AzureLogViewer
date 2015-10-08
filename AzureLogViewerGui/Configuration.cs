using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AzureLogViewerGui
{
    [Serializable]
    public class Configuration : AppConfiguration<Configuration>
    {
        public Configuration()
        {
            Accounts = new SerializableDictionary<string, string>();
            PerformanceCounterPreferences = new List<PerformanceCounterPreference>();
        }

        /// <summary>
        /// The list of accounts
        /// </summary>
        [XmlElement]
        public SerializableDictionary<string, string> Accounts { get; set; }

        /// <summary>
        /// As we want to default 'true' we use a negative setting, as a non-existing xml element will default to its default value (false). The public
        /// property is used to negate the private one, so that it is more convenient in its use.
        /// </summary>
        [XmlElement]
        protected bool DisableWADPerformanceOptimization { get; set; }
        public bool UseWADPerformanceOptimization
        {
            get { return !DisableWADPerformanceOptimization; }
            set { DisableWADPerformanceOptimization = !value; }
        }

        [XmlElement]
        protected bool DisableConvertEventTickCount { get; set; }
        public bool ConvertEventTickCount
        {
            get { return !DisableConvertEventTickCount; }
            set { DisableConvertEventTickCount = !value; }
        }

        [XmlElement]
        protected bool DoNotShowPerformanceCountersAsChart { get; set; }
        public bool ShowPerformanceCountersAsChart
        {
            get { return !DoNotShowPerformanceCountersAsChart; }
            set { DoNotShowPerformanceCountersAsChart = !value; }
        }

        [XmlElement]
        public List<PerformanceCounterPreference> PerformanceCounterPreferences { get; set; }
    }

    [Serializable]
    public class PerformanceCounterPreference
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public double Scale { get; set; }

        [XmlElement]
        public DateTime LastSeen { get; set; }
    }
}
