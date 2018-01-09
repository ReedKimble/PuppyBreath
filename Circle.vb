''' <summary>
''' Represents a circle defined by a center point and a radius.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Structure Circle
    ''' <summary>
    ''' Gets or sets the center of the circle.
    ''' </summary>
    Public Position As PointF
    ''' <summary>
    ''' Gets or sets the radius of the circle.
    ''' </summary>
    Public Radius As Single

    Public Sub New(circlePosition As PointF, circleRadius As Single)
        Position = circlePosition
        Radius = circleRadius
    End Sub

    ''' <summary>
    ''' Determines if a point is contained within the circle.
    ''' </summary>
    ''' <param name="point">The point to check.</param>
    ''' <returns>True if the point is in the circle, otherwise false.</returns>
    Public Function Contains(point As PointF) As Boolean
        Return Mathf.Distance(Position, point) <= Radius
    End Function

    ''' <summary>
    ''' Gets a rectangle bounding the circle.
    ''' </summary>
    ''' <returns>A rectangle bounding the circle.</returns>
    Public Function GetBounds() As RectangleF
        Return New RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2)
    End Function

    ''' <summary>
    ''' Determines if this circle intersects another circle.
    ''' </summary>
    ''' <param name="other">The other circle to check for intersection.</param>
    ''' <returns>True if the circles intersect, otherwise false.</returns>
    Public Function Intersects(other As Circle) As Boolean
        Return Mathf.Distance(Position, other.Position) <= Radius + other.Radius
    End Function

    ''' <summary>
    ''' Determines if this circle intersects a line.
    ''' </summary>
    ''' <param name="a">The first point on the line.</param>
    ''' <param name="b">The second point on the line.</param>
    ''' <returns>True if the line intersects the circle, otherwise false.</returns>
    Public Function IntersectsLine(a As PointF, b As PointF) As Boolean
        Return Mathf.IntersectCircleLine(a, b, Position, Radius, Nothing)
    End Function

    ''' <summary>
    ''' Determines if this circle intersects a polygon.
    ''' </summary>
    ''' <param name="polygonPoints">The points of the polygon.</param>
    ''' <returns>True if the circle intersects the polygon, otherwise false.</returns>
    Public Function IntersectsPolygon(polygonPoints As IEnumerable(Of PointF)) As Boolean
        Return Mathf.IntersectCirclePolygon(polygonPoints, Position, Radius, Nothing)
    End Function

    ''' <summary>
    ''' Determines if this circle intersects a rectangle.
    ''' </summary>
    ''' <param name="value">The rectangle to check for intersection.</param>
    ''' <returns>True if the circle intersects the rectangle, otherwise false.</returns>
    Public Function IntersectsRectangle(value As Rectangle) As Boolean
        Return Mathf.IntersectCircleRectangle(value, Position, Radius, Nothing)
    End Function

    ''' <summary>
    ''' Determines if this circle intersects a line segment.
    ''' </summary>
    ''' <param name="a">The first point on the line segment.</param>
    ''' <param name="b">The second point on the line segment.</param>
    ''' <returns>True if the line segment intersects the circle, otherwise false.</returns>
    Public Function IntersectsSegment(a As PointF, b As PointF) As Boolean
        Return Mathf.IntersectCircleLineSegment(a, b, Position, Radius, Nothing)
    End Function
End Structure