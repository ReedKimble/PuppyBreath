''' <summary>
''' Represents a circle defined by a center point and a radius.
''' </summary>
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

    Public Function Contains(point As PointF) As Boolean
        Return Mathf.Distance(Position, point) <= Radius
    End Function

    Public Function GetBounds() As RectangleF
        Return New RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2)
    End Function

    Public Function Intersects(other As Circle) As Boolean
        Return Mathf.Distance(Position, other.Position) <= Radius + other.Radius
    End Function

    Public Function IntersectsLine(a As PointF, b As PointF) As Boolean
        Return Mathf.IntersectCircleLine(a, b, Position, Radius, Nothing)
    End Function

    Public Function IntersectsPolygon(polygonPoints As IEnumerable(Of PointF)) As Boolean
        Return Mathf.IntersectCirclePolygon(polygonPoints, Position, Radius, Nothing)
    End Function

    Public Function IntersectsRectangle(value As Rectangle) As Boolean
        Return Mathf.IntersectCircleRectangle(value, Position, Radius, Nothing)
    End Function

    Public Function IntersectsSegment(a As PointF, b As PointF) As Boolean
        Return Mathf.IntersectCircleLineSegment(a, b, Position, Radius, Nothing)
    End Function
End Structure