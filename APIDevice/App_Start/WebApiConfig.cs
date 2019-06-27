using APIDevice.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace APIDevice
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GetDevices();
            // Configuration et services API Web

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void GetDevices()
        {
            List<DeviceModel> list_devices = new List<DeviceModel>();
            NpgsqlCommand MyCmd = null;
            NpgsqlConnection MyCnx = null;
            DataTable MyData = new DataTable();
            NpgsqlDataAdapter da;
            MyCnx = new NpgsqlConnection(SharedVar.Conx);
            MyCnx.Open();
            
            string select_request = "SELECT * " +
                "FROM \"device\"";

            MyCmd = new NpgsqlCommand(select_request, MyCnx);
            da = new NpgsqlDataAdapter(MyCmd);
            NpgsqlDataReader dr = MyCmd.ExecuteReader();
            while (dr.Read())
            {
                list_devices.Add(
                    new DeviceModel()
                    {
                        macaddress = (string)dr["macaddress"],
                        name = (string)dr["name"],
                        devicetype_id = (int)dr["devicetype_id"]
                    }
                );
            }
            SharedVar.init_devices_list = list_devices;
            MyCnx.Close();
    }
    }
}
