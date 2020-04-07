using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HalloReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Reflection");

#if DEBUG
            Console.WriteLine("DEBUG MODUS");
#endif

            Debug.WriteLine("Wichtige Debug Info");
            Debug.Assert(DateTime.Now.DayOfWeek == DayOfWeek.Friday);


            Console.WriteLine(Properties.Settings.Default.WERT);

            var file = @"C:\Users\rulan\source\repos\ppedvAG\csharp_pro_06042020\HalloReflection\MeineLib\bin\Debug\MeineLib.dll";
            //file = @"C:\dev\o4p\trunk\o4p\o4p.BusinessLogic\bin\Debug\EPPlus.dll";
            var ass = Assembly.LoadFrom(file);

            foreach (var item in ass.GetTypes())
            {
                Console.WriteLine($"{item.FullName}");
            }

            Type derMäher = ass.GetType("MeineLib.Rasenmäher");
            foreach (var item in derMäher.GetMembers())
            {
                Console.WriteLine($"{item.Name}");
            }

            object instance = Activator.CreateInstance(derMäher);
            MethodInfo mi = derMäher.GetMethod("Mähe");
            mi.Invoke(instance, null);


            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        enum Rechte
        {
            Lesen =78,
            Schreiben =-57,
            LesenSchreiben
        }
    }
}
