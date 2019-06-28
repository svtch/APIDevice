using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDevice.Models
{
    public static class SharedVar
    {
        private static NpgsqlCommand MyCmd = null;
        private static NpgsqlConnection MyCnx = null;
        public static List<DeviceModel> init_devices_list;
        public static string Conx = "Server=25.29.195.191;Port=5432;Database=atlantis_users_devices;User Id=postgres;Password=123456;";
        public static void MacAddressCheck(RawMetricModel mo)
        {
            Boolean macExist = false;
            foreach (DeviceModel d in SharedVar.init_devices_list)
            {
                if (d.macaddress == mo.device_macaddress)
                {
                    macExist = true;
                }
            }
            if (macExist == false)
            {
                InsertNewMacAddress(mo);
            }
        }

        public static void InsertNewMacAddress(RawMetricModel mo)
        {
            string insert = "INSERT INTO \"device\"(id,macaddress,name,devicetype_id) values(DEFAULT,:macaddress,:name,:devicetype_id)";

            MyCnx = new NpgsqlConnection(Conx);
            //La valeur DEFAULT parce que la propriété id est auto incrémenté
            MyCnx.Open();
            MyCmd = new NpgsqlCommand(insert, MyCnx);

            //Définition  et ajout des paramètres 
            /*
            MyCmd.Parameters.Add(new NpgsqlParameter("device_macaddress", NpgsqlDbType.Varchar)).Value = curMetric.device_macaddress;
            MyCmd.Parameters.Add(new NpgsqlParameter("metric_date", NpgsqlDbType.Varchar)).Value = curMetric.metric_date;
            MyCmd.Parameters.Add(new NpgsqlParameter("device_type", NpgsqlDbType.Varchar)).Value = curMetric.device_type;
            MyCmd.Parameters.Add(new NpgsqlParameter("metric_value", NpgsqlDbType.Varchar)).Value = curMetric.metric_value;*/

            MyCmd.Parameters.Add(new NpgsqlParameter("macaddress", NpgsqlDbType.Varchar)).Value = mo.device_macaddress;
            MyCmd.Parameters.Add(new NpgsqlParameter("name", NpgsqlDbType.Varchar)).Value = mo.name;
            MyCmd.Parameters.Add(new NpgsqlParameter("devicetype_id", NpgsqlDbType.Integer)).Value = mo.device_type;

            MyCmd.ExecuteNonQuery(); //Exécution
            MyCnx.Close();

            init_devices_list.Add(new DeviceModel {
                macaddress = mo.device_macaddress,
                name = mo.name,
                devicetype_id = mo.device_type
            });
        }

        public static string buildSelectRequest(int id)
        {
            /*
            if (device_macaddress != null)
            {
                return "SELECT * " +
                    "FROM \"metrics\"" +
                    " WHERE device_macaddress = " + "\'" + device_macaddress + "\'" +
                    " AND metric_date BETWEEN " +
                    "\'" + dateDebut + "\'" +
                    " AND \'" + dateFin + "\' ";
            }
            else
            {
                return "SELECT * " +
                    "FROM \"metrics\"" +
                    " WHERE metric_date BETWEEN " +
                    "\'" + dateDebut + "\'" +
                    " AND \'" + dateFin + "\' ";
            }*/
            return "SELECT * " +
                    "FROM \"metrics\"" +
                    " WHERE Id > " +
                    "\'" + id + "\'";
        }
        public static string buildInsertRequest()
        {
            return "INSERT INTO \"metrics\"(id,device_macaddress,metric_date,metric_value, device_type) values(DEFAULT,:device_macaddress,:metric_date,:metric_value,:device_type)";
        }
    }
}