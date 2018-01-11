Imports PuppyBreath

Public NotInheritable Class GameSceneManager
    Inherits ObjectModel.KeyedCollection(Of String, GameScene)

    ''' <summary>
    ''' Gets a reference to the currently executing game scene.
    ''' </summary>
    ''' <returns>A reference to the currently executing game scene.</returns>
    Public ReadOnly Property CurrentScene As GameScene
    Public ReadOnly Property IsSceneChangeQueued As Boolean
        Get
            Return queuedScene IsNot Nothing
        End Get
    End Property

    Private canvas As RenderCanvas
    Private queuedScene As GameScene

    Protected Friend Sub New(renderCanvas As RenderCanvas)
        canvas = renderCanvas
    End Sub

    Public Sub ChangeScene(sceneName As String)
        If Contains(sceneName) Then ChangeScene(Item(sceneName))
    End Sub

    ''' <summary>
    ''' Changes the currently executing game scene to the new scene.
    ''' </summary>
    ''' <param name="newScene">The new GameScene to begin executing.</param>
    Public Sub ChangeScene(newScene As GameScene)
        queuedScene = newScene
    End Sub

    Protected Friend Sub ExecuteSceneChange(state As GameState)
        '_CurrentScene?.ChangeFrom(canvas.state)
        _CurrentScene = queuedScene
        canvas.state.SetScene(queuedScene, Me)
        'queuedScene?.ChangeTo(canvas.state)
        queuedScene = Nothing
    End Sub

    Protected Overrides Function GetKeyForItem(item As GameScene) As String
        Return item.Name
    End Function

    Protected Overrides Sub InsertItem(index As Integer, item As GameScene)
        If Contains(item.Name) Then Return
        MyBase.InsertItem(index, item)
    End Sub

    Protected Overrides Sub SetItem(index As Integer, item As GameScene)
        If Contains(item.Name) Then Return
        MyBase.SetItem(index, item)
    End Sub
End Class
