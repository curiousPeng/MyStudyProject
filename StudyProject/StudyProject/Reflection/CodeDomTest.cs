using System;
using Specialized = System.Collections.Specialized;
using Reflection = System.Reflection;
using CSharp = Microsoft.CSharp;
using CodeDom = System.CodeDom.Compiler;

namespace StudyProject
{
   

    public sealed class TestCompile
    {

        static string ScriptCodeToCompileInMem = "public class Script {public void ScriptExecute(){System.Console.WriteLine(123);} }";
        public static void TestMain()
        {
            TestCompile tc = new TestCompile();
            tc.Execute(TestCompile.ScriptCodeToCompileInMem);
        }

        public void Execute(string scriptCode)
        {
            string[] source = new string[1];
            source[0] = scriptCode;
            CSharp.CSharpCodeProvider cscp = new CSharp.CSharpCodeProvider();
            this.Compile(cscp, source[0]);
        }


        private void Compile(CodeDom.CodeDomProvider provider, string source)
        {
            CodeDom.CompilerParameters param = new CodeDom.CompilerParameters();
            param.GenerateExecutable = false;
            param.IncludeDebugInformation = false;
            param.GenerateInMemory = true;
            CodeDom.CompilerResults cr = provider.CompileAssemblyFromSource(param, source);
            Specialized.StringCollection output = cr.Output;
            if (cr.Errors.Count != 0)
            {
                System.Console.WriteLine("Error invoking scripts.");
                CodeDom.CompilerErrorCollection es = cr.Errors;
                foreach (CodeDom.CompilerError s in es)
                    System.Console.WriteLine(s.ErrorText);
            }
            else
            {
                object o = cr.CompiledAssembly.CreateInstance("Script");
                System.Type type = o.GetType();
                type.InvokeMember("ScriptExecute",
                        Reflection.BindingFlags.InvokeMethod |
                        Reflection.BindingFlags.Default, null, o, null);
            }
        }
    }
}
