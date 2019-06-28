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

    }
}