using APIDevice.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace APIDevice.Controllers
{
    public class CommandController : ApiController
    {
        // GET: api/Command
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Command/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Command
        public void Post([FromBody]CommandModel value)
        {
            sendCommandToBroker(value);
        }

        // PUT: api/Command/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Command/5
        public void Delete(int id)
        {
        }

        private void sendCommandToBroker(CommandModel cmd)
        {
            var factory = new ConnectionFactory() { HostName = "25.29.195.191" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: cmd.mac_address,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(cmd.command);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: cmd.mac_address,
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", cmd.command);
            }
            
        }
    }
}
