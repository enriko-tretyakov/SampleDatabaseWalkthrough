using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleDatabaseWalkthrough
{
    // Вспомогательный класс с полями для заполнения данными для их последующего использования в методе SampleDatabaseUpdate()
    [Table(Name = "Customers")]
    public class Customer
    {
        private int _Id;
        [Column(Storage = "_Id")]
        public int Id { get; set; }

        private string _CustomerID;
        [Column(IsPrimaryKey = true, Storage = "_CustomerID")]
        public string CustomerID { get; set; }

        private int _CompanyName;
        [Column(Storage = "_CompanyName")]
        public string CompanyName { get; set; }

        private int _ContactName;
        [Column(Storage = "_ContactName")]
        public string ContactName { get; set; }

        private int _Phone;
        [Column(Storage = "_Phone")]
        public string Phone { get; set; }

    }

}



