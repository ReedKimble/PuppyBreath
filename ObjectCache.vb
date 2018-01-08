
Public Class ObjectCache

    Private typeLists As New Dictionary(Of Type, List(Of GameObject))

    Public Function GetInstance(Of T As {New, GameObject})() As T
        Dim objects = GetTypeList(Of T)()

        For Each o In objects
            If o.IsDestroyed Then
                o.Reset()
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
