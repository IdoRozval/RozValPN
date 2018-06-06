from socket import *
import os
from time import sleep
import subprocess as sp



class PSS(object):
    p = None
    def __init__(self,client,buffsize):
        self.client = client
        self.buffsize = buffsize
       
    def respond(self,data):
        info = data.split(" ")
        target = " ".join(info[1:])
        print data         
        if info[0] == "@u":
            PSS.receive_file(self,target)
            
        elif info[0] == "@s":
            st = ""
            for f in os.listdir("printdb"):   
                st = st + f + "," + str(int(os.path.getsize("printdb\%s" % f))) + ","   
            self.client.send(st[:-1])

        elif info[0] == "@p":
            PSS.p = [sp.Popen(["notepad.exe", "printdb\%s" % target]),target]

        elif info[0] == "@k":
            PSS.p[0].kill()
            #os.remove("printdb/%s" % PSS.p[1])

            
    
            
    @staticmethod
    def receive_file(self,path):
        try:
            parts = path.split("\\")
            f = open("printdb\%s" % parts[-1],"wb")
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

        
    
    
    
    
    
    

            
            
        





