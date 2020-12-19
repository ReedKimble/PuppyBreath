''' <summary>
''' Provides access to information about the current state of the game engine, along with access
''' to the game audio and input.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameState
    ''' <summary>
    ''' Gets the active GameAudio instance for the game. Use this to play sounds.
    ''' </summary>
    ''' <returns>The active GameAudio for the game.</returns>
    Public ReadOnly Property Audio As GameAudio
    ''' <summary>
    ''' Gets a collection of named audio players that can be used to associate playing audio with the scene.
    ''' </summary>
    ''' <returns>A dictionary of String/GameAudioPlayer key-value pairs.</returns>
    Public ReadOnly Property AudioPlayers As Dictionary(Of String, GameAudioPlayer)

    ''' <summary>
    ''' Gets an object providing caching services for the reuse of game objects in a scene.
    ''' </summary>
    ''' <returns>An object providing caching services for the reuse of game objects in a scene.</returns>
    Public ReadOnly Property Cache As ObjectCache

    ''' <summary>
    ''' Gets the current bounding rectangle of the RenderCanvas' client area.
    ''' </summary>
    ''' <returns>The current bounding rectangle of the RenderCanvas' client area.</returns>
    Public ReadOnly Property CanvasBounds As Rectangle
        Get
            Return canvas.DisplayRectangle
        End Get
    End Property

    Public ReadOnly Property CollisionResolution As Collision.CollisionResolution

    ''' <summary>
    ''' Gets the active GameInput instance for the game. Use this to check player keyboard and mouse input.
    ''' </summary>
    ''' <returns>The active GameInput for the game.</returns>
    Public ReadOnly Property Input As GameInput

    ''' <summary>
    ''' Gets an instance of System.Random for consistent random number generation throughout the game.
    ''' </summary>
    ''' <returns>An instance of System.Random</returns>
    Public ReadOnly Property Random As Random

    ''' <summary>
    ''' Gets a reference to the current scene loaded in the RenderCanvas.
    ''' </summary>
    ''' <returns>A reference to the current scene loaded in the RenderCanvas.</returns>
    Public ReadOnly Property Scene As GameScene

    ''' <summary>
    ''' Gets a reference to the game's scene manager instance.
    ''' </summary>
    ''' <returns>A reference to the game's scene manager instance.</returns>
    Public ReadOnly Property SceneManager As GameSceneManager

    ''' <summary>
    ''' Gets the active GameTime instance for the game. Use this to track execution time and calculate movement and
    ''' other action-over-time logic.
    ''' </summary>
    ''' <returns>The active GameTime for the game.</returns>
    Public ReadOnly Property Time As GameTime

    ''' <summary>
    ''' Gets an object containing named collections of flags, numbers and text. Use this to store game variable
    ''' data in connection with the current game state. This data will be available to all active game objects.
    ''' </summary>
    ''' <returns>An object containing named collections of flags, numbers and text.</returns>
    Public ReadOnly Property Variables As VariableBank

    Private canvas As RenderCanvas

    Public Sub New(renderCanvas As RenderCanvas)
        Audio = New GameAudio
        AudioPlayers = New Dictionary(Of String, GameAudioPlayer)
        Cache = New ObjectCache
        canvas = renderCanvas
        CollisionResolution = New Collision.CollisionResolution(Me)
        Input = New GameInput
        Random = New Random(renderCanvas.RandomSeed)
        Time = New GameTime
        Variables = New VariableBank
    End Sub

    Public Function GetRandomPosition(margin As Single) As PointF
        Return New PointF(Random.Next(CanvasBounds.Left + margin, CanvasBounds.Right - margin), Random.Next(CanvasBounds.Top + margin, CanvasBounds.Bottom - margin))
    End Function

    Public Function RayCast(startPoint As PointF, directionAngle As Single, distance As Single) As IEnumerable(Of GameObject)
        Return (From obj In Scene.GameObjects
                Where obj.HasCollision AndAlso
                    obj.Collider IsNot Nothing AndAlso
                    Not obj.IsDestroyed AndAlso
                    obj.Collider.IntersectsLine(startPoint,
                    Mathf.GetPositionToward(startPoint, directionAngle,
                    distance, Mathf.AngleUnit.Degrees))).ToArray
        'Dim result As New List(Of GameObject)
        'Dim endPoint = Mathf.GetPositionToward(startPoint, directionAngle, distance, Mathf.AngleUnit.Degrees)
        'For Each obj In Scene.GameObjects
        '    If Not obj.IsDestroyed AndAlso obj.CheckCollision AndAlso obj.Collider IsNot Nothing Then
        '        If obj.Collider.IntersectsLine(startPoint, endPoint) Then result.Add(obj)
        '    End If
        'Next
    End Function

    Public Sub Reset()
        _Random = New Random(canvas.RandomSeed)
        Time.Reset()
        Variables.Clear()
    End Sub

    'Factor this out?
    Protected Friend Sub SetScene(gameScene As GameScene, manager As GameSceneManager)
        _Scene = gameScene
        _SceneManager = manager
    End Sub
End Class
