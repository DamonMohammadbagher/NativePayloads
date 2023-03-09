using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing;
using System.Diagnostics.Tracing;
using O365.Security.ETW;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Threading;
using System.ComponentModel;

namespace ETWMonThread
{
    class Program
    {
        /// <summary>
        /// BEGIN Meterpreter Payload Detection MPD
        /// </summary>

        static string Meterpreter_Signature = "jIubno+WoIyGjKCPjZCcmoyMoJiai4+Wmw==";
        static byte[] _Meterpreter__Bytes_signature = Convert.FromBase64String(Meterpreter_Signature);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int NtQueryInformationThread(IntPtr threadHandle, ThreadInfoClass threadInformationClass, IntPtr threadInformation, int threadInformationLength, IntPtr returnLengthPtr);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, int dwThreadId);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);
                
        private const int PAGE_READWRITE = 0x04;
        private const int PAGE_WRITECOPY = 0x08;
        private const int PAGE_EXECUTE_READWRITE = 0x40;
        private const int PAGE_EXECUTE_WRITECOPY = 0x80;
        private const int PAGE_GUARD = 0x100;
        private const int WRITABLE = PAGE_READWRITE | PAGE_WRITECOPY | PAGE_EXECUTE_READWRITE | PAGE_EXECUTE_WRITECOPY | PAGE_GUARD;
        private const int MEM_COMMIT = 0x1000;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        internal static extern Int32 VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")]
        protected static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);
      

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }       

        public delegate string AsyncMethodCaller(ConsoleColor color, string input_srt_Data, string input_srt_Data2, int callDuration);
        public static string _Async_WriStr_Console(ConsoleColor color, string input_srt_Data, string input_srt_Data2, int callDuration)
        {
            try
            {
                while (true)
                {
                    if (input_srt_Data2 == null)
                    {
                        Console.ForegroundColor = color;
                        Thread.Sleep(callDuration);
                        Console.WriteLine("[mem] {0}", input_srt_Data);
                        return String.Format("time was {0}.", callDuration.ToString());

                    }
                    else
                    {
                        Console.ForegroundColor = color;
                        Thread.Sleep(callDuration);
                        Console.WriteLine("[mem] {0} {1}", input_srt_Data, input_srt_Data2);
                        return String.Format("time was {0}.", callDuration.ToString());

                    }
                    break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = color;
                return String.Format("time was {0}.", callDuration.ToString());
                //throw;
            }
        }

        public static string _Get_Arguments(Process Prcs)
        {
            string Prcs_args = "";

            try
            {
                using (ManagementObjectSearcher s = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + Prcs.Id))
                {
                    foreach (ManagementObject obj in s.Get())
                    {
                        Prcs_args += obj["CommandLine"];
                    }
                }
                return Prcs_args;
            }
            catch (Exception) { return ""; }
        }
        public static unsafe long IndexOf(byte[] Haystack, byte[] Needle)
        {
            fixed (byte* H = Haystack) fixed (byte* N = Needle)
            {
                long i = 0;
                for (byte* hNext = H, hEnd = H + Haystack.LongLength; hNext < hEnd; i++, hNext++)
                {
                    bool Found = true;
                    for (byte* hInc = hNext, nInc = N, nEnd = N + Needle.LongLength; Found && nInc < nEnd; Found = *nInc == *hInc, nInc++, hInc++) ;
                    if (Found) return i;
                }
                return -1;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ParentProcessUtilities
        {
            internal IntPtr Reserved1;
            internal IntPtr PebBaseAddress;
            internal IntPtr Reserved2_0;
            internal IntPtr Reserved2_1;
            internal IntPtr UniqueProcessId;
            internal IntPtr InheritedFromUniqueProcessId;

            [DllImport("ntdll.dll")]
            private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref ParentProcessUtilities processInformation, int processInformationLength, out int returnLength);
            public static Process GetParentProcess()
            {
                return GetParentProcess(Process.GetCurrentProcess().Handle);
            }

            public static Process GetParentProcess(int id)
            {
                Process process = Process.GetProcessById(id);
                return GetParentProcess(process.Handle);
            }
            public static Process GetParentProcess(IntPtr handle)
            {
                ParentProcessUtilities pbi = new ParentProcessUtilities();
                int returnLength;
                int status = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
                if (status != 0)
                    throw new Win32Exception(status);

                try
                {
                    return Process.GetProcessById(pbi.InheritedFromUniqueProcessId.ToInt32());
                }
                catch (ArgumentException)
                {
                    // not found
                    return null;
                }
            }
        }

        [Flags]
        public enum ThreadAccess : int
        {
            Terminate = 0x0001,
            SuspendResume = 0x0002,
            GetContext = 0x0008,
            SetContext = 0x0010,
            SetInformation = 0x0020,
            QueryInformation = 0x0040,
            SetThreadToken = 0x0080,
            Impersonate = 0x0100,
            DirectImpersonation = 0x0200
        }
        public enum ThreadInfoClass : int
        {
            ThreadQuerySetWin32StartAddress = 9
        }

        static string _Return_Threads_StartAddress(int tid)
        {
            return (string.Format("{0:X16}", (ulong)GetThreadStartAddress(tid)));
        }
        static IntPtr GetThreadStartAddress(int threadId)
        {           
            var hThread = OpenThread(ThreadAccess.QueryInformation, false, threadId);            
            var buf = Marshal.AllocHGlobal(IntPtr.Size);

            try
            {
                var result = NtQueryInformationThread(hThread, ThreadInfoClass.ThreadQuerySetWin32StartAddress, buf, IntPtr.Size, IntPtr.Zero);                
                return Marshal.ReadIntPtr(buf);
            }
            finally
            {
                CloseHandle(hThread);
                Marshal.FreeHGlobal(buf);
                GC.Collect(GC.MaxGeneration);
                GC.WaitForPendingFinalizers();
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
            }            
        }

        public static bool Kill_Thread(int Process_ID, int TID)
        {

            bool kill = false;
            try
            {

                ProcessThreadCollection _ProcessThreads_Collection = Process.GetProcessById(Process_ID).Threads;

                foreach (ProcessThread item in _ProcessThreads_Collection)
                {

                 
                    if (item.Id == TID)
                    {
                        if (item.ThreadState == System.Diagnostics.ThreadState.Wait)
                        {

                        
                            if (item.StartAddress.ToString() == "0" && item.WaitReason.ToString().Contains("Exe"))
                            {
                                IntPtr Target_Thread_for_kill = OpenThread(1, false, (uint)item.Id);
                                TerminateThread(Target_Thread_for_kill, 1);                            
                                kill = true;
                            }
                            else if (item.StartAddress.ToString() != "0" && item.WaitReason.ToString().Contains("Exe"))
                            {
                        
                                IntPtr Target_Thread_for_kill = OpenThread(1, false, (uint)item.Id);
                                TerminateThread(Target_Thread_for_kill, 1);
                                // return true;
                                kill = true;

                            }
                        }
                        else
                        {
                            if (item.StartAddress.ToString() == "0" && item.ThreadState.ToString().Contains("Run"))
                            {

                                IntPtr Target_Thread_for_kill = OpenThread(1, false, (uint)item.Id);
                                TerminateThread(Target_Thread_for_kill, 1);                            
                                kill = true;
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[X] Damn it , can't kill this thread or can't find Threads by zero address or status: Wait:Exe/Run ! : " + e.Message);

            }

            if (kill)
            {               
                return true;
            }
            else
            {               
                return false;
            }

        }

        public static DataTable Temp_Thread_InfoDebugMode;
        public static void DebugMode_ThreadsDetailShow(int Process_ID, int TID)
        {
            try
            {              
                Process _Prcss = null;
                Temp_Thread_InfoDebugMode.Rows.Clear();
                if (!Process.GetProcessById(Process_ID).HasExited)
                {
                    _Prcss = Process.GetProcessById(Process_ID);
                    DateTime _xA, _xB;
                    for (int i = 0; i < _Prcss.Threads.Count; i++)
                    {                       
                        if (_Prcss.Threads[i].Id == TID)
                        {
                            _xA = _Prcss.StartTime; /// process start time
                            _xB = _Prcss.Threads[i].StartTime; /// threads start time                    

                            if (_Prcss.Threads[i].ThreadState == System.Diagnostics.ThreadState.Wait)
                            {
                                object[] Oo = {
                                          _Prcss.Threads[i].Id,
                                          (_xB - _xA),
                                          _Prcss.Threads[i].ThreadState.ToString() + ":" + _Prcss.Threads[i].WaitReason.ToString()
                                          , _Return_Threads_StartAddress(_Prcss.Threads[i].Id)
                                          , _xB,
                                          _Prcss.ProcessName,
                                          _Prcss.Id
                                          , "no"
                                          , _Prcss.Threads[i].StartAddress.ToString()
                                          };
                                Temp_Thread_InfoDebugMode.Rows.Add(Oo);
                            }
                            else
                            {
                                object[] Oo = {
                                          _Prcss.Threads[i].Id,
                                          (_xB - _xA),
                                          _Prcss.Threads[i].ThreadState.ToString() + ":" + _Prcss.Threads[i].ThreadState.ToString()
                                          , _Return_Threads_StartAddress(_Prcss.Threads[i].Id)
                                          , _xB,
                                          _Prcss.ProcessName,
                                          _Prcss.Id
                                          , "no"
                                          , _Prcss.Threads[i].StartAddress.ToString()
                                          };
                                Temp_Thread_InfoDebugMode.Rows.Add(Oo);
                            }

                        }
                    }

                    Temp_Thread_InfoDebugMode.DefaultView.ToTable();

                    DataRow[] Rows_to_Delete = Temp_Thread_InfoDebugMode.Select("status LIKE '%Exe%' or status LIKE '%Run%' or tid_StartAddress = '0'");                    
                    foreach (DataRow item in Rows_to_Delete)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("\t\t [1] Tid:" + item.ItemArray[0].ToString());
                        Console.WriteLine("\t  Startaddress_x64: " + item.ItemArray[3].ToString());
                        if (item.ItemArray[2].ToString().Contains("Exe") || item.ItemArray[2].ToString().Contains("Run"))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("              ==>");
                            Console.WriteLine("[2] Tid " + item.ItemArray[0].ToString() + " Status: " + item.ItemArray[2].ToString());                            
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("                 ");
                            Console.WriteLine("[2] Tid " + item.ItemArray[0].ToString() + " Status: " + item.ItemArray[2].ToString());
                        }

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\t\t [3] Injected or Started Time:" + item.ItemArray[4].ToString());
                        Console.WriteLine("\t\t [4] Started Time After Parent Process:" + item.ItemArray[1].ToString());
                        Console.WriteLine("\t\t [5] Startaddress: " + item.ItemArray[8].ToString());
                    }
                }
            }
            catch (Exception)
            {
              
            }
        }

        static AsyncMethodCaller Call = new AsyncMethodCaller(_Async_WriStr_Console);
        static IAsyncResult result_Async;

        public static bool Scan_Process_Memory(Process Prcs)
        {
            byte[] buff;
                   
            try
            {
                if (Prcs.HasExited)
                {
                    buff = null;
                    return false;
                }
                try
                {
                    Console.Title = "Scanning Memory: ";
                    IntPtr Addy = new IntPtr();
                    List<MEMORY_BASIC_INFORMATION> MemReg = new List<MEMORY_BASIC_INFORMATION>();
                    while (true)
                    {
                        if (!Prcs.HasExited)
                        {
                            MEMORY_BASIC_INFORMATION MemInfo = new MEMORY_BASIC_INFORMATION();
                            int MemDump = VirtualQueryEx(Prcs.Handle, Addy, out MemInfo, Marshal.SizeOf(MemInfo));
                            if (MemDump == 0) break;
                            if (0 != (MemInfo.State & MEM_COMMIT) && 0 != (MemInfo.Protect & WRITABLE) && 0 == (MemInfo.Protect & PAGE_GUARD))
                            {
                                MemReg.Add(MemInfo);
                            }
                            Addy = new IntPtr(MemInfo.BaseAddress.ToInt64() + MemInfo.RegionSize.ToInt64());
                        }
                        if (Prcs.HasExited)
                        {
                            GC.Collect(GC.MaxGeneration);
                            GC.WaitForPendingFinalizers();
                            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
                            break;

                        }
                    }

                    for (int i = 0; i < MemReg.Count; i++)
                    {
                        if (Prcs.HasExited) { break; }
                        Console.Title = "Scanning Memory: " + Prcs.ProcessName.ToString() + " (" + Prcs.Id.ToString() + ") : " + (MemReg.Count - i).ToString();
                        if (!Prcs.HasExited)
                        {
                            buff = new byte[MemReg[i].RegionSize.ToInt64()];
                            ReadProcessMemory(Prcs.Handle, MemReg[i].BaseAddress, buff, MemReg[i].RegionSize.ToInt32(), IntPtr.Zero);
                            GC.Collect(GC.MaxGeneration);
                            GC.WaitForPendingFinalizers();
                            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);

                            for (int j = 0; j < buff.Length; j++)
                            {
                                buff[j] = (byte)(buff[j] ^ 0xFF);
                            }

                            long Result = IndexOf(buff, _Meterpreter__Bytes_signature);
                            /// result > 0 when FOUND Infected Process  ;)
                            if (Result > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                for (int f = 0; f < Prcs.Threads.Count; f++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    var Sub_Threads = Prcs.Threads[f].StartAddress.ToInt64();
                                    /// do this code if only ONCE in loop f == 0
                                    if (f <= 0)
                                    {
                                        try
                                        {                                            
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            /// Debug mode [on]
                                            /// you can use Switch 'Debug' for this code but i want to show this by default 
                                            /// view Hex for Infected memory          
                                            string source = BitConverter.ToString(buff);
                                            string pattern = BitConverter.ToString(_Meterpreter__Bytes_signature);
                                            int _index = source.IndexOf(pattern);
                                            Console.WriteLine("");
                                            Console.WriteLine("\t Infected Memory bytes :");
                                            int chunkSize_debug = 60;
                                            string temp_debug = BitConverter.ToString(buff);
                                            int stringLength_debug = temp_debug.Length;
                                            int counter_debug = 0;
                                            for (int d = _index; d < stringLength_debug; d += chunkSize_debug)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                if (d + chunkSize_debug > stringLength_debug) chunkSize_debug = stringLength_debug - d;
                                                Console.WriteLine("\t {0}", temp_debug.Substring(d, chunkSize_debug));
                                                if (counter_debug >= 4) break;
                                                counter_debug++;
                                            }

                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(" ");
                                            Console.WriteLine("\t Process Arguments :");
                                            string temp = _Get_Arguments(Prcs);
                                            /// fixing Arguments show method done ;)
                                            /// show 300 char arguments only
                                            //string temp = "Notfound!";
                                            int chunkSize = 59;
                                            int stringLength = temp.Length;
                                            for (int b = 0; b < stringLength; b += chunkSize)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                if (b + chunkSize > stringLength) chunkSize = stringLength - b;
                                                Console.WriteLine("\t {0}", temp.Substring(b, chunkSize));
                                                if (b >= 300) break;
                                            }
                                        }
                                        catch (Exception _eee)
                                        {
                                            result_Async = Call.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.ToString() + " [X]: " + _eee.Message, null, 2, null, null);
                                        }

                                        /// do something for view sub threads 
                                        for (int counter_threads = 0; counter_threads < Prcs.Threads.Count; counter_threads++)
                                        {                                          

                                            /// if Thread_startadrees was 0 maybe that thread is METERPRETER backdoor thread ;-/
                                            /// so i will show Thread_startadress 0 by Yellow color but this is not 100% correct always ;)
                                            if (Prcs.Threads[counter_threads].ThreadState == System.Diagnostics.ThreadState.Wait)
                                            {
                                                if (Prcs.Threads[counter_threads].StartAddress.ToString() == "0" || Prcs.Threads[counter_threads].WaitReason.ToString().Contains("Exe"))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    // Console.WriteLine("\t\t Tid StartAddress: {0}", _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                    /// DebugMode 
                                                    /// adding ver 1.0.0.6 , Show DETAIL for threads by colors and more info      
                                                    if(Is_DebugMode)                                              
                                                    DebugMode_ThreadsDetailShow(Prcs.Id, Prcs.Threads[counter_threads].Id);


                                                    /// killing Infected Thread in IPS Mode
                                                    if (Program.IPS_IDS)
                                                    {
                                                        try
                                                        {
                                                            bool Kill_TID = Kill_Thread(Prcs.Id, Prcs.Threads[counter_threads].Id);
                                                            if (Kill_TID)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} not Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                            }

                                                        }
                                                        catch (Exception error)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Green;
                                                            Console.WriteLine(System.DateTime.Now.ToString() + "   Maybe Thread can't kill: " + error.Message);

                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    /// debug
                                                    // Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    // Console.WriteLine("----------Tid StartAddress: {0}", _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                }
                                                /// if startadrees was 0 maybe that thread is METERPRETER backdoor thread ;-/
                                                /// so i will show startadress 0 by Yellow color but this is not 100% correct always ;)
                                            }
                                            else
                                            {
                                                if (Prcs.Threads[counter_threads].StartAddress.ToString() == "0" || Prcs.Threads[counter_threads].ThreadState.ToString().Contains("Run"))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    // Console.WriteLine("\t\t Tid StartAddress: {0}", _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                    /// DebugMode 
                                                    /// adding ver 1.0.0.6 , Show DETAIL for Suspected threads by colors and more info          
                                                    if(Is_DebugMode)                                          
                                                    DebugMode_ThreadsDetailShow(Prcs.Id, Prcs.Threads[counter_threads].Id);

                                                    /// killing Infected Thread in IPS Mode
                                                    if (Is_IPS_Mode)
                                                    {
                                                        try
                                                        {
                                                            /// warning this code is Dangerous maybe you killing wrong process with StartAdress "0"                                             
                                                            bool Kill_TID = Kill_Thread(Prcs.Id, Prcs.Threads[counter_threads].Id);
                                                            if (Kill_TID)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                                Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                            }
                                                            else
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.Red;
                                                                Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} not Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                            }
                                                        }
                                                        catch (Exception error)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Green;
                                                            Console.WriteLine(System.DateTime.Now.ToString() + "   Maybe Thread not killed: " + error.Message);

                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    /// debug
                                                    //  Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    //  Console.WriteLine("----------Tid StartAddress: {0}", _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                }
                                                /// if startadrees was 0 maybe that thread is METERPRETER backdoor thread ;-/
                                                /// so i will show startadress 0 by Yellow color but this is not 100% correct always ;)
                                            }

                                        }
                                    }

                                }

                                buff = null;
                                return true;
                            }


                            buff = null;
                            GC.Collect(GC.MaxGeneration);
                            GC.WaitForPendingFinalizers();
                            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
                        }
                        if (Prcs.HasExited) { break; }
                    }

                }
                catch (Exception ee)
                {

                    //Console.WriteLine(ee.Message);
                }

            }
            catch (Exception)
            {
                return false;
            }
            buff = null;
            return false;

        }

        public static bool IPS_IDS = false;
        public static bool Is_IPS_Mode = false;
        public static bool Is_DebugMode = false;
        static bool IsShowAllRecrds = false;

        /// <summary>
        /// END Meterpreter Payload Detection MPD
        /// </summary>


        /// <summary>
        /// BEGIN ETW Monitoring (Created Thread Events) (Platform Traget : x64 only)
        /// Test version
        /// Note : (Platform Traget in your .NET Project should be x64 only)
        /// </summary>

        static Provider P = new Provider("Microsoft-Windows-Kernel-Process");
        static UserTrace T = new UserTrace();

        static void Main(string[] args)
        {
            bool start = false;
            Injected_Processes_IDsList.Add("0");
            Injected_Processes_IDsList.Add("0");

            Console.CancelKeyPress += Console_CancelKeyPress;

            Temp_Thread_InfoDebugMode = new DataTable();
            Temp_Thread_InfoDebugMode.Columns.Add("tid", typeof(int));
            Temp_Thread_InfoDebugMode.Columns.Add("Time_Negative");
            Temp_Thread_InfoDebugMode.Columns.Add("status");
            Temp_Thread_InfoDebugMode.Columns.Add("tid_StartAddress_x64");
            Temp_Thread_InfoDebugMode.Columns.Add("StartTime");
            Temp_Thread_InfoDebugMode.Columns.Add("Proc_Name");
            Temp_Thread_InfoDebugMode.Columns.Add("Proc_id");
            Temp_Thread_InfoDebugMode.Columns.Add("IsNewProcess");
            Temp_Thread_InfoDebugMode.Columns.Add("tid_StartAddress");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("ETWMonThread 1.0 (x64 only) ");
            Console.WriteLine("Realtime Scanning/Monitoring Thread Injection for MPD (Meterpreter Payload Detection) by ETW");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Published by Damon Mohammadbagher Jan 2018");

            if (args.Length == 0) { start = true; }
            if (args.Length == 1)
            {
                if (args[0].ToUpper() == "IPS") { IPS_IDS = true; start = true; } else { IPS_IDS = false; start = true; }
                if (args[0].ToUpper() == "SHOWALL") { IsShowAllRecrds = true; start = true; }
            }

            if (args.Length >= 2)
            {
                if (args[0].ToUpper() == "IPS" && args[1].ToUpper() == "DEBUG") { IPS_IDS = true; Is_DebugMode = true; start = true; }
                if (args[0].ToUpper() == "SHOWALL" && args[1].ToUpper() == "DEBUG") { IsShowAllRecrds = true; Is_DebugMode = true; start = true; }
            }

            if (args.Length >= 1)
                if (args[0].ToUpper() == "HELP")
                {
                    start = false;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine();
                    Console.WriteLine("[!] ETWMonThread , Realtime Scanning/Monitoring Thread Injection for MPD (Meterpreter Payload Detection) by ETW");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("[!] Syntax 1: Realtime Scanning/Monitoring IPS Mode (Killing Meterpreter Injected Threads)");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[!] Syntax 1: ETWMonThread.exe \"IPS\" [optional] \"DEBUG\"");
                    Console.WriteLine("[!] Example1: ETWMonThread.exe IPS ");
                    Console.WriteLine("[!] Example2: ETWMonThread.exe IPS DEBUG");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("[!] Syntax 2: Realtime Monitoring IDS Mode");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[!] Syntax 2: ETWMonThread.exe [optional] \"SHOWALL\" [optional] \"DEBUG\" ");
                    Console.WriteLine("[!] Example1: ETWMonThread.exe");
                    Console.WriteLine("[!] Example2: ETWMonThread.exe SHOWALL");
                    Console.WriteLine("[!] Example3: ETWMonThread.exe SHOWALL DEBUG");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            if (start)
            {
                Console.WriteLine();
                if (IPS_IDS)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[!] Realtime Scanning/Monitoring IPS Mode (warning : Killing Threads)");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[!] Realtime Monitoring IDS Mode");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                /// EventID 3 is for "Thread Created"
                var ETWEventsFilter = new EventFilter(Filter.EventIdIs(3));
                ETWEventsFilter.OnEvent += ETWEventsFilter_OnEvent;
                P.OnError += P_OnError;
                P.AddFilter(ETWEventsFilter);
                T.Enable(P);
                T.Start();
            }
            
        }

        public static void DoSomething(Int32 TPid, Int32 TTid)
        {
            try
            {
                /// Debug only
                //for (int i = 0; i < Injected_Processes_IDsList.Count; i++)
                //{
                //    if (i >= Injected_Processes_IDsList.Count - 5)
                //        Console.WriteLine(Injected_Processes_IDsList[i].ToString() + " [" + i.ToString() + "]");
                //}
                Thread.Sleep(4000);
                if (Convert.ToInt32(TPid) != 0 && Convert.ToInt32(TPid) != 4 && (!Process.GetProcessById(Convert.ToInt32(TPid)).HasExited))
                {
                    try
                    {

                        if (!Process.GetProcessById(Convert.ToInt32(TPid)).HasExited)
                        {
                            Process N_Prcs = Process.GetProcessById(Convert.ToInt32(TPid));
                            Found_Signature = Scan_Process_Memory(N_Prcs);
                        }
                        if (Found_Signature)
                        {
                            TempPid = Convert.ToInt32(TPid);
                            TempTid = Convert.ToInt32(TTid);
                            Kill_Thread(TempPid, TempTid);
                        }
                    }
                    catch (Exception)
                    {


                    }
                    if (!Found_Signature) { }
                }
            }
            catch (Exception)
            {

              
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            try
            {
                T.Stop();
                T.Dispose();
                P.Dispose();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
                  
        }

        static string ProcessName, TargetProcessName;
        static bool Found_Signature = false;
        static int TempPid, TempTid;      
        static Int32 LastTID = 0;
        static Int32 LastPID = 0;
        static List<string> Injected_Processes_IDsList = new List<string>();

        private static void ETWEventsFilter_OnEvent(IEventRecord ETWRecord)
        {
            /// Injector_Pid is Injector Process <ETWRecord.ProcessId>
            uint Injector_Pid = ETWRecord.ProcessId;
            uint Target_pid = ETWRecord.GetUInt32("ProcessID");
            uint Target_tid = ETWRecord.GetUInt32("ThreadID");
            /// This LastTID injected to LastPID by Injector_Pid
            LastTID = Convert.ToInt32(Target_tid);            
            LastPID = Convert.ToInt32(Target_pid);

            ProcessName = "Process Exited";
            TargetProcessName = "Process Exited";
            try
            {
                if (!Process.GetProcessById(Convert.ToInt32(Target_pid)).HasExited)
                    TargetProcessName = Process.GetProcessById(Convert.ToInt32(Target_pid)).ProcessName;
                if (!Process.GetProcessById(Convert.ToInt32(Injector_Pid)).HasExited)
                    ProcessName = Process.GetProcessById(Convert.ToInt32(Injector_Pid)).ProcessName;
            }
            catch (Exception)
            {

            }

            /// Detecting Thread Injection :
            /// if this was True then Thread was Injected to New Process (Target_pid)
            if (Injector_Pid != Target_pid)
            {
                try
                {
                    /// adding "PID" and "TID" also "Injector PID" to list
                    if ((!Process.GetProcessById(Convert.ToInt32(Target_pid)).HasExited))
                        Injected_Processes_IDsList.Add(LastPID.ToString() + ":" + LastTID.ToString() + ":" + Convert.ToInt32(Injector_Pid));
                }
                catch (Exception)
                {

                   
                }
                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[{0}] ", DateTime.Now.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Tid {0}", Target_tid.ToString());                
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(" injected to Process ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\"{0}:{1}\"", TargetProcessName, Target_pid.ToString());
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(" by this Process ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\"{0}:{1}\" ", ProcessName, Injector_Pid.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
                if (Convert.ToInt32(Target_pid) != 4 && Convert.ToInt32(Target_pid) != 0 && Is_DebugMode)
                    DebugMode_ThreadsDetailShow(Convert.ToInt32(Target_pid), Convert.ToInt32(Target_tid));

                if (IPS_IDS)
                {

                    if (Injected_Processes_IDsList.Count > 2)
                    {
                        try
                        {
                            if (!Process.GetProcessById(Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1].Split(':')[0])).HasExited)
                            {
                                DoSomething(Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1].Split(':')[0]), Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1].Split(':')[1]));
                                //Console.ForegroundColor = ConsoleColor.Cyan;
                                //Console.WriteLine = ("[{0}] Process:Thread {1} Scanned", DateTime.Now.ToString(), Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1]);
                                Console.Title = "[ " + DateTime.Now.ToString() + " ] Process:Thread " + Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1].Split(':')[0] + ":" + Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 1].Split(':')[1] + " Scanned";
                            }
                            else if (!Process.GetProcessById(Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2].Split(':')[0])).HasExited)
                            {
                                DoSomething(Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2].Split(':')[0]), Convert.ToInt32(Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2].Split(':')[1]));
                                //Console.ForegroundColor = ConsoleColor.Cyan;
                                //Console.WriteLine = ("[{0}] Process:Thread {1} Scanned", DateTime.Now.ToString(), Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2]);
                                Console.Title = "[ " + DateTime.Now.ToString() + " ] Process:Thread " + Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2].Split(':')[0] + ":" + Injected_Processes_IDsList[Injected_Processes_IDsList.Count - 2].Split(':')[1] + " Scanned";
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        catch (Exception)
                        {


                        }
                    }                 
                }
            }
            else
            {
                try
                {
                    if (IsShowAllRecrds)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("[{0}] ", DateTime.Now.ToString());
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Tid {0}", Target_tid.ToString());
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" Created in Process ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\"{0}:{1}\"", TargetProcessName, Target_pid.ToString());
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(" by this Process ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\"{0}:{1}\" ", ProcessName, Injector_Pid.ToString());
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (Convert.ToInt32(Target_pid) != 4 && Convert.ToInt32(Target_pid) != 0 && Is_DebugMode) DebugMode_ThreadsDetailShow(Convert.ToInt32(Target_pid), Convert.ToInt32(Target_tid));
                    }
                }
                catch (Exception)
                {

                  
                }

            }
        }

        private static void P_OnError(EventRecordError error)
        {
            T.Stop();
            T.Dispose();
            P.Dispose();
            Console.WriteLine(error.Message);
        }

        /// <summary>
        /// END ETW Monitoring (Created Thread Events)
        /// Test version
        /// Note : (Platform Traget in your .NET Project should be x64 only)
        /// </summary>
    }
}
