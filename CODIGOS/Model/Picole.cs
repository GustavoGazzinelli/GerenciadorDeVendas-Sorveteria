using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Sorveteria.Controller;

namespace Sorveteria.Model
{
    internal class Picole
    {
        public int Id { get; set; }
        public int? Sabor { get; set; }
        public int? Quantidade { get; set; }
        public DateTime? Data { get; set; } = DateTime.Now;

        public Picole() { }
    }
}
