using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAcademyQuest
{
    public class Query
    {
        User user;
        string keyWord;
        int frequency;

        public Query(User user, string keyWord)
        {
            this.user = user;
            this.keyWord = keyWord;
            frequency = 1;
        }

        public User GetUser
        {
            get { return user; }
        }

        public string KeyWord
        {
            get { return keyWord; }
        }

        public int Frecvency
        {
            get { return frequency; }
        }
    }
}
