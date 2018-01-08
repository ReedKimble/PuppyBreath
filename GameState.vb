Public Class GameState
    Public ReadOnly Property Audio As New GameAudio
    Public ReadOnly Property Cache As New ObjectCache
    Public ReadOnly Property CanvasBounds As Rectangle
    Public ReadOnly Property Input As New GameInput
    Public ReadOnly Property Random As New Random
    Public ReadOnly Property Scene As GameScene
    Public ReadOnly Property Time As New GameTime

    Protected Friend Sub SetBounds(bounds As Rectangle)
        _CanvasBounds = bounds
    End Sub
    Protected Friend Sub SetScene(gameScene As GameScene)
        _Scene = gameScene
    End Sub
End Class
