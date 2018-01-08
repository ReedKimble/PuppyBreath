Imports PuppyBreath.Animation

''' <summary>
''' Represents a Sprite GameObject with multiple frames of images which play in sequence over time.
''' </summary>
Public Class AnimatedSprite
    Inherits Sprite

    Public Property Animations As New AnimationCollection
    Public Property CurrentAnimationName As String

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
            anim.Update(state)
            FrameLocation = anim.GetFramePosition
        End If
    End Sub
End Class