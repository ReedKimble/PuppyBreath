''' <summary>
''' Represents a GameObject which also includes a visual appearance on screen.
''' </summary>
Public Class Sprite
    Inherits GameObject

    Public Property FrameLocation As Point
    Public Property FrameSize As Size
    Public Property Image As Image
    Public Property OnPostRender As Action(Of Graphics)
    Public Property Rotation As Single
    Public Property Scale As Single = 1.0!
    Public Property Size As SizeF

    ''' <summary>
    ''' Gets or sets a value which determines the drawing order of the sprite (lower values are drawn first).
    ''' </summary>
    ''' <returns>A number representing the draw order of the sprite, with lower values being drawn first and
    ''' higher values being drawn on top.</returns>
    Public Property ZOrder As Single

    Public Overrides Function GetCollisionBounds() As Circle
        Return New Circle(Position, CollisionRadius * Scale)
    End Function

    Protected Friend Overridable Sub Render(g As Graphics)
        If IsDestroyed Then Return
        If Image IsNot Nothing Then
            Dim srcBounds As New Rectangle(FrameLocation, FrameSize)
            Dim dstBounds As New Rectangle(-Size.Width * 0.5!, -Size.Height * 0.5!, Size.Width, Size.Height)
            g.TranslateTransform(Position.X, Position.Y)
            g.RotateTransform(Rotation)
            g.ScaleTransform(Scale, Scale)
            g.DrawImage(Image, dstBounds, srcBounds, GraphicsUnit.Pixel)
            g.ResetTransform()
        End If
        OnPostRender?.Invoke(g)
    End Sub

End Class
