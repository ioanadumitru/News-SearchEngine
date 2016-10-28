using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAcademyQuest
{
    public class User
    {
        int userId;
        string userName;
        string emailAddress;
        byte[] picture;
        static int index = 0;

        public User(string un, string mail)
        {
            userId = index++;
            userName = un;
            emailAddress = mail;
        }

        public User(string un, string mail, byte[] picture)
        {
            userId = index++;
            userName = un;
            emailAddress = mail;
            this.picture = picture;
        }

        public byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public int UserId
        {
            get { return userId; }
        }

        public string UserName
        {
            get { return userName; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
        }


    }
}
