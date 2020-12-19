Public Class RenderState
    Public ReadOnly GameState As GameState
    Public ReadOnly Graphics As Graphics
    Public Property PerformDefaultRendering As Boolean

    Public Sub New(g As Graphics, state As GameState)
        GameState = state
        Graphics = g
        PerformDefaultRendering = True
    End Sub
End Class
