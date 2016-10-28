using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DevAcademyQuest
{
    class FileManager
    {

        public void ProcessFile(string fileName)
        {
            DataBaseConnectionManager managerInstance = DataBaseConnectionManager.getInstance();
            char[] separator = {'"'};

            if(File.Exists(fileName))
            {
                StreamReader stream = new StreamReader(fileName);

                while (stream.Peek() >= 0)
                {
                    string data = stream.ReadLine();
                    string[] values = data.Split(separator);

                    string t = values[0];
                    string a = values[1];
                    string text = values[2];

                    Article art = new Article(t, a, text);
                    managerInstance.InsertData(art);
                }
            }
        }


    }
}
