using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDatabaseWalkthrough
{
    // Вспомогательный класс с полями для заполнения данными для их последующего использования в методе SampleDatabaseUpdate()
    public class Customers
    {
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }

    }
}
