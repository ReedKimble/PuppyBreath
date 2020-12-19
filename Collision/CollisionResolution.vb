Namespace Collision
    Public Class CollisionResolution

        Public Property OnInitialize As Action(Of GameState)
        Public ReadOnly Property Resolvers As New Dictionary(Of CollisionResolutionKey, Func(Of ColliderBase, ColliderBase, PointF?))

        Public Sub New(state As GameState)
            Initialize(state)
        End Sub

        Protected Overridable Sub Initialize(state As GameState)
            Resolvers(CollisionResolutionKey.CreateNew(Of CircleCollider, CircleCollider)) = Function(value1 As ColliderBase, value2 As ColliderBase)
                                                                                                 Dim c1 = DirectCast(value1, CircleCollider)
                                                                                                 Dim c2 = DirectCast(value2, CircleCollider)
                                                                                                 If Mathf.IntersectCircleCircle(c1.Position, c1.Radius, c2.Position, c2.Radius) Then
                                                                                                     Return Mathf.GetPositionToward(c1.Position, c2.Position, c1.Radius)
                                                                                                 End If
                                                                                             End Function

            Resolvers(CollisionResolutionKey.CreateNew(Of CircleCollider, PolygonCollider)) = Function(value1 As ColliderBase, value2 As ColliderBase)
                                                                                                  Dim result As New PointF?
                                                                                                  If TypeOf (value1) Is CircleCollider Then
                                                                                                      Dim c1 = DirectCast(value1, CircleCollider)
                                                                                                      Dim c2 = DirectCast(value2, PolygonCollider)
                                                                                                      If Mathf.IntersectCirclePolygon(c2.GetPolygonPoints, c1.Position, c1.Radius, result) Then
                                                                                                          Return result
                                                                                                      End If
                                                                                                  Else
                                                                                                      Dim c1 = DirectCast(value1, PolygonCollider)
                                                                                                      Dim c2 = DirectCast(value2, CircleCollider)
                                                                                                      If Mathf.IntersectCirclePolygon(c1.GetPolygonPoints, c2.Position, c2.Radius, result) Then
                                                                                                          Return result
                                                                                                      End If
                                                                                                  End If

                                                                                              End Function

            Resolvers(CollisionResolutionKey.CreateNew(Of PolygonCollider, PolygonCollider)) = Function(value1 As ColliderBase, value2 As ColliderBase)
                                                                                                   Dim result As New PointF?
                                                                                                   Dim c1 = DirectCast(value1, PolygonCollider)
                                                                                                   Dim c2 = DirectCast(value2, PolygonCollider)
                                                                                                   Dim p1 = c1.GetPolygonPoints
                                                                                                   Dim p2 = c2.GetPolygonPoints
                                                                                                   If Mathf.IntersectPolygonPolygon(p1, p2, result) Then
                                                                                                       Return result
                                                                                                   End If
                                                                                               End Function
            OnInitialize?.Invoke(state)
        End Sub

        Public Overridable Function Resolve(value1 As ColliderBase, value2 As ColliderBase) As PointF?
            Dim key = New CollisionResolutionKey(value1, value2)
            If Resolvers.ContainsKey(key) Then Return Resolvers(key)?.Invoke(value1, value2)
            Return Nothing
        End Function
    End Class
End Namespace
