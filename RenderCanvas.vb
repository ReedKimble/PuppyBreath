''' <summary>
''' Represents the visual rendering area of the game and executes the game loop logic.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class RenderCanvas
    ''' <summary>
    ''' This event is raised when the render canvas exits execution of the game loop.
    ''' </summary>
    Public Event RenderingComplete As EventHandler

    Public Property DebugColliderView As Boolean
    Public Property DebugColliderViewColor As Color = Color.Fuchsia

    ''' <summary>
    ''' Gets or sets a value that determines if the control sets the text of its parent to the current frame rate.
    ''' </summary>
    ''' <returns>True if enabled, otherwise false.</returns>
    Public Property DebugFpsView As Boolean

    ''' <summary>
    ''' Gets a value that indicates whether the game loop is running.
    ''' </summary>
    ''' <returns>A value that indicates whether the game loop is running.</returns>
    Public ReadOnly Property IsRunning As Boolean

    Public Property RandomSeed As Integer = CInt(Now.Ticks And &HFFFF)

    Public ReadOnly Property SceneManager As New GameSceneManager(Me)

    ''' <summary>
    ''' Gets or sets the maximum target framerate. The default is 30 FPS and should generally not be changed.
    ''' </summary>
    ''' <returns>The maximum target framerate</returns>
    Public Property TargetFPS As Integer = 30

    Private isPaused As Boolean
    Private isSceneChangeQueued As Boolean
    Protected Friend state As GameState
    Private renderBuffer As BufferedGraphics
    Private rendererActive As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not DesignMode Then state = New GameState(Me)
    End Sub

    ''' <summary>
    ''' Begins execution of the main game loop.
    ''' </summary>
    ''' <returns>A Task which wrapps the executing game loop.</returns>
    Public Async Function BeginAsync() As Task
        If _IsRunning Then Exit Function
        _IsRunning = True
        rendererActive = True
        state.Audio.LoadResources()
        Do
            If isPaused Then
                Await Task.Delay(250)
                Continue Do
            End If
            Try
                OnUpdate()
            Catch ex As Exception
                Throw
            End Try
            Dim targetFrameTime As Single = CSng(1 / TargetFPS)
            Dim excessFrameTime As Single = targetFrameTime - state.Time.LastFrame
            Dim waitTime As Integer = CInt(excessFrameTime * 1000)
            Await Task.Delay(Math.Max(1, waitTime))
            If DebugFpsView Then Parent.Text = $"{(1 / state.Time.LastFrame):n2} FPS (waited {waitTime} ms)"
        Loop While rendererActive
        _IsRunning = False
        OnRenderingComplete(EventArgs.Empty)
    End Function

    ''' <summary>
    ''' Gets a reference to the active GameInput.
    ''' </summary>
    ''' <returns>A reference to the active GameInput.</returns>
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
    End Sub

    Protected Overridable Sub OnUpdate()
        state.Time.Update()
        renderBuffer.Graphics.Clear(BackColor)
        Dim scene = SceneManager.CurrentScene
        If scene IsNot Nothing Then
            If Not scene.IsInitialized AndAlso Not SceneManager.IsSceneChangeQueued Then scene.Initialize(state)
            scene.Update(state)
            Dim gameObjects = scene.GameObjects
            For i = gameObjects.Count - 1 To 0 Step -1
                Dim gameObject = gameObjects(i)
                If gameObject.IsFlaggedForDestruction Then
                    scene.GameObjects.RemoveAt(i)
                    gameObject.FinializeDestruction(state)
                ElseIf Not SceneManager.IsSceneChangeQueued Then
                    If gameObject.Enabled Then gameObject.Update(state)
                End If
            Next
            If Not SceneManager.IsSceneChangeQueued Then
                PerformCollisionChecking(gameObjects, state.CollisionResolution)
                For Each renderable In (From g In gameObjects Where g.Enabled AndAlso g.IsDestroyed = False AndAlso TypeOf (g) Is IRenderable Let s = DirectCast(g, IRenderable) Where s.Visible Order By s.ZOrder Select s)
                    renderable.Render(renderBuffer.Graphics, state)
                    If DebugColliderView Then
                        Dim obj = DirectCast(renderable, GameObject)
                        Using p As New Pen(DebugColliderViewColor)
                            obj.Collider?.DebugDraw(p, renderBuffer.Graphics)
                        End Using
                    End If
                Next
            End If
        End If
        renderBuffer.Render()
        state.Input.Update(PointToClient(MousePosition))
        If SceneManager.IsSceneChangeQueued Then
            If isSceneChangeQueued Then
                SceneManager.CurrentScene?.FinializeReset(state)
                SceneManager.ExecuteSceneChange(state)
                isSceneChangeQueued = False
            Else
                SceneManager.CurrentScene?.RequestReset(state)
                isSceneChangeQueued = True
            End If
        End If
    End Sub

    Private Sub PerformCollisionChecking(gameObjects As GameObjectCollection, resolution As Collision.CollisionResolution)
        For Each g In gameObjects
            If Not g.IsDestroyed Then DirectCast(g.Collisions, List(Of CollisionInfo)).Clear()
        Next
        Dim collisionList As New HashSet(Of GameObject)
        For i = gameObjects.Count - 1 To 1 Step -1
            Dim current As GameObject = gameObjects(i)
            If current.IsDestroyed OrElse Not current.Enabled OrElse Not current.HasCollision Then Continue For
            For j = i - 1 To 0 Step -1
                Dim other As GameObject = gameObjects(j)
                If other.IsDestroyed OrElse Not other.HasCollision Then Continue For
                Dim hitpoint = resolution.Resolve(current.Collider, other.Collider)
                If hitpoint.HasValue Then
                    Dim thisInfo As New CollisionInfo(current, other, hitpoint.Value)
                    Dim otherInfo As New CollisionInfo(other, current, hitpoint.Value)
                    DirectCast(current.Collisions, List(Of CollisionInfo)).Add(thisInfo)
                    DirectCast(other.Collisions, List(Of CollisionInfo)).Add(otherInfo)
                End If
            Next
            If current.Collisions.Count > 0 Then
                If Not collisionList.Contains(current) Then collisionList.Add(current)
            End If
        Next
        For Each g In collisionList
            g.CollisionsOccured(state)
        Next
    End Sub

    ''' <summary>
    ''' Pauses the game.
    ''' </summary>
    Public Sub Pause()
        state.Time.Pause()
        state.Audio.PauseAll()
        isPaused = True
    End Sub

    ''' <summary>
    ''' Resumes a paused game.
    ''' </summary>
    Public Sub [Resume]()
        state.Time.Resume()
        state.Audio.ResumePlaying()
        state.Input.ResetCurrentState()
        isPaused = False
    End Sub

    ''' <summary>
    ''' Stops rendering and causes the game engine to stop execution.
    ''' </summary>
    Public Sub StopRenderer()
        rendererActive = False
    End Sub

End Class
