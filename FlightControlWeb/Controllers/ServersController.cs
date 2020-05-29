using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        // GET: api/Servers
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(ServersManager.GetAllServers());
        }

        // POST: api/Servers
        [HttpPost]
        public void Post([FromBody] Server server)
        {
            ServersManager.AddServer(server);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ServersManager.DeleteServer(id);
        }
    }
}
