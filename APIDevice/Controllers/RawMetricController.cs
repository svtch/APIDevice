using APIDevice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace APIDevice.Controllers
{

    public class RawMetricController : ApiController
    {

        NpgsqlCommand MyCmd = null;
        NpgsqlConnection MyCnx = null;

        // GET: api/RawMetric
        public List<DeviceModel> Get()
        {
            return SharedVar.init_devices_list;
        }

        // GET: api/RawMetric/?id=1223
        public List<RawMetricModel> Get(int id)
        {
            string select_request = SharedVar.buildSelectRequest(id);
            List<RawMetricModel> MyData = new List<RawMetricModel>();
            NpgsqlDataAdapter da;
            try
            {
                MyCnx = new NpgsqlConnection(SharedVar.Conx);
                MyCnx.Open();
                MyCmd = new NpgsqlCommand(select_request, MyCnx);
                da = new NpgsqlDataAdapter(MyCmd);
                NpgsqlDataReader dr = MyCmd.ExecuteReader();
                while (dr.Read())
                {
                    MyData.Add(
                        new RawMetricModel()
                        {
                            id = (int)dr["id"],
                            device_macaddress = (string)dr["device_macaddress"],
                            device_type = (int)dr["device_type"],
                            metric_date = (DateTime)dr["metric_date"],
                            metric_value = (string)dr["metric_value"]
                        }
                    );
                }
                MyCnx.Close();
                return MyData;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // POST: api/RawMetric
        //[fromBody] string value
        public void Post([FromBody] RawMetricModel value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("THE VALUE IS NULL");
            }
            else
            {
                SharedVar.MacAddressCheck(value);
                try
                {
                    MyCnx = new NpgsqlConnection(SharedVar.Conx);

                    string insert_request = SharedVar.buildInsertRequest();
                    //La valeur DEFAULT parce que la propriété id est auto incrémenté
                    MyCnx.Open();
                    MyCmd = new NpgsqlCommand(insert_request, MyCnx);

                    //Définition  et ajout des paramètres
                    MyCmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Varchar)).Value = value.id;
                    MyCmd.Parameters.Add(new NpgsqlParameter("device_macaddress", NpgsqlDbType.Varchar)).Value = value.device_macaddress;
                    MyCmd.Parameters.Add(new NpgsqlParameter("metric_date", NpgsqlDbType.Timestamp)).Value = value.metric_date;
                    MyCmd.Parameters.Add(new NpgsqlParameter("device_type", NpgsqlDbType.Integer)).Value = value.device_type;
                    MyCmd.Parameters.Add(new NpgsqlParameter("metric_value", NpgsqlDbType.Varchar)).Value = value.metric_value;
                    MyCmd.ExecuteNonQuery(); //Exécution
                    MyCnx.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

        // PUT: api/RawMetric/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RawMetric/5
        public void Delete(int id)
        {
        }


    }
}
