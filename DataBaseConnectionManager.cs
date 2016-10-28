using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace DevAcademyQuest
{
    public class DataBaseConnectionManager
    {
        static DataBaseConnectionManager connectionManager = null;
        MySqlConnection connection;
        
        private DataBaseConnectionManager()
        {
            Init();
            OpenConnection();
            CreateTables();
        }

        public static DataBaseConnectionManager getInstance()
        {
            if (connectionManager == null)
                connectionManager = new DataBaseConnectionManager();

            return connectionManager;
        }

        private void Init()
        {
            string dataBase = "newsengine";
            string user = "root";
            string password = "root";
            string server = "localhost";
            string connect = "SERVER=" + server + ";" + "DATABASE=" +
                              dataBase + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connect);
        }


        private void CreateTables()
        {
            try
            {
                string q1 = "create table news(Id int primary key, title varchar(400),author varchar(100), publishedData datetime, content text)";
                string q2 = "create table users(userId int primary key, userName varchar(40), emailAddres varchar(60), picture MEDIUMBLOB )";
                string q3 = "create table userQueries(userID int, keyword varchar(40) unique,  frequency int)";
                MySqlCommand cmd = new MySqlCommand(q1, connection);
                cmd.ExecuteNonQuery();
                cmd.CommandText = q2;
                cmd.ExecuteNonQuery();
                cmd.CommandText = q3;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }
        }


        private void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }

        public List<Article> SelectArticlesByDate(DateTime from, DateTime to)
        {
            try
            {
                string query = "SELECT * from news where publishedData between @from and @to";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                MySqlDataReader data = cmd.ExecuteReader();

                List<Article> results = new List<Article>();

                while(data.Read())
                {
                    Article a = new Article(data.GetString("title"), data.GetString("author"), data.GetString("content"), data.GetDateTime("publishedData"));
                    results.Add(a);
                }
                data.Close();
                return results;
            }
            catch(MySqlException ex)
            {
                ex.ToString();
            }
            return null;
        }

        public List<Article> SelectArticlesByAuthor(string name)
        {
            try
            {
                string query = "select * from news where author like @name";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@name", "%"+name+"%" );
                
                MySqlDataReader data = cmd.ExecuteReader();
                List<Article> results = new List<Article>();

                while (data.Read())
                {
                    Article a =
                        new Article(data.GetString("title"), data.GetString("author"), data.GetString("content"), data.GetDateTime("publishedData"));
                    results.Add(a);
                }
                data.Close();
                return results;
            }
            catch (MySqlException ex)
            {
                ex.ToString();

            }
            return null;
        }

        //Find articles that have a certain word in their title
        public List<Article> SearchArticlesByTitle(string word)
        {
            try
            {
                string query = "select * from news where title like @word";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@word","%"+ word+"%");

                MySqlDataReader data = cmd.ExecuteReader();
                List<Article> results = new List<Article>();
                while (data.Read())
                {
                    Article a = new Article(data.GetString("title"), data.GetString("author"), data.GetString("content"), data.GetDateTime("publishedData"));
                    results.Add(a);
                }
                data.Close();
                return results;
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }
            return null;
        }

        public void InsertData(Article newArticle)
        {
            try
            {
                string query = "insert into news values(@Id, @title, @author,@t, @text)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", newArticle.Index);
                cmd.Parameters.AddWithValue("@title", newArticle.Title);
                cmd.Parameters.AddWithValue("@author", newArticle.Author);
                cmd.Parameters.AddWithValue("@t", newArticle.PublishDate);
                cmd.Parameters.AddWithValue("@text", newArticle.Text);

                cmd.ExecuteNonQuery();
                
            }
            catch(MySqlException ex)
            {
                ex.ToString();
            }
        }

        public void AddUser(User user)
        {
            try
            {
                string query = "insert into users values(@Id, @username, @email, @picture)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", user.UserId);
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@email", user.EmailAddress);
                cmd.Parameters.AddWithValue("@picture", user.Picture);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }
        }

        public void AddToQueryHistory(Query q)
        {
            try
            {
                string query = "select * from userQueries where keyword = @word";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@word", q.KeyWord);

                MySqlDataReader data = cmd.ExecuteReader();

                if (data.HasRows)
                {
                    data.Close();
                    UpdateQueryFrecvency(q.KeyWord);
                }
                else
                {
                    data.Close();
                    SaveQueries(q);
                }

            }
            catch(MySqlException ex)
            {
                ex.ToString();
            }

        }

        private void UpdateQueryFrecvency(string key)
        {
            try
            {
                string query = "update userQueries set frequency = frequency+1 where keyword = @word";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@word", key);
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                ex.ToString();
            }
        }

        private void SaveQueries(Query q)
        {
            try
            {
                string query = "insert into userQueries values(@Id, @key, @f)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Id", q.GetUser.UserId);
                cmd.Parameters.AddWithValue("@key", q.KeyWord);
                cmd.Parameters.AddWithValue("@f", q.Frecvency);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }
        }

        //Implement autocompletion based on most frequent word searches
        public void PeformAutocompletion(string key)
        {
            try
            {
                string query = "select * from userQueries where keyword like @word order by frequency";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@word", "%" + key + "%");

                MySqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {
                    Console.WriteLine(data.GetString("keyword"));
                }
                Console.Read();
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }

        }

        ~DataBaseConnectionManager()
        {
            connection.Close();
        }
    }
}
