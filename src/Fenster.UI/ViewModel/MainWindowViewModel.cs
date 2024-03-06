using DynamicData;
using Fenster.UI.Domain;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace Fenster.UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        // https://github.com/dotnet/roslyn/issues/39291
        private string name = string.Empty;
        public string WindowTitle
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private WindowState _windowState;

        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                ShowInTaskbar = true;
                this.RaiseAndSetIfChanged(ref _windowState, value);
                ShowInTaskbar = value != WindowState.Minimized;
            }
        }

        private bool _showInTaskbar;

        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set => this.RaiseAndSetIfChanged(ref _showInTaskbar, value);
        }

        private readonly ReadOnlyObservableCollection<FenceWindowViewModel> _fences;

        public ReadOnlyObservableCollection<FenceWindowViewModel> Fences => _fences;

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }

        public ReactiveCommand<Unit, Unit> LoadedCommand { get; }
        public ReactiveCommand<CancelEventArgs, Unit> ClosingCommand { get; }

        public MainWindowViewModel(FencesRepository fencesRepo)
        {
            WindowTitle = "Fenster";

            ObservableChangeSet.Create<FenceWindowViewModel>(observableList =>
            {
                var loader = 
                    fencesRepo.Fences.Select(x => 
                        new FenceWindowViewModel() 
                        { 
                            Left = x.Left,
                            Top = x.Top,
                            Height = x.Height,
                            Width = x.Width,
                        }
                ).ToObservable().Subscribe(observableList.Add);

                //dispose of resources
                return new CompositeDisposable(loader);
            })
                .Bind(out _fences)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            OpenCommand = ReactiveCommand.Create(() => 
            { 
                WindowState = WindowState.Normal; 
            });

            ExitCommand = ReactiveCommand.Create(() => 
            { 
                System.Windows.Application.Current.Shutdown(); 
            });

            LoadedCommand = ReactiveCommand.Create(Loaded);

            ClosingCommand = ReactiveCommand.Create<CancelEventArgs>(Closing);
        }

        private void Loaded()
        {
            WindowState = WindowState.Minimized;
        }

        private void Closing(CancelEventArgs? e)
        {
            if (e == null)
                return;
            e.Cancel = true;
            WindowState = WindowState.Minimized;
        }
    }
}
