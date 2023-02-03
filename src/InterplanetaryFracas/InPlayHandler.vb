Module InPlayHandler
    Friend Sub Run(data As InterplanetaryFracasData)
        Do
            AnsiConsole.Clear()
            DrawBoard(data)
            AnsiConsole.MarkupLine("M - Move Ship | T - End Turn")
            While Not AnsiConsole.Console.Input.IsKeyAvailable
                'just wait!
            End While
            Select Case AnsiConsole.Console.Input.ReadKey(True).Value.Key
                Case ConsoleKey.M
                    MoveShip(data)
                Case ConsoleKey.T
                    EndTurn(data)
                Case ConsoleKey.Escape
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub EndTurn(data As InterplanetaryFracasData)
        data.Board.EndTurn()
    End Sub

    Const NeverMindText = "Never Mind"
    Const ColumnLetters = "ABCDEFGHIJKLMNOPQRSTUVWX"

    Function LocationName(column As Integer, row As Integer) As String
        Return $"{ColumnLetters(column)}{row + 1}"
    End Function

    Private Sub MoveShip(data As InterplanetaryFracasData)
        Dim fromCandidates = data.Board.GetMovableShipLocations()
        Select Case fromCandidates.Count
            Case 0
                AnsiConsole.MarkupLine("No ships to move!")
                OkPrompt()
            Case 1
                MoveAShip(data, fromCandidates.First.Item1, fromCandidates.First.Item2)
            Case Else
                Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]From Where?[/]"}
                prompt.AddChoice(NeverMindText)
                Dim table = fromCandidates.ToDictionary(Function(x) LocationName(x.Item1, x.Item2), Function(x) x)
                prompt.AddChoices(table.Keys)
                Dim answer = AnsiConsole.Prompt(prompt)
                Select Case answer
                    Case NeverMindText
                        Return
                    Case Else
                        MoveAShip(data, table(answer).Item1, table(answer).Item2)
                End Select
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
    Private ReadOnly Deltas As IReadOnlyList(Of (Integer, Integer)) =
        New List(Of (Integer, Integer)) From
        {
            (0, -1),
            (1, 0),
            (0, 1),
            (-1, 0)
        }

    Private Sub MoveTheShip(data As InterplanetaryFracasData, column As Integer, row As Integer, ship As ShipData)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]To Where?[/]"}
        prompt.AddChoice(NeverMindText)
        Dim table As New Dictionary(Of String, (Integer, Integer))
        For Each delta In Deltas
            Dim destination = (column + delta.Item1, row + delta.Item2)
            Dim cell = data.Board.GetCell(destination.Item1, destination.Item2)
            If cell IsNot Nothing AndAlso cell.Ship Is Nothing Then
                table.Add(LocationName(destination.Item1, destination.Item2), (destination.Item1, destination.Item2))
            End If
        Next
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMindText
                Return
            Case Else
                ship.HasMoved = True
                data.Board.GetCell(column, row).Ship = Nothing
                data.Board.GetCell(table(answer).Item1, table(answer).Item2).Ship = ship
        End Select
    End Sub

    Private Sub DrawBoard(data As InterplanetaryFracasData)
        data.Board.UpdateVisibility()
        AnsiConsole.MarkupLine("  | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X |")
        For row = 0 To data.Board.Rows - 1
            AnsiConsole.MarkupLine("--+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+")
            AnsiConsole.Markup($"{row + 1,2}|")
            For column = 0 To data.Board.Columns - 1
                AnsiConsole.Markup(DrawBoardCell(data.Board.GetCell(column, row)))
                AnsiConsole.Markup("|")
            Next
            AnsiConsole.WriteLine()
        Next
        AnsiConsole.MarkupLine("--+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+---+")
    End Sub

    Private Function DrawBoardCell(boardCellData As BoardCellData) As String
        If Not boardCellData.IsVisible Then
            Return "[grey]???[/]"
        End If
        Dim ship = boardCellData.Ship
        If ship IsNot Nothing Then
            If ship.PlayerOwned Then
                If ship.IsMovable Then
                    Return "[black on lime] S [/]"
                Else
                    Return "[black on green] s [/]"
                End If
            Else
                Return "[black on red] E [/]"
            End If
        End If
        Return "   "
    End Function
End Module
