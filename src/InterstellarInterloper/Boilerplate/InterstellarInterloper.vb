Public Module InterstellarInterloper
    Public Sub Run(data As InterstellarInterloperData)
        Dim random As New Random
        Do While PlayGame(data, random)
        Loop
    End Sub

    Private Function PlayGame(data As InterstellarInterloperData, random As Random) As Boolean
        If data.TurnsRemaining > 0 Then
            If Not PlayGameHandler.Run(data, random) Then
                Return False
            End If
        Else
            If Not GameOverHandler.Run(data, random) Then
                Return False
            End If
        End If
        Return True
    End Function

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
