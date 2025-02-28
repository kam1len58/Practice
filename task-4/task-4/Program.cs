using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
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
                Console.WriteLine("\nНедопустимые символы: ");
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
            Console.WriteLine("\nОбработанная строка:");
            string n, m, sum;
            if (g.Length % 2 == 0)
            {
                n = g.Substring(0, g.Length / 2);
                m = g.Substring(g.Length / 2);
                n = new string(n.Reverse().ToArray());
                m = new string(m.Reverse().ToArray());
                sum = n + m;
                Console.WriteLine(sum);




            }
            else
            {
                n = new string(g.Reverse().ToArray());
                sum = n + g;
                Console.WriteLine(sum);
            }
            Console.WriteLine("\nКол-во символов,которые встречаются в строке:");
            int k = 0;
            for (char c = 'a'; c <= 'z'; c++)
            {
                k = 0;
                for (int i = 0; sum.Length > i; i++)
                {
                    if (sum[i] == c)
                    {
                        k++;
                    }

                }
                if (k > 0)
                {
                    Console.WriteLine(c + " встречается " + k + " раз(a)");
                }

            }
            string ss = "aeiouy";
            int h = -1;
            int z = -1;
            for (int i = 0; i < sum.Length; i++)
            {
                for (int j = 0; j < ss.Length; j++)
                {
                    if (sum[i] == ss[j])
                    {
                        if (h == -1)
                            h = i;
                        z = i;
                        break;
                    }

                }
            }
            string y = "";
            if (h != -1 && z != -1)
            {
                for (int i = h; i <= z; i++)
                {
                    y += sum[i];
                }
                Console.WriteLine("\nСамая длинная подстрока начинающаяся и заканчивающаяся на гласную: " + y);
            }
            else
            {
                Console.WriteLine("\nВ строке только согласные буквы");
            }



        }
    }
}
