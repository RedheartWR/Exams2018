using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClassLibrary1;

namespace Подготовка
{
    class Program
    {
        public static void Create(string path, uint s, uint k, uint f)
        {
            StreamWriter file = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate));
            Random r = new Random();
            uint n;
            for (int i = 0; i < s; i++)
            {
                n = (uint)r.Next(1, (int)k);
                string str = "";
                for (int j = 0; j < n; j++)
                {
                    str += r.Next(1, (int)f) + " ";
                }
                file.WriteLine(str);
            }

            file.Close();
        }

        static void Main(string[] args)
        {
            Console.Write("s=");
            uint.TryParse(Console.ReadLine(), out uint s);
            Console.Write("k=");
            uint.TryParse(Console.ReadLine(), out uint k);
            Console.Write("f=");
            uint.TryParse(Console.ReadLine(), out uint f);

            string file_name = @"..\..\..\data.txt"; // подъем вверх на три папки
            Create(file_name, s, k, f);

            Processing p = new Processing(file_name);
            foreach (Data a in p) Console.WriteLine(a.ToString());
            var ord = from a in p orderby a.D/a.M descending select a;
            foreach (Data a in ord) Console.WriteLine(a.D/a.M + '\t' + a.ToString());
        }
    }
}
