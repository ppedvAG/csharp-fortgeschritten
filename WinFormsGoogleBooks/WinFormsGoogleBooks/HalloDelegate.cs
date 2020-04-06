using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsGoogleBooks
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string txt);
    delegate long Calculator(int a, int b);

    class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate meinDele = EinfacheMethode;
            meinDele.Invoke();
            Action meinDeleAlsAction = EinfacheMethode;
            Action meinDeleAlsActionAno = delegate () { Console.WriteLine("HALLO"); };
            Action meinDeleAlsActionAno2 = () => { Console.WriteLine("HALLO"); };
            Action meinDeleAlsActionAno3 = () => Console.WriteLine("HALLO");
            meinDeleAlsActionAno3.Invoke();

            DelegateMitPara deleMitPara = ZeigeText;
            Action<string> deleMitAlsAction = ZeigeText;
            Action<string> deleMitAlsActionAno = (string txt) => { Console.WriteLine(txt); };
            Action<string> deleMitAlsActionAno2 = (txt) => Console.WriteLine(txt);
            Action<string> deleMitAlsActionAno3 = x => Console.WriteLine(x);

            Calculator calc = MUlti;
            Func<int, int, long> calcFunc = MUlti;
            Calculator calcAno = (x, y) => { return x + y; };
            Calculator calcAno2 = (x, y) => x + y;

            long result = calc.Invoke(5, 9);

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
        }

        private long MUlti(int a, int b)
        {
            return a * b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        private void ZeigeText(string msg)
        {
            Console.WriteLine(msg);
        }

        public void EinfacheMethode()
        {
            System.Console.WriteLine("Hallo");
        }
    }
}
