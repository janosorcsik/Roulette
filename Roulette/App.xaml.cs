using System.Windows;
using System.Windows.Threading;
using Roulette.ViewModel;

namespace Roulette
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show($"Nem várt hiba történt: {e.Exception}");
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            ViewModelLocator.Cleanup();
        }
    }
}
