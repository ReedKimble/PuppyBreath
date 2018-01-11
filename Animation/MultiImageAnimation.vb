Namespace Animation
    ''' <summary>
    ''' Represents an animation comprised of multiple individual images.
    ''' </summary>
    ''' <author>Reed Kimble 01/09/2018</author>
    Public Class MultiImageAnimation
        Implements IAnimation

        ''' <summary>
        ''' Gets or sets a value indicating if the animation should be played.
        ''' </summary>
        ''' <returns>True if the animation should be played, otherwise false.</returns>
        Public Property Active As Boolean = True Implements IAnimation.Active

        Public Property IsLoop As Boolean Implements IAnimation.IsLoop

        ''' <summary>
        ''' Gets or sets the name of the animation.
        ''' </summary>
        ''' <returns>The name of the animation.</returns>
        Public Property Name As String Implements IAnimation.Name

        ''' <summary>
        ''' Gets the collection of individual images that make up this animation.
        ''' </summary>
        ''' <returns></returns>
        Public Property Frames As New List(Of Image)

        ''' <summary>
        ''' Gets the desired play time for one loop of the animation.
        ''' </summary>
        ''' <returns></returns>
        Public Property AnimationTime As Single Implements IAnimation.AnimationTime

        Private frameIndex As Integer
        Private frameRemaining As Single

        ''' <summary>
        ''' Creates and configures a new instance of MultiImageAnimation.
        ''' </summary>
        ''' <param name="name">The name of the animation.</param>
        ''' <param name="time">The animation run time.</param>
        ''' <param name="frameImages">The images which make up the animation.</param>
        ''' <returns>The newly configured MultiImageAnimation.</returns>
        Public Shared Function CreateAnimation(name As String, time As Single, frameImages As IEnumerable(Of Image), looping As Boolean) As MultiImageAnimation
            Dim anim As New MultiImageAnimation
            anim.Name = name
            anim.AnimationTime = time
            anim.Frames.AddRange(frameImages)
            anim.IsLoop = looping
            Return anim
        End Function

        ''' <summary>
        ''' Gets the position within the source image for the current frame of animation.
        ''' </summary>
        ''' <returns>The position within the source image for the current frame of animation.</returns>
        Public Function GetFramePosition() As Point Implements IAnimation.GetFramePosition
            Return Point.Empty
        End Function

        Protected Friend Overridable Sub Update(target As AnimatedSprite, state As GameState) Implements IAnimation.Update
            If Frames.Count = 0 Then Return
            If Not Active Then
                If IsLoop Then frameIndex = 0
                Return
            End If
            frameRemaining -= state.Time.LastFrame
            If frameRemaining <= 0 Then
                frameRemaining = AnimationTime / Frames.Count
                frameIndex += 1
                If frameIndex >= Frames.Count Then
                    If IsLoop Then
                        frameIndex = 0
                    Else
                        frameIndex -= 1
                        Active = False
                    End If
                End If
                target.Image = Frames(frameIndex)
            End If
        End Sub
    End Class
End Namespace

