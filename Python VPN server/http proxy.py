

from socket import *
import thread
from select import select
from Tkinter import *


def get_porpuse(http):
    temp = http.split(' ')
    return temp[0]


def get_details(http):
    url = http.split(" ")[1]
    url = url[7:]
    index = url.find("/")
    if index != -1:
        url = url[:index]
    return url, 80
        
    

def requester(browser,browser_addr):
    http = browser.recv(1024)
    print http
    porpuse = get_porpuse(http)        
    if porpuse == "GET":
        url, port = get_details(http)      
        site = socket(AF_INET,SOCK_STREAM)
        site.connect((url,port))       
        site.send(http)
        doo_siah(browser,site)
        
        
     


def doo_siah(browser,site):
    inputs = [browser,site]
    flag = True
    while flag:
        readables,writeables,errors = select(inputs,[],[])
        for socket in readables:
            data = socket.recv(1024)
            if data:
                if socket is browser:
                    target = site
                else:
                    target = browser
                target.send(data)
            else:
                flag = False
                site.close()
                browser.close()
                break
            
    



proxy = socket(AF_INET,SOCK_STREAM)
proxy.bind(('192.168.1.114',50000))
proxy.listen(5)



while 1:
    browser, browser_addr = proxy.accept()
    thread.start_new_thread(requester,(browser,browser_addr))   

proxy.close()




