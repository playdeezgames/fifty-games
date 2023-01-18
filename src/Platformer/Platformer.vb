Public Module Platformer
    Sub Run(data As PlatformerData)
        MainMenu("Platformer", data, AddressOf PlayGame, AddressOf ShowInstructions, AddressOf ShowStats)
    End Sub

    Private Sub ShowStats(data As PlatformerData)
        If data.GamesPlayed > 0 Then
            AnsiConsole.MarkupLine($"Games Played: {data.GamesPlayed}")
            AnsiConsole.MarkupLine($"High Score: {data.HighScore}")
            AnsiConsole.MarkupLine($"Average Score: {data.TotalScore / data.GamesPlayed:f}")
        End If
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Platformer"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You move around on platforms with arrow keys.

You can climb ladders and stuff with arrow keys.

You can jump with the space bar.

The goal is to have walked on all of the platforms to complete the level before the time runs out.")
        Common.OkPrompt()
    End Sub

    Private ReadOnly level As IReadOnlyList(Of String) =
        New List(Of String) From
        {
            "                                                  ",
            "     $                     $                      ",
            "                                                  ",
            "======================|====================|======",
            "                      |    $$$$            |      ",
            "                      |                    |      ",
            "====|===================== =======================",
            "    |                             $               ",
            "    |                                             ",
            "==========================|================|===== ",
            "          $                                |      ",
            "                                           |      ",
            "====|===================================== | =====",
            "    |                           $          |      ",
            "    |                                      |      ",
            "================================|==========|======",
            "            $                   |          |  $   ",
            "                                |          |      ",
            "=====|========= ====|=========|=== ===============",
            "     |              |         |            $      ",
            "     |              |         |                   ",
            "========================= =====================|==",
            "        $                                      |  ",
            "                        @                      |  ",
            "=================================================="
        }

    Private Sub PlayGame(data As PlatformerData)
        AnsiConsole.Cursor.Hide
        Dim board As New Board(level)
        board.TimeRemaining = 90.0
        Dim currentTime = DateTimeOffset.Now
        Do
            Dim oldTime = currentTime
            currentTime = DateTimeOffset.Now
            board.TimeRemaining -= (currentTime - oldTime).TotalSeconds
            board.UpdateCharacters()
            DrawBoard(board)
            If AnsiConsole.Console.Input.IsKeyAvailable Then
                Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                    Case ConsoleKey.LeftArrow
                        board.MovePlayerLeft()
                    Case ConsoleKey.RightArrow
                        board.MovePlayerRight()
                    Case ConsoleKey.UpArrow
                        board.MovePlayerUp()
                    Case ConsoleKey.DownArrow
                        board.MovePlayerDown()
                    Case ConsoleKey.Spacebar
                        board.PlayerJump()
                End Select
            End If
        Loop Until board.IsCompleted OrElse board.TimeRemaining <= 0.0
        AnsiConsole.Cursor.Show
        data.GamesPlayed += 1
        If board.IsCompleted Then
            data.TotalScore += board.Score + CInt(board.TimeRemaining)
            data.HighScore = Math.Max(data.HighScore, board.Score)
            ShowWin(board)
        Else
            ShowLose(board)
        End If
    End Sub

    Private Sub ShowLose(board As Board)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Time's Up! You lose!")
        OkPrompt()
    End Sub

    Private Sub ShowWin(board As Board)
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Level Complete! Score: {board.Score + CInt(board.TimeRemaining)}")
        OkPrompt()
    End Sub

    Private Sub DrawBoard(board As Board)
        DrawHeader(board)
        For row = 0 To board.Rows - 1
            For column = 0 To board.Columns - 1
                Dim cell = board.GetCell(column, row)
                If Not cell.IsDirty Then
                    Continue For
                End If
                cell.IsDirty = False
                AnsiConsole.Cursor.SetPosition(column + 1, row + 2)
                If cell.Character IsNot Nothing Then
                    Select Case cell.Character.CharacterType
                        Case CharacterType.Player
                            AnsiConsole.Markup("[white]☺[/]")
                        Case Else
                    End Select
                Else
                    Select Case cell.ItemType
                        Case ItemType.Money
                            AnsiConsole.Markup("[lime]$[/]")
                        Case Else
                            Select Case cell.TerrainType
                                Case TerrainType.Floor
                                    AnsiConsole.Markup("[blue on black]═[/]")
                                Case TerrainType.WalkedFloor
                                    AnsiConsole.Markup("[black on blue]═[/]")
                                Case TerrainType.Ladder
                                    AnsiConsole.Markup("[silver]╫[/]")
                                Case Else
                                    AnsiConsole.Markup(" ")
                            End Select
                    End Select
                End If
            Next
        Next
    End Sub

    Private Sub DrawHeader(board As Board)
        If board.ShouldUpdateHeader Then
            board.ShouldUpdateHeader = False
            AnsiConsole.Cursor.SetPosition(1, 1)
            AnsiConsole.Markup("[black on olive]                                                  [/]")
            AnsiConsole.Cursor.SetPosition(1, 1)
            AnsiConsole.Markup($"[black on olive]{board.CompletionPercentage}% Complete [/]")
            AnsiConsole.Markup($"[black on olive]| ${board.Score} [/]")
            AnsiConsole.Markup($"[black on olive]| T: {CInt(board.TimeRemaining)} [/]")
        End If
    End Sub
End Module
