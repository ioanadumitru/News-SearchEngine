using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevAcademyQuest
{
    public class Article
    {
        string title { get; }
        string author { get; }
        DateTime publishDate;
        string text;
        int index;
        static int i = 0;

        public Article(string title, string author, String text)
        {
            this.index = i++;
            this.title = title;
            this.author = author;
            this.publishDate = DateTime.Now; 
            this.text = text;
        }

        public Article(string title, string author, String text, DateTime pd)
        {
            this.index = i++;
            this.title = title;
            this.author = author;
            this.publishDate = pd;
            this.text = text;
        }

        public int Index
        {
            get { return index; }
        }

        public String Text
        {
            get { return text; }
        }

        public string Author
        {
            get { return author; }
        }

        public string Title
        {
            get { return title; }
        }

        public DateTime PublishDate
        {
            get { return publishDate; }
        }

    }

}
