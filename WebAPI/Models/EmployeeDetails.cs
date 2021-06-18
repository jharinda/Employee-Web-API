using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class EmployeeDetails
    {
        public int EmpId { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string dob { get; set; }

        public int telephone { get; set; }

        public string email { get; set; }

        public int maritalStatus { get; set; }

        public int city { get; set; }

        public string remark { get; set; }

    }
}
