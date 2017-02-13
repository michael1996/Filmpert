using Filmfy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Filmfy
{
	public sealed partial class MovieDetailPage : Page
	{
		private Movie Movie { get; set; }
		private bool IsFavourited { get; set; }
		public MovieDetailPage()
		{
			this.InitializeComponent();
			Windows.Phone.UI.Input.HardwareButtons.BackPressed += BackButtonPressed;
		}

		private void BackButtonPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
		{
			if (Frame.CanGoBack)
			{
				Frame.GoBack();
				e.Handled = true;
			}
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			this.Movie = (Movie) e.Parameter;
			this.DataContext = Movie;

			// After binding hide the image if no url
			if (string.IsNullOrWhiteSpace(Movie.Poster))
				MovieImage.Visibility = Visibility.Collapsed;

			// Check if there is a trailer
			if (string.IsNullOrWhiteSpace(Movie.Trailer))
			{
				btn_Trailer.Visibility = Visibility.Collapsed;
			}

			foreach (Actor actor in Movie.Actors)
			{
				try
				{
					Image actorImage = new Image();
					actorImage.Name = actor.Name;
					actorImage.Source = new BitmapImage(new Uri(actor.Image));
					ActorsPanel.Children.Add(actorImage);
				}
				catch (Exception) { }
			}

			if (Login.LoggedInUser != null)
			{
				// Check if the User has the current movie as favourite

				Movie m = Login.LoggedInUser.Favourites.Cast<Movie>().Where(x => x.ID == Movie.ID).FirstOrDefault();

				if (m != null)
				{
					IsFavourited = true;
					SetFavouriteSymbol(IsFavourited);
				}
			}
		}

		private void btn_Favourite_Click(object sender, RoutedEventArgs e)
		{
			if (Login.LoggedInUser != null)
			{
				IsFavourited = !IsFavourited;
				if (IsFavourited)
				{
					// Add the favourite here, make the api call.
					Login.LoggedInUser.AddFavourite(Movie.ID);
				}
				else
				{
					// Remove the favourite here, make the api call.
					Login.LoggedInUser.DeleteFavourite(Movie.ID);
				}

				SetFavouriteSymbol(IsFavourited);
			}
		}

		/// <summary>
		/// Sets the favourite symbol.
		/// </summary>
		/// <param name="favourite">if set to <c>true</c> [favourite] then it shows the unfavourite icon else favourite.</param>
		private void SetFavouriteSymbol(bool favourite)
		{
			AppBarButton btn = btn_Favourite;
			if (favourite)
			{
				btn.Icon = new SymbolIcon(Symbol.UnFavorite);
			}
			else
			{
				btn.Icon = new SymbolIcon(Symbol.Favorite);
			}
		}

		private async void btn_Trailer_Click(object sender, RoutedEventArgs e)
		{
			await Launcher.LaunchUriAsync(new Uri(Movie.Trailer));
		}

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
