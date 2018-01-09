Namespace Animation
    ''' <summary>
    ''' Represents information about an animation.
    ''' </summary>
    ''' <author>Reed Kimble 01/08/2018</author>
    Public Interface IAnimation
        Property Active As Boolean
        Property AnimationTime As Single
        Property Name As String
        Function GetFramePosition() As Point
        Sub Update(target As AnimatedSprite, state As GameState)
    End Interface
End Namespace
