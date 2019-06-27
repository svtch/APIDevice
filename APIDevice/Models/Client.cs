using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace APIDevice.Models
{

    public static class Client
    {
        public static readonly HttpClient client = new HttpClient();
        
    }
}