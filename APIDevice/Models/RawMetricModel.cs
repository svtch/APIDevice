using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDevice.Models
{
    public class RawMetricModel
    {
        public string device_macaddress;
        public DateTime metric_date;
        public string metric_value;

        //oui
        public string name;
        public int device_type;

        public string Device_macaddress { get => device_macaddress; set => device_macaddress = value; }
        public DateTime Metric_date { get => metric_date; set => metric_date = value; }
        public string Metric_value { get => metric_value; set => metric_value = value; }
        public string Name { get => name; set => name = value; }
        public int Device_type { get => device_type; set => device_type = value; }
    }
}