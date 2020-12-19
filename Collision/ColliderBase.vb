Namespace Collision
    Public MustInherit Class ColliderBase

        Public ReadOnly Property Position As PointF
            Get
                Return owner?.Position
            End Get
        End Property

        Protected Friend owner As GameObject

        Public MustOverride Sub DebugDraw(pen As Pen, graphics As Graphics)

        Public MustOverride Function IntersectsLine(startPoint As PointF, endPoint As PointF) As Boolean
    End Class
End Namespace
