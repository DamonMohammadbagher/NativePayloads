﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Management.Instrumentation;
using System.ComponentModel;
using System.Reflection;
using System.Threading;



// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Meterpreter_Payload_Detection by Damon Mohammadbagher")]
[assembly: AssemblyDescription("Console version Published by Damon Mohammadbagher but Special thanks from these guys Rohan Vazarkar, David Bitner")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Meterpreter_Payload_Detection")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("51e71215-8465-4cc2-9dc4-8a512d339437")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.5")]
[assembly: AssemblyFileVersion("1.0.0.5")]

namespace Meterpreter_Payload_Detection
{
    class Program
    {
        /// <summary>
        ///  Meterpreter Signature value and Scan_Process_Memory() , _Get_Arguments() , IndexOf() and
        ///  ParentProcessUtilities class Codes made by these guys Rohan Vazarkar, David Bitner         
        ///  other Codes made by Damon Mohammadbagher ;-)
        /// </summary>
        /// <param name="Codes Authors"></param>
        /// <returns></returns>



        /// Console version Published by me but orginal code was for these guys 
        /// and Special thanks from these guys Rohan Vazarkar, David Bitner

        /// note : if you getting Error , run this tool with arguments like IPS or IDS
        /// syntax : Meterpreter_Payload_Detection.exe IPS
        /// syntax : Meterpreter_Payload_Detection.exe IDS
        /// syntax : Meterpreter_Payload_Detection.exe blobblob

        /// <summary>
        /// Meterpreter_Payload_Detection.exe
        /// .Net Framework 3.5
        /// guys if you have signatures here is for you ;-)
        /// help me for improve this code and 
        /// make Signatures by base64 and dont use RAW bytes array values in source code 
        /// sometimes this Application detect itself like a backdoor
        /// you can fix this problem with one IF too
        /// i am not pro in API programming by C#.net if you see any problem in my source code please tell me
        /// or Publish update code for this tool thank you all guys
        /// </summary>
        

