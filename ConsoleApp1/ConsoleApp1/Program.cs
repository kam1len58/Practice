using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static int FF(int x)
        {
            

                int y;
                if (x < -5)
                {
                    y = Math.Abs(x) * 2 - 1;

                }
                else if (x >= -5 && x <= 5)
                {
                    y = x;

                }
                else
                {
                    y = 2 * x;

                }
                return y;
            
            
        }
    
        static void Main(string[] args)
        {
            
            int sum = 0;
            for (int i = -25; i <= 15; i++)
            {
                sum += FF(i);
            }
            Console.WriteLine(sum);

        }
    }
}
