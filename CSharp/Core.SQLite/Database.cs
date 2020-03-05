﻿// SQLite IDatabase, IConnection, ICommand, IReader Wrapper
//
using Microsoft.Data.Sqlite;

namespace Business.Core.SQLite
{
    public class Database : IDatabase
    {
        private readonly Profile Profile;

        public Database(Profile profile) {
            Profile = profile;
        }

        SqliteConnection SQLiteConnection { get; set; }

        public IConnection Connection { get; set; }
        public ICommand Command { get; set; }

        void IDatabase.Connect() {
            SQLiteConnection = new SqliteConnection($"Filename={Profile?.SQLiteDatabasePath}");
            Connection = new Connection() { SQLiteConnection = SQLiteConnection };
            Command = new Command { SQLiteConnection = SQLiteConnection };
        }

        public Version Version() {
            return new Version() { Database = this };
        }
    }
}
