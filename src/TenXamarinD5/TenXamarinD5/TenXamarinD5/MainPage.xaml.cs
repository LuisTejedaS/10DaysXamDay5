using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenXamarinD3.Model;
using Xamarin.Forms;

namespace TenXamarinD5
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CheckIfShouldBeEnabled()
        {
            saveButton.IsEnabled = false;
            if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(contentEditor.Text))
                saveButton.IsEnabled = true;
        }

        private void TitleEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfShouldBeEnabled();
        }

        private void ContentEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfShouldBeEnabled();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {

            Experience newExperience = new Experience()
            {
                Title = titleEntry.Text,
                Content = contentEditor.Text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var inserted = 0;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Experience>();
                inserted = conn.Insert(newExperience);
            }

            if (inserted > 0)
            {
                titleEntry.Text = string.Empty;
                contentEditor.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "There was an error inserting the experience", "ok");
            }
            titleEntry.Text = string.Empty;
            contentEditor.Text = string.Empty;

        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
