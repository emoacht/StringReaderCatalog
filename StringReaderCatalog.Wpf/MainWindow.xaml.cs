using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StringReaderCatalog.Test;

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
			//await JsonSaveLoadAsync();

			await StreamBytesReadAsync();
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

		private readonly int[] bufferSizes = { 64, 128, 256, 512, }; // Buffer sizes in KiB

		private async Task StreamBytesReadAsync()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestFile");
			Debug.WriteLine($"File {filePath}");

			if (!File.Exists(filePath))
				return;

			foreach (var bufferSize in bufferSizes)
			{
				Debug.WriteLine($"Start {bufferSize}KiB");

				var sw = new Stopwatch();
				sw.Start();

				double oldValue = 0;

				var progress = new Progress<StreamProgress>(x => ShowPercentage(ref oldValue, x.Percentage));

				await StreamBytesReader.ReadBytesAsync(filePath, bufferSize * 1024, progress, CancellationToken.None);

				sw.Stop();

				Debug.WriteLine($"Complete {bufferSize}KiB {sw.Elapsed.TotalSeconds:f3}sec");
			}
		}

		private void ShowPercentage(ref double oldValue, double newValue)
		{
			var value = Math.Round(newValue * 100D) / 100D;

			if (oldValue < value)
			{
				oldValue = value;
				Debug.WriteLine($"{value:f2}");
			}
		}
	}
}