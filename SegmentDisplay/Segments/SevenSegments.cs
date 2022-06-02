using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SegmentDisplay.Segments
{
    /// <summary>
    /// A seven segments control
    /// </summary>
    public class SevenSegments : SegmentBase
    {
        #region Protected variables

        protected bool isPropertyCahnged = true;
        protected double startPointThickness;

        protected double vertRoundCoef = 0;
        protected double horizRoundCoef = 0;

        /// <summary>
        /// The width of the vert. segm
        /// </summary>
        protected double VertSegW { get; private set; }

        /// <summary>
        /// The width of the vert. segment's part
        /// </summary>
        protected double VertSegPartW { get; private set; }

        /// <summary>
        /// The height of the vert. segment's part
        /// </summary>
        protected double VertSegSmallPartH { get; private set; }

        /// <summary>
        /// The height of the horiz. segment
        /// </summary>
        protected double HorizSegH { get; private set; }

        /// <summary>
        /// The height of the horiz. segment's part
        /// </summary>
        protected double HorizSegSmallPartH { get; private set; }

        /// <summary>
        /// The width of the horiz. segment's part
        /// </summary>
        protected double HorizSegSmallPartW { get; private set; }

        /// <summary>
        /// The horizontal midlle point
        /// </summary>
        protected double MidPoint { get; private set; }

        /// <summary>
        /// The gap between segments
        /// </summary>
        protected double GapW { get; private set; }


        /// <summary>
        /// The diameter of the dot
        /// </summary>
        protected double DotDiameter { get; private set; }

        /// <summary>
        /// The diameter of the colon
        /// </summary>
        protected double ColonDiameter { get; private set; }

        /// <summary>
        /// The height depending on the decimal dot
        /// </summary>
        protected double VirtualWidth { get; private set; }

        /// <summary>
        /// The width depending on the decimal dot
        /// </summary>
        protected double VirtualHeight { get; private set; }


        /// <summary>
        /// The list of geometries to detect selected segments
        /// </summary>
        protected List<GeometryWithSegm> GeometryFigures;

        /// <summary>
        /// The width of the vert. segment's bottom part
        /// </summary>
        protected double VertSegBotPartW { get; private set; }
      
        /// <summary>
        /// Points collection for the left bottom segment
        /// </summary>
        protected PointCollection LeftBottomSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the left top segment
        /// </summary>
        protected PointCollection LeftTopSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the top segment
        /// </summary>
        protected PointCollection TopSegmPoints { get;  set; }

        /// <summary>
        /// Points collection for the bottom segment
        /// </summary>
        protected PointCollection BottomSegmPoints { get; set; }

        /// <summary>
        /// Points collection for the middle segment
        /// </summary>
        protected PointCollection MiddleSegmPoints { get; set; }

        /// <summary>
        /// Points collection for the right top segment
        /// </summary>
        protected PointCollection RightTopSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right bottom segment
        /// </summary>
        protected PointCollection RightBottomSegmPoints { get; private set; }

        protected double figureStartPointY;

        #endregion

        #region Constructor

        public SevenSegments()
        {
            this.PropertyChanged += this.OnPropertyChanged;
            this.vertRoundCoef = 5.5;
            this.horizRoundCoef = 15;
        }

        private void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SegmentBase segments = (SegmentBase)sender;
            this.isPropertyCahnged = true;

            segments.InvalidateVisual();
        }

        #endregion

        #region Drawing

        protected override void OnRender(DrawingContext drawingContext)
        {
           base.OnRender(drawingContext);
           this.CalculateMeasures();

           this.AssignSegments();
           this.ClearSegmentsSelection();
           this.SetSegments();

            // Draws segments
            foreach (var entry in this.GeometryFigures)
            {

                if (this.SegmentsBrush.Any())
                {
                    var brush = this.SegmentsBrush.SingleOrDefault(s => s.Item1 == (int)entry.SegmentNumber);
                    Pen figurePen = new Pen(new SolidColorBrush(brush != null ? brush.Item3 : this.PenColor), 
                        this.PenThickness);


                    drawingContext.DrawGeometry(brush != null ? brush.Item2 : this.FillBrush,
                        figurePen, entry.Geometry);

                }
                else
                {
                    Pen figurePen = new Pen(new SolidColorBrush(entry.IsSelected ? this.SelectedPenColor : this.PenColor), this.PenThickness);

                    drawingContext.DrawGeometry(entry.IsSelected ? this.SelectedFillBrush : this.FillBrush,
                        figurePen, entry.Geometry);
                }
            }

            // Draws decimal dot
            this.DrawDot(drawingContext);

            // Draws colon
            this.DrawColon(drawingContext);
        }

        /// <summary>
        /// Clear selected segments and value
        /// </summary>
        public void ClearSegments()
        {
            this.Value = string.Empty;
            this.SelectedSegments = new List<int>();
            this.SegmentsBrush = new List<Tuple<int, Brush, Color>>();
        }

        /// <summary>
        /// Assigns a segment number to required path geometry. Order is important!
        /// </summary>
        protected virtual void AssignSegments()
        {
            this.GeometryFigures = new List<GeometryWithSegm>();

            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftBottomSegement(), (int)SevenSegmentsNumbers.LeftBottom));
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftTopSegement(), (int)SevenSegmentsNumbers.LeftTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightTopSegement(), (int)SevenSegmentsNumbers.RightTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightBottomSegement(), (int)SevenSegmentsNumbers.RightBottom));
            this.GeometryFigures.Add(new GeometryWithSegm(this.MiddleSegement(), (int)SevenSegmentsNumbers.Middle));
            this.GeometryFigures.Add(new GeometryWithSegm(this.TopSegement(), (int)SevenSegmentsNumbers.Top));
            this.GeometryFigures.Add(new GeometryWithSegm(this.BottomSegement(), (int)SevenSegmentsNumbers.Bottom));
        }

        /// <summary>
        /// Selects required segments
        /// </summary>
        protected void SetSegments()
        {
            if (this.SelectedSegments.Any())
            {
                for (int i = 0; i < this.SelectedSegments.Count; i++)
                {
                    this.GeometryFigures.Single(t => (int)t.SegmentNumber == this.SelectedSegments[i]).IsSelected = true;
                }
            }
            else
            {
                this.ValueSegmentsSelection();
            }
        }


        /// <summary>
        /// Calculates required points and measures
        /// </summary>
        private void CalculateMeasures()
        {
            //Horiz. figure
            this.HorizSegH = this.ActualHeight / this.HorizSegDivider;
            this.HorizSegSmallPartH = this.HorizSegH / 4;

            //Vert. figure
            this.VertSegW = this.ActualWidth / this.VertSegDivider;
            this.VertSegPartW = (this.VertSegW / 3.5);
            this.VertSegSmallPartH = this.VertSegW / 3.5;
            this.VertSegBotPartW = this.VertSegW / 2;

            this.HorizSegSmallPartW = this.VertSegW / 4;

            //The points calculation
            this.MidPoint = this.ActualHeight / 2;
            this.GapW = this.GapWidth;

            this.DotDiameter = this.HorizSegH;
            this.ColonDiameter = this.HorizSegH;

            this.VirtualHeight = this.ShowDot ? this.ActualHeight - this.DotDiameter / 1.5 : this.ActualHeight;
            this.VirtualWidth = this.ShowDot ? this.ActualWidth - this.DotDiameter / 1.5 : this.ActualWidth;

            this.figureStartPointY = this.VirtualHeight - (this.HorizSegSmallPartH + this.GapW + this.VertSegSmallPartH);
            this.startPointThickness = this.PenThickness / 2;

        }

        /// <summary>
        /// Selects segments depending on the value 
        /// </summary>
        protected virtual void ValueSegmentsSelection()
        {
            int tempValue;
            if (int.TryParse(this.Value, out tempValue))
            {
                if (tempValue > 9) tempValue = 9;
                if (tempValue < 0) tempValue = 0;
                switch (tempValue)
                {
                    case 0:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.RightTop,
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.Bottom, 
                                            (int)SevenSegmentsNumbers.LeftBottom);
                        break;
                    case 1:
                        this.SelectSegments((int)SevenSegmentsNumbers.RightTop, 
                                            (int)SevenSegmentsNumbers.RightBottom);
                        break;
                    case 2:
                        this.SelectSegments((int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.RightTop, 
                                            (int)SevenSegmentsNumbers.Middle,
                                            (int)SevenSegmentsNumbers.LeftBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                    case 3:
                        this.SelectSegments((int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.RightTop,
                                            (int)SevenSegmentsNumbers.Middle, 
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                    case 4:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.RightTop,
                                            (int)SevenSegmentsNumbers.Middle, 
                                            (int)SevenSegmentsNumbers.RightBottom);
                        break;
                    case 5:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.Middle,
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                    case 6:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.Middle,
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.LeftBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                    case 7:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top,
                                            (int)SevenSegmentsNumbers.RightTop, 
                                            (int)SevenSegmentsNumbers.RightBottom);
                        break;
                    case 8:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.RightTop,
                                            (int)SevenSegmentsNumbers.Middle,
                                            (int)SevenSegmentsNumbers.LeftBottom, 
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                    case 9:
                        this.SelectSegments((int)SevenSegmentsNumbers.LeftTop, 
                                            (int)SevenSegmentsNumbers.Top, 
                                            (int)SevenSegmentsNumbers.RightTop,
                                            (int)SevenSegmentsNumbers.Middle, 
                                            (int)SevenSegmentsNumbers.RightBottom, 
                                            (int)SevenSegmentsNumbers.Bottom);
                        break;
                }
            }

            // Selects segment for the minus sign 
            if (this.Value == "-")
            {
                this.SelectSegments((int)SevenSegmentsNumbers.Middle);
            }

        }


        /// <summary>
        /// Draws decimal dot separator
        /// </summary>
        protected void DrawDot(DrawingContext drawingContext)
        {
            if (this.ShowDot)
            {
                PathGeometry pathGeometry = new PathGeometry();
                Pen dotPen = new Pen(new SolidColorBrush(this.OnDot ? this.SelectedPenColor : this.PenColor), this.PenThickness);
                var centerPoint = new Point(this.ActualWidth - this.DotDiameter / 2, this.ActualHeight - this.DotDiameter / 2);
                pathGeometry = this.CreateEllipseGeometry(centerPoint, pathGeometry, this.DotDiameter / 2);
                drawingContext.DrawGeometry(this.OnDot ? this.SelectedFillBrush : this.FillBrush,
                    dotPen, pathGeometry);
            }
        }

        /// <summary>
        /// Draws colon
        /// </summary>
        private void DrawColon(DrawingContext drawingContext)
        {
            if (this.ShowColon)
            {
                PathGeometry pathGeometry = new PathGeometry();

                var hUpper = (this.MiddleSegmPoints[2].Y - this.GapW - this.HorizSegH) - (this.HorizSegH + this.GapW);
                var yTop = this.HorizSegH + this.GapW + hUpper / 2 + this.ColonDiameter / 2;
                var xTop = this.XByAngle(yTop) + this.VertSegW;

                var hLower = (this.BottomSegmPoints[2].Y - this.GapW) - (this.MiddleSegmPoints[0].Y + this.GapW + this.HorizSegH);
                var yBottom = this.MiddleSegmPoints[0].Y + this.GapW + this.HorizSegH + hLower / 2 - this.ColonDiameter / 2;
                var xBottom = this.XByAngle(yBottom) + this.VertSegW;

                var xTopMiddle = xTop + (((this.VirtualWidth - xBottom) - xTop) / 2);
                var xBottomMiddle = xBottom + (((this.VirtualWidth - xTop) - xBottom) / 2);

                Pen colonPen = new Pen(new SolidColorBrush(this.OnColon ? this.SelectedPenColor : this.PenColor), this.PenThickness);

                // the top ellipse
                var centerPoint = new Point(xTopMiddle, yTop);
                pathGeometry = this.CreateEllipseGeometry(centerPoint, pathGeometry, this.ColonDiameter / 2);
                drawingContext.DrawGeometry(this.OnColon ? this.SelectedFillBrush : this.FillBrush,
                    colonPen, pathGeometry);

                //the bottom ellipse
                centerPoint = new Point(xBottomMiddle, yBottom);
                pathGeometry = this.CreateEllipseGeometry(centerPoint, pathGeometry, this.ColonDiameter / 2);
                drawingContext.DrawGeometry(this.OnColon ? this.SelectedFillBrush : this.FillBrush,
                    colonPen, pathGeometry);

            }
        }

        private PathGeometry CreateEllipseGeometry(Point centerPoint, 
            PathGeometry pathGeometry,
            double diameter)
        {
            EllipseGeometry ellipseGeometry;
            SkewTransform transform;
            ellipseGeometry = new EllipseGeometry();
            ellipseGeometry.Center = centerPoint;
            ellipseGeometry.RadiusX = diameter;
            ellipseGeometry.RadiusY = diameter;

            pathGeometry = PathGeometry.CreateFromGeometry(ellipseGeometry);

            transform = new SkewTransform(-this.TiltAngle,
                0, centerPoint.X, centerPoint.Y);
            pathGeometry.Transform = transform;
            return pathGeometry;
        }


        /// <summary>
        /// Sets required geometry figures as selected
        /// </summary>
        protected void SelectSegments(params int[] segmNumbers)
        {
            for (int i = 0; i < segmNumbers.Length; i++)
            {
                this.GeometryFigures.Single(t => (int)t.SegmentNumber == segmNumbers[i]).IsSelected = true;
            }

        }

        /// <summary>
        /// Clears selection for all geometry figures 
        /// </summary>
        protected void ClearSegmentsSelection()
        {
            this.GeometryFigures.ForEach(c => c.IsSelected = false);
        }


        /// <summary>
        /// Draws custom path geometry
        /// </summary>
        protected PathGeometry SegmentPathGeometry(Point startPoint, PolyLineSegment polyLineSegment)
        {
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(polyLineSegment);
            return pathGeometry;
        }

        /// <summary>
        /// Required segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry SegmentGeometry(PointCollection assignPoints, PointCollection drawnPoints)
        {
            assignPoints = drawnPoints;
            Point startPoint = assignPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = assignPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// Returns X-coord by the angle and height
        /// </summary>
        /// <param name="y">Y-coordinate to calculate height</param>
        protected double XByAngle(double y)
        {
            var h = this.figureStartPointY - y;
            return (this.TanAngle() * h);
        }

        /// <summary>
        /// Returns tangent of the tilt angle in degrees
        /// </summary>
        protected double TanAngle()
        {
            return Math.Tan(this.TiltAngle * (Math.PI / 180.0));
        }

        /// <summary>
        /// Returns gap shift for the top and bottom segments
        /// </summary>
        private double GapShift()
        {
            return this.GapW * 0.75;
        }


        #endregion

        #region Points' locations

        /// <summary>
        /// Calulates points  for the left top segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetLeftTopSegmPoints()
        {
            PointCollection points = new PointCollection();

            var intermPoint = this.VirtualHeight / 2 - this.HorizSegH / 2;
            var startTopY = this.HorizSegSmallPartH + this.GapW + this.VertSegSmallPartH + this.startPointThickness;
            var x1 = this.XByAngle(startTopY);

            // the bezier point
            Point bezPoint;
            if (this.RoundedCorners)
            {
                var yBezier = (this.VirtualHeight - this.startPointThickness) / this.vertRoundCoef;
                var xBezier = this.RoundedCorners ? this.XByAngle(yBezier) : 0;
                bezPoint = new Point(xBezier + this.startPointThickness, yBezier);
            }
            else
            {
                bezPoint = new Point(x1 + this.startPointThickness, this.HorizSegSmallPartH + this.startPointThickness + this.GapW + this.VertSegSmallPartH);
            }


            startTopY = this.HorizSegSmallPartH + this.GapShift();
            var x2 = this.XByAngle(startTopY);

            startTopY = this.HorizSegH + this.GapW / 2;
            var x3 = this.XByAngle(startTopY);

            startTopY = intermPoint - (this.GapW / 2);
            var x4 = this.XByAngle(startTopY - this.startPointThickness);

            startTopY = (this.VirtualHeight / 2) - this.GapW / 2;
            var x5 = this.XByAngle(startTopY - this.startPointThickness);


            // three top points, starting from the left point
            points.Add(new Point(x1 + this.startPointThickness, this.HorizSegSmallPartH + this.GapW + this.VertSegSmallPartH + this.startPointThickness));
            points.Add(new Point(x2 + this.VertSegPartW + this.startPointThickness, this.HorizSegSmallPartH + this.startPointThickness + this.GapShift()));
            points.Add(new Point(x3 + this.VertSegW + this.startPointThickness, this.HorizSegH + this.startPointThickness + this.GapW / 2));

            // three bottom points, starting from the right point
            points.Add(new Point(x4 + this.VertSegW + this.startPointThickness, intermPoint - (this.GapW / 2)));
            points.Add(new Point(x5 + this.VertSegBotPartW + this.startPointThickness, (this.VirtualHeight / 2) - this.GapW / 2));
            points.Add(new Point(x5 + this.startPointThickness, (this.VirtualHeight / 2) - this.GapW / 2));


            // the point for rounded Bezier curve
            points.Add(bezPoint);

            return points;
        }

        /// <summary>
        /// Calulates points for the left bottom segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetLeftBottomSegmPoints()
        {
            var points = new PointCollection();

            var startBottomY = (this.VirtualHeight / 2) + this.HorizSegH / 2 + (this.GapW / 2);
            var startBottomY2 = this.VirtualHeight - (this.HorizSegH + this.GapW / 2) - this.startPointThickness;

            var x1 = this.XByAngle((this.VirtualHeight / 2) + this.GapW / 2);
            var x = this.XByAngle(startBottomY);
            var x2 = this.XByAngle(startBottomY2);

            // the bezier point
            Point bezPoint;
            if (this.RoundedCorners)
            {
                var yBezier = this.VirtualHeight - this.startPointThickness - this.VirtualHeight / this.vertRoundCoef;
                var xBezier = this.RoundedCorners ? this.XByAngle(yBezier) : 0;
                bezPoint = new Point(xBezier + this.startPointThickness, yBezier);
            }
            else
            {
                bezPoint = new Point(this.startPointThickness, this.figureStartPointY - this.startPointThickness);
            }

            // three top points, starting from left top point
            points.Add(new Point(x1 + this.startPointThickness, (this.VirtualHeight / 2) + this.GapW / 2));
            points.Add(new Point(x1 + this.VertSegBotPartW + this.startPointThickness, (this.VirtualHeight / 2) + this.GapW / 2));
            points.Add(new Point(x + this.VertSegW + this.startPointThickness, startBottomY));

            // three bottom points, starting from right
            points.Add(new Point(x2 + this.VertSegW + this.startPointThickness, startBottomY2));
            points.Add(new Point(this.VertSegPartW + this.startPointThickness, this.VirtualHeight - this.startPointThickness - (this.HorizSegSmallPartH + this.GapShift())));
            points.Add(new Point(this.startPointThickness, this.figureStartPointY - this.startPointThickness));

            // the point for rounded Bezier curve
            points.Add(bezPoint);

            return points;
        }

        /// <summary>
        /// Calulates points for the right bottom segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetRightBottomSegmPoints()
        {
            PointCollection points = new PointCollection();

            // three top points, starting from the left point
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[3].X, this.VirtualHeight - this.LeftTopSegmPoints[3].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[4].X, this.VirtualHeight - this.LeftTopSegmPoints[4].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[5].X, this.VirtualHeight - this.LeftTopSegmPoints[5].Y));

            // the point for rounded Bezier curve
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[6].X, this.VirtualHeight - this.LeftTopSegmPoints[6].Y));


            // three bottom points, starting from the right point
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[0].X, this.VirtualHeight - this.LeftTopSegmPoints[0].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[1].X, this.VirtualHeight - this.LeftTopSegmPoints[1].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftTopSegmPoints[2].X, this.VirtualHeight - this.LeftTopSegmPoints[2].Y));

            return points;
        }


        /// <summary>
        /// Calulates points  for the right top segment
        /// </summary>
        protected PointCollection GetRightTopSegmPoints()
        {
            PointCollection points = new PointCollection();

            // three top points, starting from the left point
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[3].X, this.VirtualHeight - this.LeftBottomSegmPoints[3].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[4].X, this.VirtualHeight - this.LeftBottomSegmPoints[4].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[5].X, this.VirtualHeight - this.LeftBottomSegmPoints[5].Y));
            
            // the point for rounded Bezier curve
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[6].X, this.VirtualHeight - this.LeftBottomSegmPoints[6].Y));

            // three bottom points, starting from the right point
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[0].X, this.VirtualHeight - this.LeftBottomSegmPoints[0].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[1].X, this.VirtualHeight - this.LeftBottomSegmPoints[1].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftBottomSegmPoints[2].X, this.VirtualHeight - this.LeftBottomSegmPoints[2].Y));
            
            return points;
        }

        /// <summary>
        /// Calculates points collection for the middle segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetMiddleSegmPoints()
        {
            var x = this.XByAngle((this.VirtualHeight / 2) + this.HorizSegH / 2) + (this.VertSegW + this.GapW);
            var x1 = this.XByAngle(this.VirtualHeight / 2) + this.VertSegBotPartW + this.GapW;
            var x2 = this.XByAngle(this.VirtualHeight / 2 - this.HorizSegH / 2) + this.VertSegW + this.GapW;

            PointCollection points = new PointCollection();

            // three left points, starting from the bottom point
            points.Add(new Point(x, (this.VirtualHeight / 2) + this.HorizSegH / 2));
            points.Add(new Point(x1, (this.VirtualHeight / 2)));
            points.Add(new Point(x2, (this.VirtualHeight / 2) - this.HorizSegH / 2));

            // three right points, starting from the top point
            points.Add(new Point(this.VirtualWidth - x, this.RightTopSegmPoints[6].Y + this.GapW / 2));
            points.Add(new Point(this.VirtualWidth - x1, this.VirtualHeight / 2));
            points.Add(new Point(this.VirtualWidth - x2, this.RightBottomSegmPoints[0].Y - this.GapW / 2));           
            return points;
        }


        /// <summary>
        /// Calulates points for the top segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetTopSegmPoints()
        {
            PointCollection points = new PointCollection();
            var topLeftX = this.LeftTopSegmPoints[1].X + this.HorizSegSmallPartW;
            var topRightX = this.RightTopSegmPoints[1].X - this.HorizSegSmallPartW;
            var coefRound = this.RoundedCorners ? this.VirtualWidth / this.horizRoundCoef : 0;

            // three left points, starting from the bottom point
            points.Add(new Point(this.LeftTopSegmPoints[2].X + this.GapW, this.HorizSegH + this.startPointThickness));
            points.Add(new Point(this.LeftTopSegmPoints[1].X + this.GapShift(), this.HorizSegSmallPartH + this.startPointThickness));
            points.Add(new Point(topLeftX, this.startPointThickness));

            // two top Bezier points starting from the left point
            points.Add(new Point(topLeftX + coefRound, this.startPointThickness));
            points.Add(new Point(topRightX - coefRound, this.startPointThickness));

            // three right points, starting from the top left point
            points.Add(new Point(topRightX, this.startPointThickness));
            points.Add(new Point(this.RightTopSegmPoints[1].X - this.GapShift(), this.HorizSegSmallPartH + this.startPointThickness));
            points.Add(new Point(this.RightTopSegmPoints[0].X - this.GapW, this.HorizSegH + this.startPointThickness));

            return points;
        }


        /// <summary>
        /// Calulates points for the bottom segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetBottomSegmPoints()
        {
            PointCollection points = new PointCollection();
            var botLeftX = this.LeftBottomSegmPoints[4].X + this.HorizSegSmallPartW;
            var botRightX = this.RightBottomSegmPoints[5].X - this.HorizSegSmallPartW;
            var coefRound = this.RoundedCorners ? this.VirtualWidth / this.horizRoundCoef : 0;

            // three left points, starting from the bottom point
            points.Add(new Point(botLeftX, this.VirtualHeight - this.startPointThickness));
            points.Add(new Point(this.LeftBottomSegmPoints[4].X + this.GapShift(), this.VirtualHeight - this.HorizSegSmallPartH - this.startPointThickness));
            points.Add(new Point(this.LeftBottomSegmPoints[3].X + this.GapW, this.VirtualHeight - this.HorizSegH - this.startPointThickness));

            // three right points, starting from the top left point
            points.Add(new Point(this.RightBottomSegmPoints[6].X - this.GapW, this.VirtualHeight - this.HorizSegH - this.startPointThickness));
            points.Add(new Point(this.RightBottomSegmPoints[5].X - this.GapShift(), this.VirtualHeight - this.HorizSegSmallPartH - this.startPointThickness));
            points.Add(new Point(botRightX, this.VirtualHeight - this.startPointThickness));

            // two bottom Bezier points starting from the right point
            points.Add(new Point(botRightX - coefRound, this.VirtualHeight - this.startPointThickness));
            points.Add(new Point(botLeftX + coefRound, this.VirtualHeight - this.startPointThickness));

            return points;
        }


        #endregion

        #region Segments' geometries

        /// <summary>
        /// Right top segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry RightTopSegement()
        {

            this.RightTopSegmPoints = this.GetRightTopSegmPoints();
            Point startPoint = this.RightTopSegmPoints[0];
            LineSegment line0 = new LineSegment(this.RightTopSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.RightTopSegmPoints[1], true);
            LineSegment line4 = new LineSegment(this.RightTopSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.RightTopSegmPoints[5], true);
            LineSegment line6 = new LineSegment(this.RightTopSegmPoints[6], true);

            // The Bezier curve for rounded corners
            var pointsBezier = new PointCollection
            {
                this.RightTopSegmPoints[1],
                this.RightTopSegmPoints[2],
                this.RightTopSegmPoints[3]
            };

            PolyBezierSegment bez = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezier)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(line0);
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(bez);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(line5);
            pathFigure.Segments.Add(line6);

            return pathGeometry;
        }


        /// <summary>
        /// Middle segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry MiddleSegement()
        {
            this.MiddleSegmPoints = this.GetMiddleSegmPoints();

            Point startPoint = this.MiddleSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.MiddleSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }


        /// <summary>
        /// Right bottom segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry RightBottomSegement()
        {

            this.RightBottomSegmPoints = this.GetRightBottomSegmPoints();
            Point startPoint = this.RightBottomSegmPoints[0];
            LineSegment line0 = new LineSegment(this.RightBottomSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.RightBottomSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.RightBottomSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.RightBottomSegmPoints[3], true);
            LineSegment line6 = new LineSegment(this.RightBottomSegmPoints[6], true);

            // The Bezier curve for rounded corners
            var pointsBezier = new PointCollection
            {
                this.RightBottomSegmPoints[3],
                this.RightBottomSegmPoints[4],
                this.RightBottomSegmPoints[5]
            };

            PolyBezierSegment bez = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezier)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(line0);
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(line2);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(bez);

            pathFigure.Segments.Add(line6);

            return pathGeometry;
        }


        /// <summary>
        /// Top segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry TopSegement()
        {
            this.TopSegmPoints = this.GetTopSegmPoints();
            Point startPoint = this.TopSegmPoints[0];
            LineSegment line0 = new LineSegment(this.TopSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.TopSegmPoints[1], true);
            LineSegment line3 = new LineSegment(this.TopSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.TopSegmPoints[4], true);
            LineSegment line6 = new LineSegment(this.TopSegmPoints[6], true);
            LineSegment line7 = new LineSegment(this.TopSegmPoints[7], true);

            // The left Bezier curve for rounded corners
            var pointsBezierLeft= new PointCollection
            {
                this.TopSegmPoints[1], this.TopSegmPoints[2], this.TopSegmPoints[3]
            };

            PolyBezierSegment bezLeft = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezierLeft)
            };


            // The right Bezier curve for rounded corners
            var pointsBezierRight= new PointCollection
            {
                this.TopSegmPoints[4], this.TopSegmPoints[5], this.TopSegmPoints[6]
            };

            PolyBezierSegment bezRight = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezierRight)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(line0);
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(bezLeft);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(bezRight);
            pathFigure.Segments.Add(line6);
            pathFigure.Segments.Add(line7);

            return pathGeometry;
        }



        /// <summary>
        /// Left top segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry LeftTopSegement()
        {
            this.LeftTopSegmPoints = this.GetLeftTopSegmPoints();
            Point startPoint = this.LeftTopSegmPoints[6];
            LineSegment line0 = new LineSegment(this.LeftTopSegmPoints[6], true);
            LineSegment line1 = new LineSegment(this.LeftTopSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.LeftTopSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.LeftTopSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.LeftTopSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.LeftTopSegmPoints[5], true);

            // The Bezier curve for rounded corners
            var pointsBezier = new PointCollection
            {
                this.LeftTopSegmPoints[6],
                this.LeftTopSegmPoints[0],
                this.LeftTopSegmPoints[1]
            };

            PolyBezierSegment bez = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezier)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(bez);
            pathFigure.Segments.Add(line2);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(line4); 
            pathFigure.Segments.Add(line5);
            

            return pathGeometry;
        }


        

        /// <summary>
        /// Left Bottom segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry LeftBottomSegement()
        {
            this.LeftBottomSegmPoints = this.GetLeftBottomSegmPoints();
            Point startPoint = this.LeftBottomSegmPoints[0];
            LineSegment line0 = new LineSegment(this.LeftBottomSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.LeftBottomSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.LeftBottomSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.LeftBottomSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.LeftBottomSegmPoints[4], true);

            // The Bezier curve for rounded corners
            var pointsBezier = new PointCollection
            {
                this.LeftBottomSegmPoints[4],
                this.LeftBottomSegmPoints[5],
                this.LeftBottomSegmPoints[6]
            };

            PolyBezierSegment bez = new PolyBezierSegment  
            {
                Points = new PointCollection(pointsBezier)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            pathFigure.Segments.Add(line0);
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(line2);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(bez);
            
            return pathGeometry;
        }




        /// <summary>
        /// Bottom segment drawing
        /// </summary>
        protected PathGeometry BottomSegement()
        {
            this.BottomSegmPoints = this.GetBottomSegmPoints();
            Point startPoint = this.BottomSegmPoints[1];

            LineSegment line0 = new LineSegment(this.BottomSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.BottomSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.BottomSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.BottomSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.BottomSegmPoints[4], true);
            LineSegment line6 = new LineSegment(this.BottomSegmPoints[6], true);
            LineSegment line7 = new LineSegment(this.BottomSegmPoints[7], true);


            // The right Bezier curve for rounded corners
            var pointsBezierRight = new PointCollection
            {
                this.BottomSegmPoints[4], this.BottomSegmPoints[5], this.BottomSegmPoints[6]
            };

            PolyBezierSegment bezRight = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezierRight)
            };

            // The left Bezier curve for rounded corners
            var pointsBezierLeft = new PointCollection
            {
                this.BottomSegmPoints[7], this.BottomSegmPoints[0], this.BottomSegmPoints[1]
            };

            PolyBezierSegment bezLeft = new PolyBezierSegment
            {
                Points = new PointCollection(pointsBezierLeft)
            };

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = startPoint;
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);

            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(line2);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(bezRight);
            pathFigure.Segments.Add(line6);
            pathFigure.Segments.Add(line7);
            pathFigure.Segments.Add(bezLeft);

            return pathGeometry;
        }

        #endregion

    }
}
