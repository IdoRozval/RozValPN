from AUTHEN import *
from FTP import *
from PSS import *
class Handler(object):
    def __init__(self,client,buffsize):   
        self.client = client
        self.buffsize = buffsize
        self.authen_handler = AUTHEN(client,buffsize)
        self.ftp_handler = FTP(client,buffsize)
        self.pss_handler = PSS(client,buffsize)
      
           

    def get_respond(self,data):
        print data
        parts = data.split(" ")
        key = parts[0][0]
        print key
        if key == "-":
            print "ani1"
            self.authen_handler.respond(data)
        elif key == "#":
            print "ani2"
            self.ftp_handler.respond(data)
        elif key == "@":
            print "ani3"
            self.pss_handler.respond(data)
         
    
        
    
    
    
    
    
    

            
            
        





