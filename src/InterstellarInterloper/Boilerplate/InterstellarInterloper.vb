Public Module InterstellarInterloper
    Public Sub Run(data As InterstellarInterloperData)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Interstellar Interloper[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    PlayGame(data)
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub

    Private Sub PlayGame(data As InterstellarInterloperData)
        Do
            If data.TurnsRemaining > 0 Then
                If Not PlayGameHandler.Run(data) Then
                    Exit Do
                End If
            Else
                If Not GameOverHandler.Run(data) Then
                    Exit Do
                End If
            End If
        Loop
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Interstellar Interloper"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("The goal is to create a Interstellar Empire.

You start out with one star system.

Star systems manufacture ships each turn.

You can send fleets to other star systems in an attempt to claim them.

If you encounter an enemy fleet, you have to fight them.

Combat is resolved automatically, and either all of yer ships are destroyed or you destroy all of the enemy ships and then the star system is yers.

Before beginning a game, you select the number of turns for which to play.

The winner of the game is determined by how many star systems they own.")
        Common.OkPrompt()
    End Sub
End Module
