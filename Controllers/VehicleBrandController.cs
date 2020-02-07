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
    public class VehicleBrandController : Controller
    {
        public IEnumerable<VehicleBrand> Get()
        {
            var resultado = new List<VehicleBrand>();

            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = "SELECT id, name from vehiclebrand";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                resultado.Add(
                    new VehicleBrand(reader.GetInt32(0), reader.GetString(1))
                );

            }
            reader.Close();
            connection.Close();


            return resultado;
        }
        /*
        public void Post(int id, string name, int enterpriseid, string active)
        {
            var connection = new SqlConnection("data source=104.217.253.86;initial catalog=tracking;user id=alumno;password=12345678");
            connection.Open();

            var sql = $"INSERT INTO vehiclebrand(id, name, enterpriseid, active) value ({id}, '{name}', {id}, '{active}')";
            var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            this.Ok("La marca se inserto correctamente");
        } 
        */
    }
}