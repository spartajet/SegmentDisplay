using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SegmentDisplay.Segments
{
    public class SixteenSegments : SevenSegments
    {
        /// <summary>
        /// The width of diagonal segment
        /// </summary>
        protected double DiagSegW;

        #region Protected variables

        /// <summary>
        /// Points collection for the left top horiz. segment
        /// </summary>
        protected PointCollection LeftTopHorizSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right top horiz. segment
        /// </summary>
        protected PointCollection RightTopHorizSegmPoints { get; private set; }


        /// <summary>
        /// Points collection for the left bottom horiz. segment
        /// </summary>
        protected PointCollection LeftBottomHorizSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right bottom horiz. segment
        /// </summary>
        protected PointCollection RightBottomHorizSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the left middle segment
        /// </summary>
        protected PointCollection LeftMiddleSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right middle segment
        /// </summary>
        protected PointCollection RightMiddleSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the top vertical segment
        /// </summary>
        protected PointCollection TopVerticalSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the bottom vertical segment
        /// </summary>
        protected PointCollection BottomVerticalSegmPoints { get; private set; }


        /// <summary>
        /// Points collection for the left bottom diagonal segment
        /// </summary>
        protected PointCollection BottomLeftDiagSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the left top diagonal segment
        /// </summary>
        protected PointCollection TopLeftDiagSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right top diagonal segment
        /// </summary>
        protected PointCollection TopRightDiagSegmPoints { get; private set; }

        /// <summary>
        /// Points collection for the right bottom diagonal segment
        /// </summary>
        protected PointCollection BottomRightDiagSegmPoints { get; private set; }

        #endregion

        static SixteenSegments()
        {
            VertSegDividerProperty.OverrideMetadata(
                    typeof(SixteenSegments),
                    new FrameworkPropertyMetadata(defVertDividerSixteen));


            HorizSegDividerProperty.OverrideMetadata(
                typeof(SixteenSegments),
                new FrameworkPropertyMetadata(defHorizDividerSixteen));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            
            base.OnRender(drawingContext);
            
        }

        /// <summary>
        /// Selects segments depending on the value 
        /// </summary>
        protected override void ValueSegmentsSelection()
        {
            char tempValue = this.Value.ToCharArray().Count() > 0 ? this.Value[0] : ' ';

            if (tempValue == '0') this.SelectSegments(0, 1, 2, 3, 4, 5, 6, 7);
            else if (tempValue == '1') this.SelectSegments(10, 2, 3);
            else if (tempValue == '2') this.SelectSegments(0, 1, 2, 11, 12, 6 , 5, 4);
            else if (tempValue == '3') this.SelectSegments(0, 1, 2, 3, 4, 5, 11, 12);
            else if (tempValue == '4') this.SelectSegments(7, 11, 12, 2, 3);
            else if (tempValue == '5') this.SelectSegments(0, 1 , 12 , 11, 7 , 4, 5, 3);
            else if (tempValue == '6') this.SelectSegments(15, 16, 5, 6, 7, 8, 4, 17, 18, 2);
            else if (tempValue == '7') this.SelectSegments(0, 1, 2, 3);
            else if (tempValue == '8') this.SelectSegments(0, 1, 2, 3, 4, 5, 6, 7, 11, 12);
            else if (tempValue == '9') this.SelectSegments(0, 1, 2, 3, 4, 5, 7, 11, 12);
            else if (tempValue == 'A') this.SelectSegments(0, 1, 2, 3, 6, 7, 11, 12);
            else if (tempValue == 'B') this.SelectSegments(0, 1, 2, 3, 4, 5, 9, 14, 12);
            else if (tempValue == 'C') this.SelectSegments(0, 1, 4, 5, 6, 7);
            else if (tempValue == 'D') this.SelectSegments(0, 1, 2, 3, 4, 5, 9, 14);
            else if (tempValue == 'E') this.SelectSegments(0, 1, 4, 5, 6, 7 , 11, 12);
            else if (tempValue == 'F') this.SelectSegments(0 ,1 ,6, 7, 11, 12);
            else if (tempValue == 'G') this.SelectSegments(0, 1, 3, 4, 5, 6, 7, 12);
            else if (tempValue == 'H') this.SelectSegments(2 ,3, 6, 7 ,11, 12);
            else if (tempValue == 'I') this.SelectSegments(0, 1, 4, 5, 9 ,14);
            else if (tempValue == 'J') this.SelectSegments(2 ,3 ,4 ,5 ,6);
            else if (tempValue == 'K') this.SelectSegments(6, 7, 11, 10, 15);
            else if (tempValue == 'L') this.SelectSegments(4, 5, 6, 7);
            else if (tempValue == 'M') this.SelectSegments(2, 3, 6, 7, 8 , 10);
            else if (tempValue == 'N') this.SelectSegments(2, 3, 6, 7, 8, 15);
            else if (tempValue == 'O') this.SelectSegments(0, 1, 2, 3, 4, 5, 6, 7);
            else if (tempValue == 'P') this.SelectSegments(6, 7, 0 ,1, 2, 11, 12);
            else if (tempValue == 'Q') this.SelectSegments(0, 1, 2, 3, 4, 5, 6, 7, 15);
            else if (tempValue == 'R') this.SelectSegments(6, 7 , 0, 1, 2, 11, 12, 15 );
            else if (tempValue == 'S') this.SelectSegments(0, 1, 12, 11, 7, 4, 5, 3);
            else if (tempValue == 'T') this.SelectSegments(0, 1, 9, 14);
            else if (tempValue == 'U') this.SelectSegments(2, 3, 4, 5, 6, 7);
            else if (tempValue == 'V') this.SelectSegments(6, 7, 13, 10);
            else if (tempValue == 'W') this.SelectSegments(6 , 7,  2, 3, 13, 15);
            else if (tempValue == 'X') this.SelectSegments(8, 15, 13, 10);
            else if (tempValue == 'Y') this.SelectSegments(7, 11, 12, 14, 2);
            else if (tempValue == 'Z') this.SelectSegments(0 , 1, 10, 13, 4, 5);
            else if (tempValue == '-') this.SelectSegments(11, 12);
        }

        protected override void AssignSegments()
        {
            this.GeometryFigures = new List<GeometryWithSegm>();
            this.DiagSegW = this.VertSegBotPartW;

            // Assigns a segment number to required path geometry. Order is important!
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftBottomSegement(), (int)SixteenSegmentsNumbers.LeftVertBottom));
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftTopSegement(), (int)SixteenSegmentsNumbers.LeftVertTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightTopSegement(), (int)SixteenSegmentsNumbers.RightVertTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightBottomSegement(), (int)SixteenSegmentsNumbers.RightVertBottom));

            this.TopSegmPoints = this.GetTopSegmPoints();
            this.BottomSegmPoints = this.GetBottomSegmPoints();
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftTopHorizSegement(), (int)SixteenSegmentsNumbers.LeftHorizTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightTopHorizSegement(), (int)SixteenSegmentsNumbers.RightHorizTop));
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftBottomHorizSegement(), (int)SixteenSegmentsNumbers.LeftHorizBottom));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightBottomHorizSegement(), (int)SixteenSegmentsNumbers.RightHorizBottom));

            this.MiddleSegmPoints = this.GetMiddleSegmPoints();
            this.GeometryFigures.Add(new GeometryWithSegm(this.LeftMiddleSegement(), (int)SixteenSegmentsNumbers.LeftMiddle));
            this.GeometryFigures.Add(new GeometryWithSegm(this.RightMiddleSegement(), (int)SixteenSegmentsNumbers.RightMiddle));
            this.GeometryFigures.Add(new GeometryWithSegm(this.TopVerticalSegment(), (int)SixteenSegmentsNumbers.TopVertical));
            this.GeometryFigures.Add(new GeometryWithSegm(this.BottomVerticalSegment(), (int)SixteenSegmentsNumbers.BottomVertical));
            this.GeometryFigures.Add(new GeometryWithSegm(this.BottomLeftDiagSegment(), (int)SixteenSegmentsNumbers.LeftBottomDiagonal));
            this.GeometryFigures.Add(new GeometryWithSegm(this.TopLeftDiagSegment(), (int)SixteenSegmentsNumbers.LeftTopDiagonal));
            this.GeometryFigures.Add(new GeometryWithSegm(this.TopRightDiagSegment(), (int)SixteenSegmentsNumbers.RightTopDiagonal));
            this.GeometryFigures.Add(new GeometryWithSegm(this.BottomRightDiagSegment(), (int)SixteenSegmentsNumbers.RightBottomDiagonal));

        }


        #region Points locations


        /// <summary>
        /// Calulates points for the left top horiz. segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetLeftTopHorizSegmPoints()
        {
            PointCollection points = new PointCollection();
            var x1 = this.XByAngle(0) + this.VertSegW + this.GapW;
            var x2 = this.VirtualWidth - (this.XByAngle(this.VirtualHeight) + this.VertSegW + this.GapW);

            var topW = x2 - x1;
            var botW = this.TopSegmPoints[7].X - this.TopSegmPoints[0].X;

            // three left points, starting from the bottom point
            points.Add(this.TopSegmPoints[0]);
            points.Add(this.TopSegmPoints[1]);
            points.Add(this.TopSegmPoints[2]);

            // the Bezier point 
            points.Add(this.TopSegmPoints[3]);

            // two right points, starting from the top point
            points.Add(new Point(x1 + (topW / 2 - this.GapW / 2),
                this.TopSegmPoints[2].Y));
            points.Add(new Point(this.TopSegmPoints[0].X + (botW / 2 - this.GapW / 2),
                this.TopSegmPoints[7].Y));


            return points;
        }

        /// <summary>
        /// Calulates points for the right top horiz. segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetRightTopHorizSegmPoints()
        {
            PointCollection points = new PointCollection();
            var x1 = this.XByAngle(0) + this.VertSegW + this.GapW;
            var x2 = this.VirtualWidth - (this.XByAngle(this.VirtualHeight) + this.VertSegW + this.GapW);

            var topW = x2 - x1;
            var botW = this.TopSegmPoints[7].X - this.TopSegmPoints[0].X;

            // two left points, starting from the bottom point
            points.Add(new Point(this.TopSegmPoints[0].X + (botW / 2 + this.GapW / 2),
                this.TopSegmPoints[7].Y));
            points.Add(new Point(x1 + (topW / 2 + this.GapW / 2),
                this.TopSegmPoints[2].Y));

            // the Bezier point 
            points.Add(this.TopSegmPoints[4]);

            // three right points, starting from the top point
            points.Add(this.TopSegmPoints[5]);
            points.Add(this.TopSegmPoints[6]);
            points.Add(this.TopSegmPoints[7]);

            return points;
        }

        /// <summary>
        /// Calulates points for the left bottom horiz. segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetLeftBottomHorizSegmPoints()
        {
            PointCollection points = new PointCollection();
            // three left points, starting from the bottom point
            points.Add(this.BottomSegmPoints[0]);
            points.Add(this.BottomSegmPoints[1]);
            points.Add(this.BottomSegmPoints[2]);

            // two right points, starting from the top point
            points.Add(new Point(this.VirtualWidth - this.RightTopHorizSegmPoints[0].X,
                this.BottomSegmPoints[2].Y));
            points.Add(new Point(this.VirtualWidth - this.RightTopHorizSegmPoints[1].X,
                this.BottomSegmPoints[0].Y));


            // the Bezier point 
            points.Add(this.BottomSegmPoints[7]);

            return points;
        }

        /// <summary>
        /// Calulates points for the right bottom horiz. segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetRightBottomHorizSegmPoints()
        {
            PointCollection points = new PointCollection();
            // two left points, starting from the bottom point
            points.Add(new Point(this.VirtualWidth - this.LeftTopHorizSegmPoints[4].X,
                this.BottomSegmPoints[0].Y));
            points.Add(new Point(this.VirtualWidth - this.LeftTopHorizSegmPoints[5].X,
                this.BottomSegmPoints[2].Y));

            // three right points, starting from the top point
            points.Add(this.BottomSegmPoints[3]);
            points.Add(this.BottomSegmPoints[4]);
            points.Add(this.BottomSegmPoints[5]);

            // the Bezier point 
            points.Add(this.BottomSegmPoints[6]);

            return points;
        }

        /// <summary>
        /// Calulates points for the left middle segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetLeftMiddleSegmPoints()
        {
            PointCollection points = new PointCollection();
            var topW = this.MiddleSegmPoints[3].X - this.MiddleSegmPoints[2].X;
            var botW = this.MiddleSegmPoints[5].X - this.MiddleSegmPoints[0].X;

            // three left points, starting from the bottom point
            points.Add(this.MiddleSegmPoints[0]);
            points.Add(this.MiddleSegmPoints[1]);
            points.Add(this.MiddleSegmPoints[2]);

            // two right points, starting from the top point
            points.Add(new Point(this.MiddleSegmPoints[2].X + (topW / 2 - this.GapW / 2),
                this.MiddleSegmPoints[2].Y));
            points.Add(new Point(this.MiddleSegmPoints[0].X + (botW / 2 - this.GapW / 2),
                this.MiddleSegmPoints[5].Y));


            return points;
        }

        /// <summary>
        /// Calulates points for the right middle segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetRightMiddleSegmPoints()
        {
            PointCollection points = new PointCollection();
            var topW = this.MiddleSegmPoints[3].X - this.MiddleSegmPoints[2].X;
            var botW = this.MiddleSegmPoints[5].X - this.MiddleSegmPoints[0].X;

            // two left points, starting from the bottom point
            points.Add(new Point(this.MiddleSegmPoints[0].X + (botW / 2 + this.GapW / 2),
                this.MiddleSegmPoints[5].Y));
            points.Add(new Point(this.MiddleSegmPoints[2].X + (topW / 2 + this.GapW / 2),
                this.MiddleSegmPoints[2].Y));

            // three right points, starting from the top point
            points.Add(this.MiddleSegmPoints[3]);
            points.Add(this.MiddleSegmPoints[4]);
            points.Add(this.MiddleSegmPoints[5]);

            return points;
        }

        /// <summary>
        /// Points collection for the top vertical segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetTopVerticalSegmPoints()
        {
            PointCollection points = new PointCollection();
            var w = this.RightTopSegmPoints[0].X - this.LeftTopSegmPoints[2].X;
            var botY= this.MiddleSegmPoints[2].Y - this.GapW - this.HorizSegH;
            //var segmH = figureStartPointY - botY;
            var divider = this.VertSegW / 2.5;
            var xMid = this.XByAngle(botY) + this.VertSegW;

            // two top points, starting from the left point
            points.Add(new Point(this.LeftTopSegmPoints[2].X + (w / 2 - divider),
                this.HorizSegH + this.GapW));
            points.Add(new Point(this.LeftTopSegmPoints[2].X + (w / 2 + divider),
                this.HorizSegH + this.GapW));


            // four bottom points, starting from the right point
            points.Add(new Point(xMid + (w / 2 + divider), botY));
            points.Add(new Point(this.LeftTopSegmPoints[3].X + (w / 2 + divider / 2),
                  this.MiddleSegmPoints[2].Y - this.GapW));
            points.Add(new Point(this.LeftTopSegmPoints[3].X + (w / 2 - divider / 2),
                 this.MiddleSegmPoints[2].Y - this.GapW));
            points.Add(new Point(xMid + (w / 2 - divider), botY));

            return points;
        }


        /// <summary>
        /// Points collection for the bottom vertical segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetBottomVerticalSegmPoints()
        {
            PointCollection points = new PointCollection();
            var w = this.RightBottomSegmPoints[0].X - this.LeftBottomSegmPoints[2].X;
            var botY = this.MiddleSegmPoints[0].Y + this.GapW + this.HorizSegH;
            var xMid = this.XByAngle(botY) + this.VertSegW;
            var divider = this.VertSegW / 2.5;

            // four top points, starting from the left point
            points.Add(new Point(xMid + (w/2 - divider), botY));
            points.Add(new Point(this.LeftBottomSegmPoints[2].X + (w / 2 - divider / 2),
                this.MiddleSegmPoints[0].Y + this.GapW));
            points.Add(new Point(this.LeftBottomSegmPoints[2].X + (w / 2 + divider / 2),
                this.MiddleSegmPoints[0].Y + this.GapW));
            points.Add(new Point(xMid + (w/2 + divider), botY));


            // two bottom points, starting from the right point
            points.Add(new Point(this.LeftBottomSegmPoints[3].X + (w / 2 + divider),
                this.BottomSegmPoints[2].Y - this.GapW));

            points.Add(new Point(this.LeftBottomSegmPoints[3].X + (w / 2 - divider),
                this.BottomSegmPoints[2].Y - this.GapW));

            return points;
        }


        /// <summary>
        /// Points collection for the left bottom diagonal segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetBottomLeftDiagSegmPoints()
        {
            PointCollection points = new PointCollection();

            var yBot1 = this.BottomSegmPoints[2].Y - this.GapW;
            var xBot1 = this.XByAngle(yBot1) + this.VertSegW; 

            var yBot2 = yBot1 - this.HorizSegH/2;
            var xBot2 = this.XByAngle(yBot2) + this.VertSegW; 

            // three top points, starting from the left point
            points.Add(new Point(this.BottomVerticalSegmPoints[1].X - this.GapW - this.DiagSegW, 
                this.BottomVerticalSegmPoints[1].Y));
            points.Add(new Point(this.BottomVerticalSegmPoints[1].X - this.GapW,
                this.BottomVerticalSegmPoints[1].Y));
            points.Add(new Point(this.BottomVerticalSegmPoints[0].X - this.GapW,
                this.BottomVerticalSegmPoints[0].Y));

            // three bottom points, starting from the right point
            points.Add(new Point(xBot1 + this.GapW + this.DiagSegW, yBot1));
            points.Add(new Point(xBot1 + this.GapW, yBot1));
            points.Add(new Point(xBot2 + this.GapW, yBot2));

            return points;
        }

        /// <summary>
        /// Points collection for the left top diagonal segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetTopLeftDiagSegmPoints()
        {
            PointCollection points = new PointCollection();
            var y1 = this.HorizSegH + this.GapW + this.HorizSegH/2;
            var x1 = this.XByAngle(y1) + this.VertSegW;

            var y2 = this.HorizSegH + this.GapW;
            var x2 = this.XByAngle(y2) + this.VertSegW;

            // three top points, starting from the left point
            points.Add(new Point(x1 + this.GapW,y1));
            points.Add(new Point(x2 + this.GapW, y2));
            points.Add(new Point(x2 + this.GapW + this.DiagSegW, y2));

            // three bottom points, starting from the right point
            points.Add(new Point(this.TopVerticalSegmPoints[5].X - this.GapW,
                this.TopVerticalSegmPoints[5].Y));
            points.Add(new Point(this.TopVerticalSegmPoints[4].X - this.GapW,
                this.TopVerticalSegmPoints[4].Y));
            points.Add(new Point(this.TopVerticalSegmPoints[4].X - this.GapW - this.DiagSegW,
                this.TopVerticalSegmPoints[4].Y));

            return points;
        }

        /// <summary>
        /// Points collection for the right top diagonal segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetTopRightDiagSegmPoints()
        {
            PointCollection points = new PointCollection();

            // three top points, starting from the left point
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[3].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[3].Y));
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[4].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[4].Y));
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[5].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[5].Y));

            // three bottom points, starting from the right point
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[0].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[0].Y));
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[1].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[1].Y));
            points.Add(new Point(this.VirtualWidth - this.BottomLeftDiagSegmPoints[2].X, this.VirtualHeight - this.BottomLeftDiagSegmPoints[2].Y));

            return points;
        }

        /// <summary>
        /// Points collection for the right bottom diagonal segment
        /// </summary>
        /// <returns></returns>
        protected PointCollection GetBottomRightDiagSegmPoints()
        {
            PointCollection points = new PointCollection();

            // three top points, starting from the left point
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[3].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[3].Y));
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[4].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[4].Y));
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[5].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[5].Y));

            // three bottom points, starting from the right point
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[0].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[0].Y));
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[1].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[1].Y));
            points.Add(new Point(this.VirtualWidth - this.TopLeftDiagSegmPoints[2].X, this.VirtualHeight - this.TopLeftDiagSegmPoints[2].Y));

            return points;
        }


        #endregion

        #region Segments drawing


        /// <summary>
        /// Left top horiz. segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry LeftTopHorizSegement()
        {

            this.LeftTopHorizSegmPoints = this.GetLeftTopHorizSegmPoints();
            Point startPoint = this.LeftTopHorizSegmPoints[0];
            LineSegment line0 = new LineSegment(this.LeftTopHorizSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.LeftTopHorizSegmPoints[1], true);
            LineSegment line3 = new LineSegment(this.LeftTopHorizSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.LeftTopHorizSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.LeftTopHorizSegmPoints[5], true);

            // The Bezier curve for rounded corners
            var pointsBezierLeft = new PointCollection
            {
                this.LeftTopHorizSegmPoints[1], this.LeftTopHorizSegmPoints[2], this.LeftTopHorizSegmPoints[3]
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
            pathFigure.Segments.Add(line0);
            pathFigure.Segments.Add(line1);
            pathFigure.Segments.Add(bezLeft);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(line5);

            return pathGeometry;
        }

        /// <summary>
        /// Right top horiz. segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry RightTopHorizSegement()
        {
            this.RightTopHorizSegmPoints = this.GetRightTopHorizSegmPoints();
            Point startPoint = this.RightTopHorizSegmPoints[0];
            LineSegment line0 = new LineSegment(this.RightTopHorizSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.RightTopHorizSegmPoints[1], true); 
            LineSegment line2 = new LineSegment(this.RightTopHorizSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.RightTopHorizSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.RightTopHorizSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.RightTopHorizSegmPoints[5], true);

            // The Bezier curve for rounded corners
            var pointsBezier = new PointCollection
            {
                this.RightTopHorizSegmPoints[2], this.RightTopHorizSegmPoints[3], this.RightTopHorizSegmPoints[4]
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
            pathFigure.Segments.Add(bez);
            pathFigure.Segments.Add(line4);
            pathFigure.Segments.Add(line5);

            return pathGeometry;
        }

        /// <summary>
        /// Left bottom horiz.segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry LeftBottomHorizSegement()
        {
            this.LeftBottomHorizSegmPoints = this.GetLeftBottomHorizSegmPoints();
            Point startPoint = this.LeftBottomHorizSegmPoints[1];

            LineSegment line0 = new LineSegment(this.LeftBottomHorizSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.LeftBottomHorizSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.LeftBottomHorizSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.LeftBottomHorizSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.LeftBottomHorizSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.LeftBottomHorizSegmPoints[5], true);


            // The left Bezier curve for rounded corners
            var pointsBezierLeft = new PointCollection
            {
                this.LeftBottomHorizSegmPoints[5], this.LeftBottomHorizSegmPoints[0], this.LeftBottomHorizSegmPoints[1]
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
            pathFigure.Segments.Add(line5);
            pathFigure.Segments.Add(bezLeft);

            return pathGeometry;
        }

        /// <summary>
        /// Right bottom horiz.segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry RightBottomHorizSegement()
        {
            this.RightBottomHorizSegmPoints = this.GetRightBottomHorizSegmPoints();
            Point startPoint = this.RightBottomHorizSegmPoints[0];

            LineSegment line0 = new LineSegment(this.RightBottomHorizSegmPoints[0], true);
            LineSegment line1 = new LineSegment(this.RightBottomHorizSegmPoints[1], true);
            LineSegment line2 = new LineSegment(this.RightBottomHorizSegmPoints[2], true);
            LineSegment line3 = new LineSegment(this.RightBottomHorizSegmPoints[3], true);
            LineSegment line4 = new LineSegment(this.RightBottomHorizSegmPoints[4], true);
            LineSegment line5 = new LineSegment(this.RightBottomHorizSegmPoints[5], true);


            // The right Bezier curve for rounded corners
            var pointsBezierRight = new PointCollection
            {
                this.RightBottomHorizSegmPoints[3], this.RightBottomHorizSegmPoints[4], this.RightBottomHorizSegmPoints[5]
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
            pathFigure.Segments.Add(line2);
            pathFigure.Segments.Add(line3);
            pathFigure.Segments.Add(bezRight);
            pathFigure.Segments.Add(line5);



            return pathGeometry;
        }

        /// <summary>
        /// Left middle segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry LeftMiddleSegement()
        {
            this.LeftMiddleSegmPoints = this.GetLeftMiddleSegmPoints();
            Point startPoint = this.LeftMiddleSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.LeftMiddleSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// Middle segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry RightMiddleSegement()
        {
            this.RightMiddleSegmPoints = this.GetRightMiddleSegmPoints();

            Point startPoint = this.RightMiddleSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.RightMiddleSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }


        /// <summary>
        /// The top vertical segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry TopVerticalSegment()
        {
            this.TopVerticalSegmPoints = this.GetTopVerticalSegmPoints();
            Point startPoint = this.TopVerticalSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.TopVerticalSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// The bottom vertical segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry BottomVerticalSegment()
        {
            this.BottomVerticalSegmPoints = this.GetBottomVerticalSegmPoints();
            Point startPoint = this.BottomVerticalSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.BottomVerticalSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }


        /// <summary>
        /// The left bottom diagonal segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry BottomLeftDiagSegment()
        {
            this.BottomLeftDiagSegmPoints = this.GetBottomLeftDiagSegmPoints();
            Point startPoint = this.BottomLeftDiagSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.BottomLeftDiagSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// The left top diagonal segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry TopLeftDiagSegment()
        {
            this.TopLeftDiagSegmPoints = this.GetTopLeftDiagSegmPoints();
            Point startPoint = this.TopLeftDiagSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.TopLeftDiagSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// The right top diagonal segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry TopRightDiagSegment()
        {
            this.TopRightDiagSegmPoints = this.GetTopRightDiagSegmPoints();
            Point startPoint = this.TopRightDiagSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.TopRightDiagSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }

        /// <summary>
        /// The right bottom diagonal segment drawing
        /// </summary>
        /// <returns></returns>
        protected PathGeometry BottomRightDiagSegment()
        {
            this.BottomRightDiagSegmPoints = this.GetBottomRightDiagSegmPoints();
            Point startPoint = this.BottomRightDiagSegmPoints[0];
            PolyLineSegment segment = new PolyLineSegment { Points = this.BottomRightDiagSegmPoints };
            return this.SegmentPathGeometry(startPoint, segment);
        }



        #endregion


    }
}
