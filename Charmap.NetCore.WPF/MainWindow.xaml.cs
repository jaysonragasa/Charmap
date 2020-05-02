using Charmap.Shared.ViewModels;
using System.Windows;

namespace Charmap.NetCore.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ((ViewModel_Main)this.DataContext).Fonty = new Fonty();
            ((ViewModel_Main)this.DataContext).Logger = App.Logger;
        }
    }
}
