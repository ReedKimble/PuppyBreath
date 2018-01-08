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

    Public Shared Function Atan(ByVal value As Single) As Single
        Return CSng(System.Math.Atan(CDbl(value)))
    End Function

    Public Shared Function Atan2(ByVal y As Single, ByVal x As Single) As Single
        Return CSng(System.Math.Atan2(CDbl(y), CDbl(x)))
    End Function

    Public Shared Function Between(ByVal value As Single, ByVal min As Single, ByVal max As Single) As Boolean
        Return value >= min AndAlso value <= max
    End Function

    Public Shared Function Between(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Boolean
        Return value >= min AndAlso value <= max
    End Function

    Public Shared Function Clamp(ByVal value As Single, ByVal min As Single, ByVal max As Single) As Single
        Return Mathf.Max(min, Mathf.Min(value, max))
    End Function

    Public Shared Function Clamp(ByVal value As Integer, ByVal min As Integer, ByVal max As Integer) As Integer
        Return CInt(Mathf.Max(min, Mathf.Min(value, max)))
    End Function

    Public Shared Function CollisionPlane(normalPlane As PointF) As PointF
        Return New PointF(-normalPlane.Y, normalPlane.X)
    End Function

    Public Shared Function Cos(ByVal radians As Single) As Single
        Return CSng(System.Math.Cos(CDbl(radians)))
    End Function

    Public Shared Function DegreesToRadians(ByVal degrees As Single) As Single
        Return degrees * (PI / HALF_CIRCLE)
    End Function

    Public Shared Function Distance(a As PointF, b As PointF) As Single
        Return CSng(Math.Sqrt(Math.Pow(b.X - a.X, 2.0) + Math.Pow(b.Y - a.Y, 2.0)))
    End Function

    Public Shared Function VectorDotProduct(vector1 As PointF, vector2 As PointF) As Single
        Return vector1.X * vector2.X + vector1.Y * vector2.Y
    End Function

    Public Shared Function EqualWithin(ByVal source As Single, ByVal target As Single, ByVal tolerance As Single) As Boolean
        Return source - target <= tolerance
    End Function

    Public Shared Function EqualWithin(ByVal source As Integer, ByVal target As Integer, ByVal tolerance As Integer) As Boolean
        Return source - target <= tolerance
    End Function

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

    Public Shared Function GetCollisionVelocity(ByVal velocityOne As PointF, ByVal massOne As Single, ByVal velocityTwo As PointF, ByVal massTwo As Single) As PointF
        Dim x As Single = (massOne * velocityOne.X + massTwo * velocityTwo.X) / (massOne + massTwo)
        Dim y As Single = (massOne * velocityOne.Y + massTwo * velocityTwo.Y) / (massOne + massTwo)
        Return New PointF(x, y)
    End Function

    Public Shared Function GetPositionToward(ByVal source As PointF, ByVal target As PointF, ByVal distance As Single) As PointF
        Dim angle As Single = GetRadiansTo(source, target)
        Return GetPositionToward(source, angle, distance, AngleUnit.Radians)
    End Function

    Public Shared Function GetPositionToward(ByVal source As PointF, ByVal angle As Single, ByVal distance As Single, unit As AngleUnit) As PointF
        If unit = AngleUnit.Degrees Then angle = DegreesToRadians(angle)
        Return source + New SizeF(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance)
    End Function

    Public Shared Function GetRadiansTo(ByVal source As PointF, ByVal target As PointF) As Single
        Return WrapRadians(Mathf.Atan2(target.Y - source.Y, target.X - source.X))
    End Function

    Public Shared Function InRangeOf(source As Single, target As Single, range As Single) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    Public Shared Function InRangeOf(source As Integer, target As Integer, range As Integer) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    Public Shared Function InRangeOf(source As Double, target As Double, range As Double) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    Public Shared Function InRangeOf(source As Decimal, target As Decimal, range As Decimal) As Boolean
        Return System.Math.Abs(source - target) <= range
    End Function

    Public Shared Function InRangeOf(source As PointF, target As PointF, range As Decimal) As Boolean
        Return Distance(source, target) <= range
    End Function

    Public Shared Function IntersectCircleCircle(c1 As PointF, r1 As Single, c2 As PointF, r2 As Single) As Boolean
        Return Distance(c1, c2) <= r1 + r2
    End Function

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

    Public Shared Function IntersectCirclePoint(point As PointF, c As PointF, r As Single) As Boolean
        Return Distance(point, c) <= r
    End Function

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

    Public Shared Function IntersectCircleRectangle(rect As RectangleF, c As PointF, r As Single, ByRef intersection As Nullable(Of PointF)) As Boolean
        Return IntersectCirclePolygon(RectangleToPolygon(rect), c, r, intersection)
    End Function

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

    Public Shared Function IntersectLineLine(a1 As PointF, b1 As PointF, a2 As PointF, b2 As PointF) As Boolean
        Return Not -(b2.X - a2.X) * (b1.Y - a1.Y) + (b1.X - a1.X) * (b2.Y - a2.Y) = 0
    End Function

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


    Public Shared Function IntersectPolygonLine(polygon As IEnumerable(Of PointF), a As PointF, b As PointF) As Boolean
        Dim relation = PointRelationToLine(a, b, polygon(0))
        If Not relation = PointLineRelation.OnLine Then
            For i = 1 To polygon.Count - 1
                If Not PointRelationToLine(a, b, polygon(i)) = relation Then Return True
            Next
        End If
        Return False
    End Function

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

    Public Shared Function IntersectPolygonPolygon(polygon1 As IEnumerable(Of PointF), polygon2 As IEnumerable(Of PointF)) As Boolean
        For Each p In polygon1
            If IsPointInPolygon(p, polygon2) Then Return True
        Next
        For Each p In polygon2
            If IsPointInPolygon(p, polygon1) Then Return True
        Next
        Return False
    End Function

    Public Shared Function IsInfinite(ByVal value As Single) As Boolean
        Return Single.IsInfinity(value)
    End Function

    Public Shared Function IsInfinite(ByVal value As PointF) As Boolean
        Return Single.IsInfinity(value.X) OrElse Single.IsInfinity(value.Y)
    End Function

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

    Public Shared Function IsRealNumber(ByVal value As Single) As Boolean
        If Single.IsNaN(value) Then Return False
        Return Not Single.IsInfinity(value)
    End Function

    Public Shared Function LineCenter(a As PointF, b As PointF) As PointF
        Return New PointF((b.X - a.X) / 2, (b.Y - a.Y) / 2)
    End Function

    Public Shared Function Max(ByVal value1 As Single, ByVal value2 As Single) As Single
        If value2 > value1 Then Return value2
        Return value1
    End Function

    Public Shared Function Min(ByVal value1 As Single, ByVal value2 As Single) As Single
        If value2 < value1 Then Return value2
        Return value1
    End Function

    Public Shared Function Max(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value2 > value1 Then Return value2
        Return value1
    End Function

    Public Shared Function Min(ByVal value1 As Integer, ByVal value2 As Integer) As Integer
        If value2 < value1 Then Return value2
        Return value1
    End Function

    Public Shared Function MultiplyVector(amount As Single, vector As PointF) As PointF
        Return New PointF(vector.X * amount, vector.Y * amount)
    End Function

    Public Shared Function Normalize(value As PointF) As PointF
        Dim l = VectorLength(value)
        Return New PointF(value.X / l, value.Y / l)
    End Function

    Public Shared Function PointRelationToLine(lineA As PointF, lineB As PointF, point As PointF) As PointLineRelation
        Dim result = ((lineB.X - lineA.X) * (point.Y - lineA.Y) - (point.X - lineA.X) * (lineB.Y - lineA.Y))
        If result > 0 Then Return PointLineRelation.LeftOfLine
        If result < 0 Then Return PointLineRelation.RightOfLine
        Return PointLineRelation.OnLine
    End Function 'code converted from C++ at http://geomalgorithms.com/a03-_inclusion.html

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

    Public Shared Function Pow(ByVal value As Single, ByVal exponent As Integer) As Single
        For count As Integer = 0 To exponent - 2
            value *= value
        Next
        Return value
    End Function

    Public Shared Function RadiansToDegrees(ByVal radians As Single) As Single
        Return CSng(radians * (HALF_CIRCLE / PI))
    End Function

    Public Shared Function RectangleToPolygon(rect As RectangleF) As IEnumerable(Of PointF)
        Return {New PointF(rect.X, rect.Y),
                New PointF(rect.X, rect.Y + rect.Height),
                New PointF(rect.X + rect.Width, rect.Y + rect.Height),
                New PointF(rect.X + rect.Width, rect.Y),
                New PointF(rect.X, rect.Y)}
    End Function

    Public Shared Function ReflectAngle(initialAngleInDegrees As Single, normalAngle As Single) As Single
        Return WrapDegrees(2 * normalAngle - initialAngleInDegrees)
    End Function

    Public Shared Function ReflectHorizontal(initialAngleInDegrees As Single) As Single
        Return ReflectAngle(initialAngleInDegrees, 180)
    End Function

    Public Shared Function ReflectVerticle(initialAngleInDegrees As Single) As Single
        Return ReflectAngle(initialAngleInDegrees, 90)
    End Function
    Public Shared Function Sin(ByVal radians As Single) As Single
        Return CSng(System.Math.Sin(CDbl(radians)))
    End Function

    Public Shared Function Sqrt(ByVal value As Single) As Single
        Return CSng(CDbl(value) ^ 0.5)
    End Function

    Public Shared Function Square(ByVal value As Single) As Single
        Return value * value
    End Function

    Public Shared Sub Swap(ByRef value1 As Single, ByRef value2 As Single)
        Dim temp As Single = value2
        value1 = value2
        value2 = temp
    End Sub

    Public Shared Sub Swap(ByRef value1 As Integer, ByRef value2 As Integer)
        Dim temp As Integer = value2
        value1 = value2
        value2 = temp
    End Sub

    Public Shared Function Tan(ByVal value As Single) As Single
        Return CSng(System.Math.Tan(CDbl(value)))
    End Function

    Public Shared Function VectorLength(value As PointF) As Single
        Return CSng(Math.Sqrt(value.X ^ 2 + value.Y ^ 2))
    End Function

    Public Shared Function WrapDegrees(degrees As Single) As Single
        Return RadiansToDegrees(WrapRadians(DegreesToRadians(degrees)))
    End Function

    Public Shared Function WrapRadians(ByVal radians As Single) As Single
        While radians < -PI
            radians += TWO_PI
        End While
        While radians > PI
            radians -= TWO_PI
        End While
        Return radians
    End Function

    Public Enum AngleUnit
        Radians
        Degrees
    End Enum

    Public Enum PointLineRelation
        RightOfLine = -1
        OnLine = 0
        LeftOfLine = 1
    End Enum
End Class
