Namespace Animation
    ''' <summary>
    ''' Represents an animation comprised of multiple frame images combined on a single image file.
    ''' </summary>
    ''' <author>Reed Kimble 01/08/2018</author>
    Public Class SpriteSheetAnimation
        Implements IAnimation

        ''' <summary>
        ''' Gets or sets a value indicating if the animation should be played.
        ''' </summary>
        ''' <returns>True if the animation should be played, otherwise false.</returns>
        Public Property Active As Boolean = True Implements IAnimation.Active

        ''' <summary>
        ''' Gets or sets the name of the animation.
        ''' </summary>
        ''' <returns>The name of the animation.</returns>
        Public Property Name As String Implements IAnimation.Name

        ''' <summary>
        ''' Gets the collection of frame points on the source image.
        ''' </summary>
        ''' <returns></returns>
        Public Property Frames As New List(Of Point)

        ''' <summary>
        ''' Gets the desired play time for one loop of the animation.
        ''' </summary>
        ''' <returns></returns>
        Public Property AnimationTime As Single Implements IAnimation.AnimationTime

        Private frameIndex As Integer
        Private frameRemaining As Single

        ''' <summary>
        ''' Creates and configures a new instance of SpriteSheetAnimation.
        ''' </summary>
        ''' <param name="name">The name of the animation.</param>
        ''' <param name="time">The animation runtime.</param>
        ''' <param name="offset">The image index offset of the first frame of the animation on the spritesheet.</param>
        ''' <param name="frameSize">The size of each frame on the spritesheet.</param>
        ''' <param name="frameCount">The number of frames in this animation.</param>
        ''' <param name="gridHorizontal">True if the frames are laid out Horizontally in the spritesheet, otherwise False if the frames are vertical.</param>
        ''' <returns>The newly configured SpriteSheetAnimation.</returns>
        Public Shared Function CreateAnimation(name As String, time As Single, offset As Point, frameSize As Size, frameCount As Integer, gridHorizontal As Boolean) As SpriteSheetAnimation
            Dim anim As New SpriteSheetAnimation
            anim.Name = name
            anim.AnimationTime = time
            anim.LoadFramesByGrid(offset, frameSize, frameCount, gridHorizontal)
            Return anim
        End Function

        ''' <summary>
        ''' Gets the position within the source image for the current frame of animation.
        ''' </summary>
        ''' <returns>The position within the source image for the current frame of animation.</returns>
        Public Function GetFramePosition() As Point Implements IAnimation.GetFramePosition
            If Frames.Count = 0 Then Return Point.Empty
            Return Frames(frameIndex)
        End Function

        ''' <summary>
        ''' Loads the frame collection according to a grid-layout in the spritesheet.
        ''' </summary>
        ''' <param name="frameIndexOffset">The image index offset of the first frame of the animation on the spritesheet.</param>
        ''' <param name="frameSize">The size of each frame on the spritesheet.</param>
        ''' <param name="count">The number of frames in this animation.</param>
        ''' <param name="horizontal">True if the frames are laid out Horizontally in the spritesheet, otherwise False if the frames are vertical.</param>
        Public Sub LoadFramesByGrid(frameIndexOffset As Size, frameSize As Size, count As Integer, horizontal As Boolean)
            Dim dst As New Point(frameIndexOffset.Width * frameSize.Width, frameIndexOffset.Height * frameSize.Height)
            Dim dx, dy As Integer
            If horizontal Then dx = 1 Else dy = 1
            For i = 0 To count - 1
                Frames.Add(dst)
                dst += New Size(frameSize.Width * dx, frameSize.Height * dy)
            Next
        End Sub

        Protected Friend Overridable Sub Update(target As AnimatedSprite, state As GameState) Implements IAnimation.Update
            If Frames.Count = 0 Then Return
            If Not Active Then frameIndex = 0 : Return
            frameRemaining -= state.Time.LastFrame
            If frameRemaining <= 0 Then
                frameRemaining = AnimationTime / Frames.Count
                frameIndex += 1
                If frameIndex >= Frames.Count Then frameIndex = 0
            End If
        End Sub
    End Class
End Namespace


