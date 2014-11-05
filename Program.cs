using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bfCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            if (args.Length != 1)
            {
                Console.Write("Enter the file name: ");
                fileName = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
            }

            var rawFileLines = System.IO.File.ReadAllLines(fileName);

            var rawCode = rawFileLines.Aggregate("", (prev, next) => prev + next);

            rawCode = cleanComments(rawCode);

            var mem = new BFMemory(100);
            var interpreter = new Interpreter(rawCode, mem, (input) => Console.Write((char)input));
            try
            {
                interpreter.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fatal error in execution: " + e.Message);
                return;
            }

            Console.WriteLine("Execution complete, Press any key to exit");
            Console.WriteLine(mem.ToString());
            Console.ReadKey();
        }

        static string cleanComments(string rawCode)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in rawCode)
            {
                if (c == '>' || c == '<' || c == '.' || c == ',' || c == '+' || c == '-' || c == '[' || c == ']')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
