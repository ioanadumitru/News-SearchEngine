using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DevAcademyQuest
{
    class Program
    {
      //  public static object Image { get; private set; }

        static void Main(string[] args)
        {

            FileManager f = new FileManager();
            f.ProcessFile("articles.txt");

            DataBaseConnectionManager c = DataBaseConnectionManager.getInstance();
            c.SearchArticlesByTitle(" Option Pricing");
            c.SelectArticlesByAuthor(" Letian Ye");
            DateTime d = new DateTime(2016, 10, 19);
            c.SelectArticlesByDate(d, DateTime.Now);


            if (File.Exists("tablouIoana.png"))
            {
                byte[] bytes = File.ReadAllBytes("tablouIoana.png");
                User ioana = new User("ioanaDumitru", "ioanadumitru09@gmail.com", bytes);
                c.AddUser(ioana);

                Query q = new Query(ioana, "derivatives");

                c.AddToQueryHistory(q);
                c.AddToQueryHistory(q);
                c.PeformAutocompletion("der");
            }
        }
    }
}