        /// Special thanks from these guys Rohan Vazarkar, David Bitner
        /// Because their codes and Signature help me to make this code for console application  
        static string Meterpreter_Signature = @"jIubno+WoIyGjKCPjZCcmoyMoJiai4+Wmw==";
        static byte[] _Meterpreter__Bytes_signature = Convert.FromBase64String(Meterpreter_Signature);

        
        /// adding in version 1.0.0.5
        /// use Async Methods for Fixing Console Output Result ;) but sometime doesn't work very well
        /// i should change code for fix Colors Problem anyway.
        /// fixing huge Usage Memory in version (1.0.0.4) by SetProcessWorkingSetSize Method and GC and avoiding exception in threads 
        /// this code is faster than Previous version like (1.0.0.4)
        /// finally all error output colors changed also show them with time
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
                        Console.WriteLine("{0}", input_srt_Data);
                        return String.Format("time was {0}.", callDuration.ToString());
                       
                    }
                    else
                    {
                        Console.ForegroundColor = color;
                        Thread.Sleep(callDuration);
                        Console.WriteLine("{0} {1}", input_srt_Data, input_srt_Data2);
                        return String.Format("time was {0}.", callDuration.ToString());
                       
                    }
                    break;
                }
            }
            catch (Exception)
            {
                return String.Format("time was {0}.", callDuration.ToString());
               //throw;
            }
        }
        [DllImport("kernel32.dll")][return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetProcessWorkingSetSize(IntPtr process,UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
                
        
        /// killing threads IPS Mode [on]
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);
        public static bool Kill_Thread( int Process_ID )
        {
            try
            {
                /// Switch IPS Mode [on]
                /// 
                /// syntax ==> RunAs Administrator :  C:\> Meterpreter_Payload_Detection.exe IPS
                /// 
                /// for using this Method you should RunAs Administrator this application in Command Prompt
                /// 
                /// kill Threads with Startaddress = 0 only for infected Process
                /// just for test because my c# Backdoor OR Powershell payloads by Social Engineer Toolkit SET
                /// using Startadress 0 for their Backdoor Payload Threads .
                /// but we need better method for Detecting Infected Threads in Process 
                /// i hope someone have Idea about this , and help me for fix this ;)
                ProcessThreadCollection processThreads = Process.GetProcessById(Process_ID).Threads;
                foreach (ProcessThread pt in processThreads)
                {
                    
                    IntPtr Target_Thread_for_kill = OpenThread(1, false, (uint)pt.Id);
                    if (pt.StartAddress.ToString() == "0")
                    {                        
                        TerminateThread(Target_Thread_for_kill, 1);
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
              
                Console.WriteLine("Thread Killing Error : " ,err.Message);
                return false;
            }
            
            return false;
          
        }

        
        /// Core_Method_For_NewProcess Code here with Core_Thread_for_NewProcess         
        public static Int32 Temp_New_Process_Pid = 0;
        static string NewProcess_Name;
        static Int32 NewProcess_PID = 0;
        public static Thread Core_Thread_for_NewProcess;
        public static EventArrivedEventArgs _e_Temp;
        public static void Core_Method_For_NewProcess()
        {
            AsyncMethodCaller Call_NewPrcs_Core = new AsyncMethodCaller(_Async_WriStr_Console);
            IAsyncResult result_Async;

            try
            {
                
                bool find = false;
                Temp_New_Process_Pid = 0;
                NewProcess_PID = 0;
                NewProcess_PID = Convert.ToInt32(_e_Temp.NewEvent.Properties["ProcessID"].Value.ToString());
                NewProcess_Name = _e_Temp.NewEvent.Properties["ProcessName"].Value.ToString();
                /// Return PID for New Process
                Temp_New_Process_Pid = NewProcess_PID;
               
                Process N_Prcs = Process.GetProcessById(NewProcess_PID);
                try
                {
                   
                    /// you need time for complete loading process in memory : (5000 ... 150000)
                    /// this time was good before scanning new Process in memory 
                    System.Threading.Thread.Sleep(5000);                  
                    
                    find = Scan_Process_Memory(N_Prcs);                    

                    if (find)
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(1);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("\t Infected Process should be killed : {0}", N_Prcs.ProcessName);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\t Infected Process path : {0}", N_Prcs.MainModule.FileName);

                        }
                        catch (Exception error)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(System.DateTime.Now.TimeOfDay.ToString() + " : " + error.Message);
                            // break;
                        }
                    }
                    else
                    {
                       
                        try
                        {
                            Process _p_alive = Process.GetProcessById(N_Prcs.Id);
                            if (_p_alive != null)
                            {
                               // Console.WriteLine(" {0} : {2} {1}  is OK", System.DateTime.Now.TimeOfDay.ToString(), N_Prcs.ProcessName, N_Prcs.Id.ToString());
                                result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Gray, System.DateTime.Now.TimeOfDay.ToString() + " : " + N_Prcs.Id.ToString(), N_Prcs.ProcessName + "  is OK", 1, null, null);
                            }
                            else
                            {
                                result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + N_Prcs.Id.ToString(), N_Prcs.ProcessName + " has Exited - not Found", 1, null, null);
                            }
                        }
                        catch (Exception)
                        {
                            result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + N_Prcs.Id.ToString(), N_Prcs.ProcessName + " has Exited - not Found", 1, null, null);
                        }
                       
                        System.Threading.Thread.Sleep(1);
                    }
                }
                catch (Exception error)
                {
                    if (error.Message == "Process has exited, so the requested information is not available.")
                    {
                        //Console.WriteLine(System.DateTime.Now.TimeOfDay.ToString() + " : Error , Process has Exited - not Found");
                        result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : Error , Process has Exited - not Found",null, 1, null, null);
                    }
                    else
                    {                       
                        result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + error.Message, null, 1, null, null);
                    }

                }

            }
            catch (Exception error)
            {
                result_Async = Call_NewPrcs_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + error.Message, null, 1, null, null);
            }
        }
        public static void ProcessStartEventArrived(object sender, EventArrivedEventArgs e)
        {
            _e_Temp = e;
            Core_Thread_for_NewProcess = new Thread(Core_Method_For_NewProcess);
            Core_Thread_for_NewProcess.Priority = ThreadPriority.AboveNormal;
            Core_Thread_for_NewProcess.Start();
        }


        /// Meterpreter_Payload_Detection Code here with Core_Thread                
        public static Thread Core_Thread;
        public static string[] _myrgs;
        static Process[] myProcess;
        static bool Is_IPS_Mode = false;        
        public static void Core_Method()
        {
            AsyncMethodCaller Call_Core = new AsyncMethodCaller(_Async_WriStr_Console);
            IAsyncResult result_Async;
            
            
            try
            {               
                string[] args = new string[_myrgs.Length];
                args = _myrgs;
                ManagementEventWatcher _Watcher = null;                
                WqlEventQuery Query;
                Query = new WqlEventQuery();
                Query.EventClassName = "Win32_ProcessStartTrace";
                _Watcher = new ManagementEventWatcher(Query);
                _Watcher.EventArrived += new EventArrivedEventHandler(ProcessStartEventArrived);
                _Watcher.Start();


                while (true)
                {                                                              
                    string IPD_IDS = " ";
                    try
                    {
                        try
                        {
                            /// fixing ERROR for Arguments ;-) Done
                            if (args[0].ToUpper() == "IPS")
                            {
                                IPD_IDS = "IPS Mode [ON]";
                                /// do IPS Mode
                                Is_IPS_Mode = true;
                            }
                            else
                            {
                                IPD_IDS = "IDS Mode only";
                                /// do default Mode
                                Is_IPS_Mode = false;
                            }
                            /// fixing ERROR for Arguments ;-) Done
                        }
                        catch (Exception)
                        {
                            IPD_IDS = "IDS Mode only";
                            /// do default Mode
                            Is_IPS_Mode = false;
                           
                        }

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine(@"[#] Meterpreter Payload Detection");
                        Console.WriteLine(@"[#] IDS-IPS Version: {0}", Assembly.GetEntryAssembly().GetName().Version.ToString());
                        Console.WriteLine(@"[#] Console version Published by Damon Mohammadbagher");
                        Console.WriteLine(@"[#] API code and Meterpreter Signature by Rohan Vazarkar, David Bitner");
                        Console.WriteLine(@"[#] {0} Started time ", System.DateTime.Now.ToString());

                        if (IPD_IDS == "IPS Mode [ON]") { Console.ForegroundColor = ConsoleColor.Yellow; }
                        Console.WriteLine("[#] {0}", IPD_IDS);
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        myProcess = Process.GetProcesses();
                        Console.WriteLine("Scanning {0} process  ", myProcess.Length);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    catch (Exception)
                    {
                        IPD_IDS = " ";
                    }
                    foreach (Process P_item in myProcess)
                    {
                       
                        try
                        {
                            System.Threading.Thread.Sleep(1);
                            bool find = Scan_Process_Memory(P_item);
                            System.Threading.Thread.Sleep(1);

                            if (find)
                            {
                                try
                                {
                                    System.Threading.Thread.Sleep(1);
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\t Infected Process should be killed : {0}", P_item.ProcessName);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\t Infected Process path : {0}", P_item.MainModule.FileName);

                                }
                                catch (Exception)
                                {
                                    // break;
                                }
                            }
                            else
                            {
                                                       
                                System.Threading.Thread.Sleep(1);
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                try
                                {
                                    Process _p_alive = Process.GetProcessById(P_item.Id);
                                    if (_p_alive != null)
                                    {                                                                                
                                        result_Async = Call_Core.BeginInvoke(ConsoleColor.DarkGreen, System.DateTime.Now.TimeOfDay.ToString() + " : " + P_item.Id.ToString(), P_item.ProcessName + "  is OK", 1, null, null);
                                    }
                                    else
                                    {
                                        result_Async = Call_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + P_item.Id.ToString(), P_item.ProcessName + " has Exited - not Found", 1, null, null);                                        
                                    }
                                }
                                catch (Exception)
                                {
                                    result_Async = Call_Core.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + P_item.Id.ToString(), P_item.ProcessName + " has Exited - not Found", 1, null, null);
                                }
                               
                                System.Threading.Thread.Sleep(1);
                            }
                        }
                        catch (Exception error)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(" Error CoreThread : " + error.Message);
                        }
                       
                    }
                     Console.ForegroundColor = ConsoleColor.Gray;
                    /// wait every 1 min ;-)               
                    System.Threading.Thread.Sleep(60000);
                    GC.Collect(GC.MaxGeneration);
                    GC.WaitForPendingFinalizers();
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle,(UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);

                }
            }
            catch (Exception error)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t You should RunAs Administrator this Application : " + error.Message);                                
             
            }
            
        }       

        
        
        static void Main(string[] args)
        {
            try
            {                                             
                /// make thread for faster performance but i think i should change this code ;)
                _myrgs = args;                
                Core_Thread = new Thread(Core_Method);
                // "AboveNormal" Priority : make better performance for 
                // Async methods and Codes , i hope its works better ;)
                Core_Thread.Priority = ThreadPriority.AboveNormal;
                Core_Thread.Start();
                ///make thread for faster performance but i think i should change this code ;)                                                              
             
            }
            catch (Exception ee)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("omfg Main error: " + ee.Message);
            }
        }


        /// <summary>
        ///  Meterpreter Signature value and Scan_Process_Memory() , _Get_Arguments() , IndexOf() and
        ///  ParentProcessUtilities class Codes made by these guys Rohan Vazarkar, David Bitner         
        ///  others Codes made by Damon Mohammadbagher ;-)
        /// </summary>
        /// <param name="Codes Authors"></param>
        /// <returns></returns>
        
        public static bool Scan_Process_Memory(Process Prcs)
        {

            byte[] buff;
            AsyncMethodCaller Call = new AsyncMethodCaller(_Async_WriStr_Console);
            IAsyncResult result_Async;
            
            try
            {
                if (Prcs.HasExited)
                {

                    buff = null;
                    return false;
                }


                try
                {

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
                        if (Prcs.HasExited) { break; }
                    }

                    for (int i = 0; i < MemReg.Count; i++)
                    {
                       

                        if (!Prcs.HasExited)
                        {
                            buff = new byte[MemReg[i].RegionSize.ToInt64()];
                            ReadProcessMemory(Prcs.Handle, MemReg[i].BaseAddress, buff, MemReg[i].RegionSize.ToInt32(), IntPtr.Zero);
                         

                            for (int j = 0; j < buff.Length; j++)
                            {
                                buff[j] = (byte)(buff[j] ^ 0xFF);
                            }
                            
                            long Result = IndexOf(buff, _Meterpreter__Bytes_signature);

                            if (Result > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                for (int f = 0; f < Prcs.Threads.Count; f++)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    var Sub_Threads = Prcs.Threads[f].StartAddress.ToInt64();
                                    /// do this code in if only 1 time in loop f == 0
                                    if (f <= 0)
                                    {
                                        try
                                        {
                                            result_Async = Call.BeginInvoke(ConsoleColor.Red, "\n" + System.DateTime.Now.TimeOfDay.ToString(), null, 0, null, null);

                                            result_Async = Call.BeginInvoke(ConsoleColor.Red, "\t Warning : Meterpreter Process Found in Memory !!!", null, 1, null, null);

                                            result_Async = Call.BeginInvoke(ConsoleColor.Red, "\t Process BaseAddress : ", Prcs.MainModule.BaseAddress.ToInt64().ToString(), 2, null, null);

                                            result_Async = Call.BeginInvoke(ConsoleColor.Red, "\t Process EntryPointAddress : ", Prcs.MainModule.EntryPointAddress.ToInt64().ToString(), 2, null, null);

                                            result_Async = Call.BeginInvoke(ConsoleColor.Red, "\t Infected Process: " + Prcs.ProcessName + " : ", Prcs.Id.ToString(), 2, null, null);

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
                                            result_Async = Call.BeginInvoke(ConsoleColor.Green, System.DateTime.Now.TimeOfDay.ToString() + " : " + _eee.Message, null, 1, null, null);                                            
                                        }

                                        // do something for view sub threads with colors
                                        for (int counter_threads = 0; counter_threads < Prcs.Threads.Count; counter_threads++)
                                        {
                                            // do something for view sub threads with colors
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("\t\t Process Thread ID: {0}", Prcs.Threads[counter_threads].Id);
                                            /// if Thread_startadrees was 0 maybe that thread is backdoor thread ;-/
                                            /// so i will show Thread_startadress 0 by Yellow color but this is not 100% currect ;)
                                            if (Prcs.Threads[counter_threads].StartAddress.ToString() == "0")
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("\t\t    Tid StartAddress: {0}", Prcs.Threads[counter_threads].StartAddress.ToString());
                                                /// killing Infected Thread in IPS Mode
                                                if (Is_IPS_Mode)
                                                {
                                                    try
                                                    {
                                                        /// warning this code is Dangerous maybe you killing wrong process with StartAdress "0"                                             
                                                        bool Kill_TID = Kill_Thread(Prcs.Id);
                                                        if (Kill_TID)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                                            Console.WriteLine("\t\t Process Thread ID: {0} with StartAddress: {1} Killed", Prcs.Threads[counter_threads].Id, Prcs.Threads[counter_threads].StartAddress.ToString());
                                                        }

                                                    }
                                                    catch (Exception error)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.Green;
                                                        Console.WriteLine(System.DateTime.Now.TimeOfDay.ToString() + "   Maybe Thread can't kill: " + error.Message);

                                                    }

                                                }

                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine("\t\t    Tid StartAddress: {0}", Prcs.Threads[counter_threads].StartAddress.ToString());
                                            }
                                            /// if startadrees was 0 maybe that thread is backdoor thread ;-/
                                            /// so i will show startadress 0 by Yellow color but this is not 100% currect ;)
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
        public static string _Get_Arguments(Process Prcs)
        {
            string toret = "";

            try
            {
                using (ManagementObjectSearcher s = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + Prcs.Id))
                {
                    foreach (ManagementObject obj in s.Get())
                    {
                        toret += obj["CommandLine"];
                    }
                }
                return toret;
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

        #region pinvoke imports
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
        [DllImport("winmm.dll")]
        internal static extern uint timeBeginPeriod(uint period);
        [DllImport("winmm.dll")]
        internal static extern uint timeEndPeriod(uint period);

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
        #endregion
    }
}
