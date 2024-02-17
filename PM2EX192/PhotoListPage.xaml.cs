using PM2EX192;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PhotoLocationApp
{
	public partial class PhotoListPage : ContentPage
	{
		private List<PhotoLocation> allPhotos = new List<PhotoLocation>(); 

		public PhotoListPage()
		{
			InitializeComponent();
			PhotoCollectionView.SelectionChanged += PhotoCollectionView_SelectionChanged;
		}

		


		private void PhotoCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection.FirstOrDefault() is PhotoLocation selectedPhoto)
			{
				ShowOptionsAlert(selectedPhoto);
			}
		}

		private async void ShowOptionsAlert(PhotoLocation photoLocation)
		{
			string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Eliminar", "Actualizar");

			switch (action)
			{
				case "Eliminar":
					await DeletePhoto(photoLocation);
					break;
				case "Actualizar":
					await Navigation.PushAsync(new UpdatePhotoPage(photoLocation));
					break;
				default:
					// No hacer nada en caso de Cancelar
					break;
			}
		}


		private async Task DeletePhoto(PhotoLocation photoLocation)
		{
			try
			{
				await DatabaseService.DeletePhotoLocation(photoLocation);
				await DisplayAlert("Éxito", "La foto ha sido eliminada correctamente.", "OK");
				RefreshPhotoList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al eliminar la foto: {ex.Message}");
				await DisplayAlert("Error", "No se pudo eliminar la foto.", "OK");
			}
		}

		private void RefreshPhotoList()
		{
			try
			{
				allPhotos = DatabaseService.GetAllPhotoLocations().Result; // Espera sincrónicamente a que se complete la operación
				UpdatePhotoList(allPhotos);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al obtener las ubicaciones de fotos: {ex.Message}");
				// Manejar el error apropiadamente, por ejemplo, mostrar un mensaje de alerta
				DisplayAlert("Error", "No se pudieron cargar las ubicaciones de fotos.", "OK");
			}
		}

		private void UpdatePhotoList(List<PhotoLocation> photos)
		{
			PhotoCollectionView.ItemsSource = photos;
		}

		// Otros métodos de la clase PhotoListPage...
	}
}
