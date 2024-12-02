using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorveteria.Model
{
    class Sabores
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Valor { get; set; }

        public Sabores() { }

        public override string ToString()
        {
            return Nome;
        }
    }
}
