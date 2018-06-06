from socket import *
import random
import smtplib
from email.mime.text import MIMEText

class AUTHEN(object):
    def __init__(self,client,buffsize):
        self.client = client
        self.buffsize = buffsize
        self.code = random.randint(99,999)
        
        

    @staticmethod
    def send_mail(self,dest_mail,subject,content):
        """
        sends mails.
        """
        
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
    
    def respond(self,data):
        """
        return the right response for each request
        """
        info = data.split(" ")
        print "lur", data
        if info[0] == "-SubmitDetails": # check user's details for VPN access

            # username, password
            print "1"
            details = [info[1],info[2]]
            self.client.send(AUTHEN.submit_management(self,details))       

        elif info[0] == "-CheckValidation": # check for validation to enter DB
            # username, password, email
            print "2"
            details = [info[1],info[2],info[3]]
            self.client.send(AUTHEN.check_validation(self,details))
        elif info[0] == "-ChangePass": # password management tools

            # username, curr_password, new_password
            print "3"
            details = [info[1],info[2],info[3]]
            self.client.send(AUTHEN.pass_management(self,details))
          
        elif info[0] == "-ValidationCode": # entering code management
            print "4"
            # code
            code = info[1]
            self.client.send(AUTHEN.code_management(self,code))
           
            
    @staticmethod
    def submit_management(self,details):
        '''
        give the user accessing permission for the VPN
        '''
       
        db_dict = AUTHEN.get_db(self)

        if details[0] in db_dict: # if user is already in DB

            if db_dict[details[0]][0] == details[1] and db_dict[details[0]][2] == "ACCEPTED": # the user entered his correct password
                
                # AUTHEN.send_mail(self,db_dict[details[0]][1],"Validation Code","Hi %s, your validation code is: %d" % (details[0],self.code))
                print self.code
                return "-UserIsVarified"

            else:
                if db_dict[details[0]][0] != details[1]:
                    return "Wrong password, please retry."
                else:
                    return "Lack of membership blocked your submition."
        else:
            return "The username doesn't exist."

    @staticmethod    
    def check_validation(self,details):
        '''
        if the user is valid, save the request
        '''    
        db_dict = AUTHEN.get_db(self)

        if details[0] in db_dict: # if user is already in DB

            if db_dict[details[0]][2] == "ACCEPTED": # if this user is already a valid client
                return "The details are already belong to a valid client."

            elif db_dict[details[0]][2] == "DENIED": # if this user is already in DB, but lacking an admin authorization
                return "The details are corrently waiting for an admin's approval."

        else: # if user isn't found in the DB, save his request

            db_dict[details[0]] = [details[1],details[2],"DENIED","MEMBER"] 
            AUTHEN.set_db(self,db_dict)        
            return "Account request recieved succesfully, an email will be sent to you when it'll be approved."

    @staticmethod
    def pass_management(self,details):
        """
        suppose to grant the client password management service.
        """
        db_dict = AUTHEN.get_db(self)

        if details[0] in db_dict: # if user is in DB

            if db_dict[details[0]][0] == details[1]: # if the currpass matches, change password
                db_dict[details[0]] = [details[2],db_dict[details[0]][1],db_dict[details[0]][2],db_dict[details[0]][3]] 
                AUTHEN.set_db(self,db_dict)    
                return "Password changed succesfully."

            else: # if the currpass  doesn't match, dont act
                return "The current password you've entered doesn't match to the username, try again."

        else: # if user isn't found in the DB    
            return "The username you've entered doesn't exist."

    @staticmethod
    def code_management(self,code):
        """
        if the code inserted by the client is indeed the one which was sent to him, grant him access
        """
        print self.code
        if code == str(self.code):
            return "-GrantAccess"
        return "The code you've inserted isn't corrent."

    @staticmethod
    def get_db(self):
        """
        Get the DB as dictionary
        """
        DB = open("usersDB.txt","rb+")
        db_dict = DB.read()
        DB.close()
        return eval(db_dict)

    @staticmethod
    def set_db(self,db_dict):
        """
        Write the DB as plain text
        """
        DB = open("usersDB.txt","wb+")
        DB.write(str(db_dict))
        DB.close()
    
    



