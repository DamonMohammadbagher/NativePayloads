using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETWNetMon3Log
{
  
}
class Program
{
    public static class XMLDataTableClass
    {
        public static DataTable TCPIPTable2 = new DataTable("ETW_Tcpip_log2");
        public static DataColumn TCPIPcolumn2;
        public static DataRow TCPIProw2;

        public static void TCPIP_settable2()
        {
            try
            {
                TCPIPTable2.Columns.Clear();
                TCPIPTable2.Rows.Clear();

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.DateTime");
                TCPIPcolumn2.ColumnName = "TIME";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                // Create second column.
                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = Type.GetType("System.String");
                TCPIPcolumn2.ColumnName = "events";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.Int32");
                TCPIPcolumn2.ColumnName = "PID";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.String");
                TCPIPcolumn2.ColumnName = "PName";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.String");
                TCPIPcolumn2.ColumnName = "lhost";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.Int32");
                TCPIPcolumn2.ColumnName = "lport";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.String");
                TCPIPcolumn2.ColumnName = "rhost";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = System.Type.GetType("System.Int32");
                TCPIPcolumn2.ColumnName = "rport";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);

                TCPIPcolumn2 = new DataColumn();
                TCPIPcolumn2.DataType = Type.GetType("System.String");
                TCPIPcolumn2.ColumnName = "state";
                TCPIPTable2.Columns.Add(TCPIPcolumn2);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);
            }
        }

        public static void GetRowsTODataTable(DateTime Time, string ETW_eventlog_Text, Int32 PID, string pname
            , string lhost, Int32 lport, string rhost, Int32 rport, string state)
        {

            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            try
            {
                TCPIProw2 = TCPIPTable2.NewRow();
                TCPIProw2["TIME"] = Time;
                TCPIProw2["events"] = ETW_eventlog_Text;
                TCPIProw2["PID"] = PID;
                TCPIProw2["pname"] = pname;
                TCPIProw2["lhost"] = lhost;
                TCPIProw2["lport"] = lport;
                TCPIProw2["rhost"] = rhost;
                TCPIProw2["rport"] = rport;
                TCPIProw2["state"] = state;
                TCPIPTable2.Rows.Add(TCPIProw2);
            }
            catch (Exception err)
            {


            }


        }
    }
    public delegate void MyDelegate_LogFileReader_Method();
    public delegate void MyDelegate_showdatagrid();
    public static string tmp = "";
    public static string ResultText = "";
    public static string ResultText2 = "";
    public static string Logfile = "EtwNetMonv3logs.txt";
    public static int line = 1;
    public static bool init = true;
    public static string _findstr(string s1, string str)
    {
        int index1 = str.LastIndexOf(s1);
        if (index1 == -1)
        {
            return " ";
        }
        else
        {
            return str.Substring(index1 + s1.Length, str.Length - index1 - s1.Length);
        }
    }
    public static string _findstr(string str)
    {
        if (str.ToUpper().Contains("ESTABLISHED") || str.ToUpper().Contains("CONNECT COMPLETED")) return "ESTABLISHED";
        if (str.ToUpper().Contains("SYNSENT") || str.ToUpper().Contains("REQUESTED TO CONNECT")) return "SYNSENT";
        if (str.ToUpper().Contains("CLOSEWAIT")) return "CLOSEWAIT";

        return "";
    }

    public static void logfilewrite(string filename, string text)
    {
        using (StreamWriter _file = new StreamWriter(filename))
        {
            _file.WriteLine(text);
        };
    }

    public static void help()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("ETWNetMonv3Log Tool , Published by Damon Mohammadbagher , May 2021");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("ETWNetMonv3Log cmd tool is simple ETW log Reader & (XML/HTML) Report Maker for ETW logs, which made by ETWNetMonv3Agent");
        Console.WriteLine();
        Console.WriteLine("syntax  1: switch XML, All ETW records from text [EtwNetMonv3logs.txt] will save to xml file (convert all records to xml)");
        Console.WriteLine("example 1: ETWNetMonv3Log.exe xml filename.xml ");
        Console.WriteLine("syntax  2: switch HTML , simple Filter for ETW Records and Save report to HTML format with simple table");
        Console.WriteLine("syntax  2-1: switch HTML2 , integrating ETWProcessMon Logs with ETW Network Log + simple Filter to HTML format with simple table");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"your query\" ");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rport >=80 OR events LIKE '*Established*' AND events LIKE '*connect complete*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*requested to connect*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"state NOT LIKE '*synsent*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rhost LIKE '192.168*' or lhost LIKE '192.168.1*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rport <=80 OR lport >=50000\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"pname LIKE '*.exe*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*C:\\*'\"");
        Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"PID = 1452\"");
        Console.WriteLine("syntax 2-1: ETWNetMonv3Log.exe html2 [MODE 0=SYNSENT , 1=ESTABLISHED] [Query]");
        Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"PID = 1452\"");
        Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  0  \"PID = 1452\"");
        Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"rport >= 80\"");
        Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"events LIKE '*192.168*'\"");
        Console.WriteLine("Note: for (syntax 2-1) EtwNetMonv3logs.txt & ETWProcessMonlog.txt should be in current folder");
        Console.WriteLine();

    }
    static void Main(string[] args)
    {
        try
        {


            Console.WriteLine();
            if (args.Length <= 1 && args[0].ToUpper() == "HELP")
            {
                help();
            }
            else
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("ETWNetMonv3Log Tool , Published by Damon Mohammadbagher , May 2021");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("ETWNetMonv3Log cmd tool is simple ETW log Reader & (XML/HTML) Report Maker for ETW logs, which made by ETWNetMonv3Agent");
                    Console.WriteLine();
                    Console.WriteLine("syntax  1: switch XML, All ETW records from text [EtwNetMonv3logs.txt] will save to xml file (convert all records to xml)");
                    Console.WriteLine("example 1: ETWNetMonv3Log.exe xml filename.xml ");
                    Console.WriteLine("syntax  2: switch HTML , simple Filter for ETW Records and Save report to HTML format with simple table");
                    Console.WriteLine("syntax  2-1: switch HTML2 , integrating ETWProcessMon Logs with ETW Network Log + simple Filter to HTML format with simple table");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"your query\" ");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rport >=80 OR events LIKE '*Established*' AND events LIKE '*connect complete*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*requested to connect*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"state NOT LIKE '*synsent*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rhost LIKE '192.168*' or lhost LIKE '192.168.1*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"rport <=80 OR lport >=50000\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"pname LIKE '*.exe*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"events LIKE '*C:\\*'\"");
                    Console.WriteLine("example 2: ETWNetMonv3Log.exe html \"PID = 1452\"");
                    Console.WriteLine("syntax 2-1: ETWNetMonv3Log.exe html2 [MODE 0=SYNSENT , 1=ESTABLISHED] [Query]");
                    Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"PID = 1452\"");
                    Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  0  \"PID = 1452\"");
                    Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"rport >= 80\"");
                    Console.WriteLine("example 2-1: ETWNetMonv3Log.exe html2  1  \"events LIKE '*192.168*'\"");
                    Console.WriteLine("Note: for (syntax 2-1) EtwNetMonv3logs.txt & ETWProcessMonlog.txt should be in current folder");
                    Console.WriteLine();


                    Thread.CurrentThread.Priority = ThreadPriority.Highest;
                    XMLDataTableClass.TCPIP_settable2();

                    string[] ETW_TCPIP_data = new string[8];
                    try
                    {

                        byte[] b;
                        string s;
                        Thread.CurrentThread.Priority = ThreadPriority.Highest;
                        using (FileStream myfile = new FileStream(Logfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            b = new byte[myfile.Length];
                            myfile.Read(b, 0, b.Length);
                        };

                        ResultText = Encoding.ASCII.GetString(b);

                    }
                    catch (Exception)
                    {


                    }
                    string[] textitems = null;

                    textitems = ResultText.Split('\r');


                    foreach (string item in textitems)
                    {
                        if (item != " " && (!item.Contains("\n ")))
                        {
                            line++;
                            try
                            {
                                if ((!item.Contains("exists. State = ")) && (!item.Contains("::ffff:")))
                                {

                                    XMLDataTableClass.GetRowsTODataTable(DateTime.Now, item,
                                         Convert.ToInt32(item.Split(')')[1].Split(']', ':')[1])
                                         , _findstr("ProcessPath:", item),
                                         _findstr("local=", item).Split(':')[0],
                                         Convert.ToInt32(_findstr("local=", item).Split(':', ' ')[1])
                                         , _findstr("remote=", item).Split(':')[0],
                                         Convert.ToInt32(_findstr("remote=", item).Split(':', ')')[1]),
                                         _findstr(item));

                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }

                    GC.GetTotalMemory(true);

                    Console.WriteLine("[>] ETW records loaded from file: \"" + "EtwNetMonv3logs.txt" + "\"");

                    if (args[0].ToUpper() == "XML")
                    {

                        XMLDataTableClass.TCPIPTable2.WriteXml(args[1].ToString());
                        XMLDataTableClass.TCPIPTable2.WriteXmlSchema("xml_schema.xml", true);
                        Console.WriteLine("[>] Full ETW log records saved to xml file: \"" + args[1].ToString() + "\"");
                        Console.WriteLine("[>] Xml schema saved to file: \"" + "xml_schema.xml" + "\".");
                        GC.GetTotalMemory(true);

                    }
                    else if (args[0].ToUpper() == "HTML2")
                    {
                        byte[] bb2;
                        string s2;
                        List<string> listx = new List<string>();
                        try
                        {
                            string[] textitems2 = null;
                            Thread.CurrentThread.Priority = ThreadPriority.Highest;
                            if (!File.Exists("ETWProcessMonlog.txt")) { Console.WriteLine("[>] Error => ETWProcessMon log file: \"" + "ETWProcessMonlog.txt" + "\" not found!"); }
                            using (FileStream myfile = new FileStream("ETWProcessMonlog.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                bb2 = new byte[myfile.Length];
                                myfile.Read(bb2, 0, bb2.Length);

                                ResultText2 = Encoding.ASCII.GetString(bb2);

                            };
                            textitems2 = ResultText2.Split('\r');
                            foreach (string item in textitems2)
                            {
                                listx.Add(item);
                            }
                            Console.WriteLine("[>] ETWProcessMon records loaded from file: \"" + "ETWProcessMonlog.txt" + "\"");

                        }
                        catch (Exception eeee)
                        {
                            Console.WriteLine(eeee.Message);
                        }

                        int counter = 1;
                        string html = "";
                        string query = args[2].ToString().Replace('"', ' ');
                        string style = "<html> <style>ul, #myUL {list-style-type: none;        }# myUL {    margin: 0;    padding: 0;    }" +
            ".caret { font:10pt Tahoma; cursor: pointer;" + "  -webkit-user-select: none; /* Safari 3.1+ */  -moz-user-select: none; /* Firefox 2+ */  -ms-user-select: none; /* IE 10+ */" +
            "  user-select: none;}.caret::before {  content: \"\\25B6\";  color: black;  display: inline-block;  margin-right: 0px;}" +
            ".caret-down::before {  -ms-transform: rotate(90deg); /* IE 9 */  -webkit-transform: rotate(90deg); /* Safari */'  transform: rotate(90deg);  }" +
            ".nested {  display: none;}.active {  display: block;}</style>";
                        string htmstring1 = style + "<H2>Search Result for ETW logs " + "<H3> Query (" + query + ")</H3></H2><table border=\"1\"><tr style =\"background-color:#a0a0ff;font:10pt Tahoma;font-weight:bold;\" align = \"left\" > ";
                        string htmstring2 = "<td>id</td><td>localtime</td><td>events</td><td>PID</td><td>PName/ProcessPath</td><td>lhost</td><td>lport</td><td>rhost</td><td>rport</td><td>state</td>";
                        string htmstring3 = " ";
                        string tmp1 = "";
                        string htmstring4 = "";
                        DataRow[] dt = XMLDataTableClass.TCPIPTable2.Select(args[2].ToString());
                        bool clr = false;
                        foreach (DataRow Rowitem in dt)
                        {

                            if (clr) { htmstring4 += "<tr style=\"background-color:#9b9b9b;font:10pt Tahoma;\">"; clr = false; }
                            else if (!clr) { htmstring4 += "<tr style=\"background-color:#00c0f0;font:10pt Tahoma;\">"; clr = true; }
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + counter.ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[0].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[1].ToString().Replace('\n', ' ') + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[2].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[3].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[4].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[5].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[6].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[7].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[8].ToString() + "</td>" + "</tr>";
                            if (args[1].ToUpper() == "0")
                            {
                                if (Rowitem[8].ToString().ToUpper() == "SYNSENT")
                                {
                                    try
                                    {

                                        foreach (string item in listx.FindAll(z => z.Contains("(" + Rowitem[2].ToString() + ")")))
                                        {
                                            htmstring4 += "<div\"><tr style=\"background-color:#19c0f0;\"><td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<ul id=\"myUL\">";
                                            if (item.Contains("[VirtualMemAlloc]"))
                                            {
                                                htmstring4 += "<li><span  class=\"caret\">VirtualMemAlloc</span><ul class=\"nested\"><li style=\"font:10pt Tahoma;\">" + item + "</li></ul></li>";
                                            }
                                            else if (item.Contains("[Injected"))
                                            {
                                                tmp1 = "";
                                                try
                                                {
                                                    tmp1 = item.Split(')', ':')[5];
                                                    if (!(Int32.Parse(tmp1) >= 0)) { tmp1 = "-1"; }
                                                }
                                                catch (Exception) { tmp1 = "x"; }

                                                htmstring4 += "<li><span class=\"caret\">Thread-Injection [TID:" + tmp1 + "]</span><ul class=\"nested\"><li style=\"color:darkred; font:11pt Tahoma;\">" + item + "</li></ul></li>";
                                            }

                                            /// old code
                                            //htmstring4 += "<div><tr style=\"background-color:#00c0f0;\"><td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            //htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            //htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + item + "</td></tr></div>";

                                            htmstring4 += "</ul>";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"font:10pt Tahoma; height: 0px; overflow - y:hidden\">" + Rowitem[3].ToString();
                                            htmstring4 += "</td></tr></div>";

                                        }
                                    }
                                    catch (Exception)
                                    {


                                    }
                                }
                            }
                            else if (args[1].ToUpper() == "1")
                            {
                                if (Rowitem[8].ToString().ToUpper() == "ESTABLISHED")
                                {
                                    try
                                    {

                                        foreach (string item in listx.FindAll(z => z.Contains("(" + Rowitem[2].ToString() + ")")))
                                        {
                                            htmstring4 += "<div\"><tr style=\"background-color:#19c0f0;\"><td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 0px; overflow - y:hidden\">";
                                            htmstring4 += "<ul id=\"myUL\">";
                                            if (item.Contains("[VirtualMemAlloc]"))
                                            {
                                                htmstring4 += "<li><span  class=\"caret\">VirtualMemAlloc</span><ul class=\"nested\"><li style=\"font:10pt Tahoma;\">" + item + "</li></ul></li>";
                                            }
                                            else if (item.Contains("[Injected"))
                                            {
                                                tmp1 = "";
                                                try
                                                {
                                                    tmp1 = item.Split(')', ':')[5];
                                                    if (!(Int32.Parse(tmp1) >= 0)) { tmp1 = "-1"; }
                                                }
                                                catch (Exception) { tmp1 = "x"; }

                                                htmstring4 += "<li><span class=\"caret\">Thread-Injection [TID:" + tmp1 + "]</span><ul class=\"nested\"><li style=\"color:darkred; font:11pt Tahoma;\">" + item + "</li></ul></li>";
                                            }

                                            /// old code
                                            //htmstring4 += "<div><tr style=\"background-color:#00c0f0;\"><td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            //htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            //htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + item + "</td></tr></div>";

                                            htmstring4 += "</ul>";
                                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">";
                                            htmstring4 += "<td align = \"left\"><span style=\"font:10pt Tahoma; height: 0px; overflow - y:hidden\">" + Rowitem[3].ToString();
                                            htmstring4 += "</td></tr></div>";

                                        }
                                    }
                                    catch (Exception)
                                    {


                                    }
                                }
                            }

                            counter++;

                        }
                        htmstring4 += "</tr></table></html>";
                        html = htmstring1 + htmstring2 + htmstring3 + htmstring4;

                        logfilewrite("QueryReport2.html", html);
                        Console.WriteLine("[!] Your query => \"" + query + "\"");
                        Console.WriteLine("[!] Query Result2 [" + dt.Length.ToString() + " Records]");
                        Console.WriteLine("[>] Query Result2 for ETW records saved to file: \"" + "QueryReport2.html" + "\"");

                        GC.GetTotalMemory(true);
                    }
                    else if (args[0].ToUpper() == "HTML")
                    {

                        byte[] bb2;
                        string s2;
                        List<string> listx = new List<string>();
                       

                        int counter = 1;
                        string html = "";
                        string query = args[1].ToString().Replace('"', ' ');

                        string htmstring1 = "<html><H2>Search Result for ETW logs " + "<H3> Query (" + query + ")</H3></H2><table border=\"1\"><tr style =\"background-color:#a0a0ff;font:10pt Tahoma;font-weight:bold;\" align = \"left\" > ";
                        string htmstring2 = "<td>id</td><td>localtime</td><td>events</td><td>PID</td><td>PName/ProcessPath</td><td>lhost</td><td>lport</td><td>rhost</td><td>rport</td><td>state</td>";
                        string htmstring3 = " ";
                        string tmp1 = "";
                        string htmstring4 = "";
                        DataRow[] dt = XMLDataTableClass.TCPIPTable2.Select(args[1].ToString());
                        bool clr = false;
                        foreach (DataRow Rowitem in dt)
                        {

                            if (clr) { htmstring4 += "<tr style=\"background-color:#9b9b9b;font:10pt Tahoma;\">"; clr = false; }
                            else if (!clr) { htmstring4 += "<tr style=\"background-color:#00c0f0;font:10pt Tahoma;\">"; clr = true; }
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + counter.ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[0].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[1].ToString().Replace('\n', ' ') + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[2].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[3].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[4].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[5].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[6].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[7].ToString() + "</td>";
                            htmstring4 += "<td align = \"left\"><span style=\"height: 1px; overflow - y:hidden\">" + Rowitem[8].ToString() + "</td>" + "</tr>";


                            counter++;

                        }
                        htmstring4 += "</tr></table></html>";
                        html = htmstring1 + htmstring2 + htmstring3 + htmstring4;

                        logfilewrite("QueryReport.html", html);
                        Console.WriteLine("[!] Your query => \"" + query + "\"");
                        Console.WriteLine("[!] Query Result2 [" + dt.Length.ToString() + " Records]");
                        Console.WriteLine("[>] Query Result2 for ETW records saved to file: \"" + "QueryReport.html" + "\"");

                        GC.GetTotalMemory(true);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    help();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            help();
        }
    }
}

