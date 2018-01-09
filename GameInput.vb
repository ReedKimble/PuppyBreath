''' <summary>
''' Provides access to information about the current input state. You do not create instances of this class in code,
''' rather, utilize the instance provided by GameState and RenderCanvas.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
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

    ''' <summary>
    ''' Removes all keys mapped to the specified name.
    ''' </summary>
    ''' <param name="name">The name of the keymap to clear.</param>
    Public Sub ClearKeyMap(name As String)
        If namedKeyMap.ContainsKey(name) Then namedKeyMap(name).Clear()
    End Sub

    ''' <summary>
    ''' Gets a value indicating if the mouse position has changed since the last frame.
    ''' </summary>
    ''' <returns>True if the mouse position has changed since the last frame, otherwise false.</returns>
    Public Function DidMouseMove() As Boolean
        Return currentMousePosition <> lastMousePosition
    End Function

    ''' <summary>
    ''' Gets the list of keys mapped to a name in the keymap.
    ''' </summary>
    ''' <param name="name">The name of the key list to get.</param>
    ''' <returns>A list of keys mapped to the name.</returns>
    Public Function GetKeyMap(name As String) As List(Of Keys)
        If Not namedKeyMap.ContainsKey(name) Then namedKeyMap(name) = New List(Of Keys)
        Return namedKeyMap(name)
    End Function

    ''' <summary>
    ''' The mouse position as of the last update.
    ''' </summary>
    ''' <returns>The mouse position as of the last update.</returns>
    Public Function GetMousePosition() As PointF
        Return currentMousePosition
    End Function

    ''' <summary>
    ''' Gets a value indicating whether a button is currently pressed.
    ''' </summary>
    ''' <param name="button">The button to check.</param>
    ''' <returns>True if the button is currently pressed, otherwise false.</returns>
    Public Function IsButtonDown(button As MouseButtons) As Boolean
        If currentMouseButtonState.ContainsKey(button) Then Return currentMouseButtonState(button)
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether a key in the keymap is currently pressed.
    ''' </summary>
    ''' <param name="keyMapName">The name of the keymap to check.</param>
    ''' <returns>True if a key is currently pressed, otherwise false.</returns>
    Public Function IsKeyDown(keyMapName As String) As Boolean
        For Each k In GetKeyMap(keyMapName)
            If IsKeyDown(k) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether a key is currently pressed.
    ''' </summary>
    ''' <param name="key">The key to check.</param>
    ''' <returns>True if the key is currently pressed, otherwise false.</returns>
    Public Function IsKeyDown(key As Keys) As Boolean
        If currentKeyboardState.ContainsKey(key) Then Return currentKeyboardState(key)
        Return False
    End Function

    ''' <summary>
    ''' Removes the keymap for the specified name.
    ''' </summary>
    ''' <param name="name">The name of the keymap to remove.</param>
    Public Sub RemoveKeyMap(name As String)
        If namedKeyMap.ContainsKey(name) Then namedKeyMap.Remove(name)
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether a button is was pressed and released on the last frame.
    ''' </summary>
    ''' <param name="button">The button to check.</param>
    ''' <returns>True if the button was pressed, otherwise false.</returns>
    Public Function WasButtonPressed(button As MouseButtons) As Boolean
        If lastMouseButtonState.ContainsKey(button) Then
            If lastMouseButtonState(button) AndAlso Not currentMouseButtonState(button) Then Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether a key in a keymap is was pressed and released on the last frame.
    ''' </summary>
    ''' <param name="keyMapName">The name of the keymap to check.</param>
    ''' <returns>True if the key was pressed, otherwise false.</returns>
    Public Function WasKeyPressed(keyMapName As String) As Boolean
        For Each k In GetKeyMap(keyMapName)
            If WasKeyPressed(k) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets a value indicating whether a key is was pressed and released on the last frame.
    ''' </summary>
    ''' <param name="key">The key to check.</param>
    ''' <returns>True if the key was pressed, otherwise false.</returns>
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
