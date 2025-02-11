using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string s = Console.ReadLine();

            bool f = false;
            foreach (char c in s)
            {
                if (!alphabet.Contains(c))
                {
                    f = true;
                    break;
                }
            }

            if (f)
            {
                Console.WriteLine("Недопустимые символы:");
                foreach (char c in s)
                {
                    if (!alphabet.Contains(c))
                    {
                        Console.Write(c + " ");
                    }
                }
                Console.WriteLine();
                return;
            }


            string g = "";
            foreach (char c in s)
            {
                if (alphabet.Contains(c))
                {
                    g += c;
                }
            }

            string n, m;
            if (g.Length % 2 == 0)
            {
                n = g.Substring(0, g.Length / 2);
                m = g.Substring(g.Length / 2);
                n = new string(n.Reverse().ToArray());
                m = new string(m.Reverse().ToArray());
                string sum = n + m;
                Console.WriteLine(sum);
            }
            else
            {
                n = new string(g.Reverse().ToArray());
                string sum = n + g;
                Console.WriteLine(sum);
            }
        }
      
    }

}