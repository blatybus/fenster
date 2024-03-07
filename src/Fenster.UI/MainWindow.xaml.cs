using Fenster.UI.Controls;
using Fenster.UI.ViewModel;
using Microsoft.Win32;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace Fenster.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IViewFor<MainWindowViewModel>
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty
            .Register(nameof(ViewModel), typeof(MainWindowViewModel), typeof(MainWindow));

    public FenceCollection FenceCollection { get; private set; }

    public MainWindow()
    {
        InitializeComponent();

        this.Loaded += new RoutedEventHandler(Window_Loaded);

        Observable
            .FromEventPattern<EventHandler, EventArgs>(
                _ => SystemEvents.DisplaySettingsChanged += _, 
                _ => SystemEvents.DisplaySettingsChanged -= _)
            .Select(_ => SystemParameters.WorkArea)
            .Do(_ =>
            {
                Left = _.Right - Width;
                Top = _.Bottom - Height;
            })
            .Subscribe();

        ViewModel = new MainWindowViewModel(new Domain.FencesRepository());

        FenceCollection = new FenceCollection(this);

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(this.ViewModel, x => x.WindowTitle, x => x.Title)
                .DisposeWith(disposable);

            this.OneWayBind(this.ViewModel, x => x.ShowInTaskbar, x => x.ShowInTaskbar)
                .DisposeWith(disposable);

            this.OneWayBind(this.ViewModel, x => x.WindowState, x => x.WindowState)
                .DisposeWith(disposable);

            this.OneWayBind(this.ViewModel, x => x.Fences, x => x.FenceCollection.ItemsSource)
                .DisposeWith(disposable);
        });
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
        this.Left = desktopWorkingArea.Right - this.Width;
        this.Top = desktopWorkingArea.Bottom - this.Height;
    }

    public MainWindowViewModel? ViewModel
    {
        get => (MainWindowViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object? IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (MainWindowViewModel)value!;
    }
}