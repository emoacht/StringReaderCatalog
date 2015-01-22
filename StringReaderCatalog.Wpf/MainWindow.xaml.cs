using StringReaderCatalog.Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StringReaderCatalog.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await JsonSaveLoadAsync();
        }

        private async Task JsonSaveLoadAsync()
        {
            var filePath = Path.GetTempFileName();

            try
            {
                await JsonStringReaderTest.SaveAsync(filePath, true, 0);

                Debug.WriteLine(await JsonStringReaderTest.LoadAsync(filePath, true));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}