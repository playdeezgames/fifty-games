Public Module Platformer
    Sub Run(data As PlatformerData)
        MainMenu("Platformer", data, AddressOf PlayGame, AddressOf ShowInstructions)
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

    Private ReadOnly level1 As IReadOnlyList(Of String) =
        New List(Of String) From
        {
            "                                                  ",
            "                                                  ",
            "                                                  ",
            "==================================================",
            "                                                  ",
            "                                                  ",
            "==================================================",
            "                                                  ",
            "                                                  ",
            "==================================================",
            "                                                  ",
            "                                                  ",
            "==================================================",
            "                                                  ",
            "                                                  ",
            "==================================================",
            "                                              $   ",
            "                                                  ",
            "=====|============================================",
            "     |                                     $      ",
            "     |                                            ",
            "===============================================|==",
            "        $                                      |  ",
            "                        @                      |  ",
            "=================================================="
        }

    Private Sub PlayGame(data As PlatformerData)
        AnsiConsole.Cursor.Hide
        Dim board As New Board(level1)
        Do
            DrawBoard(board)
            If AnsiConsole.Console.Input.IsKeyAvailable Then
                Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                    Case ConsoleKey.LeftArrow
                        board.MovePlayerLeft()
                    Case ConsoleKey.RightArrow
                        board.MovePlayerRight()
                End Select
            End If
        Loop
        AnsiConsole.Cursor.Show
    End Sub

    Private Sub DrawBoard(board As Board)
        For row = 0 To board.Rows - 1
            For column = 0 To board.Columns - 1
                Dim cell = board.GetCell(column, row)
                If Not cell.IsDirty Then
                    Continue For
                End If
                cell.IsDirty = False
                AnsiConsole.Cursor.SetPosition(column + 1, row + 1)
                Select Case cell.CharacterType
                    Case CharacterType.Player
                        AnsiConsole.Markup("[white]@[/]")
                    Case Else
                        Select Case cell.ItemType
                            Case ItemType.Money
                                AnsiConsole.Markup("[lime]$[/]")
                            Case Else
                                Select Case cell.TerrainType
                                    Case TerrainType.Floor
                                        AnsiConsole.Markup("[blue]=[/]")
                                    Case TerrainType.Ladder
                                        AnsiConsole.Markup("[silver]|[/]")
                                    Case Else
                                        AnsiConsole.Markup(" ")
                                End Select
                        End Select
                End Select
            Next
        Next
    End Sub
End Module
