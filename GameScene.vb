''' <summary>
''' Represents a scene within the game comprised of game objects, playing audio, and state variables.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameScene
    ''' <summary>
    ''' Gets the collection of game objects currently associated with (loaded into) the scene.
    ''' </summary>
    ''' <returns>A list of GameObject instances currently loaded into the scene.</returns>
    Public ReadOnly Property GameObjects As New GameObjectCollection

    ''' <summary>
    ''' Gets a value indicated whether this scene instance has been initialized.
    ''' </summary>
    ''' <returns>True if the scene is initialized, otherwise false.</returns>
    Public ReadOnly Property IsInitialized As Boolean

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when this scene is intialized.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the current scene is intialized.</returns>
    Public Property OnInitialize As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the scene is initialized for the first time.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the scene is initialized by the game engine.</returns>
    Public Property OnLoadOnce As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the scene is reset.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the scene is reset by game logic.</returns>
    Public Property OnReset As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when this scene is updated.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the current scene is updated.</returns>
    Public Property OnUpdate As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets the name of the scene.
    ''' </summary>
    ''' <returns>A string containing the scene name.</returns>
    Public Property Name As String

    ''' <summary>
    ''' Gets an object containing name-keyed collections of flags, numbers and text.
    ''' </summary>
    ''' <returns>A VariableBank containing name-keyed collections of flags, numbers and text.</returns>
    Public ReadOnly Property Variables As New VariableBank

    Private loadOnceExecuted As Boolean

    Protected Friend Overridable Sub Initialize(state As GameState)
        If _IsInitialized Then Return
        OnInitialize?.Invoke(state)
        _IsInitialized = True
        If Not loadOnceExecuted Then
            loadOnceExecuted = True
            LoadOnce(state)
        End If
    End Sub

    Protected Friend Overridable Sub LoadOnce(state As GameState)
        OnLoadOnce?.Invoke(state)
    End Sub

    ''' <summary>
    ''' Resets a destroyed game scene by clearing the initialized flag and destroying all game objects
    ''' so that it can be reused. The scene is reinitialized on the first pass of the game loop after 
    ''' showing scene again.
    ''' </summary>
    Protected Friend Overridable Sub RequestReset(state As GameState)
        For Each g In GameObjects
            g.Destroy(state)
        Next
    End Sub

    Protected Friend Sub FinializeReset(state As GameState)
        _IsInitialized = False
        OnReset?.Invoke(state)
    End Sub

    Protected Friend Overridable Sub Update(state As GameState)
        OnUpdate?.Invoke(state)
    End Sub
End Class
