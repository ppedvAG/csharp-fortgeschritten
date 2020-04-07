using System;
using System.Linq;
using System.Threading.Tasks;

namespace HalloWeb.Models
{
    public class Eis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public bool IsMilcheis { get; set; }
        public DateTime Herstelldatum { get; set; }
    }
}
