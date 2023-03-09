# ETWNetMonv3 & ETWProcessMon 
## "ETWNetMonv3/ETWNetMonv3Agent" is simple C# code for Monitoring TCP Network Connection via ETW & "ETWProcessMon" is for Monitoring Process/Thread/Memory/Imageload etc. 
-----------------
### ETWNetMonv3.exe
"ETWNetMonv3.exe" is Windows App for read/watching ETW Logs (which made by ETWNetMonv3Agent.exe), this code will publish with pdf files for chapter15 of my ebook soon. 

Note: i will talk about these codes in ch15 of ebook Bypassing AVs by C# Programming

### ETWNetMonv3Agent.exe
"ETWNetMonv3Agent.exe" is ETW Agent C# code and with this simple code you can have realtime TCPv4/v6 Connection Logs also with this tool you can Detect Meterpreter Payload via Signature (with switch SCAN , Detection for Remote Thread Injection) But this Signature worked very well only on Msfv4 (tested on Kali 2018, msf v4.17), Metasploit v6 (msf6) signature is changed so this old signature will not work on Msf6 ;)   

### ETWNetMon3Log.exe
"ETWNetMon3Log.exe" is Command base tool for make XML/HTML report via "Queries" from ETW Logs (which made by ETWNetMonv3Agent.exe tool).

