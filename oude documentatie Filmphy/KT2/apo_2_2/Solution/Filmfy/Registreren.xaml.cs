using System;
using Filmfy;
using Filmfy.Models;
using FilmfyLibrary;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Filmfy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registreren : Page
    {
        public Registreren()
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var SaveUser = new User();
            SaveUser.Username = Username_txt.Text;
            SaveUser.Password = Password_txt.Text;
            SaveUser.Email = email_txt.Text;
            SaveUser.Birthday = birthday_txt.Date.Date;
            SaveUser.Register();
            this.Frame.GoBack();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
