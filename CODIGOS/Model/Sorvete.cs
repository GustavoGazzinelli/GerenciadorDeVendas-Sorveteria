using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Sorveteria.Controller;

namespace Sorveteria.Model
{
    public class Sorvete
    {
        public int Id { get; set; }
        public float? Peso { get; set; }
        public float? Valor { get; set; }
        public DateTime? Data { get; set; } = DateTime.Now;

        public Sorvete() { }
    }
}
