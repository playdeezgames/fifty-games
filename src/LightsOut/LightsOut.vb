Public Module LightsOut
    Public Sub Run(data As LightsOutData)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Lights Out[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case HowToPlayText
                    ShowInstructions()
                Case PlayText
                    PlayGame(data)
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub
    Private Const GridColumns = 10
    Private Const GridRows = 10

    Private Sub PlayGame(data As LightsOutData)
        Dim random As New Random
        Dim board As New Board(GridColumns, GridRows)
        Dim level = 1
        Dim gameover = False
        Do
            board.GenerateBoard(level, random)
            Dim movesLeft = level
            Dim column As Integer = GridColumns \ 2
            Dim row As Integer = GridRows \ 2
            While movesLeft > 0 AndAlso board.AnyLit
                AnsiConsole.Clear()
                AnsiConsole.MarkupLine($"Level: {level}, Moves Remaining: {movesLeft}")
                ShowBoard(board)
                Dim cell = ChooseCell(column, row)
                column = cell.Item1
                row = cell.Item2
                board.MakeMove(column, row)
                movesLeft -= 1
            End While
            gameover = board.AnyLit
            If Not gameover Then
                AnsiConsole.Clear()
                AnsiConsole.MarkupLine($"Level: {level}, Moves Remaining: {movesLeft}")
                ShowBoard(board)
                AnsiConsole.MarkupLine("You beat the level!")
                level += 1
                OkPrompt()
            End If
        Loop Until gameover
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"You lose on level {level}!")
        ShowBoard(board)
        OkPrompt()
    End Sub

    Private Function ChooseCell(column As Integer, row As Integer) As (Integer, Integer)
        AnsiConsole.Cursor.Hide
        Do
            'draw outline of cell
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 3)
            AnsiConsole.Markup("[lime]+-+[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 5)
            AnsiConsole.Markup("[lime]+-+[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 4)
            AnsiConsole.Markup("[lime]|[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 5, row * 2 + 4)
            AnsiConsole.Markup("[lime]|[/]")
            'wait for key
            Dim key As ConsoleKey
            Do
                Dim info = AnsiConsole.Console.Input.ReadKey(True)
                If info.HasValue Then
                    key = info.Value.Key
                    Select Case info.Value.Key
                        Case ConsoleKey.UpArrow,
                            ConsoleKey.DownArrow,
                            ConsoleKey.LeftArrow,
                            ConsoleKey.RightArrow,
                            ConsoleKey.Spacebar,
                            ConsoleKey.Enter
                            Exit Do
                    End Select
                End If
            Loop
            'undraw outline of cell
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 3)
            AnsiConsole.Markup("[silver]+-+[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 5)
            AnsiConsole.Markup("[silver]+-+[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 3, row * 2 + 4)
            AnsiConsole.Markup("[silver]|[/]")
            AnsiConsole.Cursor.SetPosition(column * 2 + 5, row * 2 + 4)
            AnsiConsole.Markup("[silver]|[/]")
            'process key
            Select Case key
                Case ConsoleKey.UpArrow
                    row = (row + GridRows - 1) Mod GridRows
                Case ConsoleKey.DownArrow
                    row = (row + 1) Mod GridRows
                Case ConsoleKey.LeftArrow
                    column = (column + GridColumns - 1) Mod GridColumns
                Case ConsoleKey.RightArrow
                    column = (column + 1) Mod GridColumns
                Case Else
                    AnsiConsole.Cursor.Show
                    Return (column, row)
            End Select
        Loop
    End Function


    Private Function ChooseColumn() As Integer
        Do
            Select Case AnsiConsole.Ask(Of String)("[olive]Column:[/] ")
                Case "a", "A"
                    Return 0
                Case "b", "B"
                    Return 1
                Case "c", "C"
                    Return 2
                Case "d", "D"
                    Return 3
                Case "e", "E"
                    Return 4
                Case "f", "F"
                    Return 5
                Case "g", "G"
                    Return 6
                Case "h", "H"
                    Return 7
                Case "i", "I"
                    Return 8
                Case "j", "J"
                    Return 9
            End Select
        Loop
    End Function

    Private Function ChooseRow() As Integer
        Do
            Dim answer = AnsiConsole.Ask(Of Integer)("[olive]Row:[/] ")
            Select Case answer
                Case 1 To 10
                    Return answer - 1
            End Select
        Loop
    End Function

    Private Sub ShowBoard(board As Board)
        AnsiConsole.MarkupLine("  |A|B|C|D|E|F|G|H|I|J|")
        AnsiConsole.MarkupLine("--+-+-+-+-+-+-+-+-+-+-+")
        For row = 0 To GridRows - 1
            If row + 1 < 10 Then
                AnsiConsole.Markup(" ")
            End If
            AnsiConsole.Markup($"{row + 1}|")
            For column = 0 To GridColumns - 1
                If board.IsLit(column, row) Then
                    AnsiConsole.Markup("#|")
                Else
                    AnsiConsole.Markup(" |")
                End If
            Next
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine("--+-+-+-+-+-+-+-+-+-+-+")
        Next
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Lights Out"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("Lights Out is played in a series of levels of increasing difficulty.

Each level has a number of lights that are on that you need to put out.

To put out a light, you pick a square on the grid, and that light, as well as any light one square above, below, to the right or to the left will switch between the on state and the off state.

To finish a level, all the lights have to be out.")

        Common.OkPrompt()
    End Sub
End Module
