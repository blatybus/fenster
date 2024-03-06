using System.Collections;

namespace Fenster.UI.Controls
{
    public class FenceCollection
    {
        public IList<FenceWindowXaml> FenceWindows { get; private set; }

        public IEnumerable _itemsSource;
        
        public IEnumerable ItemsSource 
        {
            get => _itemsSource;
            set
            {
                Console.WriteLine(value);
                _itemsSource = value;
            }    
        }

        public FenceCollection()
        {
            _itemsSource = IEnumerableExtensions.Empty();
            FenceWindows = new List<FenceWindowXaml>(50);
        }
    }
}
