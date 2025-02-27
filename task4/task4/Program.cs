using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
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
            string n, m,sum;
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
            
            int iy=  -1;
            int yx = -1;

            string letter = "aeiouy";
            string b = "";
            for (int i=0;i<sum.Length;i++)
            {
                for (int j= letter.Length-1; j>=0;j--)
                {
                    if (sum[i] == letter[j])
                    {
                        if (iy==-1) 
                            iy = i;
                        yx = i;
                        break;
                        
                    }
                }
            }
            
            Console.WriteLine("Кол-во символов встречающихся в строке:");
            int k = 0;
            for (char c = 'a'; c <= 'z'; c++)
            {
                k = 0;
                for (int i = 0; s.Length > i; i++)
                {
                    if (s[i] == c)
                    {
                        k++;
                    }

                }
                if (k > 0)
                {
                    Console.WriteLine(c + " встречается " + k + " раз(a)");
                }


            }
            string ss = "";
            if (iy != -1 && yx != -1)
            {

                for (int i = iy; i <= yx; i++)
                {

                    ss += sum[i];
                }
                Console.WriteLine("\nСамая длинная подстрока начинающаяся и заканчивающаяся на гласную: " + ss);
            }
            else
            {
                Console.WriteLine("Cтрока содержит только согласные буквы");
            }
        }

    }

}
    

