using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCustomers.Domain.Models
{
    public class EfQueryResult
    {
        public bool IsSuccessfull { get; set; }
        public string ErrorMessage { get; set; }
    }
}
