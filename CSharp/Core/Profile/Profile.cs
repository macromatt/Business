﻿using System;
using System.IO;

namespace Business.Core.Profile
{
	public class Profile
	{
		public ILog Log { get; set; }

		public string SQLiteDatabasePath {
			get {
				String path;

				if (String.IsNullOrEmpty(SQLiteProfile.Path))
					path = "sandbox/Business/business.sqlite3";
				else
					path = SQLiteProfile.Path;

				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
			}
		}

		public SQLite SQLiteProfile { get; set; }
		public NuoDB NuoDBProfile { get; set; }
		public PostgreSQL PostgreSQLProfile { get; set; }

		public Profile() {
			SQLiteProfile = new SQLite();
			NuoDBProfile = new NuoDB();
			PostgreSQLProfile = new PostgreSQL();

			var filePath = GetBasePath() + "profile.json";
			try {
				Newtonsoft.Json.Linq.JObject profileJSON = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(filePath));

				var sqlite = profileJSON["SQLite"];
				if(sqlite != null) {
					SQLiteProfile = sqlite.ToObject<SQLite>();
					SQLiteProfile.Active = true;
				}

				var nuoDb = profileJSON["NuoDb"];
				if (nuoDb != null) {
					NuoDBProfile = nuoDb.ToObject<NuoDB>();
					NuoDBProfile.Active = true;
				}

				var postgreSQL = profileJSON["PostgreSQL"];
				if (postgreSQL != null) {
					PostgreSQLProfile = postgreSQL.ToObject<PostgreSQL>();
					PostgreSQLProfile.Active = true;
				}
			} catch {
				// Use the profile object defaults
			}
		}

		public static string GetBasePath() {
			return AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
		}
	}

	public class SQLite
	{
		public Boolean Active { get; set; } = true;
		public string Path { get; set; }
	}

	public class NuoDB
	{
		public Boolean Active { get; set; } = false;
		public string Server { get; set; } = "nuodb";
		public string Database { get; set; } = "MyCo";
		public string User { get; set; } = "test";
		public string Password { get; set; } = "secret";
	}

	public class PostgreSQL
	{
		public Boolean Active { get; set; } = false;
		public string Host { get; set; } = "postgresql";
		public string Database { get; set; } = "MyCo";
		public string User { get; set; } = "test";
	}
}
