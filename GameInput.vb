Public NotInheritable Class GameInput

    Protected Friend Sub New()
    End Sub

    Private currentKeyboardState As New Dictionary(Of Keys, Boolean)
    Private currentMouseButtonState As New Dictionary(Of MouseButtons, Boolean)
    Private currentMousePosition As PointF
    Private namedKeyMap As New Dictionary(Of String, List(Of Keys))
    Private lastKeyboardState As New Dictionary(Of Keys, Boolean)
    Private lastMouseButtonState As New Dictionary(Of MouseButtons, Boolean)
    Private lastMousePosition As PointF

    Public Sub ClearKeyMap(name As String)
        If namedKeyMap.ContainsKey(name) Then namedKeyMap(name).Clear()
    End Sub

    Public Function DidMouseMove() As Boolean
        Return currentMousePosition <> lastMousePosition
    End Function

    Public Function GetKeyMap(name As String) As List(Of Keys)
        If Not namedKeyMap.ContainsKey(name) Then namedKeyMap(name) = New List(Of Keys)
        Return namedKeyMap(name)
    End Function

    Public Function GetMousePosition() As PointF
        Return currentMousePosition
    End Function

    Public Function IsButtonDown(button As MouseButtons) As Boolean
        If currentMouseButtonState.ContainsKey(button) Then Return currentMouseButtonState(button)
        Return False
    End Function

    Public Function IsKeyDown(keyMapName As String) As Boolean
        For Each k In GetKeyMap(keyMapName)
            If IsKeyDown(k) Then Return True
        Next
        Return False
    End Function

    Public Function IsKeyDown(key As Keys) As Boolean
        If currentKeyboardState.ContainsKey(key) Then Return currentKeyboardState(key)
        Return False
    End Function

    Public Sub RemoveKeyMap(name As String)
        If namedKeyMap.ContainsKey(name) Then namedKeyMap.Remove(name)
    End Sub

    Public Function WasButtonPressed(button As MouseButtons) As Boolean
        If lastMouseButtonState.ContainsKey(button) Then
            If lastMouseButtonState(button) AndAlso Not currentMouseButtonState(button) Then Return True
        End If
        Return False
    End Function

    Public Function WasKeyPressed(keyMapName As String) As Boolean
        For Each k In GetKeyMap(keyMapName)
            If WasKeyPressed(k) Then Return True
        Next
        Return False
    End Function

    Public Function WasKeyPressed(key As Keys) As Boolean
        If lastKeyboardState.ContainsKey(key) Then
            If lastKeyboardState(key) AndAlso Not currentKeyboardState(key) Then Return True
        End If
        Return False
    End Function

    Protected Friend Sub ResetCurrentState()
        For Each key In currentKeyboardState.Keys.ToArray
            currentKeyboardState(key) = False
        Next
        For Each key In currentMouseButtonState.Keys.ToArray
            currentMouseButtonState(key) = False
        Next
    End Sub

    Protected Friend Sub SetMouseDown(button As MouseButtons)
        currentMouseButtonState(button) = True
    End Sub

    Protected Friend Sub SetMouseUp(button As MouseButtons)
        currentMouseButtonState(button) = False
    End Sub

    Protected Friend Sub SetKeyDown(key As Keys)
        currentKeyboardState(key) = True
    End Sub

    Protected Friend Sub SetKeyUp(key As Keys)
        currentKeyboardState(key) = False
    End Sub

    Protected Friend Sub Update(mousePosition As PointF)
        For Each key In currentKeyboardState.Keys
            lastKeyboardState(key) = currentKeyboardState(key)
        Next
        For Each key In currentMouseButtonState.Keys
            lastMouseButtonState(key) = currentMouseButtonState(key)
        Next
        lastMousePosition = currentMousePosition
        currentMousePosition = mousePosition
    End Sub
End Class
