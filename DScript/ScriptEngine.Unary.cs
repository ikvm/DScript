﻿/*
Copyright (c) 2014 - 2020 Darren Horrocks

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace DScript
{
    public partial class ScriptEngine
    {
        private ScriptVarLink Unary(ref bool execute)
        {
            ScriptVarLink a;
            if (currentLexer.TokenType == (ScriptLex.LexTypes)'!')
            {
                currentLexer.Match((ScriptLex.LexTypes)'!');
                a = Factor(ref execute);
                if (execute)
                {
                    var zero = new ScriptVar(0);
                    var res = a.Var.MathsOp(zero, ScriptLex.LexTypes.Equal);

                    CreateLink(ref a, res);
                }
            }
            else if (currentLexer.TokenType == ScriptLex.LexTypes.RTypeOf)
            {
                currentLexer.Match(ScriptLex.LexTypes.RTypeOf);
                a = Factor(ref execute);
                if (execute)
                {
                    var varType = new ScriptVar(a.Var.GetObjectType());

                    CreateLink(ref a, varType);
                }
            }
            else if (currentLexer.TokenType == ScriptLex.LexTypes.PlusPlus || currentLexer.TokenType == ScriptLex.LexTypes.MinusMinus)
            {
                var op = currentLexer.TokenType == ScriptLex.LexTypes.PlusPlus ? '+' : '-';
                currentLexer.Match(currentLexer.TokenType);

                a = Factor(ref execute);
                if (execute)
                {
                    var one = new ScriptVar(1);
                    var res = a.Var.MathsOp(one, (ScriptLex.LexTypes)op);
                    a.ReplaceWith(res);
                    //CreateLink(ref a, res);
                }
            }
            else
            {
                a = Factor(ref execute);
            }

            return a;
        }
    }
}
