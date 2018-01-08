Namespace Animation
    Public Class SpriteSheetAnimation
        Implements IAnimation
        Public Property Active As Boolean = True Implements IAnimation.Active
        Public Property Name As String Implements IAnimation.Name
        Public Property Frames As New List(Of Point)
        Public Property AnimationTime As Single Implements IAnimation.AnimationTime

        Private frameIndex As Integer
        Private frameRemaining As Single

        Public Function GetFramePosition() As Point Implements IAnimation.GetFramePosition
            If Frames.Count = 0 Then Return Point.Empty
            Return Frames(frameIndex)
        End Function

        Protected Friend Overridable Sub Update(state As GameState) Implements IAnimation.Update
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


