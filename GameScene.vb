''' <summary>
''' Represents a scene within the game comprised of game objects, playing audio, and state variables.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameScene
    ''' <summary>
    ''' Gets a collection of named audio players that can be used to associate playing audio with the scene.
    ''' </summary>
    ''' <returns>A dictionary of String/GameAudioPlayer key-value pairs.</returns>
    Public ReadOnly Property AudioPlayers As New Dictionary(Of String, GameAudioPlayer)

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
    ''' Gets or sets a value which points to an external method that will be executed when the current scene is changed from this instance.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the current scene is changed from this instance.</returns>
    Public Property OnChangeFrom As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the current scene is changed to this instance.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the current scene is changed to this instance.</returns>
    Public Property OnChangeTo As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when this scene is intialized.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game scene instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the current scene is intialized.</returns>
    Public Property OnInitialize As Action(Of GameState)

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

    Protected Friend Overridable Sub ChangeFrom(state As GameState)
        OnChangeFrom?.Invoke(state)
    End Sub

    Protected Friend Overridable Sub ChangeTo(state As GameState)
        OnChangeTo?.Invoke(state)
    End Sub

    Protected Friend Overridable Sub Initialize(state As GameState)
        If _IsInitialized Then Return
        OnInitialize?.Invoke(state)
        _IsInitialized = True
    End Sub

    Protected Friend Overridable Sub Update(state As GameState)
        OnUpdate?.Invoke(state)
    End Sub
End Class
