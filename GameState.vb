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
    Public ReadOnly Property Audio As New GameAudio
    ''' <summary>
    ''' Gets a collection of named audio players that can be used to associate playing audio with the scene.
    ''' </summary>
    ''' <returns>A dictionary of String/GameAudioPlayer key-value pairs.</returns>
    Public ReadOnly Property AudioPlayers As New Dictionary(Of String, GameAudioPlayer)

    ''' <summary>
    ''' Gets an object providing caching services for the reuse of game objects in a scene.
    ''' </summary>
    ''' <returns>An object providing caching services for the reuse of game objects in a scene.</returns>
    Public ReadOnly Property Cache As New ObjectCache

    ''' <summary>
    ''' Gets the current bounding rectangle of the RenderCanvas' client area.
    ''' </summary>
    ''' <returns>The current bounding rectangle of the RenderCanvas' client area.</returns>
    Public ReadOnly Property CanvasBounds As Rectangle

    ''' <summary>
    ''' Gets the active GameInput instance for the game. Use this to check player keyboard and mouse input.
    ''' </summary>
    ''' <returns>The active GameInput for the game.</returns>
    Public ReadOnly Property Input As New GameInput

    ''' <summary>
    ''' Gets an instance of System.Random for consistent random number generation throughout the game.
    ''' </summary>
    ''' <returns>An instance of System.Random</returns>
    Public ReadOnly Property Random As New Random

    ''' <summary>
    ''' Gets a reference to the current scene loaded in the RenderCanvas.
    ''' </summary>
    ''' <returns>A reference to the current scene loaded in the RenderCanvas.</returns>
    Public ReadOnly Property Scene As GameScene

    Public ReadOnly Property SceneManager As GameSceneManager

    ''' <summary>
    ''' Gets the active GameTime instance for the game. Use this to track execution time and calculate movement and
    ''' other action-over-time logic.
    ''' </summary>
    ''' <returns>The active GameTime for the game.</returns>
    Public ReadOnly Property Time As New GameTime

    ''' <summary>
    ''' Gets an object containing named collections of flags, numbers and text. Use this to store game variable
    ''' data in connection with the current game state. This data will be available to all active game objects.
    ''' </summary>
    ''' <returns>An object containing named collections of flags, numbers and text.</returns>
    Public ReadOnly Property Variables As New VariableBank

    Protected Friend Sub SetBounds(bounds As Rectangle)
        _CanvasBounds = bounds
    End Sub

    Protected Friend Sub SetScene(gameScene As GameScene, manager As GameSceneManager)
        _Scene = gameScene
        _SceneManager = manager
    End Sub
End Class
