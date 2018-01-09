Imports PuppyBreath.Animation

''' <summary>
''' Represents a Sprite GameObject with multiple frames of images which play in sequence over time.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class AnimatedSprite
    Inherits Sprite

    ''' <summary>
    ''' Gets or sets the collection of animations that this sprite can play.
    ''' </summary>
    ''' <returns>An AnimationCollection containing the sprite's animations.</returns>
    Public Property Animations As New AnimationCollection

    ''' <summary>
    ''' Gets or sets the current animation name.
    ''' </summary>
    ''' <returns>A string containing the current animation name.</returns>
    Public Property CurrentAnimationName As String

    ''' <summary>
    ''' Gets the animation with CurrentAnimationName from the Animations collection.
    ''' </summary>
    ''' <returns>The Animation named by CurrentAnimationName if it exists in Animations, otherwise nothing.</returns>
    Public ReadOnly Property CurrentAnimation As IAnimation
        Get
            If Not String.IsNullOrEmpty(CurrentAnimationName) Then
                If Animations.Contains(CurrentAnimationName) Then
                    Return Animations(CurrentAnimationName)
                End If
            End If
            Return Nothing
        End Get
    End Property

    Protected Friend Overrides Sub Update(state As GameState)
        MyBase.Update(state)
        If Not String.IsNullOrEmpty(CurrentAnimationName) Then
            Dim anim = Animations(CurrentAnimationName)
            If anim Is Nothing Then Return
            anim.Update(Me, state)
            FrameLocation = anim.GetFramePosition
        End If
    End Sub
End Class