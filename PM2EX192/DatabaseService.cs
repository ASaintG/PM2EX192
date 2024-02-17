using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PM2EX192
{
	public class DatabaseService
	{
		private static SQLiteAsyncConnection _databaseConnection;

		public static async Task<SQLiteAsyncConnection> GetDatabaseConnection()
		{
			try
			{
				if (_databaseConnection == null)
				{
					var databasePath = Path.Combine(FileSystem.AppDataDirectory, "photolocation.db3");
					Console.WriteLine($"Database path: {databasePath}");

					_databaseConnection = new SQLiteAsyncConnection(databasePath);
					await _databaseConnection.CreateTableAsync<PhotoLocation>();
					Console.WriteLine("Database connection opened successfully.");
				}

				return _databaseConnection;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error opening database connection: {ex.Message}");
				throw; // Relanzar la excepción para que pueda ser manejada en otro lugar si es necesario
			}
		}

		public static async Task SavePhotoLocation(PhotoLocation photoLocation)
		{
			try
			{
				var connection = await GetDatabaseConnection();
				await connection.InsertAsync(photoLocation);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al guardar la ubicación: {ex.Message}");
				throw; // Relanzar la excepción para que pueda ser manejada en otro lugar si es necesario
			}
		}

		public static async Task<List<PhotoLocation>> GetAllPhotoLocations()
		{
			try
			{
				var connection = await GetDatabaseConnection();
				return await connection.Table<PhotoLocation>().ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener todas las ubicaciones de fotos: {ex.Message}");
				throw; // Relanzar la excepción para que pueda ser manejada en otro lugar si es necesario
			}
		}

		public static async Task DeletePhotoLocation(PhotoLocation photoLocation)
		{
			try
			{
				var connection = await GetDatabaseConnection();
				await connection.DeleteAsync(photoLocation);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al eliminar la ubicación: {ex.Message}");
				throw; // Relanzar la excepción para que pueda ser manejada en otro lugar si es necesario
			}
		}
	}
}
