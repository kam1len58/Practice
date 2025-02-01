using System;

class pracrice
{
    static void Main()
    {
        string s = Console.ReadLine();
        string n;
        string m;
        if (s.Length % 2 == 0)
        {
            n = s.Substring(0, s.Length / 2);
            m = s.Substring(s.Length / 2);
            n = new string(n.Reverse().ToArray());
            m = new string(m.Reverse().ToArray());
            string sum = n + m;
            Console.WriteLine(sum);
        }
        else
        {
            n = new string(s.Reverse().ToArray());
            string sum = n + s;
            Console.WriteLine(sum);
        }
    }
}

