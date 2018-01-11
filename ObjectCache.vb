''' <summary>
''' Provides caching of game objects for reuse within a scene.  Any objects which are frequently created and
''' destroyed (such as bullets, enemies and item pickups) should be created using the ObjectCache.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class ObjectCache

    Private typeLists As New Dictionary(Of Type, List(Of GameObject))

    ''' <summary>
    ''' Gets an instance of a GameObject using the cache and adds it to the current scene.
    ''' </summary>
    ''' <typeparam name="T">The type of GameObject to get.</typeparam>
    ''' <param name="state">The current game state.</param>
    ''' <returns>An instance of the specified type of GameObject.</returns>
    Public Function AddInstanceToScene(Of T As {New, GameObject})(state As GameState) As T
        Dim result = GetInstance(Of T)(state)
        state.Scene.GameObjects.Add(result)
        Return result
    End Function

    ''' <summary>
    ''' Removes all cached game objects.
    ''' </summary>
    Public Sub Clear()
        typeLists.Clear()
    End Sub

    ''' <summary>
    ''' Gets an instance of a GameObject. If one has been previously destroyed, it is reset and
    ''' returned, otherwise a new instance is created.
    ''' </summary>
    ''' <typeparam name="T">The type of GameObject to get.</typeparam>
    ''' <param name="state">The current game state.</param>
    ''' <returns>An instance of the specified type of GameObject.</returns>
    Public Function GetInstance(Of T As {New, GameObject})(state As GameState) As T
        Dim objects = GetTypeList(Of T)()

        For Each o In objects
            If o.IsDestroyed Then
                o.Reset(state)
                Return CType(o, T)
            End If
        Next
        Dim n As New T
        objects.Add(n)
        Return n
    End Function

    Private Function GetTypeList(Of T As {New, GameObject})() As List(Of GameObject)
        Dim typeT As Type = GetType(T)
        If Not typeLists.ContainsKey(typeT) Then typeLists(typeT) = New List(Of GameObject)
        Return typeLists(typeT)
    End Function
End Class
