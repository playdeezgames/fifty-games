Module InPlayHandler
    Friend Sub Run(data As InterplanetaryFracasData)
        AnsiConsole.Clear()
        DrawBoard(data)
        OkPrompt()
    End Sub

    Private Sub DrawBoard(data As InterplanetaryFracasData)
        data.Board.UpdateVisibility()
        AnsiConsole.MarkupLine("  | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X |")
        For row = 0 To data.Board.Rows - 1
            AnsiConsole.MarkupLine("--+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+")
            AnsiConsole.Markup($"{row + 1,2}|")
            For column = 0 To data.Board.Columns - 1
                DrawBoardCell(data.Board.GetCell(column, row))
                AnsiConsole.Markup("|")
            Next
            AnsiConsole.WriteLine()
        Next
        AnsiConsole.MarkupLine("--+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+")
    End Sub

    Private Sub DrawBoardCell(boardCellData As BoardCellData)
        If Not boardCellData.IsVisible Then
            AnsiConsole.Markup("[grey]???[/]")
            Return
        End If
        If boardCellData.Ships.Any(Function(x) x.PlayerOwned) Then
            AnsiConsole.Markup("[lime] S [/]")
            Return
        End If
        AnsiConsole.Markup("   ")
    End Sub
End Module
