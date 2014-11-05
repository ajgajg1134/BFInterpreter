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

            rawCode = CleanComments(rawCode);

            rawCode = OptimizeRepeated(rawCode);

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

        static string CleanComments(string rawCode)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in rawCode)
            {
                if (c == '>' || c == '<' || c == '.' || c == ',' || c == '+' || c == '-' || c == '[' || c == ']' || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        static string OptimizeRepeated(string rawCode)
        {
            int counter = 1;
            char prevChar = '\0';
            StringBuilder sb = new StringBuilder();
            foreach(char c in rawCode)
            {
                if (prevChar == '\0')
                {
                    prevChar = c;
                    continue;
                }

                if (c == '>' || c == '<' || c == '+' || c == '-')
                {
                    if (c == prevChar)
                    {
                        counter++;
                    }
                    else
                    {
                        if (counter > 1)
                        {
                            sb.Append(counter);
                            sb.Append(prevChar);
                            counter = 1;
                        }
                        else
                        {
                            sb.Append(prevChar);
                        }
                        prevChar = c;
                    }
                }
                else
                {
                    if (counter > 1)
                    {
                        sb.Append(counter);
                        sb.Append(prevChar);
                        counter = 1;
                    }
                    else
                    {
                        sb.Append(prevChar);
                    }
                    prevChar = c;
                }
            }
            if (counter > 1)
            {
                sb.Append(counter);
                sb.Append(prevChar);
            }
            else
            {
                sb.Append(prevChar);
            }
            return sb.ToString();
        }
    }
}
