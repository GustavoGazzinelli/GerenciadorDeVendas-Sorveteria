using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorveteria.Model
{
    class PicoleComSabor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public float? Valor { get; set; }
        public int? Quantidade { get; set; }
        public DateTime? Data { get; set; } = DateTime.Now;

        public PicoleComSabor() { }
    }
}
