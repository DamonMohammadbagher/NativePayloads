# NativePayload_CTX
NativePayload_CTX Create Thread via _beginthreadex function in msvcrt.dll

you can use "_beginthreadex " function in "msvcrt.dll" instead using CreateThread func in "Kernel32.dll" to create Thread and this code working very well and this code not detected by Windows Defender with last updates . 


(sliver payloads are safe more often and cause of bypassing maybe is not _beginthreadex but when you do not use Creathread Api this will help you a lot to bypass some AVs but still here VirtualAlloc and WriteProcessMemory used so this code will detect by some avs IF your payload was not Safe [like meterpreter] ;D)


 ![](https://github.com/DamonMohammadbagher/NativePayload_CTX/blob/main/CTX1.png)


 ![](https://github.com/DamonMohammadbagher/NativePayload_CTX/blob/main/CTX2.png)


Usage: 
    
     NativePayload_CTX.exe 
      
 
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_CTX/"/></a></p>
