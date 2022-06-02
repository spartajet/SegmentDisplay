using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SegmentDisplay
{
    /// <summary>
    /// A panel children of which fill maximum space
    /// </summary>
    [DesignTimeVisible(false)] 
    internal class ArrangedPanel : Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            double y = 0;
            double w = finalSize.Width / this.InternalChildren.Count;
            double h = finalSize.Height;

            foreach (UIElement child in this.InternalChildren)
            {
                child.Arrange(new Rect(new Point(x, y), new Size(w, h)));
                x += w;
            }
            return finalSize;
        }
    }
}
