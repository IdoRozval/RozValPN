from socket import *
from select import select
import random
import smtplib
from email.mime.text import MIMEText

def get_response(data):
    """
    return the right response for each request
    """
    print data
    info = data.split(" ")
    
    if info[0] == "-SubmitDetails": # check Admin's details for VPN access
        # username, password
        details = [info[1],info[2]]
        return submit_management(details)

    elif info[0] == "-BasicInfo": # get the users information
        return get_basic_info()

    elif info[0] == "-ChangeAccess": # grant/deprive the user access to the server
        set_user_access(info[1])
        return "-NONE"

    elif info[0] == "-ChangeLevel": # grant/deprive the user level to the server
        set_user_level(info[1])
        return "-NONE"

    elif info[0] == "-DeleteUser": # delete the specified user from the DB
        del_user(info[1])
        return "-NONE"

    elif info[0] == "-ComposeMail": # send the specified email to the specified user
        # username, subject, content
        details = [info[1],info[2],info[3]]
        compose_management(details)
        return "-NONE"
        
        
        
        
        
def submit_management(details):
    """
    give the user accessing permission for the VPN
    """    
    db_dict = get_db()

    if details[0] in db_dict: # if user is already in DB

        if db_dict[details[0]][0] == details[1] and db_dict[details[0]][3] == "ADMIN" and db_dict[details[0]][2] == "ACCEPTED": # the user entered his correct password and he's admin
            return "-AdminIsVarified"

        else:
            return "The user isn't an varified admin, or the password is incorrect."
    else:
        return "The username doesn't exist."    


def set_user_access(user):
    """
    alternate the user access to the server
    """
    db_dict = get_db()

    if db_dict[user][2] == "ACCEPTED":
        print "1"
        db_dict[user][2] = "DENIED"
    else:
        print "2"
        db_dict[user][2] = "ACCEPTED"
        send_mail(db_dict[user][1],"Access Permission","hi %s,\r\nAn admin accepted your registration and granted you with access permission to the server." % user) 

    set_db(db_dict)
      

def set_user_level(user):
    """
    alternate the user rank
    """
    db_dict = get_db()

    if db_dict[user][3] == "ADMIN":
        db_dict[user][3] = "MEMBER"
    else:
        db_dict[user][3] = "ADMIN"

    set_db(db_dict)

def del_user(user):
    """
    remove the specified user from the DB
    """
    db_dict = get_db()

    del db_dict[user]

    set_db(db_dict)


def compose_management(details):
    """
    Set the right details in order to send a valid email
    """
    db_dict = get_db()
    send_mail(db_dict[details[0]][1],details[1],details[2])

    
def get_db():
    """
    Get the DB as dictionary
    """
    DB = open("usersDB.txt","rb+")
    db_dict = DB.read()
    DB.close()
    return eval(db_dict)

def set_db(db_dict):
    """
    Write the DB as plain text
    """
    DB = open("usersDB.txt","wb+")
    DB.write(str(db_dict))
    DB.close()

def get_basic_info():
    """
    Get the total needed information about all users
    """
    db_dict = get_db()
    st = ""
    for user in db_dict:
        st = st + user + "," + db_dict[user][2] + "," + db_dict[user][3] + ","
    st = st[:len(st)-1]
    return st

def send_mail(dest_mail,subject,content):
    src_mail = "RozValPN@gmail.com"
    src_pass = "rozvalVPN"

    msg = MIMEText(content) # create a plain email

    msg['Subject'] = subject
    msg['From'] = src_mail
    msg['To'] = dest_mail

    mail = smtplib.SMTP("smtp.gmail.com",587)
    mail.ehlo()
    mail.starttls()
    mail.login(src_mail,src_pass)
        
    mail.sendmail(src_mail, dest_mail, msg.as_string())
    mail.close()
    

    
    
server = socket(AF_INET,SOCK_STREAM)
server.bind(("192.168.1.114",50001))
server.listen(5)

inputs = [server]
done = False
buffsize = 1024


while not done:
    readables, writeables, exceptions = select(inputs, [], [])

    for socket in readables:
        if socket is server:
            client, addr = server.accept()
            inputs.append(client)
        else:
            try:
                data = socket.recv(buffsize)
                response = get_response(data)
                if response != "-NONE": # if the server suppose to send the client data
                    socket.send(response)
            except:
                inputs.remove(socket)


