from socket import *
import os
from time import sleep


class FTP(object):
    def __init__(self,client,buffsize):
        self.client = client
        self.buffsize = buffsize
       
    def respond(self,data):
        info = data.split(" ")
        order = info[0]
        target = " ".join(info[1:])
        print data
    
        if info[0] == "#d":
            print "1"
            FTP.send_file(self,target)
            print "1"
            
        elif info[0] == "#u":
            print "2"
            FTP.receive_file(self,target)
            print "2"
            
        elif info[0] == "#s":
            print "3"
            st = ""
            for f in os.listdir("db"):   
                st = st + f + "," + str(int(os.path.getsize("db\%s" % f))) + ","   
            self.client.send(st[:-1])
            print "3"
    
        elif info[0] == "#h":
            print "4"
            self.client.send("Welcome User!\r\n-d [filename]{Download file}\r\n-u [filename]{Upload file}\r\n-s {show all files}\r\n-h {help}")
            print "4"
            
    @staticmethod
    def receive_file(self,path):
        print "sholet2"
        try:
            parts = path.split("\\")
            f = open("db\%s" % parts[-1],"wb")
            self.client.settimeout(0.5)
            content = self.client.recv(self.buffsize)
            while content:
                f.write(content)
                content = self.client.recv(self.buffsize)
            f.close()
            self.client.settimeout(0)
        except timeout:
            f.close()
            self.client.settimeout(0)
        except:
            f.close()
            self.client.settimeout(0)

    @staticmethod
    def send_file(self,fname):
        f = open("db\%s" % fname,"rb")
        content = f.read(self.buffsize)
        while content:
            self.client.send(content)
            content = f.read(self.buffsize)
        f.close()
    
        
    
    
    
    
    
    

            
            
        





