''' <summary>
''' Montiors game execution time and provides access to the total and last frame execution times.
''' You do not create instances of this class in code, rather, utilize the instance provided by GameState.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public Class GameTime
    ''' <summary>
    ''' Gets the total amount of time elapsed in this game, in seconds.
    ''' </summary>
    ''' <returns>The total amount of time elapsed in this game, in seconds.</returns>
    Public ReadOnly Property Elapsed As Single

    ''' <summary>
    ''' Gets the amount of time elapsed on the last frame, in seconds.
    ''' </summary>
    ''' <returns>The amount of time elapsed on the last frame, in seconds.</returns>
    Public ReadOnly Property LastFrame As Single

    Private timer As New Stopwatch

    ''' <summary>
    ''' Pauses time monitoring.
    ''' </summary>
    Public Sub Pause()
        timer.Stop()
    End Sub

    ''' <summary>
    ''' Resets the game time to zero.
    ''' </summary>
    Public Sub Reset()
        timer.Stop()
        timer.Reset()
        _Elapsed = 0!
        _LastFrame = 0!
    End Sub

    ''' <summary>
    ''' Resumes paused time monitoring.
    ''' </summary>
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
