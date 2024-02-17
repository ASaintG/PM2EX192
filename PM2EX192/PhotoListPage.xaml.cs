using PM2EX192;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PhotoLocationApp
{
	public partial class PhotoListPage : ContentPage
	{
		private List<PhotoLocation> allPhotos; // Lista que contiene todas las fotos

		public PhotoListPage()
		{
			InitializeComponent();
			PhotoCollectionView.SelectionChanged += PhotoCollectionView_SelectionChanged;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// Obtener la lista de fotos desde la base de datos o cualquier otra fuente de datos
			allPhotos = DatabaseService.GetAllPhotoLocations();

			// Mostrar todas las fotos al cargar la página
			UpdatePhotoList(allPhotos);
		}



		private async void PhotoCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.CurrentSelection.FirstOrDefault() is PhotoLocation selectedPhoto)
			{
				bool answer = await DisplayAlert("Eliminar", "¿Desea eliminar esta foto?", "Sí", "No");

				if (answer)
				{
					var connection = DatabaseService.GetDatabaseConnection();
					DatabaseService.DeletePhotoLocation(selectedPhoto, connection);
					allPhotos.Remove(selectedPhoto);
					UpdatePhotoList(allPhotos);
				}
			}
		}


		private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
		{
			string searchText = e.NewTextValue;

			// Filtrar la lista de todas las fotos en función del texto de búsqueda
			List<PhotoLocation> searchedPhotos = allPhotos.Where(photo => photo.Description.Contains(searchText)).ToList();

			// Actualizar la lista de fotos en CollectionView
			UpdatePhotoList(searchedPhotos);
		}

		private void UpdatePhotoList(List<PhotoLocation> photos)
		{
			PhotoCollectionView.ItemsSource = photos;
		}
	}
}