Note: this code was for (eBook Bypassing AVs by C# Programming, Chapter-15), so i will publish pdf file for Part1 of ch15, and C# code for "ETWNetMonv3.exe" will publish with pdf (soon), but  "ETWNetMonv3Agent.cs" & "ETWNetMon3Log.cs" Codes are ready and you can use them and both PE files should be in the "same folder" because of log file "EtwNetMonv3logs.txt". (both codes need to work with this log file, this log file made by Agent & with ETWNetMon3Log.exe you can make HTML report from this file by simple Queries... , "for make report with XML/HTML format this ETWNetMon3Log.cs is useful tool for [blue teams & defenders], [i hope]".

Note: i will publish new codes + pdf file about this code which is for chapter-15 (ebook: Bypassing Avs by C# Programming) & Some bugs in the codes will fix (soon). (some bugs are here in code ETWNetMonv3Agent.cs , especially for Scanning Memory, which i need to change code from single thread to Multithreading Code, i knew that but now i am working on other codes & pdf file for chapter ch15-part1, but i will fix these problems too). 

### ETWProcessMon.exe
"ETWProcessMon" is simple tool for Monitoring Processes/Threads/Memory/Imageloads/TCPIP Events via ETW, with this code you can Monitor New Processes also you can See New Threads (Thread Started event) + Technique Detection for Remote-Thread-Injection (Which Means Your New Thread Created into Target Process by Another Process), also with this code you can Monitor VirtualMemAllocation Events in Memory for All Processes (which sometimes is very useful for Payload Detection in-memory) also you can see ImageLoads for each Process & you can see TCPIP Send Events for each Process too. 

Note: VirtualMemAlloc for (Payload-Detection) + ImageLoad & Remote-Thread-Injection Detection for (Technique-Detection) are useful for Blue Teams/Defenders.

Video [1], [Video-1 of Chapter15-Part1]: 

    ETW + C# and Monitoring Network Connections via ETW (CH15-Part1): 
    link1 => https://www.youtube.com/watch?v=zDG4Tze9mts
    link2 => https://share.vidyard.com/watch/5ybRwUbt2b3d3M3ggQiuYQ

Video [2], [Video-2 of Chapter15-Part1&2]:

    ETW + C# & Monitoring Process/Memory/Threads + Network Connection via ETW (CH15-Part2):
    link1 => https://www.youtube.com/watch?v=1Aeor_NqpUA
    link2 => https://share.vidyard.com/watch/6bYvcF75FqQ3BomZELpQUj

Video [3], [Video-3 of Chapter15-Part2]: (video is about C# + ETW vs Process Hollowing, DInvoke (syscall),Loading dll/functions from Memory,Classic-RemoteThreadInjection)  

    C# + ETW vs Some Thread/Process/Code Injection Techniques (CH15-Part2):
    link1 => https://www.youtube.com/watch?v=d1a8WqOvE84
    link2 => https://share.vidyard.com/watch/4kB2Xy1bLfhRxaTD6pwaLD

### ETWProcessMon2.exe
Note: in "ETWProcessMon2.cs" (Version 2) NewProcess events + Remote-Thread-Injection Detecetion events + TCPIP send events all will save in Windows Event Log which with EventViewer you can watch them also VirtualMemAlloc events + Remote-thread-injection Detection Events will save in text "ETWProcessMonlog.txt" log file too (at the same time). so in this version2 we have two type of Events log files => 1."windows event logs [ETWPM2]" , 2."ETWProcessMonlog.txt"

ETW Events in event log [ETWPM2]:

    [Information] Event ID 1  => NewProcess event 
    [Warning]     Event ID 2  => Remote-Thread-Injection Detection event 
    [Information] Event ID 3  => TCPIP Send event

for more information about "ETWProcessMon2.exe" & "ETWPM2Monitor.exe" (step by step with images & details) => https://github.com/DamonMohammadbagher/ETWProcessMon2 

 -----------------------------------------------------------    
 
 1. ETWNetMonv3.exe Windows App (i will publish Code for this tool + pdf Chapter15 soon) 
 
 Note: in win10 you should run this tool with Admin ("Run as Admin")
 
   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/ETWNetMonv3.png)

 -----------------------------------------------------------    
  
  2. ETWNetMonv3Agent.cs (ETW Agent) 
  
  Note: switch SCAN has bugs [not recommanded]  ¯\_(ツ)_/¯
 
 usage: 
    
    step1: [win] ETWNetMonv3Agent.exe [SCAN]/[DBG]
    example: [win] ETWNetMonv3Agent.exe
    example: [win (Run As Admin)] ETWNetMonv3Agent.exe SCAN
    Note: you need Run As Admin only for switch SCAN , ETWNetMonv3Agent.exe code with/without [SCAN/DBG] will make log file "EtwNetMonv3logs.txt"

   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/ETWNetMonv3_02.png)

 -----------------------------------------------------------      
  
  3. ETWNetMon3Log.cs (Command base tool for make XML/HTML report via "Queries" from ETW Log file EtwNetMonv3logs.txt)
  
  Note: this file with ETWNetMonv3Agent.exe should be in the same folder (both work with log file EtwNetMonv3logs.txt)
 
 usage: 
    
    step1: [win] ETWNetMonv3Log.exe  [XML xmlfile.xml] [HTML "YOUR QUERY"]
    example: ETWNetMonv3Log.exe HTML "state LIKE '*synsent*'"
    syntax  1: switch XML, All ETW records from text [EtwNetMonv3logs.txt] will save to xml file (convert all records to xml)
    example 1: ETWNetMonv3Log.exe XML filename.xml
    syntax  2: ETWNetMonv3Log.exe HTML "your query"
    example 2: ETWNetMonv3Log.exe html "rport >=80 OR events LIKE '*Established*' AND events LIKE '*connect complete*'"
    example 2: ETWNetMonv3Log.exe html "events LIKE '*requested to connect*'"
    example 2: ETWNetMonv3Log.exe html "state NOT LIKE '*synsent*'"
    example 2: ETWNetMonv3Log.exe html "events LIKE '*'"
    example 2: ETWNetMonv3Log.exe html "rhost LIKE '192.168*' or lhost LIKE '192.168.1*'"
    example 2: ETWNetMonv3Log.exe html "rport <=80 OR lport >=50000"
    example 2: ETWNetMonv3Log.exe html "pname LIKE '*.exe*'"
    example 2: ETWNetMonv3Log.exe html "events LIKE '*C:\\*'"
    example 2: ETWNetMonv3Log.exe html "PID = 1452"
    syntax 2-1: ETWNetMonv3Log.exe html2 [MODE 0=SYNSENT , 1=ESTABLISHED] [Query]
    example 2-1: ETWNetMonv3Log.exe html2  1  "PID = 1452");
    example 2-1: ETWNetMonv3Log.exe html2  0  "PID = 1452");
    example 2-1: ETWNetMonv3Log.exe html2  1  "rport >= 80");
    example 2-1: ETWNetMonv3Log.exe html2  1  "events LIKE '*192.168*'"
        
