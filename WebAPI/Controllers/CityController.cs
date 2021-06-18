using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Cities";
            return database(query);
        }

        [HttpGet("{id}")]
        public JsonResult GetCity(int id)
        {
            string query = @"select cityName from dbo.Cities where cityId = "+id+"";
            return database(query);
        }



        [HttpPost]
        public JsonResult Post(Cities cities)
        {
            string query = @"INSERT INTO dbo.Cities VALUES  ( '"+cities.cityName+@"')";
            return database(query);
        }

        [HttpPut("{id}")]
        public JsonResult Put(Cities cities,int id)
        {
            string query = @"UPDATE dbo.Cities SET  cityName ='" + cities.cityName + @"' WHERE cityId="+id+"";
            return database(query);
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Cities WHERE  cityId =" + id+ @"";
            return database(query);
        }

        public JsonResult database(string query)
        {
            DataTable table = new DataTable();
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader); ;

                        myReader.Close();
                        myCon.Close();
                        return new JsonResult(table);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }


            return new JsonResult("Fail");
        }

    }
}
