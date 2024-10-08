using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
namespace NativePayload_PingSend
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

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_PingSend , Published by Damon Mohammadbagher , oct 2024");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_PingSend send data/string by ICMP Ping Requests ");
            Console.WriteLine("syntax : NativePayload_PingSend.exe 192.168.56.102 \"your string as payload to send by icmp packets\"");
            /// Initialize Winsock
            WSAData wsaData;
            if (WSAStartup(0x0202, out wsaData) != 0)
            {
                Console.WriteLine("WSAStartup failed");
                return;
            }

            /// Create a raw socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));

            /// Destination IP
            IPAddress destinationIP = IPAddress.Parse(args[0]);

            /// Construct ICMP packet
            byte[] icmpPacket = CreateIcmpPacket(args[1]);

            /// Send ICMP packet
            socket.SendTo(icmpPacket, new IPEndPoint(destinationIP, 0));

            
            WSACleanup();
        }

        private static byte[] CreateIcmpPacket(string payload)
        {
            /// ICMP Echo Request
            byte type = 8;
            byte code = 0;
            ushort checksum = 0;
            ushort identifier = (ushort)Environment.TickCount;
            ushort sequenceNumber = 1;

            byte[] payloadBytes = Encoding.ASCII.GetBytes(payload);
            /// 8 bytes for ICMP header
            int packetSize = 8 + payloadBytes.Length;

            byte[] packet = new byte[packetSize];
            packet[0] = type;
            packet[1] = code;
            Array.Copy(BitConverter.GetBytes(checksum), 0, packet, 2, 2);
            Array.Copy(BitConverter.GetBytes(identifier), 0, packet, 4, 2);
            Array.Copy(BitConverter.GetBytes(sequenceNumber), 0, packet, 6, 2);
            Array.Copy(payloadBytes, 0, packet, 8, payloadBytes.Length);

            /// Calculate checksum
            checksum = CalculateChecksum(packet);
            Array.Copy(BitConverter.GetBytes(checksum), 0, packet, 2, 2);

            return packet;
        }

        private static ushort CalculateChecksum(byte[] buffer)
        {
            int length = buffer.Length;
            int index = 0;
            long sum = 0;

            while (length > 1)
            {
                sum += BitConverter.ToUInt16(buffer, index);
                index += 2;
                length -= 2;
            }

            if (length > 0)
            {
                sum += buffer[index];
            }

            while ((sum >> 16) != 0)
            {
                sum = (sum & 0xFFFF) + (sum >> 16);
            }

            return (ushort)~sum;
        }
    }
}