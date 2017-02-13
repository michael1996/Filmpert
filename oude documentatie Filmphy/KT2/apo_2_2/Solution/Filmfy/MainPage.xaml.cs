using Filmfy.Models;
using FilmfyLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Filmfy
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public static List<Movie> Movies { get; set; }
		public MainPage()
		{
			this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Required;
			LoadMovies();
		}

		private void LoadListItems(List<Movie> movies = null)
		{
			List<Movie> allMovies = movies ?? new List<Movie>();
			allMovies = allMovies.OrderBy(x => x.ReleaseDate).ToList();
			MovieList.DataContext = new ObservableCollection<Movie>(allMovies);
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			// Check if the user was logged in, this might not happen when first startup.
			if (Login.LoggedInUser != null)
			{
				// This means the user was logged in
				SetAppBarIcons(true);
				
			}
			else
			{
				SetAppBarIcons(false);
				btn_Favourites.Visibility = Visibility.Collapsed;
			}
		}

		public void LoadMovies()
		{
			ProgressRingMovieList.IsActive = true;
			Task<List<Movie>> movieGetAllTask = new Movie().GetAllAsync();
			Task continueTask = movieGetAllTask.ContinueWith((x) =>
			{
				List<Movie> movies = x.Result;
				Movies = movies;
				LoadListItems(movies);
				ProgressRingMovieList.IsActive = false;
			}, TaskScheduler.FromCurrentSynchronizationContext());

			LoadListItems();
		}

		private void MovieList_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (!(e.ClickedItem is Movie))
				return;
			Movie m = (Movie) e.ClickedItem;

			this.Frame.Navigate(typeof(MovieDetailPage), m);
		}

		private void btn_Login_Click(object sender, RoutedEventArgs e)
		{
			if (Login.LoggedInUser != null)
			{
				Login.LoggedInUser.LogoutAsync().ContinueWith((x) =>
				{
					Login.LoggedInUser = null;
					SetAppBarIcons(false);
					LoadMovies();
				}, TaskScheduler.FromCurrentSynchronizationContext());
			}
			else
			{
				this.Frame.Navigate(typeof(Login));
			}
		}

		private void SetAppBarIcons(bool loggedIn = false)
		{
			if (loggedIn)
			{
				btn_Login.Icon = new SymbolIcon(Symbol.BlockContact);
				btn_Favourites.Visibility = Visibility.Visible;
			}
			else
			{
				btn_Login.Icon = new SymbolIcon(Symbol.Contact);
				btn_Favourites.Visibility = Visibility.Collapsed;
				btn_Favourites.IsChecked = false;
			}
		}

		private void btn_Reload_Click(object sender, RoutedEventArgs e)
		{
			LoadMovies();
		}

		private void btn_Favourites_Click(object sender, RoutedEventArgs e)
		{
			if (sender is AppBarToggleButton)
			{
				AppBarToggleButton btn = sender as AppBarToggleButton;
				if ((bool) btn.IsChecked)
				{
					// Display favourites
					if (Login.LoggedInUser != null)
					{
						ProgressRingMovieList.IsActive = true;
						LoadListItems(Login.LoggedInUser.Favourites.Cast<Movie>().ToList());
						ProgressRingMovieList.IsActive = false;
					}
				}
				else
				{
					LoadMovies();
				}
			}
		}

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
