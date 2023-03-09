# NativePayload_ARP2.sh

Simple Script "NativePayload_ARP2.sh" for Sending DATA via ARP Bcast Traffic to all systems in (LAN) by "Vid" tag

Note : this Script Tested on Kali linux (only)

Video (step by step) : https://www.youtube.com/watch?v=C4fB7NQJHO4

Article link1 (PDF) : https://github.com/DamonMohammadbagher/NativePayload_ARP2/tree/master/Article

Article link2 : https://www.peerlyst.com/posts/sending-data-via-arp-broadcast-traffic-to-all-systems-in-lan-by-vid-tag-damon-mohammadbagher

# Method 1: Using NativePayload_ARP2.sh both Sides

	Step1: (System A ) ./NativePayload_ARP2.sh -listen (Packet Number)
  
	Step2: (System B ) ./NativePayload_ARP2.sh -send TextFile.txt [VlanName] [vlan-Subnet/mask] [vlan-Broadcast]  -p [vlan-PingIPv4] [(wlan0,eth0,vboxnet0,etc.)]
	
Method 1, Examples :
  
  	example Step1 (System A1 ) IPv4:192.168.56.101 : ./NativePayload_ARP2.sh -listen 72
  
  	example Step1 (System A2 ) IPv4:192.168.56.102 : ./NativePayload_ARP2.sh -listen 72
  
  	example  Step2 (System B ) IPv4:192.168.56.1 : ./NativePayload_ARP2.sh -send Test.txt vlan3 192.168.222.1/24 192.168.222.255 -p 192.168.222.2 vboxnet0
  
  	Description: with Step1 this script will get packets from (system B) , with Step2 you will send textfile.txt to all systems in (LAN) via ARP Broadcast Traffic by "Vid Tag".
  
  	Note: (System B) is "VM host or Physical Machine" and (System A1/A2) are "Virtual Machine"
  
  
  	Important Point about "switch -listen (Packet Number)" : 
  
  	your "PacketNumber" will be TextFile.txt Length * 2 it means :
  
  	for example this is our mytest.txt file :
  
		#cat mytest.txt | xxd -c 10
		0000000: 5365 6e64 696e 6720 4441  Sending DA
		000000a: 5441 2076 6961 2041 5250  TA via ARP
		0000014: 2042 726f 6164 6361 7374   Broadcast
		000001e: 2026 2056 4944 0a          & VID.
 
 	as you can see we have 36 Bytes so  (36 * 2 = 72) now your PacketNumber is 72
 
 		system A  , Step 1: ./NativePayload_ARP2.sh -listen 72
 
		 system B , Step 2: ./NativePayload_ARP2.sh -send mytest.txt vlan1 192.168.160.1/24 192.168.160.255 -p 192.168.160.2 eth0
	
# Method 2: Using NativePayload_ARP2.sh (system B) , tcpdump -XX -v broadcast | grep 0x0000 (system A)

	Step1: (System A ) tcpdump -XX -v broadcast | grep 0x0000
  
	Step2: (System B ) ./NativePayload_ARP2.sh -send TextFile.txt [VlanName] [vlan-Subnet/mask] [vlan-Broadcast]  -p [vlan-PingIPv4] [(wlan0,eth0,vboxnet0,etc.)]
	
Method 2, Examples :
	
	example Step1 (system A): tcpdump -XX -v broadcast | grep 0x0000
	
	example Step2 (system B): ./NativePayload_ARP2.sh -send mytest.txt vlan1 192.168.160.1/24 192.168.160.255 -p 192.168.160.2 eth0
	
	
	
  # Method 1 Pictures : Using NativePayload_ARP2.sh both Sides
  
  ![](https://github.com/DamonMohammadbagher/NativePayload_ARP2/blob/master/Pictures/Method1Step1.png)
  Picture Method 1 , Step 1:

  ![](https://github.com/DamonMohammadbagher/NativePayload_ARP2/blob/master/Pictures/Method1Step2.png)
    Picture Method 1 , Step 2:

  # Method 2 Pictures : Using NativePayload_ARP2.sh (system B) , Tcpdump -XX -v broadcast | grep 0x0000 (system A)

  ![](https://github.com/DamonMohammadbagher/NativePayload_ARP2/blob/master/Pictures/Method2Step1.png)
    Picture Method 2 , Step 1:

  ![](https://github.com/DamonMohammadbagher/NativePayload_ARP2/blob/master/Pictures/Method2Step2.png)
  Picture Method 2 , Step 2:
  
   ![](https://github.com/DamonMohammadbagher/NativePayload_ARP2/blob/master/Pictures/Method2Step3.png)
   Picture Method 2 , Step 3:
    
<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_ARP2/"/></a></p>
