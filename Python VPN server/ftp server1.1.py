from socket import *
from select import select
import os
from time import sleep

def create_respond(client,data):
    info = data.split(" ")
    order = info[0]
    target = " ".join(info[1:])
    
    if info[0] == "-d":
        send_file(client,target)
    
    elif info[0] == "-u":
        receive_file(client,target)
    
    elif info[0] == "-s":
        st = ""
        for f in os.listdir("db"):   
            st = st + f + "," + str(int(os.path.getsize("db\%s" % f))) + ","
        print st    
        client.send(st[:-1])
    
    elif info[0] == "-h":
        client.send("Welcome User!\r\n-d [filename]{Download file}\r\n-u [filename]{Upload file}\r\n-s {show all files}\r\n-h {help}")
        
    
def receive_file(client,path):
    try:
        parts = path.split("\\")
        f = open("db\%s" % parts[-1],"wb")
        content = client.recv(buffsize)
        while content:
            f.write(content)
            content = client.recv(buffsize)
        f.close()
    except timeout:
        f.close()       
    except:
        f.close()

def send_file(client,fname):
    f = open("db\%s" % fname,"rb")
    content = f.read(buffsize)
    while content:
        client.send(content)
        content = f.read(buffsize)
    f.close()

    
        
    
    
    
    
    
    


server = socket(AF_INET,SOCK_STREAM)
server.bind(("192.168.1.114",50000))
server.listen(5)

inputs = [server]
done = False
buffsize = 1024


while not done:
    readables, writeables, exceptions = select(inputs, [], [])

    for socket in readables:
        if socket is server:
            client, addr = server.accept()
            client.settimeout(0.5)
            inputs.append(client)
            client.send("Welcome User!\r\n-d [filename]{Download file}\r\n-u [filename]{Upload file}\r\n-s {show all files}\r\n-h {help}")
        else:
            try:
                data = socket.recv(buffsize)
                create_respond(socket,data)
            except:
                inputs.remove(socket)
                
    
    
            
            
        





