
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using test_backend.Models;

namespace haris_edin_rs1.ModelsAutentififkacija
{
    public class AutentifikacijaToken
    {
        [Key]
        public int id { get; set; }
        public string value { get; set; }
        [ForeignKey(nameof(account))]
        public int accountId { get; set; }
        public Account account { get; set; }

    }
}
