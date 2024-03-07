using Fenster.UI.ViewModel;
using ReactiveUI;
using System.Windows;

namespace Fenster.UI.Controls
{
    //<!--ShowInTaskbar="False" local:WindowExtensions.AlwaysOnBottom="True"-->
    public class FenceWindow : Window, IViewFor<FenceWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty
                .Register(nameof(ViewModel), typeof(FenceWindowViewModel), typeof(FenceWindow));

        public static readonly DependencyProperty AlwaysOnBottomProperty =
            DependencyProperty
                .Register(nameof(AlwaysOnBottom), typeof(bool), typeof(FenceWindow));

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
        public bool AlwaysOnBottom
        {
            get => (bool)GetValue(AlwaysOnBottomProperty);
            set => SetValue(AlwaysOnBottomProperty, value);
        }

        public FenceWindow()
        {
            
        }

    }
}
