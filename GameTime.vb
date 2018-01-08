Public Class GameTime
    Public ReadOnly Property Elapsed As Single
    Public ReadOnly Property LastFrame As Single

    Private timer As New Stopwatch

    Public Sub Pause()
        timer.Stop()
    End Sub

    Public Sub Reset()
        timer.Stop()
        timer.Reset()
        _Elapsed = 0!
        _LastFrame = 0!
    End Sub

    Public Sub [Resume]()
        timer.Start()
    End Sub

    Protected Friend Sub Update()
        timer.Stop()
        _LastFrame = CSng(timer.Elapsed.TotalSeconds)
        _Elapsed += _LastFrame
        timer.Restart()
    End Sub
End Class
