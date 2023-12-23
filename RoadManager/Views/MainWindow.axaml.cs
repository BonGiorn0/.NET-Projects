using Avalonia.Controls;
using RoadManager.ViewModels;

namespace RoadManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        
    }
}