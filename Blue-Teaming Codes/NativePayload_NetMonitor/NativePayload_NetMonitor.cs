using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace NativePayload_NetMonitor
{
    class Program
    {
        [DllImport("ws2_32.dll")]
        private static extern int WSAStartup(ushort wVersionRequested, out WSAData wsaData);

        [DllImport("ws2_32.dll")]
        private static extern int WSACleanup();

        [StructLayout(LayoutKind.Sequential)]
        private struct WSAData
        {
            public ushort wVersion;
            public ushort wHighVersion;
            public IntPtr szDescription;
            public IntPtr szSystemStatus;
            public ushort iMaxSockets;
            public ushort iMaxUdpDg;
            public IntPtr lpVendorInfo;
        }
        public static StreamWriter logWriter;
        public static string result1 = "";
        public static string result2 = "";
        public static string result3 = "";
        public static string result4 = "";
        public static string result5 = "";
        public static string result6 = "";
        public static int logCounter = 0;
        public static string logFileName;
        static async Task Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_NetMonitor , Published by Damon Mohammadbagher , 2024");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_NetMonitor Monitoring NetworkTraffic over [ICMP/ARP/TCP/UDP + HTTP + DNS]");
            Console.WriteLine("NativePayload_NetMonitor all logs saved into txt [file]");
            Console.WriteLine("NativePayload_NetMonitor syntax NativePayload_NetMonitor.exe ipaddress");
            Console.WriteLine("NativePayload_NetMonitor example NativePayload_NetMonitor.exe 192.168.56.101");
            Console.WriteLine("NativePayload_NetMonitor example NativePayload_NetMonitor.exe 127.0.0.1");
            Console.WriteLine();
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                Task.Run(() =>
                 {
                    /// Initialize Winsock
                    WSAData wsaData;
                     if (WSAStartup(0x0202, out wsaData) != 0)
                     {
                         Console.WriteLine("WSAStartup failed");
                         return;
                     }
                    

                     string baseFileName = $"network_log_{DateTime.Now:yyyyMMdd}";                    
                     logCounter = GetNextCounter(baseFileName);                    
                     logFileName = $"{baseFileName}_{logCounter}.txt";
                     Console.Title = Console.Title + " , logs saved into " + logFileName;
                     Console.WriteLine("[!] logs saved into " + logFileName);
                     logWriter = new StreamWriter(logFileName, true) { AutoFlush = true };

                     /// Create a raw socket
                     Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                     socket.Bind(new IPEndPoint(IPAddress.Parse(args[0]), 0));
                     socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

                     byte[] inValue = new byte[4] { 1, 0, 0, 0 };
                     byte[] outValue = new byte[4];
                     socket.IOControl(IOControlCode.ReceiveAll, inValue, outValue);


                     byte[] buffer = new byte[socket.ReceiveBufferSize];
                     while (true)
                     {
                         int bytesRead = socket.Receive(buffer);
                         if (bytesRead > 0)
                         {
                             ParsePacket(buffer, bytesRead).GetAwaiter().GetResult();
                         }
                     }
                 });
            });
            t.Priority = System.Threading.ThreadPriority.AboveNormal;            
            t.Start();

            /// Cleanup
            WSACleanup();
            Console.CancelKeyPress += Console_CancelKeyPress;

            Console.WriteLine("Press Ctrl+C to exit the program.");
            while (true) { var f = Console.ReadKey(); await Task.Delay(1000); }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);

        }

        private static async Task ParsePacket(byte[] buffer, int length)
        {
            // Parse the IP header
            int ipHeaderLength = (buffer[0] & 0x0F) * 4;
            int protocol = buffer[9];
            string srcIP = $"{buffer[12]}.{buffer[13]}.{buffer[14]}.{buffer[15]}";
            string destIP = $"{buffer[16]}.{buffer[17]}.{buffer[18]}.{buffer[19]}";

            Console.WriteLine($"Protocol: {protocol}, Source IP: {srcIP}, Destination IP: {destIP}");
        
            /// Check for different protocols
            if (protocol == 1) // ICMP
            {
               await logWriter.WriteLineAsync(await ParseICMPPacket(buffer, ipHeaderLength, length));
            }
            else if (protocol == 6) // TCP
            {
                await logWriter.WriteLineAsync(await ParseTCPPacket(buffer, ipHeaderLength, length));
            }
            else if (protocol == 17) // UDP
            {
                await logWriter.WriteLineAsync(await ParseUDPPacket(buffer, ipHeaderLength, length));
            }
            else if (BitConverter.ToUInt16(buffer, 12) == 0x0806) // ARP
            {
                await logWriter.WriteLineAsync(await ParseARPPacket(buffer, ipHeaderLength, length));
            }
            else if (IsBroadcast(buffer))
            {
                await logWriter.WriteLineAsync(await ParseBroadcastPacket(buffer, length));
            }
        }

        private static async Task<string> ParseICMPPacket(byte[] buffer, int ipHeaderLength, int length)
        {
            int icmpHeaderStart = ipHeaderLength;
            int type = buffer[icmpHeaderStart];
            int code = buffer[icmpHeaderStart + 1];
            int checksum = BitConverter.ToUInt16(buffer, icmpHeaderStart + 2);
            int identifier = BitConverter.ToUInt16(buffer, icmpHeaderStart + 4);
            int sequenceNumber = BitConverter.ToUInt16(buffer, icmpHeaderStart + 6);
            result1 = "";
            //Console.WriteLine($"ICMP Type: {type}, Code: {code}, Checksum: {checksum}, Identifier: {identifier}, Sequence Number: {sequenceNumber}");
            result1 += ($"ICMP Type: {type}, Code: {code}, Checksum: {checksum}, Identifier: {identifier}, Sequence Number: {sequenceNumber}") + "\n";
            result1 += await PrintHex(buffer, icmpHeaderStart, length - icmpHeaderStart);
            Console.WriteLine(result1);
            return result1 + "\n";
        }

        private static async Task<string> ParseTCPPacket(byte[] buffer, int ipHeaderLength, int length)
        {
            int tcpHeaderStart = ipHeaderLength;
            int srcPort = (buffer[tcpHeaderStart] << 8) + buffer[tcpHeaderStart + 1];
            int destPort = (buffer[tcpHeaderStart + 2] << 8) + buffer[tcpHeaderStart + 3];
            int seqNumber = BitConverter.ToInt32(buffer, tcpHeaderStart + 4);
            int ackNumber = BitConverter.ToInt32(buffer, tcpHeaderStart + 8);
            int dataOffset = (buffer[tcpHeaderStart + 12] >> 4) * 4;
            result2 = "";
            result2 += ($"TCP Source Port: {srcPort}, Destination Port: {destPort}, Sequence Number: {seqNumber}, Acknowledgment Number: {ackNumber}") + "\n";
            result2 += await PrintHex(buffer, tcpHeaderStart, length - tcpHeaderStart);

            // Check for HTTP (port 80 or 443)
            if (srcPort == 80 || destPort == 80 || srcPort == 443 || destPort == 443)
            {
                result2 += ("HTTP Packet") + "\n";
                result2 += await PrintHex(buffer, tcpHeaderStart + dataOffset, length - tcpHeaderStart - dataOffset) + "\n";
            }
            Console.WriteLine(result2);
            return result2;
        }

        private static async Task<string> ParseUDPPacket(byte[] buffer, int ipHeaderLength, int length)
        {
            int udpHeaderStart = ipHeaderLength;
            int srcPort = (buffer[udpHeaderStart] << 8) + buffer[udpHeaderStart + 1];
            int destPort = (buffer[udpHeaderStart + 2] << 8) + buffer[udpHeaderStart + 3];
            result3 = "";
            result3 += ($"UDP Source Port: {srcPort}, Destination Port: {destPort}") + "\n"; 
            
            // Check for DNS (port 53)
            if (srcPort == 53 || destPort == 53)
            {
                result3 += ("DNS Packet") + "\n";
                result3 += await PrintHex(buffer, udpHeaderStart, length - udpHeaderStart) + "\n";
            }
            
            // Check for DHCP (ports 67 and 68)
            if (srcPort == 67 || destPort == 67 || srcPort == 68 || destPort == 68)
            {
                result3 += ("DHCP Packet") + "\n";
                result3 += await PrintHex(buffer, udpHeaderStart, length - udpHeaderStart) + "\n";
            }
            Console.WriteLine(result3);
            return result3;
        }

        private static async Task<string> ParseARPPacket(byte[] buffer, int ipHeaderLength, int length)
        {
            result4 = "";
            int arpHeaderStart = ipHeaderLength;
            string senderIP = $"{buffer[arpHeaderStart + 14]}.{buffer[arpHeaderStart + 15]}.{buffer[arpHeaderStart + 16]}.{buffer[arpHeaderStart + 17]}";
            string targetIP = $"{buffer[arpHeaderStart + 24]}.{buffer[arpHeaderStart + 25]}.{buffer[arpHeaderStart + 26]}.{buffer[arpHeaderStart + 27]}";

            result4 += ($"ARP Sender IP: {senderIP}, Target IP: {targetIP}") + "\n";
            result4 += await PrintHex(buffer, arpHeaderStart, length - arpHeaderStart) + "\n";
            Console.WriteLine(result4);
            return result4;
        }

        private static async Task<string> ParseBroadcastPacket(byte[] buffer, int length)
        {
            Console.WriteLine("Broadcast Packet");
            return await PrintHex(buffer, 0, length);
        }

        private static async Task<string> PrintHex(byte[] buffer, int start, int length)
        {
            StringBuilder result = new StringBuilder();

            for (int i = start; i < start + length; i += 16)
            {
                result.AppendFormat("{0:X4}: ", i);

                /// Append hex values
                for (int j = 0; j < 16 && i + j < start + length; j++)
                    result.AppendFormat("{0:X2} ", buffer[i + j]);
                

                /// Append spaces to align ASCII characters
                for (int j = 16; j > (start + length - i); j--)
                    result.Append("   ");
                

                /// Append ASCII characters
                result.Append(" | ");
                for (int j = 0; j < 16 && i + j < start + length; j++)
                {
                    byte b = buffer[i + j];
                    if (b >= 32 && b <= 126) // Printable ASCII range
                    {
                        result.Append((char)b);
                    }
                    else
                    {
                        result.Append('.');
                    }
                }
                result.AppendLine();
            }

            return await Task.FromResult(result.ToString());
        }
        
        private static bool IsBroadcast(byte[] buffer)
        {
           
            return false;
        }

        static int GetNextCounter(string baseFileName)
        {             
            var existingFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), $"{baseFileName}_*.txt");           
            var counters = existingFiles
                .Select(file => Path.GetFileNameWithoutExtension(file))
                .Select(name => name.Split('_').Last())
                .Select(counter => int.TryParse(counter, out int result) ? result : 0)
                .ToList();
            
            return counters.Any() ? counters.Max() + 1 : 1;
        }

        static void WriteLog(string message)
        {           
            logCounter++;             
            using (StreamWriter logWriter = new StreamWriter(logFileName, true) { AutoFlush = true })
            {
                 logWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logCounter}] {message}");
            }
        }
    }
}