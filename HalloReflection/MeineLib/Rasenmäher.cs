using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineLib
{
    public class Rasenmäher
    {
        public string Hersteller { get; set; }
        public int Lautstärke { get; set; } = 12;

        public void Mähe()
        {
            Console.WriteLine($"Ich mähe mit der Lautstärke {Lautstärke} deinen Garten");
        }
    }
}
