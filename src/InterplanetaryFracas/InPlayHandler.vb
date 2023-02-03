Module InPlayHandler
    Friend Sub Run(data As InterplanetaryFracasData)
        Do
            AnsiConsole.Clear()
            DrawBoard(data)
            AnsiConsole.MarkupLine("M - Move Ship")
            While Not AnsiConsole.Console.Input.IsKeyAvailable
                'just wait!
            End While
            Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                Case ConsoleKey.M
                    MoveShip(data)
                Case ConsoleKey.Escape
                    Exit Do
            End Select
        Loop
    End Sub
    Const NeverMindText = "Never Mind"
    Const ColumnLetters = "ABCDEFGHIJKLMNOPQRSTUVWX"

    Function LocationName(column As Integer, row As Integer) As String
        Return $"{ColumnLetters(column)}{row + 1}"
    End Function

    Private Sub MoveShip(data As InterplanetaryFracasData)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]From Where?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim fromCandidates = data.Board.GetMovableShipLocations()
        Dim table = fromCandidates.ToDictionary(Function(x) LocationName(x.Item1, x.Item2), Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                MoveAShip(data, table(answer).Item1, table(answer).Item2)
        End Select
    End Sub

    Private Sub MoveAShip(data As InterplanetaryFracasData, column As Integer, row As Integer)
        Dim ships = data.Board.GetCell(column, row).GetPlayerShips
        Select Case ships.Count
            Case 0
                AnsiConsole.MarkupLine("No ships!")
            Case 1
                MoveTheShip(data, column, row, ships.Single)
            Case 2
                Dim ship = PickShip(ships)
                If ship IsNot Nothing Then
                    MoveTheShip(data, column, row, ship)
                End If
        End Select
    End Sub

    Private Function PickShip(ships As IEnumerable(Of ShipData)) As ShipData
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Ship?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim index = 1
        Dim table As New Dictionary(Of String, ShipData)
        For Each ship In ships
            table.Add($"#{index}: {ship.ShipType}", ship)
            index += 1
        Next
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return Nothing
            Case Else
                Return table(answer)
        End Select
    End Function

    Private Sub MoveTheShip(data As InterplanetaryFracasData, column As Integer, row As Integer, ship As ShipData)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]To Where?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table As New Dictionary(Of String, (Integer, Integer))
        If data.Board.GetCell(column, row - 1) IsNot Nothing Then
            table.Add(LocationName(column, row - 1), (column, row - 1))
        End If
        If data.Board.GetCell(column + 1, row) IsNot Nothing Then
            table.Add(LocationName(column + 1, row), (column + 1, row))
        End If
        If data.Board.GetCell(column, row + 1) IsNot Nothing Then
            table.Add(LocationName(column, row + 1), (column, row + 1))
        End If
        If data.Board.GetCell(column - 1, row) IsNot Nothing Then
            table.Add(LocationName(column - 1, row), (column - 1, row))
        End If
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                data.Board.GetCell(column, row).RemoveShip(ship)
                data.Board.GetCell(table(answer).Item1, table(answer).Item2).AddShip(ship)
        End Select
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
