Imports PuppyBreath
''' <summary>
''' Represents a GameObject which also includes a visual appearance on screen.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class Sprite
    Inherits GameObject
    Implements IRenderable

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
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a sprite instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed after the sprite is drawn.</returns>
    Public Property OnPostRender As Action(Of Graphics, GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed before the sprite is drawn.
    ''' Set this to a Sub(state As RenderState) lambda method to add functionality to a sprite instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed before the sprite is drawn.</returns>
    Public Property OnPreRender As Action(Of RenderState)

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
    ''' Gets or sets a value that determines if the sprite is drawn.
    ''' </summary>
    ''' <returns>True if the sprite is visisble, otherwise false.</returns>
    Public Property Visible As Boolean Implements IRenderable.Visible

    ''' <summary>
    ''' Gets or sets a value which determines the drawing order of the sprite (lower values are drawn first).
    ''' </summary>
    ''' <returns>A number representing the draw order of the sprite, with lower values being drawn first and
    ''' higher values being drawn on top.</returns>
    Public Property ZOrder As Single Implements IRenderable.ZOrder

    ''' <summary>
    ''' Allows the sprite to perform any initialization which should occur before the object begins
    ''' executing in the scene, or after being reset. This method does not execute if the object has already been initialized, unless it is reset.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is initialized.</param>
    Protected Overrides Sub Initialize(state As GameState)
        MyBase.Initialize(state)
        Visible = True
    End Sub

    ''' <summary>
    ''' Allows a chance to execute custom code before rendering begins and/or bypass default rendering (post rendering will still occur).
    ''' </summary>
    ''' <param name="state">A RenderState containing the current graphics, game state and default rendering flag.</param>
    Protected Friend Overridable Sub PreRender(state As RenderState)
        OnPreRender?.Invoke(state)
    End Sub

    ''' <summary>
    ''' Allows custom drawing after the default Render has occured.
    ''' </summary>
    ''' <param name="g">The graphics instance to draw with.</param>
    ''' <param name="state">The current game state.</param>
    Protected Friend Overridable Sub PostRender(g As Graphics, state As GameState)
        OnPostRender?.Invoke(g, state)
    End Sub

    ''' <summary>
    ''' Performs the default rendering of the sprite.
    ''' </summary>
    ''' <param name="g">The graphics instance to draw with.</param>
    ''' <param name="state">The current game state.</param>
    Protected Friend Overridable Sub Render(g As Graphics, state As GameState) Implements IRenderable.Render
        If IsDestroyed Then Return
        Dim rstate As New RenderState(g, state)
        PreRender(rstate)
        If rstate.PerformDefaultRendering Then
            If Image IsNot Nothing Then
                Dim srcBounds As New Rectangle(FrameLocation, FrameSize)
                Dim dstBounds As New Rectangle(-Size.Width * 0.5!, -Size.Height * 0.5!, Size.Width, Size.Height)
                g.TranslateTransform(Position.X, Position.Y)
                g.RotateTransform(Rotation)
                g.ScaleTransform(Scale, Scale)
                g.DrawImage(Image, dstBounds, srcBounds, GraphicsUnit.Pixel)
                g.ResetTransform()
            End If
        End If
        PostRender(g, state)
    End Sub

End Class
