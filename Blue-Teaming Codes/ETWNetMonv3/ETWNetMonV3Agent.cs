using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;

namespace ETWNetMonV3Agent
{
    class Program
    {
        // static string Meterpreter_Signature = @"jIubno+WoIyGjKCPjZCcmoyMoJiai4+Wmw==";
        //  static byte[] _Meterpreter__Bytes_signature = Convert.FromBase64String(Meterpreter_Signature);

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
        static AsyncMethodCaller Call = new AsyncMethodCaller(_Async_WriStr_Console);
        static IAsyncResult result_Async;
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

        public static bool IPS_IDS = false;
        public static bool Is_IPS_Mode = true;
        public static bool Is_DebugMode = true;
        public static async Task logfilewrite(string filename, string text)
        {
            using (StreamWriter _file = new StreamWriter(filename, true))
            {
                await _file.WriteLineAsync(text);
            };

        }
        public static string PRCOCESSFileName = "";
        public static string PRCOCESSFileName2 = "";
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("ETWNetMonv3Agent Tool , Published by Damon Mohammadbagher , May 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("ETWNetMonv3Agent tool is simple ETW Network Monitor tool for TCP Connections (only)");
            Console.WriteLine();

            string Netmon = "ETWMonNet1_Ops";
            string Target_PID = "";
            List<MEMORY_BASIC_INFORMATION> MemReg = new List<MEMORY_BASIC_INFORMATION>();
            byte[] buff;
            bool Kill_TID = false;
            string Meterpreter_Signature = "jIubno+WoIyGjKCPjZCcmoyMoJiai4+Wmw==";
            byte[] _Meterpreter__Bytes_signature = Convert.FromBase64String(Meterpreter_Signature);
            string TPID = "";
            string logfile = "EtwNetMonv3logs.txt";
            using (var ETWs = new TraceEventSession(Netmon, null))
            {

                ETWs.StopOnDispose = true;
                int i = 0;
                Task t_asyn2;
                using (var source = new ETWTraceEventSource(Netmon, TraceEventSourceType.Session))
                {
                    GC.GetTotalMemory(true);
                    RegisteredTraceEventParser EvtNetMon1 = new RegisteredTraceEventParser(source);
                    EvtNetMon1.All += delegate (TraceEvent data)
                    {


                        PRCOCESSFileName2 = "";
                        PRCOCESSFileName = "";
                        GC.GetTotalMemory(true);
                        // Task t_asyn = logfilewrite(logfile, "test");
                        Kill_TID = false;
                        string ETWMsg = data.FormattedMessage;

                        Target_PID = "";
                        if (ETWMsg.Contains("(local=") || ETWMsg.Contains("PID"))
                        {
                            try
                            {
                                if (!Process.GetProcessById(data.ProcessID).HasExited)
                                    PRCOCESSFileName = Process.GetProcessById(data.ProcessID).MainModule.FileName;
                            }
                            catch (Exception)
                            {


                            }


                            if (ETWMsg.Contains("exists. State ="))
                            {
                                Target_PID = ETWMsg.Split('=')[4];
                                try
                                {
                                    if (!Process.GetProcessById(Convert.ToInt32(Target_PID)).HasExited)
                                        PRCOCESSFileName2 = Process.GetProcessById(Convert.ToInt32(Target_PID)).MainModule.FileName;
                                }
                                catch (Exception)
                                {


                                }
                                t_asyn2 = logfilewrite(logfile, "[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + Target_PID + "]");
                                Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + Target_PID + "]" + " " + "ProcessPath: " + PRCOCESSFileName2);

                                Target_PID = "";
                            }
                            if ((ETWMsg.Contains("PID")) && (!ETWMsg.Contains("exists. State =")))
                            {
                                try
                                {
                                    if (!Process.GetProcessById(data.ProcessID).HasExited && data.ProcessID != 0)
                                    {
                                        PRCOCESSFileName = Process.GetProcessById(data.ProcessID).ProcessName;
                                    }
                                    else
                                    {
                                        PRCOCESSFileName = "null";
                                    }
                                }
                                catch (Exception)
                                {
                                }
                                if (data.ProcessID == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString());
                                }
                                else if (data.ProcessID != 0)
                                {
                                    if (ETWMsg.Contains("connect completed. PID ="))
                                    {
                                        /// something like error or bug (sometmes) or ... , data.processid is not same with ETW events_text
                                        Console.ForegroundColor = ConsoleColor.DarkRed; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString());
                                    }
                                    else if (!ETWMsg.Contains("connect completed. PID ="))
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "PID: " + data.ProcessID + " " + "ProcessPath: " + PRCOCESSFileName);
                                    }
                                }

                                /// switch to show some details about ETW Records  (by default off)
                                ///                               
                                if (args.Length >= 1 && args[0].ToUpper() == "DBG")
                                {
                                    foreach (var item in data.PayloadNames)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;

                                        if (i == 2) Console.Write('\n' + "[etw]");
                                        if (i == 0) Console.Write("[etw] [" + item + ": " + data.PayloadValue(i).ToString() + "]");
                                        if (i > 0) Console.Write(" [" + item + ": " + data.PayloadValue(i).ToString() + "]");

                                        i++;
                                    }
                                    Console.Write("\n");
                                    i = 0;
                                }
                            }

