''' <summary>
''' Represents information about the collision of two game objects, including a reference to each object and their point of contact.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class CollisionInfo
    ''' <summary>
    ''' Gets a reference to the game object on which the collision event occured.
    ''' </summary>
    ''' <returns>The game object on which collision occured.</returns>
    Public ReadOnly Property Source As GameObject

    ''' <summary>
    ''' Gets a reference to the game object that the source collided with.
    ''' </summary>
    ''' <returns>The game object that collided with the source object.</returns>
    Public ReadOnly Property Other As GameObject

    ''' <summary>
    ''' Gets the point of contact between the two game object colliders.
    ''' </summary>
    ''' <returns>The point at which the two game object colliders came into contact.</returns>
    Public ReadOnly Property CollisionPoint As PointF

    '<dev>:
    ' The game engine will create CollisionInfo instances internally as the result of collision evaluation.
    Protected Friend Sub New(sourceSprite As GameObject, otherSprite As GameObject, hit As PointF)
        Source = sourceSprite
        Other = otherSprite
        CollisionPoint = hit
    End Sub
End Class
