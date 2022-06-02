using System.Windows.Media;

namespace SegmentDisplay.Segments
{
    /// <summary>
    /// The class to detect selected segment
    /// </summary>
    public class GeometryWithSegm
    {
        public PathGeometry Geometry { get; set; }
        public int SegmentNumber { get; set; }
        public bool IsSelected { get; set; }

        public GeometryWithSegm(PathGeometry geometry, int segm, bool isSelected = false)
        {
            this.Geometry = geometry;
            this.SegmentNumber = segm;
            this.IsSelected = isSelected;
        }
    }
}
