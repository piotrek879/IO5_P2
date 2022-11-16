using Botex.Model;
using Botex.scripts;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Data.Entity;

namespace Botex.database
{
    sealed class DbControl
    {
        //Tutaj są wszystkie operacje na bazach danych
        #region ConnectionHandler
        private SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source= Botex.db; Version = 3; New = True; Compress = True; ");
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        private void CloseConn(SQLiteConnection conn)
        {
            conn.Close();
        }
        #endregion

        #region DBOperations
        public void insertDataToDB(string sqlQueryCommand)
        {
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            

            sqlite_cmd.CommandText = sqlQueryCommand;
            sqlite_cmd.ExecuteNonQuery();


            CloseConn(sqlite_conn);
        }

        public MailModel GetMailModelFromDb(string sqlQueryCommand)
        {
            MailModel model = new MailModel();

            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            model.IdMail = (int)sqlite_cmd.ExecuteReader()[0];
            model.Content = (string)sqlite_cmd.ExecuteReader()[1];
            model.Title = (string)sqlite_cmd.ExecuteReader()[2];
            model.Content = (string)sqlite_cmd.ExecuteReader()[3];

            CloseConn(sqlite_conn);
            return model;
        }

        public TweetModel GetTweetModelFromDb(string sqlQueryCommand)
        {
            TweetModel model = new TweetModel();

            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            model.IdTweet = (int)sqlite_cmd.ExecuteReader()[0];
            model.Content = (string)sqlite_cmd.ExecuteReader()[1];
            model.Group = (string)sqlite_cmd.ExecuteReader()[2];

            CloseConn(sqlite_conn);
            return model;
        }

        public int getPermissionsFromDb(string sqlQueryCommand)
        {
            int permissionLevel;
                
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            permissionLevel = (int)(long)sqlite_cmd.ExecuteScalar();
            CloseConn(sqlite_conn);
            return permissionLevel;
        }

        public int getIdFromDb(string sqlQueryCommand)
        {
            int myValue;

            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();


            sqlite_cmd.CommandText = sqlQueryCommand;
            myValue = sqlite_cmd.ExecuteScalar() == null ? -1: (int)(long)sqlite_cmd.ExecuteScalar();
            
            if (myValue == -1)
            {
                CloseConn(sqlite_conn);
                return -1;
            }
            else
            {
                //int iduser = int.Parse(myValue.ToString());
                CloseConn(sqlite_conn);
                return myValue;
            }
        }

        public void ReadDataFromDB(string sqlQueryCommand, RichTextBox richTextBoxTarget)
        {
            SQLiteConnection sqlite_conn = CreateConnection();
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = sqlQueryCommand;

            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);

                RichTextBoxDataChanging.changeTextRichAnswerBoxWithoutClear(myreader, richTextBoxTarget);
            }

            CloseConn(sqlite_conn);
        }
        #endregion
    }
}
