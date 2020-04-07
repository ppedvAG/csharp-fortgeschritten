using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HalloAsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(600);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(60);
                }
                this.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = true);
            });
        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            cts = new CancellationTokenSource();

            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Task.Factory.StartNew(() => pb1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts);
                    Thread.Sleep(60);

                    if (i > 80)
                        throw new ApplicationException();

                    if (cts.IsCancellationRequested)
                        break;
                }
                this.Dispatcher.Invoke(() => ((Button)sender).IsEnabled = true);
            }, cts.Token)
                .ContinueWith(t => MessageBox.Show($"ERROR:{t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);

        }

        CancellationTokenSource cts = null;

        private void Abort(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAsyncAwait(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            cts = new CancellationTokenSource();
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                try
                {
                    await Task.Delay(30, cts.Token);
                }
                catch (TaskCanceledException ex)
                {
                    MessageBox.Show($"Task wurde erfolgreich abbgebrochen: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fehler: {ex.Message}");
                }

                if (cts.IsCancellationRequested)
                    break;
            }

            ((Button)sender).IsEnabled = !false;
        }

        private async void AsyncAwaitDB(object sender, RoutedEventArgs e)
        {
            var conString = "Server=(localdb)\\MSSQLLOCALDB;Database=Northwind;Trusted_Connection=true;";

            using (var con = new SqlConnection(conString))
            {
                await con.OpenAsync();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT count(*) FROM Employees;WAITFOR DELAY '00:00:05'";
                    var count = await cmd.ExecuteScalarAsync();
                    MessageBox.Show($"{count} Employees in DB");
                }
            }//-->con.Dispose(); //-->con.Close();
        }

        private long AlteLangsameFunktion(string text)
        {
            Thread.Sleep(4000);
            return text.Length * 32987324;
        }

        private Task<long> AlteLangsameFunktionAsync(string text)
        {
            return Task.Run(()=> AlteLangsameFunktion(text));
        }

        private async void AlteFunktion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{await AlteLangsameFunktionAsync("98437")}");
        }
    }
}
