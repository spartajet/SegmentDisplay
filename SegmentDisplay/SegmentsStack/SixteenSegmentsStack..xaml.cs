using System.Collections.ObjectModel;
using System.Windows;

namespace SegmentDisplay.SegmentsStack
{
    /// <summary>
    /// Interaction logic for SixteenSegmentsStack.xaml
    /// </summary>
    public partial class SixteenSegmentsStack : SegmentsStackBase
    {
        /// <summary>
        /// Stores chars from the splitted value string
        /// </summary>
        private ObservableCollection<CharItem> ValueChars;

        public SixteenSegmentsStack()
        {
            this.InitializeComponent();

            this.VertSegDivider = defVertDividerSixteen;
            this.HorizSegDivider = defHorizDividerSixteen;
        }

        public override void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            this.ValueChars = this.GetCharsArray();
            this.SegmentsArray.ItemsSource = this.ValueChars;
        }
    }
}
