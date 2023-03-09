# ETWMonThread

ETWMonThread.cs , Simple C# Source Code with ETW Sample for Detecting Injected Meterpreter Payloads via Thread Injection Method & meterpreter signature was for msf4 & tested on Metasploit ver4 [kali 2018 with metasploit v4.17], "msf6" meterpreter/payloads signature is changed so this code will not work on msf6 ;)  

Note: for Installing "nupkg" Package File Please Visit This Page : https://www.nuget.org/packages/O365.Security.Native.ETW/

----------------------------
Note: RunAs Admin "Required"
----------------------------

Note: in this New Article i talked about how an attacker can Bypass this Code Sometimes with Code Chunking Technique.

    New Article: 
    Detecting Thread Injection by ETW & One Simple Technique [ https://damonmohammadbagher.github.io/Posts/7jun2020x.html ]
    

---------------------------------------
Video (Step by Step) :  https://www.youtube.com/watch?v=nIoDrqeQ2es
---------------------------------------


Help: ETWMonThread , Realtime Scanning/Monitoring Thread Injection for MPD (Meterpreter Payload Detection) by ETW

[!] Syntax 1: Realtime Scanning/Monitoring IPS Mode (Killing Meterpreter Injected Threads)
----------------------------------------------

[!] Syntax 1: ETWMonThread.exe "IPS" [optional] "DEBUG"

[!] Example1: ETWMonThread.exe IPS 

[!] Example2: ETWMonThread.exe IPS DEBUG

[!] Syntax 2: Realtime Monitoring IDS Mode
----------------------------------------------


[!] Syntax 2: ETWMonThread.exe [optional] "SHOWALL" [optional] "DEBUG" 

[!] Example1: ETWMonThread.exe

[!] Example2: ETWMonThread.exe SHOWALL

[!] Example3: ETWMonThread.exe SHOWALL DEBUG

![](https://github.com/DamonMohammadbagher/Meterpreter_Payload_Detection/blob/master/MPD/ETWMonThread/ETWMonThread.png)
