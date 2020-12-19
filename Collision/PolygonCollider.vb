Namespace Collision
    Public Class PolygonCollider
        Inherits ColliderBase
        Public PolygonPointOffsets As New List(Of PointF)

        Public Overrides Sub DebugDraw(pen As Pen, graphics As Graphics)
            graphics.DrawPolygon(pen, GetPolygonPoints.ToArray)
        End Sub

        Public Shared Function CreateNew(sides As Integer, radius As Single) As PolygonCollider
            Dim result As New PolygonCollider
            result.PolygonPointOffsets.AddRange(Mathf.GenerateEvenPolygon(radius, sides, PointF.Empty))
            Return result
        End Function

        Public Shared Function CreateNew(size As SizeF) As PolygonCollider
            Dim result As New PolygonCollider
            result.PolygonPointOffsets.Add(New PointF(-size.Width * 0.5, -size.Height * 0.5))
            result.PolygonPointOffsets.Add(New PointF(-size.Width * 0.5, size.Height * 0.5))
            result.PolygonPointOffsets.Add(New PointF(size.Width * 0.5, size.Height * 0.5))
            result.PolygonPointOffsets.Add(New PointF(size.Width * 0.5, -size.Height * 0.5))
            Return result
        End Function

        Public Function GetPolygonPoints() As IEnumerable(Of PointF)
            Return (From p In PolygonPointOffsets Select Position + New SizeF(p)).ToArray
        End Function

        Public Overrides Function IntersectsLine(startPoint As PointF, endPoint As PointF) As Boolean
            Return Mathf.IntersectPolygonLineSegment(GetPolygonPoints, startPoint, endPoint)
        End Function
    End Class
End Namespace

