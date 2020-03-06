﻿// NuoDB IDatabase, IConnection, ICommand, IReader Wrapper
//
using NuoDb.Data.Client;

namespace Business.Core.NuoDB
{
	public class Database : IDatabase
	{
		public Profile.Profile Profile { get; set; }

		public Database(Profile.Profile profile) {
			Profile = profile;
		}

		public string Type => "NuoDB";

		NuoDbConnection NuoDBClientConnection { get; set; }

		public IConnection Connection { get; set; }
		public ICommand Command { get; set; }

		void IDatabase.Connect() {
			NuoDbConnectionStringBuilder builder = new NuoDbConnectionStringBuilder {
				Server = Profile?.NuoDBProfile.Server,
				Database = Profile?.NuoDBProfile.Database,
				User = Profile?.NuoDBProfile.User,
				Password = Profile?.NuoDBProfile.Password,
				Schema = "Business"
			};

			NuoDBClientConnection = new NuoDbConnection(builder.ConnectionString);
			Connection = new Connection() { NuoDBClientConnection = NuoDBClientConnection };
			Command = new Command { NuoDBClientConnection = NuoDBClientConnection };
		}

		public Version SchemaVersion() {
			return Core.SchemaVersion.Get(this);
		}
	}
}