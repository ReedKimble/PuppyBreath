''' <summary>
''' Represents a GameObject which also includes a visual appearance on screen.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class Sprite
    Inherits GameObject

    ''' <summary>
    ''' Gets the point at which the current frame's image is located within the source Image.
    ''' </summary>
    ''' <returns>The point at which the current frame's image is located within the source Image.</returns>
    Public Property FrameLocation As Point

    ''' <summary>
    ''' Gets or sets the size of the current image frame.
    ''' </summary>
    ''' <returns>The size of the current image frame.</returns>
    Public Property FrameSize As Size

    ''' <summary>
    ''' Gets or sets the image to be drawn for this sprite.
    ''' </summary>
    ''' <returns>The image to be drawn for this sprite.</returns>
    Public Property Image As Image

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed after the sprite is drawn.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed after the sprite is drawn.</returns>
    Public Property OnPostRender As Action(Of Graphics)

    ''' <summary>
    ''' Gets or sets a value determining the rotation angle at which the sprite will be drawn. Sprites are
    ''' rotated around their position.
    ''' </summary>
    ''' <returns>The rotation angle at which the sprite will be drawn.</returns>
    Public Property Rotation As Single

    ''' <summary>
    ''' Gets or sets a value determining the scale at which the sprite will be drawn.
    ''' </summary>
    ''' <returns>The scale at which the sprite will be drawn.</returns>
    Public Property Scale As Single = 1.0!

    ''' <summary>
    ''' Gets or sets the size of the sprite in pixels when drawn at 100% scale.
    ''' </summary>
    ''' <returns>The size of the sprite in pixels when drawn at 100% scale.</returns>
    Public Property Size As SizeF

    ''' <summary>
    ''' Gets or sets a value which determines the drawing order of the sprite (lower values are drawn first).
    ''' </summary>
    ''' <returns>A number representing the draw order of the sprite, with lower values being drawn first and
    ''' higher values being drawn on top.</returns>
    Public Property ZOrder As Single

    ''' <summary>
    ''' Gets a circle located at the sprite's position with a radius equal to the sprite's collision radius.
    ''' </summary>
    ''' <returns>The collision circle bounding this sprite.</returns>
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
