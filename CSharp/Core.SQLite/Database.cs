﻿// SQLite IDatabase, IConnection, ICommand, IReader Wrapper
//
using Microsoft.Data.Sqlite;

namespace Business.Core.SQLite
{
	public class Database : IDatabase
	{
		public Profile.Profile Profile { get; set; }

		public Database(Profile.Profile profile) {
			Profile = profile;
		}

		public string Type => "SQLite";

		SqliteConnection SQLiteConnection { get; set; }

		public IConnection Connection { get; set; }
		public ICommand Command { get; set; }

		void IDatabase.Connect() {
			SQLiteConnection = new SqliteConnection($"Filename={Profile?.SQLiteDatabasePath}");
			Connection = new Connection() { SQLiteConnection = SQLiteConnection };
			Command = new Command { SQLiteConnection = SQLiteConnection };
		}

		public Version SchemaVersion() {
			return Core.SchemaVersion.Get(this);
		}
	}
}