                            /// this section of code, used for Memory SCAN....
                            /// 
                            if (ETWMsg.Contains("connect completed. PID ="))
                            {
                                TPID = ETWMsg.Split('=')[3].ToString().Split('.')[0];
                                if (TPID.Contains(','))
                                {
                                    Target_PID = TPID.Split(',')[0] + TPID.Split(',')[1];
                                    try
                                    {
                                        PRCOCESSFileName = "";
                                        try
                                        {
                                            if (!Process.GetProcessById(Convert.ToInt32(Target_PID)).HasExited)
                                                PRCOCESSFileName = Process.GetProcessById(Convert.ToInt32(Target_PID)).ProcessName;
                                        }
                                        catch (Exception)
                                        {
                                        }
                                        t_asyn2 = logfilewrite(logfile, "[>] [" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + Target_PID.ToString() + "]" + " " + "ProcessPath: " + PRCOCESSFileName);

                                        /// dont use this switch if you want only woRk with ETW Log files , this switch "SCAN" is only for Meterpreter Payload Detection (Tested & worked on KALI 2018 with MSF4 ver 4.x & client WIN7X64SP1)
                                        /// note: this Meterpreter Signature was not work on new version of Metasploit MSF6 (ver 6).
                                        /// SCAN => switch to scan memory for each process which has network connection (ESTABLISHED state) to find Meterpreter Signature (by default off)
                                        if (args.Length >= 1 && args[0].ToUpper() == "SCAN")
                                        {
                                           
                                            Task.Factory.StartNew(() =>
                                            {
                                               

                                                if (!Process.GetProcessById(Convert.ToInt32(Target_PID)).HasExited)
                                                {
                                                    Process Prcs = Process.GetProcessById(Convert.ToInt32(Target_PID));
                                                    try
                                                    {
                                                        if (Prcs.HasExited)
                                                        {
                                                            buff = null;
                                                        }
                                                        try
                                                        {
                                                            try
                                                            {
                                                                Console.Title = "Scanning Memory: ";
                                                                IntPtr Addy = new IntPtr();
                                                                MemReg.Clear();
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
                                                            }
                                                            catch (Exception e)
                                                            {

                                                            }
                                                            for (int _i = 0; _i < MemReg.Count; _i++)
                                                            {
                                                                if (Kill_TID) break;
                                                                if (Prcs.HasExited) { break; }
                                                                Console.Title = "Scanning Memory: " + Prcs.ProcessName.ToString() + " (" + Prcs.Id.ToString() + ") :: Memory.Count[" + (MemReg.Count - _i).ToString() + "]";
                                                                if (!Prcs.HasExited)
                                                                {
                                                                    buff = new byte[MemReg[_i].RegionSize.ToInt64()];
                                                                    ReadProcessMemory(Prcs.Handle, MemReg[_i].BaseAddress, buff, MemReg[_i].RegionSize.ToInt32(), IntPtr.Zero);
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
                                                                                    t_asyn2 = logfilewrite(logfile, "[X] [" + DateTime.Now.ToString() + "]::" + "[Meterpreter Process Found in-Memory]" + "[PID:" + Prcs.Id.ToString() + "] [PName:" + Prcs.ProcessName + "]");

                                                                                    string _source = BitConverter.ToString(buff);
                                                                                    string pattern = BitConverter.ToString(_Meterpreter__Bytes_signature);
                                                                                    int _index = _source.IndexOf(pattern);
                                                                                    Console.WriteLine("\t Network Connection for Meterpreter Process Found in-Memory");
                                                                                    Console.WriteLine("\t Network Connection via PID:{0}", Prcs.Id.ToString());

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
                                                                                        //  Console.WriteLine("bingo8, {0}", Target_PID);
                                                                                        if (Prcs.Threads[counter_threads].StartAddress.ToString() == "0" || Prcs.Threads[counter_threads].WaitReason.ToString().Contains("Exe"))
                                                                                        {
                                                                                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                                                                                            try
                                                                                            {
                                                                                                Kill_TID = Kill_Thread(Prcs.Id, Prcs.Threads[counter_threads].Id);
                                                                                                if (Kill_TID)
                                                                                                {
                                                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                    t_asyn2 = logfilewrite(logfile, "[X] [" + DateTime.Now.ToString() + "]::" + "[Thread Killed => TID:" + Prcs.Threads[counter_threads].Id + " with StartAddress:" + _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id) + "] [PID:" + Prcs.Id.ToString() + "] [PName:" + Prcs.ProcessName + "]");

                                                                                                    Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                                                                    break;
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
                                                                                        else
                                                                                        {
                                                                                            /// debug
                                                                                            // Console.ForegroundColor = ConsoleColor.DarkRed;
                                                                                            // Console.WriteLine("----------Tid StartAddress: {0}", _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (Prcs.Threads[counter_threads].StartAddress.ToString() == "0" || Prcs.Threads[counter_threads].ThreadState.ToString().Contains("Run"))
                                                                                        {
                                                                                            //  Console.WriteLine("bingo9, {0}", Target_PID);

                                                                                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                                                                                            try
                                                                                            {
                                                                                                /// warning this code is Dangerous maybe you killing wrong process with StartAdress "0"                                             
                                                                                                Kill_TID = Kill_Thread(Prcs.Id, Prcs.Threads[counter_threads].Id);
                                                                                                if (Kill_TID)
                                                                                                {
                                                                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                                                                    Console.WriteLine("\t Process Thread ID: {0} with StartAddress: {1} Killed", Prcs.Threads[counter_threads].Id, _Return_Threads_StartAddress(Prcs.Threads[counter_threads].Id));
                                                                                                    break;
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
                                                                    }


                                                                    buff = null;
                                                                    GC.Collect(GC.MaxGeneration);
                                                                    GC.WaitForPendingFinalizers();
                                                                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);
                                                                }
                                                                if (Prcs.HasExited) { break; }
                                                                if (Kill_TID) break;
                                                            }

                                                        }
                                                        catch (Exception ee)
                                                        {
                                                            //Console.WriteLine(ee.Message);
                                                        }

                                                    }
                                                    catch (Exception)
                                                    {
                                                        //  return false;
                                                    }
                                                    buff = null;
                                                    //  return false;
                                                }
                                            });
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }

                            }
                            System.Threading.Thread.Sleep(5);
                            if (ETWMsg.Contains(" requested to connect"))
                            {

                                t_asyn2 = logfilewrite(logfile, "[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + data.ProcessID.ToString() + "]" + " ProcessPath: " + PRCOCESSFileName);
                                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "by PID:" + data.ProcessID.ToString() + " ProcessPath: " + PRCOCESSFileName);
                            }
                            System.Threading.Thread.Sleep(5);
                            if (ETWMsg.Contains("connect proceeding"))
                            {
                                t_asyn2 = logfilewrite(logfile, "[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + data.ProcessID.ToString() + "]");

                                Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString());
                            }
                            System.Threading.Thread.Sleep(5);
                            if (ETWMsg.Contains(" close issued") || ETWMsg.Contains(" close"))
                            {
                                t_asyn2 = logfilewrite(logfile, "[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + data.ProcessID.ToString() + "]");

                                Console.ForegroundColor = ConsoleColor.DarkGreen; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString());
                            }
                            System.Threading.Thread.Sleep(5);
                            if (ETWMsg.Contains("shutdown initiated"))
                            {
                                t_asyn2 = logfilewrite(logfile, "[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString() + "[PID:" + data.ProcessID.ToString() + "]");

                                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("[" + DateTime.Now.ToString() + "]::" + ETWMsg.Split('(')[1].ToString());
                            }

                        }
                    };
                    Guid provider = new Guid("2F07E2EE-15DB-40F1-90EF-9D7BA282188A");
                    ETWs.EnableProvider(provider, TraceEventLevel.Verbose);
                    source.Process();
                    Console.CancelKeyPress += (object s, ConsoleCancelEventArgs e) => ETWs.Stop();
                    ETWs.Dispose();
                };

            }

        }

    }

}
