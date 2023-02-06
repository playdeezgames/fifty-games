Public Module InterplanetaryFracas
    Sub Run(data As InterplanetaryFracasData)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Interplanetary Fracas![/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    PlayGame(data)
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    Exit Do
            End Select
        Loop
    End Sub

    Private Sub PlayGame(data As InterplanetaryFracasData)
        If data.Board Is Nothing Then
            CreateBoard(data)
        End If
        InPlayHandler.Run(data)
    End Sub

    Const BoardColumns = 24
    Const BoardRows = 12

    Private Sub CreateBoard(data As InterplanetaryFracasData)
        InitializeBoard(data)
        PlacePlayerShip(data, ShipType.MarkI)
        PlacePlayerShip(data, ShipType.MarkII)
        PlacePlayerShip(data, ShipType.MarkIII)
        PlacePlayerShip(data, ShipType.MarkIV)
        PlaceEnemyShip(data, ShipType.MarkI)
        PlaceEnemyShip(data, ShipType.MarkI)
        PlaceEnemyShip(data, ShipType.MarkI)
        PlaceEnemyShip(data, ShipType.MarkI)
        PlaceEnemyShip(data, ShipType.MarkII)
        PlaceEnemyShip(data, ShipType.MarkII)
        PlaceEnemyShip(data, ShipType.MarkII)
        PlaceEnemyShip(data, ShipType.MarkIII)
        PlaceEnemyShip(data, ShipType.MarkIII)
        PlaceEnemyShip(data, ShipType.MarkIV)
    End Sub

    Private Sub PlaceEnemyShip(data As InterplanetaryFracasData, shipType As ShipType)
        Do
            Dim column As Integer = random.Next(data.Board.Columns)
            Dim row As Integer = random.Next(data.Board.Rows)
            If data.Board.GetCell(column, row).Ship IsNot Nothing Then
                Continue Do
            End If
            data.Board.GetCell(column, row).Ship = New ShipData With {.PlayerOwned = False, .ShipType = shipType}
            Exit Do
        Loop
    End Sub

    Private Sub PlacePlayerShip(data As InterplanetaryFracasData, shipType As ShipType)
        Do
            Dim column As Integer = random.Next(data.Board.Columns)
            Dim row As Integer = random.Next(data.Board.Rows)
            If data.Board.GetCell(column, row).Ship IsNot Nothing Then
                Continue Do
            End If
            data.Board.GetCell(column, row).Ship = New ShipData With
                {
                    .PlayerOwned = True,
                    .ShipType = shipType,
                    .Torpedos = shipType.GetStartingTorpedos
                }
            Exit Do
        Loop
    End Sub

    Private Sub InitializeBoard(data As InterplanetaryFracasData)
        data.Board = New BoardData With
            {
                .Columns = BoardColumns,
                .Rows = BoardRows
            }
        While data.Board.Cells.Count < data.Board.Columns * data.Board.Rows
            data.Board.Cells.Add(New BoardCellData)
        End While
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("How to play Interplanetary Fracas")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You controls ships.

And fight other ships.

And mine stuff on planetoids and things!")
        OkPrompt()
    End Sub
End Module
