

using PM2EX192;
using System.Data;

namespace PhotoLocationApp

{
	public partial class CapturePage : ContentPage
	{
		private Location CurrentLocation;
		private string CapturedImagePath; 

		public CapturePage()
		{
			InitializeComponent();
		}

		private async void CapturePhoto_Clicked(object sender, EventArgs e)
		{
			var photo = await MediaPicker.CapturePhotoAsync();
			var stream = await photo.OpenReadAsync();
			CapturedImagePath = Path.Combine(FileSystem.AppDataDirectory, $"{Guid.NewGuid()}.jpg"); // Guardar la ruta de la imagen capturada
			using (var fileStream = File.OpenWrite(CapturedImagePath))
			{
				await stream.CopyToAsync(fileStream);
			}

			
			PhotoPreview.Source = ImageSource.FromFile(CapturedImagePath);
		}

		private async void GetLocation_Clicked(object sender, EventArgs e)
		{
			try
			{
				var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default));
				if (location != null)
				{
					Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
					CurrentLocation = location; 

					
					LatitudeLabel.Text = $"Latitude: {location.Latitude}";
					LongitudeLabel.Text = $"Longitude: {location.Longitude}";
				}
				else
				{
					
					LatitudeLabel.Text = "Latitude: ";
					LongitudeLabel.Text = "Longitude: ";
				}
			}
			catch (FeatureNotSupportedException fnsEx)
			{
				
				Console.WriteLine(fnsEx.Message);
			}
			catch (PermissionException pEx)
			{
				
				Console.WriteLine(pEx.Message);
			}
			catch (Exception ex)
			{
				
				Console.WriteLine(ex.Message);
			}
		}

		private async void Save_Clicked(object sender, EventArgs e)
		{
			try
			{
				if (CurrentLocation != null)
				{
					var photoLocation = new PhotoLocation
					{
						ImagePath = CapturedImagePath,
						Description = DescriptionEntry.Text,
						Latitude = CurrentLocation.Latitude,
						Longitude = CurrentLocation.Longitude
					};

					
					var connection = DatabaseService.GetDatabaseConnection();

					
					connection.Insert(photoLocation);

					
					await Shell.Current.GoToAsync("///PhotoListPage");
				}
				else
				{
					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al guardar la ubicación: {ex.Message}");
			}
		}
	}

	}
