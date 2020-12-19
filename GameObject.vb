''' <summary>
''' Represents an game entity with per-frame processing, position and collision, but no visual representation.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
''' <remarks>Last Updated 12/19/2020</remarks>
Public Class GameObject
    <Obsolete>
    Const DEFAULT_COLLISION_RADIUS = 1

    ''' <summary>
    ''' Gets the collider used by this game object.
    ''' </summary>
    ''' <returns>The collider currently in use.</returns>
    Public ReadOnly Property Collider As Collision.ColliderBase

    ''' <summary>
    ''' Gets or sets a value that determines whether or not this object participates in collision detection.
    ''' </summary>
    ''' <returns>True if collision is enabled on the game object, otherwise false.</returns>
    <Obsolete("Use the Collider property instead. Setting this property true sets the Collider to a circle with CollisionRadius; setting it to false clears the current Collider.")>
    Public Property CheckCollision As Boolean
        Get
            Return Collider IsNot Nothing
        End Get
        Set(value As Boolean)
            If value Then
                If Collider Is Nothing Then _Collider = Collision.CircleCollider.CreateNew(DEFAULT_COLLISION_RADIUS)
            Else
                _Collider = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets a value indicating if the game object has a collider and participates in collision.
    ''' </summary>
    ''' <returns>True if the game object has a collider, otherwise false.</returns>
    Public ReadOnly Property HasCollision As Boolean
        Get
            Return Collider IsNot Nothing
        End Get
    End Property

    '*** Refactored collision into collision object to support circle and polygon colliders***
    '
    ''' <summary>
    ''' * Obsolete: DO NOT USE in new development.  Use Collider objects from the Collision namespace instead.
    ''' Gets or sets a value that specifies the radius of the collision circle around the object's position.
    ''' This value is only used if value of the CheckCollision property is true.
    ''' </summary>
    ''' <returns>The collision radius in pixels.</returns>
    <Obsolete("Use the Collider property instead. Setting this property sets the Collider to a circle with the specified radius. Reading this property returns the radius of a circle collider, if set, otherwise and unspecified value.")>
    Public Property CollisionRadius As Single
        Get
            If Collider IsNot Nothing Then
                If TypeOf Collider Is Collision.CircleCollider Then
                    Return DirectCast(Collider, Collision.CircleCollider).Radius
                End If
            End If
            Return DEFAULT_COLLISION_RADIUS
        End Get
        Set(value As Single)
            If Collider IsNot Nothing Then
                If TypeOf Collider Is Collision.CircleCollider Then
                    DirectCast(Collider, Collision.CircleCollider).Radius = value
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets a collection containing a CollisionInfo instance for each object that this object had collision with on the last frame.
    ''' </summary>
    ''' <returns>An IEnumerable collection of CollisionInfo for each object collided with last frame.</returns>
    Public ReadOnly Property Collisions As IEnumerable(Of CollisionInfo) = New List(Of CollisionInfo)

    ''' <summary>
    ''' Gets or sets a value that determines whether or not this object is active in the game world. Disabled object are not updated or rendered.
    ''' </summary>
    ''' <returns></returns>
    Public Property Enabled As Boolean = True

    ''' <summary>
    ''' Gets the unique identifier assigned to this game object instance.
    ''' </summary>
    ''' <returns>The unique identifier assigned to this game object instance.</returns>
    Public ReadOnly Property InstanceId As Guid = Guid.NewGuid()

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

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object has collisions during a frame.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns></returns>
    Public Property OnCollisions As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is finished being destroyed.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is finished being destroyed by the game engine.</returns>
    Public Property OnDestroyed As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is flagged for destruction.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is flagged for destruction by the game engine.</returns>
    Public Property OnDestroying As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is initialized.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is initialized by the game engine.</returns>
    Public Property OnInitialize As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is initialized for the first time.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is initialized by the game engine.</returns>
    Public Property OnLoadOnce As Action(Of GameState)

    ''' <summary>
    ''' Gets or sets a value which points to an external method that will be executed when the object is reset.
    ''' Set this to a Sub(state As GameState) lambda method to add functionality to a game object instance without creating a decendent class.
    ''' </summary>
    ''' <returns>A delegate sub to be executed when the object is reset by game logic.</returns>
    Public Property OnReset As Action(Of GameState)

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

    Private loadOnceExecuted As Boolean

    ''' <summary>
    ''' Allows the game object to respond to collision events, indepenent of the Update method.
    ''' Code in this method should only affect the local game object state and changes to the
    ''' game world or other game objects should occur in the Update handler.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time collision occured.</param>
    Public Overridable Sub CollisionsOccured(state As GameState)
        If IsDestroyed OrElse Not Enabled Then Return
        OnCollisions?.Invoke(state)
    End Sub

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
    <Obsolete("Use the Collider property instead. This function relies on the obsolete CollisionRadius property whose value is not guaranteed.")>
    Public Overridable Function GetCollisionBounds() As Circle
        Return New Circle(Position, CollisionRadius)
    End Function

    ''' <summary>
    ''' Assigns a new collider to this game object.
    ''' </summary>
    ''' <param name="newCollider">The collider to use.</param>
    Public Sub AssignCollider(newCollider As Collision.ColliderBase)
        newCollider.owner = Me
        _Collider = newCollider
    End Sub

    ''' <summary>
    ''' Allows the game object to perform any initialization which should occur before the object begins
    ''' executing in the scene, or after being reset. This method does not execute if the object has already been initialized, unless it is reset.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is initialized.</param>
    Protected Overridable Sub Initialize(state As GameState)
        If _IsInitialized Then Exit Sub
        _IsInitialized = True
        OnInitialize?.Invoke(state)
        If Not loadOnceExecuted Then
            loadOnceExecuted = True
            LoadOnce(state)
        End If
    End Sub

    '<dev>:
    ' This method is used internally by the engine to determine if this object has been flagged for destruction.
    Protected Friend Function IsFlaggedForDestruction() As Boolean
        Return flaggedForDesctruction
    End Function

    ''' <summary>
    ''' Allows the game object to perform any one-time loading of assets or resources.  This method is only called once
    ''' during the lifetime of the object by Initialize (it is not called again after a reset).
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is initialized.</param>
    Protected Overridable Sub LoadOnce(state As GameState)
        OnLoadOnce?.Invoke(state)
    End Sub

    ''' <summary>
    ''' Resets a destroyed game object by clearing the initialized, destroyed, and flaggedForDestruction flags
    ''' and collisions so that it can be reused in the scene. The object is enabled and reinitialized on the first
    ''' pass of the game loop after adding the object to the scene.
    ''' </summary>
    Public Overridable Sub Reset(state As GameState)
        _IsInitialized = False
        _IsDestroyed = False
        _Enabled = True
        flaggedForDesctruction = False
        DirectCast(_Collisions, List(Of CollisionInfo)).Clear()
        OnReset?.Invoke(state)
    End Sub

    ''' <summary>
    ''' Allows the game object to perform any game logic processing on each iteration of the game loop. This method only
    ''' executes if the object is enabled and not destroyed, will initialize the object if it is not already initialized, 
    ''' and will call the OnUpdate delegate if one has been assigned.
    ''' </summary>
    ''' <param name="state">The current game state instance at the time the object is updated by the game loop.</param>
    Protected Friend Overridable Sub Update(state As GameState)
        If IsDestroyed OrElse Not Enabled Then Return
        If Not _IsInitialized Then Initialize(state)
        OnUpdate?.Invoke(state)
    End Sub
End Class
