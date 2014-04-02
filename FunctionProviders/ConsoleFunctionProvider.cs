﻿/*
Copyright (c) 2014 Darren Horrocks

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

using System;

namespace DScript.FunctionProviders
{
	public class ConsoleFunctionProvider : IFunctionProvider
	{
		public void RegisterFunctions(ScriptEngine engine)
		{
			String[] ns = {"Console"};

			engine.AddMethod(ns, "WriteLine", new[] { "a" }, WriteLine, null);
			engine.AddMethod(ns, "ReadLine", null, ReadLine, null);
			engine.AddMethod(ns, "Write", new[] { "a" }, Write, null);
			engine.AddMethod(ns, "ReadChar", null, ReadChar, null);
		}

		public static void WriteLine(ScriptVar var, object userData)
		{
			String a = var.GetParameter("a").GetString();
			Console.WriteLine(a);
		}

		public static void ReadLine(ScriptVar var, object userData)
		{
			String retVal = Console.ReadLine();
			var.SetReturnVar(new ScriptVar(retVal));
		}

		public static void Write(ScriptVar var, object userData)
		{
			String a = var.GetParameter("a").GetString();
			Console.Write(a);
		}

		public static void ReadChar(ScriptVar var, object userData)
		{
			Int32 retVal = Console.Read();
			var.SetReturnVar(new ScriptVar(retVal));
		}
	}
}