
# NativePayload_PE1/PE2
### NativePayload_PE1 , Injecting Meterpreter Payload bytes into local Process via Delegation Technique + in-memory with delay Changing RWX to X or RX or (both), simple Technique to bypass some Anti-viruses.

Note: tested on WIN11 + WinDefender [update 2023/1/25] (bypassed)

Note: tested on WIN10 + WinDefender [update 2023/1/10] (bypassed)

Note: tested on WIN10 + Kaspersky cloud security v21.3 [update 2023/1/22] (bypassed)

### Some Real Sources: some engineers in anti-virus companies say "COME-ON", like Kaspersky ;)

#### Note: "as Security Researcher this was not my first time to bypass all Anti-viruses (or almost all of them ;D) but this one really was fun more than other methods which i have done in the past."

Simple Technique to Load Assembly/Bytes into local process (in-memory) via C# Delegation + Native APIs and Bypassing Anti-viruses ;), some part of code changed via [D]elegate Techniques which i called [Technique ;D] to change some behavior of code (also change source code) and ... 

note: as pentester you really need to change your own codes sometimes very fast , these codes changed and again worked very well and as security researcher this is really fun to find out new method/codes to bypass AVs always ;D

Method is not really new but C# code a little bit is ;D [since 2022 i used this], changing RWX to X and after 2 min to RX ;D 

#### Note: so in my opinion playing with R W X to X or sometimes to RX or (both) will help you to avoid get red-flag via AVs, so changing default + delays will help you to confuse AVs sometimes.

### NativePayload_PE2 , Injecting Meterpreter Payload bytes into local Process via Delegation Technique + in-memory with delay Changing RWX to X only, simple Technique to bypass some Anti-viruses.

Note: .NET 4.0 or 4.5 Tested

Article: https://www.linkedin.com/pulse/2-simple-c-techniques-bypassing-anti-virus-damon-mohammadbagher/

Article: https://damonmohammadbagher.github.io/Posts/22Jan2023x.html

Video1 [NativePayload_PE2.cs and NativePaylod_AsynASM.cs] => https://www.youtube.com/watch?v=T57pWzS59Y8 

Video2 [NativePayload_PE3.cs] => https://www.youtube.com/watch?v=sqyKqiU1lsE

Video3 [New] [NativePaylod_AsynASM.cs] => https://www.linkedin.com/posts/damonmohammadbagher_bypassing-redteaming-pentesting-activity-7031685536918458369-U9XY


Usage: 
    
     NativePayload_PE1.exe "meterpreter/cobaltstrike payload"
     example: NativePayload_PE1.exe "fc,48,e8,00,....."
     
Usage: 
    
     NativePayload_PE2.exe "meterpreter/cobaltstrike payload"
     example: NativePayload_PE2.exe "fc,48,e8,00,....."     


### NativePayload_PE1 steps [Win11]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/W11_1.png)
   
### NativePayload_PE2 steps [Win11]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/W11_2.png)   
---------------------------
### NativePayload_PE1 steps [Win10]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/_x1.png)
   
### NativePayload_PE1 steps [Win10]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/_x2.png)
   
### NativePayload_PE1 steps [Win10]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/_x3.png)
   
-------------------------   

### NativePayload_PE2 steps [Win10]
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/pe2.png)   

### NativePayload_PE2 vs ETW tools
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/pe2_blueteaming_tool.png)   
--------------------------

### NativePayload_PE1 vs Kaspersky v21.3 (bypassed)
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/kasperskyPE1-1.png)   
   
### NativePayload_PE1 vs Kaspersky v21.3 (bypassed)
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/kasperskyPE1-2.png)   
   
   ### NativePayload_PE2 vs Kaspersky v21.3 (bypassed)
   ![](https://github.com/DamonMohammadbagher/NativePayload_PE1/blob/main/pic/kasperskyPE2.png)   
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_PE1/"/></a></p>
