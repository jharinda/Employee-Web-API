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
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        SELECT id,firstName,
                        lastName,
                        dob,
                        telephone,
                        email,
                        maritalStatus,
                        city,
                        remark FROM dbo.EmployeeDetails";
            return database(query);

        }

        [HttpGet("city/{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                        SELECT id,firstName,
                        lastName 
                        FROM dbo.EmployeeDetails where city="+id+"";
            return database(query);

        }

        [HttpPost]
        public JsonResult Post(EmployeeDetails emp)
        {
            string query = @"
                        INSERT INTO dbo.EmployeeDetails(firstName,
                        lastName,
                        dob,
                        telephone,
                        email,
                        maritalStatus,
                        city,
                        remark) values('" + emp.firstName + @"',
                                                        '" + emp.lastName + @"',
                                                        '" + emp.dob + @"',
                                                        '" + emp.telephone + @"',
                                                        '" + emp.email + @"',
                                                        '" + emp.maritalStatus + @"',
                                                        '" + emp.city + @"',
                                                        '" + emp.remark + @"'
                                                        )
                                                        ";
            return database(query);
        }

        [HttpPut("{id}")]
        public JsonResult Put(EmployeeDetails emp,int id)
        {
            string query = @"
                        update dbo.EmployeeDetails set 

                        firstName='" + emp.firstName + @"',
                        lastName = '"+emp.lastName+ @"',
                        dob = '" + emp.dob + @"',
                        email='" + emp.email + @"',
                        maritalStatus ='" + emp.maritalStatus + @"',
                        city = '" + emp.city + @"',
                        remark = '" + emp.remark + @"'
                        where id = '" + id+ @"'
                        ";
            return database(query);

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from dbo.EmployeeDetails 
                        where id = '" + id + @"'
                        ";
            return database(query);
        }

        [HttpGet("{id}")]
        public JsonResult GetEmployee(int id)
        {
            string query = @"SELECT firstName,
                        lastName,
                        dob,
                        telephone,
                        email,
                        maritalStatus,
                        city,
                        remark FROM dbo.EmployeeDetails where id="+id+@""; 

            
            return database(query);
        }

        [Route("GetAllCities")]
        public JsonResult GetAllCities()
        {
            string query = @"select * from dbo.Cities";
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
