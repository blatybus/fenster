using Fenster.UI.ViewModel;
using System.Windows;

namespace Fenster.UI.Controls
{
    public class FenceCollection
    {
        public IList<FenceWindowXaml> FenceWindows { get; private set; }

        private Window _owner;

        public IEnumerable<FenceWindowViewModel> _itemsSource;

        public IEnumerable<FenceWindowViewModel> ItemsSource
        {
            get => _itemsSource;
            set
            {
                _itemsSource = value;
                CreateFenceWindows();
            }
        }

        public FenceCollection(Window owner)
        {
            _owner = owner;
            _itemsSource = Empty<FenceWindowViewModel>();
            FenceWindows = new List<FenceWindowXaml>(50);
        }

        private void CreateFenceWindows()
        {
            foreach (var vm in _itemsSource)
            {
                CreateFenceWindow(vm);
            }
        }

        private void CreateFenceWindow(FenceWindowViewModel fenceViewModel)
        {
            FenceWindows.Add(new FenceWindowXaml(_owner, fenceViewModel));
        }

        public static IEnumerable<T> Empty<T>()
        {
            yield break;
        }
    }
}
