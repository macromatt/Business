﻿using System;

namespace Business.Core.Fake
{
	public class Reader : IReader
	{
		public Profile.Profile Profile { get; set; }
		public Exception ReaderGetException { get; set; }

		public object Value { get; set; }
		object[] objects;

		public bool HasRows {
			get {
				if (objects == null)
					return false;
				return true;
			}
		}

		public void Add(string[] strings) {
			this.objects = strings;
		}

		public void Add(object[] objects) {
			this.objects = objects;
		}

		public string GetString(int i) {
			if (ReaderGetException != null) {
				Profile.Log.Error(ReaderGetException);
				throw ReaderGetException;
			}
			if (this.IsDBNull(i))
				return String.Empty;

			return (string)objects[i];
		}

		public UInt32? GetInt32(int i) {
			if (this.HasRows && this.IsDBNull(i))
				return null;

			return (UInt32?)objects[i];
		}

		public bool? GetBoolean(int i) {
			if (this.IsDBNull(i))
				return null;

			return (Boolean?)objects[i];
		}

		public float? GetFloat(int i) {
			if (this.IsDBNull(i))
				return null;

			return (float?)objects[i];
		}

		public Boolean IsDBNull(int i) {
			return this.objects[i] == null || this.objects[i] == System.DBNull.Value;
		}

		public bool Read() {
			return true;
		}

		internal void SetValue(object value) {
			this.Value = value;
		}
	}
}