Note: for "syntax 2-1" or switch "html2" these two files => 1.EtwNetMonv3logs.txt & 2.ETWProcessMonlog.txt should be in current folder, this switch "html2" is for integration between log files  [EtwNetMonv3logs.txt & ETWProcessMonlog.txt], switch "html" was only for EtwNetMonv3logs TCPIP Network reports.

### Note: for "syntax 2-1" report you need these steps:
 1. run => ETWNetMonv3Agent.exe without switch (run as admin is better) ;D (step1)
 2. run => ETWProcessMon.exe without switch (run as admin) (step2)
 3. run => your own payload injector (meterpreter payload injector for example)
 4. wait for etw events, sometimes more than 1-2 min needs ;) (depends on systems/vm)
 5. after 2-3 mins, copy log file "ETWProcessMonlog.txt for ETWProcessMon.exe to folder which ETWNetMonv3Agent.exe & EtwNetMonv3logs.log exist.
 6. run ETWNetMonv3Log.exe with switch "html2" + [query] (ETWNetMonv3Log.exe should be in folder step5)
 7. you will have html report (i hope) ;) , watch videos before run codes, video => https://share.vidyard.com/watch/4kB2Xy1bLfhRxaTD6pwaLD  
    
   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/4.png)

ETWNetMon3Log.exe + integration with ETWProcessMon.exe log file (EtwNetMonv3logs.txt + ETWProcessMonlog.txt)

Note: in this case you should use switch (html2) like picture in this time you will have some details from ETWProcessMon log file in your report which is Thread-injection & VirtualMemAlloc for each Process in your TCPIP/Network Connections report (integration between tcpip events & Process/Mem/Threads Events). as you can see sometimes very simple you can find Meterpreter payload via this information/events. (useful for Blue teams & Defenders)

   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/4-1.png)

 -----------------------------------------------------------    
  4. ETWProcessMon.cs (ETWProcessMon v1.2 tool is for RemoteThreadInjection Detection)
  
  Note: "this code tested for Detection against some new/old methods like C# code for Process Hollowing, DInvoke (syscall), Loading dll/functions from Memory [32BIT], Classic-RemoteThreadInjection, APC Queue Code Injection, Process-Ghosting, Process Hollowing & Process Doppelganging by [Minjector], ..."

in these videos you can watch Result of tests:
  
Demo Video1: https://www.linkedin.com/posts/damonmohammadbagher_this-video-is-my-research-result-about-using-activity-6809542015475355648-fsfX/ 

Demo Video2: https://share.vidyard.com/watch/4kB2Xy1bLfhRxaTD6pwaLD 
 
 usage: 
    
    step1: [win] ETWProcessMon.exe
    example 1: ETWProcessMon.exe
    example 2: ETWProcessMon.exe > Save_all_outputs.txt
    Note: in "example 2" you can have all outputs in text file [Imageload/TCPIP/NewProcess/NewThreads events + Injection Detection + Details etc] also at the same time VirMemAlloc events + Injections Detection events saved into log file ETWProcessMonlog.txt too.
    
   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/1.png)
--- 
### ETWProcessMon.exe & Imageload + TCPIP Events

   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/2.png) 

---
### ETWProcessMon.exe & Remote Thread Injection Detection by Log File (ETW Events)

   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/etwinj.png)

---
### ETWProcessMon.exe & Payload Detection by Log File (ETW Events)

   ![](https://github.com/DamonMohammadbagher/ETWNetMonv3/blob/main/Pic/detection.png)
 -----------------------------------------------------------    

    
    
    
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FDamonMohammadbgher%2FETWNetMonv3"/></a></p>
