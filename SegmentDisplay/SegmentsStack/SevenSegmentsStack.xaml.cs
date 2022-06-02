using System.Collections.ObjectModel;
using System.Windows;

namespace SegmentDisplay.SegmentsStack
{
    /// <summary>
    /// Interaction logic for SevenSegmentsStack.xaml
    /// </summary>
    public partial class SevenSegmentsStack : SegmentsStackBase
    {

        /// <summary>
        /// Stores chars from the splitted value string
        /// </summary>
        private ObservableCollection<CharItem> ValueChars;

        public SevenSegmentsStack()
        {
            this.InitializeComponent();
        }

        public override void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            this.ValueChars = this.GetCharsArray();
            this.SegmentsArray.ItemsSource = this.ValueChars;
        }



    }
}
