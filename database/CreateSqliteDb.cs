using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Botex.database
{
    internal class CreateSqliteDb
    {
        //Utworzenie bazy danych SQLite
        // baza jest w Botex\bin\Debug\net6.0-windows nazywa się Botex.db
        public CreateSqliteDb()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateDb(sqlite_conn);
            CloseConn(sqlite_conn);
        }
            
        

        static SQLiteConnection CreateConnection()
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
       
        static void CreateDb(SQLiteConnection conn)
        {

            SQLiteCommand cmd;
            cmd = conn.CreateCommand();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS users(idUzytkownika INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            login TEXT, haslo TEXT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS notepad(idNotepad INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            userId INTEGER, Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, content TEXT, title TEXT, FOREIGN KEY(userId) REFERENCES users(idUzytkownika) )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS tweeter(idTweet INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, content TEXT, grup TEXT )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS mail(idMail INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, content TEXT, title TEXT, grup TEXT)";
            cmd.ExecuteNonQuery();

          
        }

        static void CloseConn(SQLiteConnection conn)
        {
            conn.Close();
        }
    }
}
