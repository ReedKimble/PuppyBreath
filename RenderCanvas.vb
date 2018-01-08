Public Class RenderCanvas
    Public Event RenderingComplete As EventHandler

    Public Property DebugFpsView As Boolean
    Public ReadOnly Property IsRunning As Boolean
    Public ReadOnly Property Scene As GameScene
    Public Property TargetFPS As Integer = 30

    Private isPaused As Boolean
    Private state As New GameState
    Private renderBuffer As BufferedGraphics
    Private rendererActive As Boolean

    Public Async Function BeginAsync() As Task
        If isRunning Then Exit Function
        _IsRunning = True
        rendererActive = True
        Do
            If isPaused Then
                Await Task.Delay(250)
                Continue Do
            End If
            OnUpdate()

            Dim targetFrameTime As Single = CSng(1 / TargetFPS)
            Dim excessFrameTime As Single = targetFrameTime - state.Time.LastFrame
            Dim waitTime As Integer = CInt(excessFrameTime * 1000)
            Await Task.Delay(Math.Max(1, waitTime))
            If DebugFpsView Then Parent.Text = $"{(1 / state.Time.LastFrame):n2} FPS (waited {waitTime} ms)"
        Loop While rendererActive
        _IsRunning = False
        OnRenderingComplete(EventArgs.Empty)
    End Function

    Public Sub ChangeScene(newScene As GameScene)
        _Scene = newScene
        state.SetScene(newScene)
    End Sub

    Public Function GetGameInput() As GameInput
        Return state.Input
    End Function

    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Return True
            Case Else
                Return MyBase.IsInputKey(keyData)
        End Select
    End Function

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        state.Input.SetKeyDown(e.KeyCode)
    End Sub

    Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
        MyBase.OnKeyUp(e)
        state.Input.SetKeyUp(e.KeyCode)
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        state.Input.SetMouseDown(e.Button)
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        state.Input.SetMouseUp(e.Button)
    End Sub

    Protected Overridable Sub OnRenderingComplete(e As EventArgs)
        RaiseEvent RenderingComplete(Me, e)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        If renderBuffer IsNot Nothing Then renderBuffer.Dispose()
        renderBuffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics, New Rectangle(0, 0, Width, Height))
        state.SetBounds(ClientRectangle)
    End Sub

    Protected Overridable Sub OnUpdate()
        state.Time.Update()
        state.Input.Update(PointToClient(MousePosition))
        renderBuffer.Graphics.Clear(BackColor)
        If _Scene IsNot Nothing Then
            If Not _Scene.IsInitialized Then _Scene.Initialize(state)
            Dim gameObjects = _Scene.GameObjects
            PerformCollisionChecking(gameObjects)
            For i = gameObjects.Count - 1 To 0 Step -1
                Dim gameObject = gameObjects(i)
                If gameObject.IsFlaggedForDestruction Then
                    _Scene.GameObjects.RemoveAt(i)
                    gameObject.FinializeDestruction(state)
                Else
                    gameObject.Update(state)
                End If
            Next
            For Each gobj In (From g In gameObjects Where TypeOf (g) Is Sprite Let s = DirectCast(g, Sprite) Where g.IsDestroyed = False Order By s.ZOrder Select s)
                gobj.Render(renderBuffer.Graphics)
            Next
        End If
        renderBuffer.Render()
    End Sub

    Private Sub PerformCollisionChecking(gameObjects As List(Of GameObject))
        For Each g In gameObjects
            DirectCast(g.Collisions, List(Of CollisionInfo)).Clear()
        Next
        For i = gameObjects.Count - 1 To 1 Step -1
            Dim current As GameObject = gameObjects(i)
            Dim currentBounds = current.GetCollisionBounds
            If current.IsDestroyed OrElse Not current.CheckCollision Then Continue For
            For j = i - 1 To 0 Step -1
                Dim other As GameObject = gameObjects(j)
                If other.IsDestroyed OrElse Not other.CheckCollision Then Continue For
                If currentBounds.Intersects(other.GetCollisionBounds) Then
                    Dim hitpoint = Mathf.GetPositionToward(current.Position, other.Position, currentBounds.Radius)
                    Dim info As New CollisionInfo(current, other, hitpoint)
                    DirectCast(current.Collisions, List(Of CollisionInfo)).Add(info)
                    DirectCast(other.Collisions, List(Of CollisionInfo)).Add(info)
                End If
            Next
        Next
    End Sub

    Public Sub Pause()
        state.Time.Pause()
        state.Audio.PauseAll()
        isPaused = True
    End Sub

    Public Sub [Resume]()
        state.Time.Resume()
        state.Audio.ResumePlaying()
        state.Input.ResetCurrentState()
        isPaused = False
    End Sub

    Public Sub StopRenderer()
        rendererActive = False
    End Sub
End Class
