using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            var mc = new MyClass();

            mc.HierZeug<DateTime, Action>(DateTime.Now, DateTime.Now, () => { });

            var mcg = new MyClassGeneric<MyClass>();
            //mcg.Add(5);

            Type t = typeof(MyClass);
            Type tt = mc.GetType();

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }


    interface Repo<T>
    {
        void Add(T dings);
        void Delete(T dings);
        void Update(T dings);
        T Get();
    }

    class MyClassGeneric<T> where T : class,new()
    {
        T derTyp;


        public void Add(T dings)
        {

        }
        public void Remove(T dings)
        {

        }
    }

    class MyClass
    {
        public void HierZeug<T, K>(T mpl, T mpl2, K k)
        {
            T dings;
            K dings2;
        }
    }
}
