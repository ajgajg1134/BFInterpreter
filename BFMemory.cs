using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bfCompiler
{
    class BFMemory
    {
        internal sbyte[] rawMemory;

        public BFMemory()
        {
            rawMemory = new sbyte[100];
        }

        public BFMemory(int size)
        {
            rawMemory = new sbyte[size];
        }

        public sbyte this[int index]
        {
            get
            {
                return rawMemory[index];
            }

            set
            {
                rawMemory[index] = value;
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (sbyte datum in rawMemory)
            {
                s += "| " + datum + " ";
            }
            s += "|";
            return s;
        }
    }
}
