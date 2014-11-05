using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bfCompiler
{
    class Interpreter
    {
        private BFMemory _mem;
        private string _code;
        private int _codePtr;
        private int _memPtr;
        private Stack<int> _lastOpen = new Stack<int>();
        private Action<sbyte> _output;

        public Interpreter(string code, BFMemory mem, Action<sbyte> output)
        {
            _code = code;
            _mem = mem;
            _codePtr = 0;
            _memPtr = 0;
            _output = output;
        }

        public void Execute()
        {
            while (_codePtr < _code.Length)
            {
                Step();
            }
        }

        public void Step()
        {
            if (_codePtr >= _code.Length)
            {
                throw new ArgumentException("Reached end of program");
            }
            char executing = _code[_codePtr];
            switch (executing)
            {
                case '+':
                    _mem[_memPtr]++;
                    _codePtr++;
                    break;
                case '-':
                    _mem[_memPtr]--;
                    _codePtr++;
                    break;
                case '>':
                    _memPtr++;
                    _codePtr++;
                    break;
                case '<':
                    _memPtr--;
                    _codePtr++;
                    break;
                case '[':
                    StartLoop();
                    break;

                case ']':
                    try
                    {
                        _codePtr = _lastOpen.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Error, mismatching [] at " + _codePtr);
                        throw;
                    }
                    break;
                // IO
                case '.':
                    _output.Invoke(_mem[_memPtr]);
                    _codePtr++;
                    break;
                case ',':
                    _mem[_memPtr] = (sbyte) sbyte.Parse(Console.ReadLine().Trim());
                    _codePtr++;
                    break;
                default:
                    throw new Exception("Unknown character at " + _codePtr);
            }
        }

        public string DebugInfo()
        {
            throw new NotImplementedException();
        }

        private void StartLoop()
        {
             if (_mem[_memPtr] != 0)
             {
                _lastOpen.Push(_codePtr);
                _codePtr++;
             }
             else
             {
                // Jump to end of loop
                int braceCounter = 0;
                while (_codePtr < _code.Length)
                {
                    _codePtr++;
                    if (_code[_codePtr] == ']' && braceCounter == 0)
                    {
                        _codePtr++;
                        break;
                    }
                    if (_code[_codePtr] == '[')
                    {
                        braceCounter++;
                    }
                    else if (_code[_codePtr] == ']')
                    {
                        braceCounter--;
                        if (braceCounter < 0)
                        {
                            throw new Exception("Mismatched [] at: " + _codePtr);
                        }
                    }
                }
            }
        }
    }
}
