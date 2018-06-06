from socket import *
from select import select
from Handler import *

    
main_server = socket(AF_INET,SOCK_STREAM)
main_server.bind(("192.168.1.114",50002))
main_server.listen(5)

inputs = [main_server]
done = False
buffsize = 1024
handlers = {}


while not done:
    readables, writeables, exceptions = select(inputs, [], [])

    for socket in readables:
        if socket is main_server:
            client, addr = main_server.accept()
            inputs.append(client)
            handlers[client] = Handler(client,buffsize)
        else:
            try:
                data = socket.recv(buffsize)
                handlers[socket].get_respond(data)
                #socket.send(response)
            except:
                inputs.remove(socket)
                del handlers[socket]

main_server.close()
