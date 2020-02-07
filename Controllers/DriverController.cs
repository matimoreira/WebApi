using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
        public IEnumerable<Driver> Get()
        {
            var resultado = new List<Driver>();

            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = "SELECT id, firstname, lastname from driver";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                resultado.Add(
                    new Driver
                    {
                        id = reader.GetInt32(0),
                        firstname = reader.GetString(1),
                        lastname = reader.GetString(2)
                    
                    }
                );
                
            }
            reader.Close();
            connection.Close();

            
            return resultado;
        }
    }
}