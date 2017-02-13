using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Filmfy.Models;
using Filmfy;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Filmfy
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Login : Page
	{
		/// <summary>
		/// Gets or sets the logged in user.
		/// </summary>
		/// <value>
		/// The logged in user.
		/// </value>
		public static User LoggedInUser { get; set; }

		public Login()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.
		/// This parameter is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void btn_Register_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Registreren));
		}

		private void btn_Login_Click(object sender, RoutedEventArgs e)
		{
			User user = new User();
			user.Username = txt_Username.Text;
			user.Password = txt_Password.Password;
			user.LoginAsync().ContinueWith(async (x) =>
            {
                if (user.LoggedIn == true)
                {
                    LoggedInUser = user;
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    MessageDialog error = new MessageDialog("please enter username and password", "Wrong username or password");
                    await error.ShowAsync();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
		}

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
