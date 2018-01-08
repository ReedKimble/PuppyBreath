Public Class GameScene
    Public ReadOnly Property AudioPlayers As New Dictionary(Of String, GameAudioPlayer)
    Public ReadOnly Property GameObjects As New List(Of GameObject)
    Public ReadOnly Property IsInitialized As Boolean
    Public Property OnInitialize As Action(Of GameState)
    Public Property Name As String

    Protected Friend Sub Initialize(state As GameState)
        If _IsInitialized Then Return
        OnInitialize?.Invoke(state)
        _IsInitialized = True
    End Sub
End Class
