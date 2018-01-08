Namespace Animation
    Public Interface IAnimation
        Property Active As Boolean
        Property AnimationTime As Single
        Property Name As String
        Function GetFramePosition() As Point
        Sub Update(state As GameState)
    End Interface
End Namespace
