using FilmfyApi.Attributes;
using FilmfyApi.Helpers;
using FilmfyApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Security.Principal;

namespace FilmfyApi.Models
{
	/// <summary>
	/// The base model for all database bound objects
	/// </summary>
	public class BaseModel
	{
		/// <summary>
		/// The ID
		/// </summary>
		public int ID { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseModel"/> class.
		/// </summary>
		public BaseModel() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseModel"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public BaseModel(int id)
		{
			this.ID = id;
			Load();
		}

		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <exception cref="System.Exception">Error executing query!</exception>
		public virtual void Load()
		{
			if (ID == 0)
				return;

			string tableName = GetTableName();
			string query = "SELECT * FROM " + tableName + " WHERE ID = @ID";

			Type type = this.GetType();
			PropertyInfo[] properties = type.GetProperties();

			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@ID", this.ID);
				try
				{
					command.Connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						if (!dataReader.HasRows)
						{
							ID = 0;
							return;
						}

						while (dataReader.Read())
						{
							foreach (PropertyInfo property in properties)
							{
								if (property.PropertyType.IsGenericType)
								{
									object obj = Activator.CreateInstance(property.PropertyType);
									property.SetValue(this, obj);
									continue;
								}

								object value = dataReader[property.Name];
								if (property.PropertyType.IsEnum)
								{
									property.SetValue(this, Enum.Parse(property.PropertyType, value.ToString()));
									continue;
								}

								property.SetValue(this, Convert.ChangeType(value, property.PropertyType), null);
							}
						}
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error executing query!", ex);
				}
			}

			LoadOthers();
		}

		/// <summary>
		/// Gets all the objects.
		/// </summary>
		/// <typeparam name="T">The model to load</typeparam>
		/// <returns></returns>
		/// <exception cref="System.Exception">Error loading data!</exception>
		public static List<T> GetAll<T>() where T : BaseModel
		{
			Type type = typeof(T);
			TableName tableNameAttribute = (TableName) Attribute.GetCustomAttribute(type, typeof(TableName));
			string tableName = tableNameAttribute.Name;

			List<T> models = new List<T>();
			string query = "SELECT ID FROM " + tableName;

			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				try
				{
					command.Connection.Open();
					using (SqlDataReader dataReader = command.ExecuteReader())
					{
						while (dataReader.Read())
						{
							T model = (T) Activator.CreateInstance(type, (int) dataReader["ID"]);
							models.Add(model);
						}
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error loading data!", ex);
				}
			}

			return models;
		}

		/// <summary>
		/// Saves this instance.
		/// </summary>
		/// <exception cref="System.Exception">Error executing query</exception>
		public virtual void Save()
		{
			Type type = this.GetType();
			string tableName = GetTableName();

			PropertyInfo[] properties = type.GetProperties();
			Dictionary<string, object> parametersAndValues = new Dictionary<string, object>();
			string query = string.Empty;
			if (ID != 0)
			{
				// Generate a Update Query
				query = "UPDATE " + tableName + " SET ";
			}
			else
			{
				// Generate the insert query
				query = "INSERT INTO " + tableName + " VALUES (";
			}

			for (int i = 0; i < properties.Length; i++)
			{
				string propertyName = properties[i].Name;
				object value = properties[i].GetValue(this);

				if (propertyName == "ID")
					continue;

				if (properties[i].PropertyType.IsGenericType)
				{
					continue;
				}

				if (value is Enum)
				{
					value = (int) value;
				}

				if (value != null && value is DateTime)
				{
					if ((DateTime) value < SqlDateTime.MinValue.Value)
					{
						value = SqlDateTime.MinValue.Value;
					}
					else if ((DateTime) value > SqlDateTime.MaxValue.Value)
					{
						value = SqlDateTime.MaxValue.Value;
					}
				}

				string parameterValue = "@" + propertyName;
				if (ID != 0)
				{
					query += propertyName + " = " + parameterValue + ", ";
				}
				else
				{
					query += parameterValue + ", ";
				}

				parametersAndValues.Add(parameterValue, value);
			}
			query = query.Remove(query.Length - 2, 2);

			if (ID != 0)
			{
				query += " WHERE ID = @ID";
				parametersAndValues.Add("@ID", ID);
			}
			else
			{
				query += "); SELECT SCOPE_IDENTITY()";
			}

			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				foreach (KeyValuePair<string, object> value in parametersAndValues)
				{
					object valueToUse = value.Value;
					if (valueToUse == null)
						valueToUse = "";
					command.Parameters.AddWithValue(value.Key, valueToUse);
				}

				try
				{
					command.Connection.Open();
					if (ID != 0)
					{
						command.ExecuteNonQuery();
					}
					else
					{
						object lastInsertedID = command.ExecuteScalar();
						ID = int.Parse(lastInsertedID.ToString());
					}
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error executing query", ex);
				}
			}

			SaveOthers();
		}

		/// <summary>
		/// Saves the others.
		/// </summary>
		protected virtual void SaveOthers() { }

		/// <summary>
		/// Loads the others.
		/// </summary>
		protected virtual void LoadOthers() { }

		/// <summary>
		/// Deletes this instance.
		/// </summary>
		/// <exception cref="System.Exception">Error executing query!</exception>
		protected virtual void Delete()
		{
			if (ID == 0)
				return;

			// Delete all the others first!
			DeleteOthers();

			// Delete the real object
			string query = "DELETE FROM " + GetTableName() + "WHERE ID = @ID";
			using (SqlCommand command = new SqlCommand(query, SqlHelper.GetConnection()))
			{
				command.Parameters.AddWithValue("@ID", ID);
				try
				{
					command.Connection.Open();
					command.ExecuteNonQuery();
					command.Connection.Close();
				}
				catch (Exception ex)
				{
					throw new Exception("Error executing query!", ex);
				}
			}
		}

		/// <summary>
		/// Deletes the others.
		/// </summary>
		protected virtual void DeleteOthers() { }

		/// <summary>
		/// Gets the name of the table of the class.
		/// </summary>
		/// <returns></returns>
		protected string GetTableName()
		{
			Type type = this.GetType();
			TableName tableNameAttribute = (TableName) Attribute.GetCustomAttribute(type, typeof(TableName));
			string tableName = tableNameAttribute.Name;

			return tableName;
		}
	}
}