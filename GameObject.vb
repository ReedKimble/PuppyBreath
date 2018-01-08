''' <summary>
''' Represents an game entity with per-frame processing, position and collision, but no visual representation.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameObject

    ''' <summary>
    ''' Gets or sets a value that determines whether or not this object participates in collision detection.
    ''' </summary>
    ''' <returns>True if collision is enabled on the game object, otherwise false.</returns>
    Public Property CheckCollision As Boolean

    ''' <summary>
    ''' Gets or sets a value that specifies the radius of the collision circle around the object's position.
    ''' This value is only used if value of the CheckCollision property is true.
    ''' </summary>
    ''' <returns>The collision radius in pixels.</returns>
    Public Property CollisionRadius As Single

    ''' <summary>
    ''' Gets a collection containing a CollisionInfo instance for each object that this object had collision with on the last frame.
    ''' </summary>
    ''' <returns>An IEnumerable collection of CollisionInfo for each object collided with last frame.</returns>
    Public ReadOnly Property Collisions As IEnumerable(Of CollisionInfo) = New List(Of CollisionInfo)

    ''' <summary>
    ''' Gets a value indicating if this object has been disabled and removed from the scene.
    ''' A Destroyed object can be re-used by calling the Reset() method and adding the object to the scene.
    ''' </summary>
    ''' <returns>True if the object has been destroyed, otherwise false.</returns>
    Public ReadOnly Property IsDestroyed As Boolean

    ''' <summary>
    ''' Gets a value indicating if the object has been initialized.  An object is only initialized once per lifetime.
    ''' A destroyed object is initialized if it is reset and added to the scene (this constitutes a new lifetime).
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsInitialized As Boolean

    Public Property OnDestroyed As Action(Of GameState)
    Public Property OnDestroying As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is updated.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is updated by the game engine.</returns>
    Public Property OnUpdate As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets the pixel location of the game object within the world space.
    ''' </summary>
    ''' <returns>A point containing the pixel location of the game object in world space.</returns>
    Public Property Position As PointF

    '<dev>:
    ' This flag will be set internally when Destroy() is called to indicate that the game object is
    ' ready for destruction and should be removed by the game engine on the next pass of the game loop.
    Private flaggedForDesctruction As Boolean

    ''' <summary>
    ''' Flags the game object for destruction by the game engine. The object will be disabled and
    ''' removed from the scene on the next pass of the game loop after Destroy() is called.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is destroyed.</param>
    Public Overridable Sub Destroy(state As GameState)
        If _IsDestroyed Then Exit Sub
        flaggedForDesctruction = True
        OnDestroying?.Invoke(state)
    End Sub

    '<dev>:
    ' This method is used internally by the game engine to indicate that the process of
    ' destroying the object and removing it from the scene is complete.
    Protected Friend Overridable Sub FinializeDestruction(state As GameState)
        _IsDestroyed = True
        OnDestroyed?.Invoke(state)
    End Sub

    ''' <summary>
    ''' Gets a circle located at the game object's current position with a radius of the game object's collision radius.
    ''' </summary>
    ''' <returns>A circle with the game object's collision radius, centered on the game object's position.</returns>
    Public Overridable Function GetCollisionBounds() As Circle
        Return New Circle(Position, CollisionRadius)
    End Function

    ''' <summary>
    ''' Allows the game object to perform any 1-time initialization which should occur before the object begins
    ''' executing in the scene. This method does not execute if the object has already been initialized.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is initialized.</param>
    Protected Overridable Sub Initialize(state As GameState)
        If _IsInitialized Then Exit Sub
        _IsInitialized = True
    End Sub

    '<dev>:
    ' This method is used internally by the engine to determine if this object has been flagged for destruction.
    Protected Friend Function IsFlaggedForDestruction() As Boolean
        Return flaggedForDesctruction
    End Function

    ''' <summary>
    ''' Resets a destroyed game object by clearing the initialized, destroyed, and flaggedForDestruction flags
    ''' so that it can be reused in the scene. The object is reinitialized on the first
    ''' pass of the game loop after adding the object to the scene.
    ''' </summary>
    Public Overridable Sub Reset()
        _IsInitialized = False
        _IsDestroyed = False
        flaggedForDesctruction = False
    End Sub

    ''' <summary>
    ''' Allows the game object to perform any game logic processing on each iteration of the game loop. This method only
    ''' executes if the object is not destroyed, will initialize the object if it is not already initialized, and will
    ''' call the OnUpdate delegate if one has been assigned.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is updated by the game loop.</param>
    Protected Friend Overridable Sub Update(state As GameState)
        If IsDestroyed Then Return
        If Not _IsInitialized Then Initialize(state)
        OnUpdate?.Invoke(state)
    End Sub
End Class
