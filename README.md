BFInterpreter
=============

An command line interpreter for the esoteric language BrainF***

<table>
  <tr>
    <td>Character</td>
    <td>Meaning</td>
  </tr>
  <tr>
    <td>&lt;</td>
    <td>Move the data pointer to the left</td>
  </tr>
  <tr>
    <td>&gt;</td>
    <td>Move the data pointer to the right</td>
  </tr>
  <tr>
    <td>+</td>
    <td>Increment the byte at the data pointer</td>
  </tr>
  <tr>
    <td>-</td>
    <td>Decrement the byte at the data pointer</td>
  </tr>
  <tr>
    <td>[</td>
    <td>If the byte at the data pointer != 0 jump past the matching ]</td>
  </tr>
  <tr>
    <td>]</td>
    <td>Jump to matching [</td>
  </tr>
  <tr>
    <td>.</td>
    <td>Output the byte at the data pointer</td>
  </tr>
  <tr>
    <td>,</td>
    <td>Input a byte and store at the data pointer</td>
  </tr>
</table>

Protected by the MIT License

Copyright (c) 2014

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
