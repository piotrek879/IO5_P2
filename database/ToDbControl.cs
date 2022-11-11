using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Botex.database
{
    internal class ToDbControl
    {

        private static readonly DbControl dbControl = new DbControl();

        public static void ToDbNotepad(string msg, string title, int idUser )
        {
            string myDbQuery = $"INSERT INTO notepad(userId,content,title) VALUES({idUser},'{msg}','{title}')"; dbControl.insertDataToDB(myDbQuery); 
        }

       public static void FromDbNotepad(string title, int idUser, RichTextBox botexAnswerBox)
        {
            string myDbQuery = $"SELECT content FROM notepad WHERE title LIKE '{title}' AND userId = {idUser}";
            dbControl.ReadDataFromDB(myDbQuery, botexAnswerBox);
        }
    }
}
/*
 *  notepad(idNotepad INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            userId INTEGER, Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
content,
title TEXT, FOREIGN KEY(userId) REFERENCES users(idUzytkownika) )";

 */