Public Module WanderMaze
    Sub Run(data As WanderMazeData)
        MainMenu("WanderMaze", data, AddressOf PlayGame, AddressOf ShowInstructions, AddressOf ShowStatistics)
    End Sub

    Private Sub ShowStatistics(data As WanderMazeData)
        If data.GamesPlayed > 0 Then
            AnsiConsole.MarkupLine($"Games Played: {data.GamesPlayed}")
            AnsiConsole.MarkupLine($"High Score: {data.HighScore}")
            AnsiConsole.MarkupLine($"Average Score: {data.TotalScore / data.GamesPlayed:f}")
        End If
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""WanderMaze"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You[purple on teal]@[/] move with the arrows.

Don't touch fire[maroon on red]~[/], or yer dead.

You leave a trail of fire[maroon on red]~[/] behind you.

I don't have a good reason for this.

Just go with it.

Don't ask questions.

Collect water[navy on teal]+[/] in order to ignore fire for one move.

You cannot move into stone[black on grey]#[/].

Collect all the money[yellow on teal]$[/] to complete the level and move on to the next.")
        Common.OkPrompt()
    End Sub

    Private Sub PlayGame(data As WanderMazeData)
        AnsiConsole.Cursor.Hide
        Dim board As New Board
        AnsiConsole.Clear()
        Do While Not board.IsGameOver
            AnsiConsole.Cursor.SetPosition(1, 1)
            board.Draw()
            While Not AnsiConsole.Console.Input.IsKeyAvailable

            End While
            Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                Case ConsoleKey.UpArrow
                    board.Move(0, -1)
                Case ConsoleKey.DownArrow
                    board.Move(0, 1)
                Case ConsoleKey.LeftArrow
                    board.Move(-1, 0)
                Case ConsoleKey.RightArrow
                    board.Move(1, 0)
            End Select
        Loop
        data.GamesPlayed += 1
        data.TotalScore += board.Score
        data.HighScore = Math.Max(board.Score, data.HighScore)
        AnsiConsole.Cursor.Show
        OkPrompt()
    End Sub
End Module
