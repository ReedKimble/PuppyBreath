Namespace Collision
    Public Class CollisionResolutionKey
        Implements IEquatable(Of CollisionResolutionKey)

        Public Shared Function CreateNew(Of TCollider1 As ColliderBase, TCollider2 As ColliderBase)() As CollisionResolutionKey
            Dim result As New CollisionResolutionKey
            result.SetTypes(Of TCollider1, TCollider2)()
            Return result
        End Function

        Public Collider1 As Type
        Public Collider2 As Type

        Public Sub New()
        End Sub

        Public Sub New(value1 As ColliderBase, value2 As ColliderBase)
            Collider1 = value1.GetType()
            Collider2 = value2.GetType()
        End Sub

        Public Sub SetTypes(Of TCollider1 As ColliderBase, TCollider2 As ColliderBase)()
            Collider1 = GetType(TCollider1)
            Collider2 = GetType(TCollider2)
        End Sub

        Public Overrides Function GetHashCode() As Integer
            Dim n1, n2 As String
            If Collider1.Name > Collider2.Name Then
                n1 = Collider1.Name
                n2 = Collider2.Name
            Else
                n1 = Collider2.Name
                n2 = Collider1.Name
            End If
            Return (n1 & n2).GetHashCode
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf (obj) Is ColliderBase Then Return Equals(DirectCast(obj, ColliderBase))
            Return False
        End Function

        Public Overloads Function Equals(other As CollisionResolutionKey) As Boolean Implements IEquatable(Of CollisionResolutionKey).Equals
            Return (Collider1 Is other.Collider1 AndAlso Collider2 Is other.Collider2) OrElse
                (Collider1 Is other.Collider2 AndAlso Collider2 Is other.Collider1)
        End Function
    End Class
End Namespace

