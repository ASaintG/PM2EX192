using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace PM2EX192
{
	public class DatabaseService
	{
		private static SQLiteConnection _databaseConnection;

		public static SQLiteConnection GetDatabaseConnection()
		{
			try
			{
				if (_databaseConnection == null)
				{
					var databasePath = Path.Combine(FileSystem.AppDataDirectory, "photolocation.db3");
					Console.WriteLine($"Database path: {databasePath}");

					_databaseConnection = new SQLiteConnection(databasePath);
					_databaseConnection.CreateTable<PhotoLocation>();
					Console.WriteLine("Database connection opened successfully.");
				}

				return _databaseConnection;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error opening database connection: {ex.Message}");
				throw; 
			}
		}

		public static void SavePhotoLocation(PhotoLocation photoLocation)
		{
			try
			{
				using var connection = GetDatabaseConnection();
				connection.Insert(photoLocation);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al guardar la ubicación: {ex.Message}");
				throw; 
			}
		}

		public static List<PhotoLocation> GetAllPhotoLocations()
		{
			try
			{
				using var connection = GetDatabaseConnection();
				return connection.Table<PhotoLocation>().ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener todas las ubicaciones de fotos: {ex.Message}");
				throw; 
			}
		}
		public static void DeletePhotoLocation(PhotoLocation photoLocation, SQLiteConnection connection)
		{
			try
			{
				connection.Delete(photoLocation);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al eliminar la ubicación de la foto: {ex.Message}");
				throw;
			}
		}




	}
}
