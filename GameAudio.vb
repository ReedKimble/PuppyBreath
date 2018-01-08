'Add Project References to:
'   PresentationCore
'   WindowsBase

Imports System.Windows.Media

Public NotInheritable Class GameAudio
    'Public Shared ReadOnly [Default] As New GameAudio

    Private players As New List(Of GameAudioPlayer)
    Protected Friend resources As New Dictionary(Of String, Uri)

    Protected Friend Sub New()
        For Each file In IO.Directory.GetFiles(IO.Path.Combine(My.Application.Info.DirectoryPath, "Resources"), "*.wav")
            resources(IO.Path.GetFileNameWithoutExtension(file)) = New Uri(file)
        Next
    End Sub

    Private Function GetPlayer() As GameAudioPlayer
        For Each p In players
            If Not p.IsPlaying Then Return p
        Next
        Dim gap As New GameAudioPlayer
        players.Add(gap)
        Return gap
    End Function

    Public Sub PauseAll()
        For Each p In players
            If p.IsPlaying Then p.Pause()
        Next
    End Sub

    Public Sub PlayEffect(effectName As String)
        If resources.ContainsKey(effectName) Then
            Dim p = GetPlayer()
            p.Play(resources(effectName))
        End If
    End Sub

    Public Function PlayMusic(songName As String) As GameAudioPlayer
        If resources.ContainsKey(songName) Then
            Dim p = GetPlayer()
            p.PlayLooping(resources(songName))
            Return p
        End If
        Return Nothing
    End Function

    Public Sub ResumePlaying()
        For Each p In players
            If p.IsPlaying Then p.Resume()
        Next
    End Sub
End Class

Public Class GameAudioPlayer
    Inherits MediaPlayer

    Public ReadOnly Property IsPlaying As Boolean

    Private isLooping As Boolean
    Private lastPlayedResource As Uri

    Public Sub New()
        AddHandler Me.MediaEnded, AddressOf OnMediaEnded
        AddHandler Me.MediaOpened, AddressOf OnMediaOpened
    End Sub

    Public Shadows Sub Play(resource As Uri)
        If lastPlayedResource IsNot Nothing AndAlso lastPlayedResource = resource Then
            Position = TimeSpan.Zero
            MyBase.Play()
        Else
            Open(resource)
            MyBase.Play()
        End If
    End Sub

    Public Sub PlayLooping(resource As Uri)
        isLooping = True
        Play(resource)
    End Sub

    Public Sub [Resume]()
        MyBase.Play()
    End Sub

    Public Sub StopLoop()
        isLooping = False
        [Stop]()
    End Sub

    Protected Sub OnMediaEnded(sender As Object, e As EventArgs)
        If isLooping Then
            Position = TimeSpan.Zero
            MyBase.Play()
            Exit Sub
        End If
        _IsPlaying = False
        Close()
    End Sub

    Protected Sub OnMediaOpened(sender As Object, e As EventArgs)
        _IsPlaying = True
    End Sub
End Class