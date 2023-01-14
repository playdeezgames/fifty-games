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
            While movesLeft > 0 AndAlso board.AnyLit
                AnsiConsole.Clear()
                AnsiConsole.MarkupLine($"Level: {level}, Moves Remaining: {movesLeft}")
                ShowBoard(board)
                Dim column = ChooseColumn()
                Dim row = ChooseRow()
                board.Toggle(column, row)
                board.Toggle(column - 1, row)
                board.Toggle(column + 1, row)
                board.Toggle(column, row - 1)
                board.Toggle(column, row + 1)
                movesLeft -= 1
            End While
            gameover = board.AnyLit
            If Not gameover Then
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
