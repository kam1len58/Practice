using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace task5
{
    internal class Program
    {
        static async Task Main(string[] args)
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





            Console.WriteLine("\nВыберите способ сортировки:\n1(Быстрая сортировка) | 2(Сортировка деревом)\n ");
            int menu = Convert.ToInt32(Console.ReadLine());
            switch ((SortMethod)menu)
            {
                case (SortMethod.quickSort):
                    {
                        char[] Sm = sum.ToCharArray();
                        Console.WriteLine($"Неотсортированная строка:{sum}\n ");
                        QuickSort(Sm, 0, Sm.Length - 1);
                        string p = new string(Sm);
                        Console.WriteLine($"Отсортированная строка:{p}");
                        using var client = new HttpClient();
                        string url = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=100&count=1";

                        int number;
                        do
                        {
                            number = await GetRandomNumber(client, url, sum);
                        }
                        while (number >= p.Length || p.Length == 0);

                        Console.WriteLine($"Число: {number}");
                        sum = sum.Remove(number, 1);
                        Console.WriteLine($"Урезанная строка: {sum}");


                        break;
                    }
                case (SortMethod.treeSort):
                    {
                        Console.WriteLine($"Неотсортированная строка:{sum}\n ");
                        string p = TreeSort(sum);
                        Console.WriteLine($"Отсортированная строка:{p}");
                        using var client = new HttpClient();
                        string url = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=100&count=1";

                        int number;
                        do
                        {
                            number = await GetRandomNumber(client, url, sum);
                        }
                        while (number >= sum.Length || sum.Length == 0);

                        Console.WriteLine($"Число: {number}");
                        sum = sum.Remove(number, 1);
                        Console.WriteLine($"Урезанная строка: {sum}");

                        break;
                    }

            }





        }
        static async Task<int> GetRandomNumber(HttpClient client, string url, string sum)
        {
            try
            {
                string response = await client.GetStringAsync(url);
                return int.Parse(response.Trim('[', ']'));
            }
            catch
            {
                return new Random().Next(0, sum.Length);
            }
        }
        static void Swap(char[] a, int i, int j)
        {
            char t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
        static int Partition(char[] a, int l, int m)
        {
            int k = l + 1;
            char pivot = a[l];
            for (int i = l + 1; i <= m; i += 1)
            {
                if (a[i] < pivot)
                {

                    Swap(a, k, i);
                    k++;

                }
            }
            Swap(a, l, k - 1);
            return k - 1;
        }
        static void QuickSort(char[] a, int l, int m)
        {
            if (l < m)
            {
                var p1 = Partition(a, l, m);
                QuickSort(a, l, p1 - 1);
                QuickSort(a, p1 + 1, m);

            }
        }
        class Node
        {
            public char Value;
            public Node left, right;
            public Node(char value)
            {
                Value = value;
                left = right = null;
            }
        }
        static Node Insert(Node root, char value)
        {
            if (root == null)
            {
                return new Node(value);
            }
            if (value < root.Value)
                root.left = Insert(root.left, value);
            else
                root.right = Insert(root.right, value);

            return root;
        }
        static string IOT(Node root)
        {
            if (root == null)
                return "";

            return IOT(root.left) + root.Value + IOT(root.right);
        }
        static string TreeSort(string input)
        {
            Node root = null;
            foreach (char c in input)
            {
                root = Insert(root, c);
            }
            return IOT(root);
        }
        enum SortMethod
        {
            quickSort = 1,
            treeSort,

        }





    }
}
