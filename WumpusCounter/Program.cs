using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\ludau\Documents\goodData0.txt";
            Counter counter = new Counter();
            counter.readFile(path);
        }
    }
}
