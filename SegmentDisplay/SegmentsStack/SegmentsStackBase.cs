using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SegmentDisplay.Segments;

namespace SegmentDisplay.SegmentsStack
{
    /// <summary>
    /// A base classs for stack of segment controls
    /// </summary>
    [DesignTimeVisible(false)]
    public class SegmentsStackBase : SegmentBase
    {
        public static DependencyProperty ElementsCountProperty;

        /// <summary>
        /// Number of segment elements to show
        /// </summary>
        public int ElementsCount
        {
            get { return (int)this.GetValue(ElementsCountProperty); }
            set { this.SetValue(ElementsCountProperty, value); }
        }

        static SegmentsStackBase()
        {
            ElementsCountProperty = DependencyProperty.Register("ElementsCount", typeof(int),
                typeof(SegmentsStackBase), new PropertyMetadata(1, CountChanged));
        }

        public SegmentsStackBase()
        {
            this.PropertyChanged += this.OnPropertyChanged;
        }

        public virtual void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void CountChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SegmentsStackBase segments = (SegmentsStackBase)sender;
            segments.OnPropertyChanged(sender, e);
        }

        public ObservableCollection<CharItem> GetCharsArray()
        {
            // converts value to char array
            char[] charArray = this.Value.ToCharArray();
            // the dots count
            var dotCount = charArray.Where(c => c == '.').Count();
            // the colons count
            var colonCount = charArray.Where(c => c == ':').Count();

            // the chars count without dots and colons
            var charCount = charArray.Count() - dotCount;
            
            var valueChars = new ObservableCollection<CharItem>();
            int index = 0;

            if (charArray.Count() > 0)
            {
                for (int i = 0; i < this.ElementsCount; i++)
                {
                    // sets properties for the each seven segment item
                    var item = new CharItem();
                    item.ShowDot = this.ShowDot;
                    item.ShowColon = this.ShowColon;
                    item.FillBrush = this.FillBrush;
                    item.SelectedFillBrush = this.SelectedFillBrush;
                    item.PenColor = this.PenColor;
                    item.SelectedPenColor = this.SelectedPenColor;
                    item.PenThickness = this.PenThickness;
                    item.GapWidth = this.GapWidth;
                    item.RoundedCorners = this.RoundedCorners;
                    item.TiltAngle = this.TiltAngle;
                    item.VertSegDivider = this.VertSegDivider;
                    item.HorizSegDivider = this.HorizSegDivider;

                    valueChars.Add(item);

                    if (i >= this.ElementsCount - charCount)
                    {
                        if (index <= charArray.Count() - 1)
                        {
                            // sets char for the element
                            if (charArray[index] != '.' && charArray[index] != ':')
                            {
                                valueChars[i].Item = charArray[index];
                            }

                            // sets ":" for the element
                            if (charArray[index] == ':')
                            {
                                valueChars[i].OnColon = true;
                            }

                            // sets dot for the element
                            if (charArray[index] == '.')
                            {
                                valueChars[i - 1].OnDot = true;
                                valueChars[i].Item = charArray[index + 1];
                                index++;
                            }
                        }

                        index++;
                    }
                }


                // sets dot for the last element if required
                if (this.ElementsCount >= charCount)
                {
                    if (charArray[charArray.Count() - 1] == '.')
                    {
                        var item = valueChars.Last();
                        item.OnDot = true;
                    }
                }
                else
                {
                    if (charArray[index] == '.')
                    {
                        var item = valueChars[this.ElementsCount - 1];
                        item.OnDot = true;
                    }
                }

            }

            return valueChars;
        }

    }
}
