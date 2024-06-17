
# NativePayload_CDynApp3
### NativePayload_CDynApp3 , New Techniqe to load .NET C2 Client-side Exe/dll/bin files via Dynamic AppDomains

#### Loading Csharp C2 Client-side codes in RAM by Very Simple New Technique to avoid Detection

in this article i want to talk about very useful Method which you should use in Modern C2 Servers (in client side) to avoid Client Side Code Detection in RAM.

before start you need to see steps for this method in C#, but the background of Technique is something like these steps:
step1: Client Side code will run in memory by loader csharp code for 30secs or 60secs (depends on code)

step1.1: in this step your bytes of bin/dll/exe file will download from server by HTTP/S traffic (encrypted/encoded by xor or rc4 or ...) , it is very simple method in .NET and Csharp programming you can do this only with 10 lines of code  to execute your virtual code in target process (which is local process in this case), so in this step you can run "TargetMethod" in your Exe file which created by csharp codes Then you can call that TargetMethod in Exe file Independently without executing all codes of exe , so you will run that section of source code of Exe/bin/dll file Which could be Main() Method or some other methods in your code which we called them "TargetMethod"

step1.2: in this step you should do all things in step1.1 via new Virtual AppDomain in local .NET Process , so in this time you should create new AppDomain in your loader to run in memory for 30sec,  

step2: THEN after 30sec you need to unload that new AppDomain by csharp Method AppDomain.Unload() for 20sec, that means your code which is your C2 Server (Client Side) file (exe/dll/bin/...) will load/execute in Memory for 30secs and then will remove from Memory of Process for 20sec ... , Unload() Method will Remove your Byte of Codes which Executed in memory by New AppDomain via GC Global Catalog Automatically and immediately so in this time your code will Remove from RAM by .NET GC , it is something like dispose Codes from Process etc.

Note: so in step2 your c2 server connection with client side code  will remove because your client side code totally removed from process memory and in this time anti-viruses or other tools for dumping process memory bytes can not detect your client side codes which loaded in to memory in step1.1 and step1.2.
   
all these steps will be in loop to repeat these steps again ....




### for Step by step about background of code please read article:

Article: https://www.linkedin.com/pulse/loading-csharp-c2-client-side-codes-ram-very-simple-mohammadbagher-estif


Usage: 
    
     NativePayload_CDynApp3.exe 
      

### NativePayload_CDynApp3 steps [Win10]
   ![](https://github.com/DamonMohammadbagher/NativePayloads/blob/main/NativePayload_CDynApp3/Pic/server1.png)
   
 
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_CDynApp3/"/></a></p>
