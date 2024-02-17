using PM2EX192;

namespace PhotoLocationApp
{
	public partial class UpdatePhotoPage : ContentPage
	{
		private PhotoLocation _photoLocationToUpdate;

		public UpdatePhotoPage(PhotoLocation photoLocation)
		{
			InitializeComponent();
			_photoLocationToUpdate = photoLocation;
		}

		private void InitializeComponent()
		{
			throw new NotImplementedException();
		}

		private async Task UpdatePhoto()
		{
			try
			{

				await Navigation.PopToRootAsync(); 
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al actualizar la foto: {ex.Message}");
			
				await DisplayAlert("Error", "No se pudo actualizar la foto.", "OK");
			}
		}
	}
}