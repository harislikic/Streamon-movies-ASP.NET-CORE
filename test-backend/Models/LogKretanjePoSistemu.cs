using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test_backend.Models
{
    public class LogKretanjePoSistemu
    {
        [Key]
        public int id { get; set; }
        [ForeignKey(nameof(account))]
        public string accountID { get; set; }
        public Account account { get; set; }
        public string queryPath { get; set; }
        public string postData { get; set; }
       
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
