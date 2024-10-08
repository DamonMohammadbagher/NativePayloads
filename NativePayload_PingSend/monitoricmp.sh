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

