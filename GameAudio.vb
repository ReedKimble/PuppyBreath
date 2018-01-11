''' <summary>
''' Provides the ability to play music and sound effects. You do not create instances of this class in code,
''' rather, utilize the instance provided by GameState.
''' </summary>
''' <author>Reed Kimble 01/08/2018</author>
Public NotInheritable Class GameAudio

    Protected Friend players As New List(Of GameAudioPlayer)
    Protected Friend resources As New Dictionary(Of String, Uri)

    Protected Friend Sub LoadResources()
        Dim resourcePath As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "Resources")
        If Not IO.Directory.Exists(resourcePath) Then Return
        For Each file In IO.Directory.GetFiles(resourcePath, "*.wav")
            resources(IO.Path.GetFileNameWithoutExtension(file)) = New Uri(file)
        Next
    End Sub

    Private Function GetPlayer() As GameAudioPlayer
        For Each p In players
            If Not p.IsPlaying Then Return p
        Next
        Return New GameAudioPlayer(Me)
    End Function

    ''' <summary>
    ''' Pause all playing music and sound effects.
    ''' </summary>
    Public Sub PauseAll()
        For Each p In players
            If p.IsPlaying Then p.Pause()
        Next
    End Sub

    ''' <summary>
    ''' Play a sound effect. The sound begins playing and ends when the duration is reached.
    ''' </summary>
    ''' <param name="effectName">The name of the WAV file in the Resources folder.</param>
    Public Sub PlayEffect(effectName As String)
        If resources.ContainsKey(effectName) Then
            Dim p = GetPlayer()
            p.Play(resources(effectName))
        End If
    End Sub

    ''' <summary>
    ''' Play a sound as looping music. The sound begins playing and restarts from the beginning when the duration is reached.
    ''' </summary>
    ''' <param name="songName">The name of the WAV file in the Resources folder.</param>
    ''' <returns></returns>
    Public Function PlayMusic(songName As String) As GameAudioPlayer
        If resources.ContainsKey(songName) Then
            Dim p = GetPlayer()
            p.PlayLooping(resources(songName))
            Return p
        End If
        Return Nothing
    End Function

    Public Sub ChangeMusic(player As GameAudioPlayer, songName As String)
        StopPlayer(player)
        If resources.ContainsKey(songName) Then
            player.PlayLooping(resources(songName))
        End If
    End Sub

    ''' <summary>
    ''' Resumes playing all paused music and sound effects.
    ''' </summary>
    Public Sub ResumePlaying()
        For Each p In players
            If p.IsPlaying Then p.Resume()
        Next
    End Sub

    Public Sub StopPlayer(player As GameAudioPlayer)
        If player.IsLooping Then player.StopLoop() Else player.Stop()
    End Sub
End Class