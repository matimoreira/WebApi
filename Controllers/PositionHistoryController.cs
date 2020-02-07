using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionHistoryController : ControllerBase
    {
        // GET: api/PositionHistory
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PositionHistory/5
        [HttpGet("{from}/{to}")]
        public IList<Vehicle> Get(DateTime from, DateTime to)
        {
            var resultados = new List<Vehicle>();

            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = string.Format(@"SELECT top 100 d.id, d.lastname, d.firstname, d.phone, d.email,
                        v.id, v.name, v.licenseplate, v.seriesnumber, v.motornumber, g.id, g.latitude, g.longitude, g.timestamp
                        FROM vehicle v
                        INNER JOIN driver d ON (v.driverid = d.id)
                        INNER JOIN gpsreporthistory as g ON (g.vehicleid = v.id)
                        WHERE g.timestamp BETWEEN '{0}' AND '{1}'", from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"));
            var commmand = new SqlCommand(sql, connection);
            var reader = commmand.ExecuteReader();
            while (reader.Read())
            {
                var vehicleid = reader.GetInt32(5);
                var vehicle = resultados.Where(v => v.id == vehicleid).SingleOrDefault();
                if (vehicle == null)
                {
                    vehicle = new Vehicle();
                    vehicle.Position = new List<Position>();
                    vehicle.id = reader.GetInt32(5);
                    vehicle.name = reader.GetString(6);
                    vehicle.licenseplate = reader.GetString(7);
                    vehicle.seriesnumber = reader.GetString(8);
                    vehicle.motornumber = reader.GetString(9);
                    var driver = new Driver();
                    driver.id = reader.GetInt32(0);
                    driver.firstname = reader.GetString(1);
                    driver.lastname = reader.GetString(2);
                    driver.phone = reader.GetString(3);
                    driver.email = reader.GetString(4);
                    vehicle.Driver = driver;

                    
                    resultados.Add(vehicle);
                }
                var position = new Position();
                position.id = reader.GetInt32(10);
                position.Latitude = reader.GetDouble(11);
                position.Longitude = reader.GetDouble(12);
                position.timestamp = reader.GetDateTime(13);
                vehicle.Position.Add(position);
            }
            return resultados;
        }

        // POST: api/PositionHistory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PositionHistory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
