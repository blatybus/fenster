using Fenster.UI.ViewModel;
using ReactiveUI;
using System.ComponentModel;
using System.Windows;

namespace Fenster.UI.Controls
{
    /// <summary>
    /// Interaction logic for FenceWindowXaml.xaml
    /// </summary>
    public partial class FenceWindowXaml : Window, IViewFor<FenceWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty
                .Register(nameof(ViewModel), typeof(FenceWindowViewModel), typeof(FenceWindowXaml));

        public FenceWindowXaml(
            Window owner, FenceWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            Name = viewModel.Name;
            // Owner = owner;
            
            Closing += Window2_Closing;

            Show();
        }

        public FenceWindowViewModel? ViewModel
        {
            get => (FenceWindowViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (FenceWindowViewModel)value!;
        }

        private void Window2_Closing(object? sender, CancelEventArgs e)
        {
            var w2 = sender as FenceWindowXaml;
            var w1 = System.Windows.Application.Current.MainWindow as MainWindow;
            //var arr = w1?.WindowArray;
            //var index = ((int)(w2.Tag));
            //arr[index] = null;
        }
    }
}
