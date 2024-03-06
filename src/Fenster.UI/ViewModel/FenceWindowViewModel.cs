using ReactiveUI;

namespace Fenster.UI.ViewModel
{
    public class FenceWindowViewModel : ViewModelBase
    {
        private string name = string.Empty;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private double _height = 400;

        public double Height
        {
            get => _height;
            set => this.RaiseAndSetIfChanged(ref _height, value);
        }

        private double _width = 400;

        public double Width
        {
            get => _width;
            set => this.RaiseAndSetIfChanged(ref _width, value);
        }        
        
        private double _left = 400;

        public double Left
        {
            get => _left;
            set => this.RaiseAndSetIfChanged(ref _left, value);
        }        
        
        private double _top = 400;

        public double Top
        {
            get => _top;
            set => this.RaiseAndSetIfChanged(ref _top, value);
        }
    }
}
