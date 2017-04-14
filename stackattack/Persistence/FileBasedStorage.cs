using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using stackattack.Users;
using System.Data.SQLite;
using stackattack.Questions;
using stackattack.Answers;

namespace stackattack.Persistence
{
    public class FileBasedStorage
    {
        private string dbName;

        private UserTable userTable;
        private QuestionTable questionTable;
        private AnswerTable answerTable;

        public FileBasedStorage(string dbName)
        {
            this.dbName = dbName;

            this.userTable = new UserTable();
            this.questionTable = new QuestionTable();
            this.answerTable = new AnswerTable();

            this.Load();
        }

        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile(this.dbName);
        }

        private SQLiteConnection GetConnection()
        {
            SQLiteConnection con;

            try
            {
                con = new SQLiteConnection($"Data Source={this.dbName};Version=3;");
                con.Open();
            }
            catch
            {
                CreateDatabase();
                con = new SQLiteConnection($"Data Source={this.dbName};Version=3;");
                con.Open();
            }

            return con;
        }

        private bool TablesExist(SQLiteConnection con)
        {
            try
            {
                return this.userTable.Exists(con);
            }
            catch
            {
                return false;
            }
        }

        private void CreateTables(SQLiteConnection con)
        {
            this.userTable.Create(con);
            this.questionTable.Create(con);
            this.answerTable.Create(con);
        }

        private void Load()
        {
            using (SQLiteConnection con = GetConnection())
            {
                if (!TablesExist(con))
                {
                    CreateTables(con);
                }
            }
        }

        public SQLiteConnection GetUserTable(out IDataStore<User> table)
        {
            table = this.userTable;
            return GetConnection();
        }

        public SQLiteConnection GetQuestionTable(out IDataStore<Question> table)
        {
            table = this.questionTable;
            return GetConnection();
        }

        public SQLiteConnection GetAnswerTable(out IDataStore<Answer> table)
        {
            table = this.answerTable;
            return GetConnection();
        }

        public IDataStore<User> GetUserTable()
        {
            return this.userTable;
        }

        public IDataStore<Question> GetQuestionTable()
        {
            return this.questionTable;
        }

        public IDataStore<Answer> GetAnswerTable()
        {
            return this.answerTable;
        }
    }
}