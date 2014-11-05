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
        private string _prevNum = "";
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
                    int incAmount = 1;
                    if (!string.IsNullOrEmpty(_prevNum))
                    {
                        incAmount = int.Parse(_prevNum);
                    }
                    _mem[_memPtr] += (sbyte) incAmount;
                    _codePtr++;
                    _prevNum = "";
                    break;
                case '-':
                    int decAmount = 1;
                    if (!string.IsNullOrEmpty(_prevNum))
                    {
                        decAmount = int.Parse(_prevNum);
                    }
                    _mem[_memPtr] -= (sbyte) decAmount;
                    _codePtr++;
                    _prevNum = "";
                    break;
                case '>':
                    incAmount = 1;
                    if (!string.IsNullOrEmpty(_prevNum))
                    {
                        incAmount = int.Parse(_prevNum);
                    }
                    _memPtr += incAmount;
                    _codePtr++;
                    _prevNum = "";
                    break;
                case '<':
                    decAmount = 1;
                    if (!string.IsNullOrEmpty(_prevNum))
                    {
                        decAmount = int.Parse(_prevNum);
                    }
                    _memPtr -= decAmount;
                    _codePtr++;
                    _prevNum = "";
                    break;
                case '[':
                    StartLoop();
                    break;

                case ']':
                    try
                    {
                        _codePtr = _lastOpen.Pop();
                    }
                    catch (InvalidOperationException) 
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

                // Optimizations
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    _prevNum += executing;
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
