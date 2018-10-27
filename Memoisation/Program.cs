using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Memoisation
{
    class Program
    {

        static void Main()
        {
            new Program().Principal();
        }
        
        static readonly Dictionary<int, BigInteger> CacheExterne = new Dictionary<int, BigInteger>();

        public void Principal()
        {
            Pilote();
        }

        public void Pilote()
        {
            for (; ; )
            {
                DisplayMenu();

                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.A)
                    DisplayTribonachi(128, Tribonachi.Externe);
                else if (key == ConsoleKey.B)
                    DisplayTribonachi(128, Tribonachi.Interne);
                else if (key == ConsoleKey.X)
                    break;
            }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("///Pilote///\n" +
                              "A : Externe Tribonachi 128\n" +
                              "B : Interne Tribonachi 128\n" +
                              "X : Exit");
        }

        public void DisplayTribonachi(int n, Tribonachi tribonachi)
        {
            for (int i = 0; i < n; i++)
            {
                try
                {
                    if (tribonachi is Tribonachi.Interne)
                        Console.WriteLine(ExterneTribonachi(i));
                    else if (tribonachi is Tribonachi.Externe)
                        Console.WriteLine(InterneTribonachi(i));
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Overflow Detected");
                    break;
                }
            }
        }

        private decimal InterneTribonachi(int n)
        {
            Dictionary<int, decimal> CacheInterne = new Dictionary<int, decimal>();

            if (n <= 2)
                return 1;

            if (!CacheInterne.ContainsKey(n))
                CacheInterne[n] = CalculerTribonachiInterne(n);

            return CacheInterne[n];
        }

        public decimal CalculerTribonachiInterne(int n)
        {
            if (n % 2 == 0)
                return Carre(InterneTribonachi(n / 2 + 1)) - Carre(InterneTribonachi(n / 2 - 1));
            else
                return Carre(InterneTribonachi(n - 1 / 2 + 1)) - Carre(InterneTribonachi(n - 1 / 2));

            decimal Carre(decimal nb) => nb * nb;
        }

        public BigInteger ExterneTribonachi(int n)
        {
            if (n <= 3)
                return 1;

            if (!CacheExterne.ContainsKey(n))
                CacheExterne[n] = ExterneTribonachi(n - 1) + ExterneTribonachi(n - 2) + ExterneTribonachi(n - 3);

            return CacheExterne[n];
        }

        public enum Tribonachi
        {
            Externe,
            Interne
        }

    }
}
