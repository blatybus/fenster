using Fenster.UI.Controls;
using Fenster.UI.ViewModel;
using ReactiveUI;
using System.Reactive.Disposables;
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

    public IList<FenceWindowXaml> FenceWindows { get; private set; }

    public FenceCollection FenceCollection { get; private set; } = new FenceCollection(); 

    public MainWindow()
    {
        InitializeComponent();

        ViewModel = new MainWindowViewModel(new Domain.FencesRepository());

        FenceWindows = new List<FenceWindowXaml>(ViewModel.Fences.Count);

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

        foreach (var fence in ViewModel.Fences) 
        {
            CreateFenceWindow(fence);
        }
    }

    private void CreateFenceWindow(FenceWindowViewModel fenceViewModel)
    {
        FenceWindows.Add(new FenceWindowXaml(fenceViewModel, this));
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