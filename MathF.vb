''' <summary>
''' Provides methods for performing common game logic calculations along with Single-typed versions of System.Math trig operations.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class Mathf
    Public Const ZERO As Single = 0.0F
    Public Const ONE As Single = 1.0F
    Public Const TWO As Single = 2.0F

    Public Const HALF_CIRCLE As Single = 180.0F
    Public Const FULL_CIRCLE As Single = 360.0F
    Public Const RIGHT_ANGLE As Single = 90.0F

    Public Const PI As Single = CSng(System.Math.PI)
    Public Const TWO_PI As Single = PI * TWO
    Public Const PI_SQUARED As Single = PI * PI

    Protected Sub New()
    End Sub

    ''' <summary>
    ''' System.Math method typed for Single values
    ''' </summary>
    Public Shared Function Atan(ByVal value As Single) As Single
        Return CSng(System.Math.Atan(CDbl(value)))
    End Function

    ''' <summary>
    ''' System.Math method typed for Single values
    ''' </summary>
    Public Shared Function Atan2(ByVal y As Single, ByVal x As Single) As Single
        Return CSng(System.Math.Atan2(CDbl(y), CDbl(x)))
    End Function

    ''' <summary>
    ''' Determines if a value falls within a range.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <param name="min">The minimum acceptable value.</param>
    ''' <param name="max">The maximum acceptable value.</param>
    ''' <returns>True if the value is in range, otherwise false.</returns>
    Public Shared Function Between(ByVal value As Single, ByVal min As Single, ByVal max As Single) As Boolean
        Return value >= min AndAlso value <= max
    End Function

    ''' <summary>
    ''' Determines if a value falls within a range.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <param name="min">The minimum acceptable value.</param>
    ''' <param name="max">The maximum acceptable value.</param>
    ''' <returns>True if the value is in range, otherwise false.</returns>
    Public Shared Function Between(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Boolean
        Return value >= min AndAlso value <= max
    End Function

    ''' <summary>
    ''' Ensures that a value falls within an acceptable range.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <param name="min">The minimum acceptable value.</param>
    ''' <param name="max">The maximum acceptable value.</param>
    ''' <returns>True if the value is in range, otherwise false.</returns>
    Public Shared Function Clamp(ByVal value As Single, ByVal min As Single, ByVal max As Single) As Single
        Return Mathf.Max(min, Mathf.Min(value, max))
    End Function

    ''' <summary>
    ''' Ensures that a value falls within an acceptable range.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <param name="min">The minimum acceptable value.</param>
    ''' <param name="max">The maximum acceptable value.</param>
    ''' <returns>True if the value is in range, otherwise false.</returns>
    Public Shared Function Clamp(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Integer
        Return CInt(Mathf.Max(min, Mathf.Min(value, max)))
    End Function

    ''' <summary>
    ''' Gets the collision plane for a given normal plane.
    ''' </summary>
    ''' <param name="normalPlane">The normal plane.</param>
    ''' <returns>The collision plane.</returns>
    Public Shared Function CollisionPlane(normalPlane As PointF) As PointF
        Return New PointF(-normalPlane.Y, normalPlane.X)
    End Function

    ''' <summary>
    ''' System.Math method typed for Single values
    ''' </summary>
    Public Shared Function Cos(ByVal radians As Single) As Single
        Return CSng(System.Math.Cos(CDbl(radians)))
    End Function

    ''' <summary>
    ''' Converts degrees to radians.
    ''' </summary>
    ''' <param name="degrees">The value to convert in degrees.</param>
    ''' <returns>The value in radians.</returns>
    Public Shared Function DegreesToRadians(ByVal degrees As Single) As Single
        Return degrees * (PI / HALF_CIRCLE)
    End Function

    ''' <summary>
    ''' Calculates the distance between two points.
    ''' </summary>
    ''' <param name="a">The first point.</param>
    ''' <param name="b">The second point.</param>
    ''' <returns>The distance between points a and b.</returns>
    Public Shared Function Distance(a As PointF, b As PointF) As Single
        Return CSng(Math.Sqrt(Math.Pow(b.X - a.X, 2.0) + Math.Pow(b.Y - a.Y, 2.0)))
    End Function

    ''' <summary>
    ''' Gets the dot product of two vectors.
    ''' </summary>
    ''' <param name="vector1">The first vector.</param>
    ''' <param name="vector2">The second vector.</param>
    ''' <returns>The dot product of vectors one and two.</returns>
    Public Shared Function VectorDotProduct(vector1 As PointF, vector2 As PointF) As Single
        Return vector1.X * vector2.X + vector1.Y * vector2.Y
    End Function

    ''' <summary>
    ''' Determines if two values are equal within a given tolerance.
    ''' </summary>
    ''' <param name="source">The first number.</param>
    ''' <param name="target">The second number.</param>
    ''' <param name="tolerance">The tolerance for equivelancy.</param>
    ''' <returns>True if the values are equal within the tolerance, otherwise false.</returns>
    Public Shared Function EqualWithin(ByVal source As Single, ByVal target As Single, ByVal tolerance As Single) As Boolean
        Return source - target <= tolerance
    End Function

    ''' <summary>
    ''' Determines if two values are equal within a given tolerance.
    ''' </summary>
    ''' <param name="source">The first number.</param>
    ''' <param name="target">The second number.</param>
    ''' <param name="tolerance">The tolerance for equivelancy.</param>
    ''' <returns>True if the values are equal within the tolerance, otherwise false.</returns>
    Public Shared Function EqualWithin(ByVal source As Integer, ByVal target As Integer, ByVal tolerance As Integer) As Boolean
        Return source - target <= tolerance
    End Function

    ''' <summary>
    ''' Gets a collection of points representing an even polygon centered at position.
    ''' </summary>
    ''' <param name="radius">The radius of a cirle circumscribing the polygon.</param>
    ''' <param name="sides">The number of sides of the polygon.</param>
    ''' <param name="position">The center position of the polygon.</param>
    ''' <param name="closed">True if the last point should be the same as the first point.</param>
    ''' <returns>A collection of points representing an even polygon centered at position.</returns>
    Public Shared Function GenerateEvenPolygon(ByVal radius As Single, ByVal sides As Integer, ByVal position As PointF, Optional closed As Boolean = True) As PointF()
        Dim bounds As Integer = sides
        If Not closed Then bounds = sides - 1
        Dim points(bounds) As PointF
        For side As Integer = 0 To sides - 1
            Dim delta As Single = PI / sides + side * 2 * PI / sides
            points(side) = New PointF(position.X + radius * CSng(Math.Sin(delta)), position.Y + radius * CSng(Math.Cos(delta)))
        Next
        If closed Then points(points.Length - 1) = points(0)
        Return points
    End Function

    ''' <summary>
    ''' Gets the velocity fo an object after collision with another object.
    ''' </summary>
    ''' <param name="velocityOne">The velocity of the first object before collision.</param>
    ''' <param name="massOne">The mass of the first object.</param>
    ''' <param name="velocityTwo">The velocity of the second object before collision.</param>
    ''' <param name="massTwo">The mass of the second object.</param>
    ''' <returns>The velocity of the first object after collision with the second.</returns>
    Public Shared Function GetCollisionVelocity(ByVal velocityOne As PointF, ByVal massOne As Single, ByVal velocityTwo As PointF, ByVal massTwo As Single) As PointF
        Dim x As Single = (massOne * velocityOne.X + massTwo * velocityTwo.X) / (massOne + massTwo)
        Dim y As Single = (massOne * velocityOne.Y + massTwo * velocityTwo.Y) / (massOne + massTwo)
        Return New PointF(x, y)
    End Function

    ''' <summary>
    ''' Gets a position a given distance toward a target position.
    ''' </summary>
    ''' <param name="source">The starting position.</param>
    ''' <param name="target">The target position.</param>
    ''' <param name="distance">The distance toward the target.</param>
    ''' <returns>A point that is the given distance toward the target.</returns>
    Public Shared Function GetPositionToward(ByVal source As PointF, ByVal target As PointF, ByVal distance As Single) As PointF
        Dim angle As Single = GetRadiansTo(source, target)
        Return GetPositionToward(source, angle, distance, AngleUnit.Radians)
    End Function

    ''' <summary>
    ''' Gets a position a given distance at a specific angle.
    ''' </summary>
    ''' <param name="source">The starting position.</param>
    ''' <param name="angle">The angle to move at.</param>
    ''' <param name="distance">The distance to move along the angle.</param>
    ''' <param name="unit">The unit of measure for the angle.</param>
    ''' <returns>A position a given distance at the specified angle.</returns>
    Public Shared Function GetPositionToward(ByVal source As PointF, ByVal angle As Single, ByVal distance As Single, unit As AngleUnit) As PointF
        If unit = AngleUnit.Degrees Then angle = DegreesToRadians(angle)
        Return source + New SizeF(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance)
    End Function

    ''' <summary>
    ''' Gets the angle between two points, in radians.
    ''' </summary>
    ''' <param name="source">The first point.</param>
    ''' <param name="target">The second point.</param>
    ''' <returns>The angle between the two points, in radians.</returns>
    Public Shared Function GetRadiansTo(ByVal source As PointF, ByVal target As PointF) As Single
        Return WrapRadians(Mathf.Atan2(target.Y - source.Y, target.X - source.X))
    End Function

    ''' <summary>
    ''' Determines if the source point is within range of the target point.
    ''' </summary>
    ''' <param name="source">The source point.</param>
    ''' <param name="target">The target point.</param>
    ''' <param name="range">The desired range.</param>
    ''' <returns>True if the source is within range of the target, otherwise false.</returns>
    Public Shared Function InRangeOf(source As Single, target As Single, range As Single) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    ''' <summary>
    ''' Determines if the source point is within range of the target point.
    ''' </summary>
    ''' <param name="source">The source point.</param>
    ''' <param name="target">The target point.</param>
    ''' <param name="range">The desired range.</param>
    ''' <returns>True if the source is within range of the target, otherwise false.</returns>
    Public Shared Function InRangeOf(source As Integer, target As Integer, range As Integer) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    ''' <summary>
    ''' Determines if the source point is within range of the target point.
    ''' </summary>
    ''' <param name="source">The source point.</param>
    ''' <param name="target">The target point.</param>
    ''' <param name="range">The desired range.</param>
    ''' <returns>True if the source is within range of the target, otherwise false.</returns>
    Public Shared Function InRangeOf(source As Double, target As Double, range As Double) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    ''' <summary>
    ''' Determines if the source point is within range of the target point.
    ''' </summary>
    ''' <param name="source">The source point.</param>
    ''' <param name="target">The target point.</param>
    ''' <param name="range">The desired range.</param>
    ''' <returns>True if the source is within range of the target, otherwise false.</returns>
    Public Shared Function InRangeOf(source As Decimal, target As Decimal, range As Decimal) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    ''' <summary>
    ''' Determines if the source point is within range of the target point.
    ''' </summary>
    ''' <param name="source">The source point.</param>
    ''' <param name="target">The target point.</param>
    ''' <param name="range">The desired range.</param>
    ''' <returns>True if the source is within range of the target, otherwise false.</returns>
    Public Shared Function InRangeOf(source As PointF, target As PointF, range As Decimal) As Boolean
        Return Distance(source, target) <= range
    End Function

    ''' <summary>
    ''' Determines if two circles intersect.
    ''' </summary>
    ''' <param name="c1">The center of the first circle.</param>
    ''' <param name="r1">The radius of the first circle.</param>
    ''' <param name="c2">The center of the second circle.</param>
    ''' <param name="r2">The radius of the second circle.</param>
    ''' <returns>True if the circles intersect, otherwise false.</returns>
    Public Shared Function IntersectCircleCircle(c1 As PointF, r1 As Single, c2 As PointF, r2 As Single) As Boolean
        Return Distance(c1, c2) <= r1 + r2
    End Function

    ''' <summary>
    ''' Determines if a circle and line intersect.
    ''' </summary>
    ''' <param name="a">The first point on the line.</param>
    ''' <param name="b">The second point on the line.</param>
    ''' <param name="c">The center of the circle.</param>
    ''' <param name="r">The radius of the circle.</param>
    ''' <param name="intersection">The first point of intersection.</param>
    ''' <returns>True if the circle and line intersect, otherwise false.</returns>
    Public Shared Function IntersectCircleLine(a As PointF, b As PointF, c As PointF, r As Single, ByRef intersection As Nullable(Of PointF)) As Boolean
        Dim d = Mathf.Distance(a, b)
        Dim alpha As Single = CSng((1 / Math.Pow(d, 2)) * ((b.X - a.X) * (c.X - a.X) + (b.Y - a.Y) * (c.Y - a.Y)))
        Dim m = New PointF(a.X + (b.X - a.X) * alpha, a.Y + (b.Y - a.Y) * alpha)
        If Distance(c, m) <= r Then
            intersection = m
            Return True
        End If
        intersection = Nothing
        Return False
    End Function

    ''' <summary>
    ''' Determines if a point intersects a circle.
    ''' </summary>
    ''' <param name="point">The point to check.</param>
    ''' <param name="c">The center of the circle.</param>
    ''' <param name="r">The radius of the circle.</param>
    ''' <returns>True if the point intersects the circle, otherwise false.</returns>
    Public Shared Function IntersectCirclePoint(point As PointF, c As PointF, r As Single) As Boolean
        Return Distance(point, c) <= r
    End Function

    ''' <summary>
    ''' Determines if a circle intersects a polygon.
    ''' </summary>
    ''' <param name="polygonPoints">The points of the polygon.</param>
    ''' <param name="c">The center of the circle.</param>
    ''' <param name="r">The radius of the circle.</param>
    ''' <param name="intersection">The first point of intersection.</param>
    ''' <returns>True if the circle intersects the polygon, otherwise false.</returns>
    Public Shared Function IntersectCirclePolygon(polygonPoints As IEnumerable(Of PointF), c As PointF, r As Single, ByRef intersection As Nullable(Of PointF)) As Boolean
        If IsPointInPolygon(c, polygonPoints) Then
            intersection = Nothing
            Return True
        End If
        For i As Integer = 0 To polygonPoints.Count - 2
            If IntersectCircleLineSegment(polygonPoints(i), polygonPoints(i + 1), c, r, intersection) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Determines if a circle intersects a rectangle.
    ''' </summary>
    ''' <param name="rect">The rectangle to check.</param>
    ''' <param name="c">The center of the circle.</param>
    ''' <param name="r">The radius of the circle.</param>
    ''' <param name="intersection">The first intersection point.</param>
    ''' <returns>True if the circle intersects the rectangle, otherwise false.</returns>
    Public Shared Function IntersectCircleRectangle(rect As RectangleF, c As PointF, r As Single, ByRef intersection As Nullable(Of PointF)) As Boolean
        Return IntersectCirclePolygon(RectangleToPolygon(rect), c, r, intersection)
    End Function

    ''' <summary>
    ''' Determines if a circle and line segment intersect.
    ''' </summary>
    ''' <param name="a">The beginning point of the line segment.</param>
    ''' <param name="b">The end point of the line segment.</param>
    ''' <param name="c">The center of the circle.</param>
    ''' <param name="r">The radius of the circle.</param>
    ''' <param name="intersection">The first point of intersection.</param>
    ''' <returns>True if the circle and line segment intersect, otherwise false.</returns>
    Public Shared Function IntersectCircleLineSegment(a As PointF, b As PointF, c As PointF, r As Single, ByRef intersection As Nullable(Of PointF)) As Boolean
        Dim d = Mathf.Distance(a, b)
        Dim alpha As Single = CSng((1 / Math.Pow(d, 2)) * ((b.X - a.X) * (c.X - a.X) + (b.Y - a.Y) * (c.Y - a.Y)))
        Dim m = New PointF(a.X + (b.X - a.X) * alpha, a.Y + (b.Y - a.Y) * alpha)
        If Mathf.Distance(m, c) <= r Then
            If Mathf.Distance(m, a) <= d AndAlso Mathf.Distance(m, b) <= d Then
                intersection = m
                Return True
            End If
            If Mathf.Distance(a, c) <= r OrElse Mathf.Distance(b, c) <= r Then
                intersection = m
                Return True
            End If
        End If
        Return False
    End Function 'formula from: http:'math.stackexchange.com/questions/408002/newbie-determine-if-line-segment-intersects-circle

    ''' <summary>
    ''' Determines if two lines intersect.
    ''' </summary>
    ''' <param name="a1">The first point on the first line.</param>
    ''' <param name="b1">The second point on the first line.</param>
    ''' <param name="a2">The first point on the second line.</param>
    ''' <param name="b2">The second point on the second line.</param>
    ''' <returns>True if the lines intersect, otherwise false.</returns>
    Public Shared Function IntersectLineLine(a1 As PointF, b1 As PointF, a2 As PointF, b2 As PointF) As Boolean
        Return Not -(b2.X - a2.X) * (b1.Y - a1.Y) + (b1.X - a1.X) * (b2.Y - a2.Y) = 0
    End Function

    ''' <summary>
    ''' Determines if a line and line segment intersect.
    ''' </summary>
    ''' <param name="lineA">The first point on the line.</param>
    ''' <param name="lineB">The second point on the line.</param>
    ''' <param name="segA">The beginning point of the line segment.</param>
    ''' <param name="segB">The end point of the line segment.</param>
    ''' <returns>True if the line and line segment intersect, otherwise false.</returns>
    Public Shared Function IntersectLineLineSegment(lineA As PointF, lineB As PointF, segA As PointF, segB As PointF) As Boolean
        Dim relationA = PointRelationToLine(lineA, lineB, segA)
        If Not relationA = PointLineRelation.OnLine Then
            Dim relationB = PointRelationToLine(lineA, lineB, segB)
            If Not relationB = PointLineRelation.OnLine Then
                Return Not relationA = relationB
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Determines if two line segments intersect.
    ''' </summary>
    ''' <param name="segA1">The beginning point of the first line segment.</param>
    ''' <param name="segB1">The end point of the first line segment.</param>
    ''' <param name="segA2">The beginning point of the second line segment.</param>
    ''' <param name="segB2">The end point of the second line segment.</param>
    ''' <param name="intersection">The point of intersection.</param>
    ''' <returns>True if the line segments intersect, otherwise false.</returns>
    Public Shared Function IntersectLineSegmentLineSegment(segA1 As PointF, segB1 As PointF, segA2 As PointF, segB2 As PointF, ByRef intersection As Nullable(Of PointF)) As Boolean
        Dim s1x = segB1.X - segA1.X
        Dim s1y = segB1.Y - segA1.Y
        Dim s2x = segB2.X - segA2.X
        Dim s2y = segB2.Y - segA2.Y
        Dim d = -s2x * s1y + s1x * s2y
        If Not d = 0 Then
            Dim n1 = -s1y * (segA1.X - segA2.X) + s1x * (segA1.Y - segA2.Y)
            Dim n2 = s2x * (segA1.Y - segA2.Y) - s2y * (segA1.X - segA2.X)
            If Math.Abs(Math.Sign(n1) + Math.Sign(n2) + Math.Sign(d)) = 3 AndAlso
               Math.Abs(d) > Math.Abs(n1) AndAlso Math.Abs(d) > Math.Abs(n2) Then 'check for intersection
                n2 = n2 / d
                intersection = New PointF(segA1.X + (n2 * s1x), segA1.Y + (n2 * s1y))
                Return True
            Else 'check end points
                If (segB1.X - segA1.X) * (segB2.Y - segA1.Y) - (segB1.Y - segA1.Y) * (segB2.X - segA1.X) = 0 Then
                    intersection = segB2
                    Return True
                End If
                If (segB1.X - segA1.X) * (segA2.Y - segA1.Y) - (segB1.Y - segA1.Y) * (segA2.X - segA1.X) = 0 Then
                    intersection = segA2
                    Return True
                End If
                If (segB2.X - segA2.X) * (segB1.Y - segA2.Y) - (segB2.Y - segA2.Y) * (segB1.X - segA2.X) = 0 Then
                    intersection = segB1
                    Return True
                End If
                If (segB2.X - segA2.X) * (segA1.Y - segA2.Y) - (segB2.Y - segA2.Y) * (segA1.X - segA2.X) = 0 Then
                    intersection = segA1
                    Return True
                End If
            End If
        Else 'lines are parallel, check colinear
            intersection = Nothing
            Return (segA1.X = segA2.X AndAlso segB1.X = segB2.X) OrElse (segA1.Y = segA2.Y AndAlso segB1.Y = segB2.Y)
        End If
        intersection = Nothing
        Return False
    End Function 'derived from various info at: http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect

    ''' <summary>
    ''' Determines if a line intersects a polygon.
    ''' </summary>
    ''' <param name="polygon">The points of the polygon.</param>
    ''' <param name="a">The first point on the line.</param>
    ''' <param name="b">The second point on the line.</param>
    ''' <returns>True if the line intersects the polygon, otherwise false.</returns>
    Public Shared Function IntersectPolygonLine(polygon As IEnumerable(Of PointF), a As PointF, b As PointF) As Boolean
        Dim relation = PointRelationToLine(a, b, polygon(0))
        If Not relation = PointLineRelation.OnLine Then
            For i = 1 To polygon.Count - 1
                If Not PointRelationToLine(a, b, polygon(i)) = relation Then Return True
            Next
        End If
        Return False
    End Function

    ''' <summary>
    ''' Determines if a line segment intersects a polygon.
    ''' </summary>
    ''' <param name="polygon">The points of the polygon.</param>
    ''' <param name="a">The beginning point of the line segment.</param>
    ''' <param name="b">The end point of the line segment.</param>
    ''' <returns>True if the line segment intersects the polygon, otherwise false.</returns>
    Public Shared Function IntersectPolygonLineSegment(polygon As IEnumerable(Of PointF), a As PointF, b As PointF) As Boolean
        Dim lineRadius = Distance(a, b) / 2
        Dim dist = Distance(PolygonCentroid(polygon), LineCenter(a, b))
        If dist <= lineRadius Then
            Dim relation = PointRelationToLine(a, b, polygon(0))
            If Not relation = PointLineRelation.OnLine Then
                For i = 1 To polygon.Count - 1
                    If Not PointRelationToLine(a, b, polygon(i)) = relation Then Return True
                Next
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Determines if two polygons intersect.
    ''' </summary>
    ''' <param name="polygon1">The points of the first polygon.</param>
    ''' <param name="polygon2">The points of the second polygon.</param>
    ''' <returns>True if the polygons intersect, otherwise false.</returns>
    Public Shared Function IntersectPolygonPolygon(polygon1 As IEnumerable(Of PointF), polygon2 As IEnumerable(Of PointF)) As Boolean
        For Each p In polygon1
            If IsPointInPolygon(p, polygon2) Then Return True
        Next
        For Each p In polygon2
            If IsPointInPolygon(p, polygon1) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Determines if a value is infinite.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <returns>True if the value is infinite, otherwise false.</returns>
    Public Shared Function IsInfinite(ByVal value As Single) As Boolean
        Return Single.IsInfinity(value)
    End Function

    ''' <summary>
    ''' Determines if a value is infinite.
    ''' </summary>
    ''' <param name="value">The value to check.</param>
    ''' <returns>True if the value is infinite, otherwise false.</returns>
    Public Shared Function IsInfinite(ByVal value As PointF) As Boolean
        Return Single.IsInfinity(value.X) OrElse Single.IsInfinity(value.Y)
    End Function

    ''' <summary>
    ''' Determines if a point is contained within a polygon.
    ''' </summary>
    ''' <param name="point">The point to check.</param>
    ''' <param name="polygonPoints">The points of the polygon.</param>
    ''' <returns>True if the point is within the polygon, otherwise false.</returns>
    Public Shared Function IsPointInPolygon(point As PointF, polygonPoints As IEnumerable(Of PointF)) As Boolean
        Dim result = 0
        For i = 0 To polygonPoints.Count - 2
            If (polygonPoints(i).Y <= point.Y) Then
                If (polygonPoints(i + 1).Y > point.Y) Then
                    If (PointRelationToLine(polygonPoints(i), polygonPoints(i + 1), point) = PointLineRelation.LeftOfLine) Then
                        result += 1
                    End If
                End If
            Else
                If (polygonPoints(i + 1).Y <= point.Y) Then
                    If (PointRelationToLine(polygonPoints(i), polygonPoints(i + 1), point) = PointLineRelation.RightOfLine) Then
                        result -= 1
                    End If
                End If
            End If
        Next
        Return (result <> 0)
    End Function 'code converted from C++ at http://geomalgorithms.com/a03-_inclusion.html

    ''' <summary>
    ''' Determines if a number is a real number.
    ''' </summary>
    ''' <param name="value">The number to check.</param>
    ''' <returns>True if the number is a real number, otherwise false.</returns>
    Public Shared Function IsRealNumber(ByVal value As Single) As Boolean
        If Single.IsNaN(value) Then Return False
        Return Not Single.IsInfinity(value)
    End Function

    ''' <summary>
    ''' Calculates the center point between two points on a line.
    ''' </summary>
    ''' <param name="a">The first point on the line.</param>
    ''' <param name="b">The second point on the line.</param>
    ''' <returns>The center point between points a and b.</returns>
    Public Shared Function LineCenter(a As PointF, b As PointF) As PointF
        Return New PointF((b.X - a.X) / 2, (b.Y - a.Y) / 2)
    End Function

    ''' <summary>
    ''' Gets the greater of two numbers.
    ''' </summary>
    ''' <param name="value1">The first number.</param>
    ''' <param name="value2">The second number.</param>
    ''' <returns>The greater of the two numbers.</returns>
    Public Shared Function Max(ByVal value1 As Single, ByVal value2 As Single) As Single
        If value2 > value1 Then Return value2
        Return value1
    End Function

    ''' <summary>
    ''' Gets the greater of two numbers.
    ''' </summary>
    ''' <param name="value1">The first number.</param>
    ''' <param name="value2">The second number.</param>
    ''' <returns>The greater of the two numbers.</returns>
    Public Shared Function Max(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value2 > value1 Then Return value2
        Return value1
    End Function

    ''' <summary>
    ''' Gets the lesser of two numbers.
    ''' </summary>
    ''' <param name="value1">The first number.</param>
    ''' <param name="value2">The second number.</param>
    ''' <returns>The lesser of the two numbers.</returns>
    Public Shared Function Min(ByVal value1 As Single, ByVal value2 As Single) As Single
        If value2 < value1 Then Return value2
        Return value1
    End Function

    ''' <summary>
    ''' Gets the lesser of two numbers.
    ''' </summary>
    ''' <param name="value1">The first number.</param>
    ''' <param name="value2">The second number.</param>
    ''' <returns>The lesser of the two numbers.</returns>
    Public Shared Function Min(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value2 < value1 Then Return value2
        Return value1
    End Function

    ''' <summary>
    ''' Multiplies both dimensions of a vector by a given amount.
    ''' </summary>
    ''' <param name="amount">The amount to multiply by.</param>
    ''' <param name="vector">The vector to multiply.</param>
    ''' <returns>The multiplied vector value.</returns>
    Public Shared Function MultiplyVector(amount As Single, vector As PointF) As PointF
        Return New PointF(vector.X * amount, vector.Y * amount)
    End Function

    ''' <summary>
    ''' Normalizes a vector value.
    ''' </summary>
    ''' <param name="value">The vector to normalize.</param>
    ''' <returns>The normalized vector.</returns>
    Public Shared Function Normalize(value As PointF) As PointF
        Dim l = VectorLength(value)
        Return New PointF(value.X / l, value.Y / l)
    End Function

    ''' <summary>
    ''' Evaluates the relation of a point to a line.
    ''' </summary>
    ''' <param name="lineA">The first point on the line.</param>
    ''' <param name="lineB">The second point on the line.</param>
    ''' <param name="point">The point to evaluate.</param>
    ''' <returns>A value indicating the relation of the point location to the line location.</returns>
    Public Shared Function PointRelationToLine(lineA As PointF, lineB As PointF, point As PointF) As PointLineRelation
        Dim result = ((lineB.X - lineA.X) * (point.Y - lineA.Y) - (point.X - lineA.X) * (lineB.Y - lineA.Y))
        If result > 0 Then Return PointLineRelation.LeftOfLine
        If result < 0 Then Return PointLineRelation.RightOfLine
        Return PointLineRelation.OnLine
    End Function 'code converted from C++ at http://geomalgorithms.com/a03-_inclusion.html

    ''' <summary>
    ''' Calculates the area of a polygon.
    ''' </summary>
    ''' <param name="polygon">The points of the polygon.</param>
    ''' <returns>The area of the polygon.</returns>
    Public Shared Function PolygonArea(polygon As IEnumerable(Of PointF)) As Single
        Dim dx, dy As Single
        For i = 0 To polygon.Count - 2
            Dim p = polygon(i)
            Dim p1 = polygon(i + 1)
            dx += p.X * p1.Y
            dy += p.Y * p1.X
        Next
        Return (dx - dy) / 2
    End Function

    ''' <summary>
    ''' Calculates the centroid of a polygon.
    ''' </summary>
    ''' <param name="polygon">The points of the polygon.</param>
    ''' <returns>The polygon centroid point.</returns>
    Public Shared Function PolygonCentroid(polygon As IEnumerable(Of PointF)) As PointF
        Dim accumulatedArea = 0.0F
        Dim centerX = 0.0F
        Dim centerY = 0.0F
        For i = 0 To polygon.Count - 2
            Dim j = i + 1
            Dim temp = polygon(i).X * polygon(j).Y - polygon(j).X * polygon(i).Y
            accumulatedArea += temp
            centerX += (polygon(i).X + polygon(j).X) * temp
            centerY += (polygon(i).Y + polygon(j).Y) * temp
        Next
        accumulatedArea *= 3.0F
        If accumulatedArea = 0 Then Return PointF.Empty
        Return New PointF(centerX / accumulatedArea, centerY / accumulatedArea)
    End Function 'http://stackoverflow.com/questions/9815699/how-to-calculate-centroid

    ''' <summary>
    ''' Raises a number to a power.
    ''' </summary>
    ''' <param name="value">The value.</param>
    ''' <param name="exponent">The exponent to raise the value to.</param>
    ''' <returns>The result of raising value to exponent.</returns>
    Public Shared Function Pow(ByVal value As Single, ByVal exponent As Integer) As Single
        For count As Integer = 0 To exponent - 2
            value *= value
        Next
        Return value
    End Function

    ''' <summary>
    ''' Converts an angle in radians to degrees.
    ''' </summary>
    ''' <param name="radians">An angle in radians.</param>
    ''' <returns>The angle value in degrees.</returns>
    Public Shared Function RadiansToDegrees(ByVal radians As Single) As Single
        Return CSng(radians * (HALF_CIRCLE / PI))
    End Function

    ''' <summary>
    ''' Converts a rectangle structure to a polygon array of points.
    ''' </summary>
    ''' <param name="rect">The rectangle to convert.</param>
    ''' <returns>The collection of rectangle points.</returns>
    Public Shared Function RectangleToPolygon(rect As RectangleF) As IEnumerable(Of PointF)
        Return {New PointF(rect.X, rect.Y),
                New PointF(rect.X, rect.Y + rect.Height),
                New PointF(rect.X + rect.Width, rect.Y + rect.Height),
                New PointF(rect.X + rect.Width, rect.Y),
                New PointF(rect.X, rect.Y)}
    End Function

    ''' <summary>
    ''' Reflects an angle.
    ''' </summary>
    ''' <param name="initialAngleInDegrees">The angle to reflect.</param>
    ''' <param name="normalAngle">The normal angle of the reflection.</param>
    ''' <returns>The reflected angle.</returns>
    Public Shared Function ReflectAngle(initialAngleInDegrees As Single, normalAngle As Single) As Single
        Return WrapDegrees(2 * normalAngle - initialAngleInDegrees)
    End Function

    ''' <summary>
    ''' Reflects an angle horizontally.
    ''' </summary>
    ''' <param name="initialAngleInDegrees">The angle to reflect.</param>
    ''' <returns>The reflected angle.</returns>
    Public Shared Function ReflectHorizontal(initialAngleInDegrees As Single) As Single
        Return ReflectAngle(initialAngleInDegrees, 180)
    End Function

    ''' <summary>
    ''' Reflects an angle vertically.
    ''' </summary>
    ''' <param name="initialAngleInDegrees">The angle to reflect.</param>
    ''' <returns>The reflected angle.</returns>
    Public Shared Function ReflectVerticle(initialAngleInDegrees As Single) As Single
        Return ReflectAngle(initialAngleInDegrees, 90)
    End Function

    ''' <summary>
    ''' System.Math method typed for Single values
    ''' </summary>
    Public Shared Function Sin(ByVal radians As Single) As Single
        Return CSng(System.Math.Sin(CDbl(radians)))
    End Function

    ''' <summary>
    ''' Calculates the square root of a value.
    ''' </summary>
    ''' <param name="value">The value to calculate.</param>
    ''' <returns>The square root of value.</returns>
    Public Shared Function Sqrt(ByVal value As Single) As Single
        Return CSng(CDbl(value) ^ 0.5)
    End Function

    ''' <summary>
    ''' Calculates the square of a value.
    ''' </summary>
    ''' <param name="value">The value to square.</param>
    ''' <returns>The squared value.</returns>
    Public Shared Function Square(ByVal value As Single) As Single
        Return value * value
    End Function

    ''' <summary>
    ''' Swaps the value of two variables.
    ''' </summary>
    ''' <param name="value1">The first variable to swap.</param>
    ''' <param name="value2">The second variable to swap.</param>
    Public Shared Sub Swap(ByRef value1 As Single, ByRef value2 As Single)
        Dim temp As Single = value2
        value1 = value2
        value2 = temp
    End Sub

    ''' <summary>
    ''' Swaps the value of two variables.
    ''' </summary>
    ''' <param name="value1">The first variable to swap.</param>
    ''' <param name="value2">The second variable to swap.</param>
    Public Shared Sub Swap(ByRef value1 As Integer, ByRef value2 As Integer)
        Dim temp As Integer = value2
        value1 = value2
        value2 = temp
    End Sub

    ''' <summary>
    ''' System.Math method typed for Single values
    ''' </summary>
    Public Shared Function Tan(ByVal value As Single) As Single
        Return CSng(System.Math.Tan(CDbl(value)))
    End Function

    ''' <summary>
    ''' Calculates the length of a vector.
    ''' </summary>
    ''' <param name="value">The vector to calculate.</param>
    ''' <returns>The length of the vector.</returns>
    Public Shared Function VectorLength(value As PointF) As Single
        Return CSng(Math.Sqrt(value.X ^ 2 + value.Y ^ 2))
    End Function

    ''' <summary>
    ''' Wraps an angle value within 0 and 359.
    ''' </summary>
    ''' <param name="degrees">The raw angle value in degrees.</param>
    ''' <returns>The wrapped angle value.</returns>
    Public Shared Function WrapDegrees(degrees As Single) As Single
        Return RadiansToDegrees(WrapRadians(DegreesToRadians(degrees)))
    End Function

    ''' <summary>
    ''' Wraps an angle value within a circle.
    ''' </summary>
    ''' <param name="radians">The raw angle value in radians.</param>
    ''' <returns>The wrapped angle value.</returns>
    Public Shared Function WrapRadians(ByVal radians As Single) As Single
        While radians < -PI
            radians += TWO_PI
        End While
        While radians > PI
            radians -= TWO_PI
        End While
        Return radians
    End Function

    ''' <summary>
    ''' Represents the unit of measure for an angle value
    ''' </summary>
    Public Enum AngleUnit
        Radians
        Degrees
    End Enum

    ''' <summary>
    ''' Represents a point's relation to a line in the same plane.
    ''' </summary>
    Public Enum PointLineRelation
        RightOfLine = -1
        OnLine = 0
        LeftOfLine = 1
    End Enum
End Class
