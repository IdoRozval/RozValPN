from socket import *
from select import select
import os
from time import sleep
from FTP import *

        

server = socket(AF_INET,SOCK_STREAM)
server.bind(("192.168.1.114",50000))
server.listen(5)

inputs = [server]
done = False
buffsize = 1024
handlers = {}

while not done:
    readables, writeables, exceptions = select(inputs, [], [])

    for socket in readables:
        if socket is server:
            client, addr = server.accept()
            client.settimeout(0.5)
            inputs.append(client)
            handlers[client] = FTP(client,buffsize)
            client.send("Welcome User!\r\n-d [filename]{Download file}\r\n-u [filename]{Upload file}\r\n-s {show all files}\r\n-h {help}")
        else:
            try:
                data = socket.recv(buffsize)
                handlers[socket].create_respond(data)
            except:
                inputs.remove(socket)
                del handlers[socket]
                
   
  
            
            
        





