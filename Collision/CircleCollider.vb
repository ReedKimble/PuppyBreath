Namespace Collision
    Public Class CircleCollider
        Inherits ColliderBase

        Public Radius As Single

        Public Overrides Sub DebugDraw(pen As Pen, graphics As Graphics)
            graphics.DrawEllipse(pen, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2)
        End Sub

        Public Shared Function CreateNew(radius As Single) As CircleCollider
            Return New CircleCollider With {.Radius = radius}
        End Function

        Public Overrides Function IntersectsLine(startPoint As PointF, endPoint As PointF) As Boolean
            Return Mathf.IntersectCircleLineSegment(startPoint, endPoint, Position, Radius, Nothing)
        End Function
    End Class
End Namespace

