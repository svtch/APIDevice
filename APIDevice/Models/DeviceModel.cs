using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDevice.Models
{
    public class DeviceModel
    {
        public string macaddress;
        public string name;
        public int devicetype_id;

        public string Macaddress { get => macaddress; set => macaddress = value; }
        public string Name { get => name; set => name = value; }
        public int Devicetype_id { get => devicetype_id; set => devicetype_id = value; }
    }
}