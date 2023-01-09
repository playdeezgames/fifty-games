Imports System.ComponentModel

Public Module QueensPuzzle
    Public Sub Run(data As QueensPuzzleData)
        Do
            AnsiConsole.Clear()
            If data.GamesAttempted > 0 Then
                AnsiConsole.MarkupLine($"Attempts: {data.GamesAttempted}")
                AnsiConsole.MarkupLine($"Completion Percentage: {100 * data.GamesCompleted / (data.GamesAttempted):f}%")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Queen's Puzzle[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    PlayGame(data)
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub

    Private Sub PlayGame(data As QueensPuzzleData)
        Dim queens As New List(Of (Integer, Integer))
        data.GamesAttempted += 1
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("  A B C D E F G H")
            For row = 1 To 8
                AnsiConsole.Markup(" ")
                For column = 1 To 8
                    AnsiConsole.Markup("+-")
                Next
                AnsiConsole.MarkupLine("+")

                AnsiConsole.Markup($"{row}")
                For column = 1 To 8
                    AnsiConsole.Markup("|")
                    Dim position = (column, row)
                    If queens.Any(Function(q) q.Item1 = position.column AndAlso q.Item2 = position.row) Then
                        AnsiConsole.Markup("Q")
                    Else
                        AnsiConsole.Markup(" ")
                    End If
                Next
                AnsiConsole.MarkupLine("|")

            Next

            AnsiConsole.Markup(" ")
            For column = 1 To 8
                AnsiConsole.Markup("+-")
            Next
            AnsiConsole.MarkupLine("+")

            Dim answer = AnsiConsole.Ask(Of String)("[olive]Location(or Q to quit)?[/] ").ToUpper.Trim
            If answer(0) = "Q" Then
                Exit Do
            End If
            If answer.Length <> 2 Then
                AnsiConsole.MarkupLine("[red]Invalid response![/]")
                OkPrompt()
                Continue Do
            End If
            Dim boardColumn = "ABCDEFGH".IndexOf(answer(0))
            Dim boardRow = "12345678".IndexOf(answer(1))
            If boardColumn = -1 OrElse boardRow = -1 Then
                AnsiConsole.MarkupLine("[red]Invalid response![/]")
                OkPrompt()
                Continue Do
            End If
            boardColumn += 1
            boardRow += 1
            If queens.Any(Function(q) CanCapture(q, (boardColumn, boardRow))) Then
                AnsiConsole.MarkupLine("[red]Invalid response![/]")
                OkPrompt()
                Continue Do
            End If
            queens.Add((boardColumn, boardRow))
            If queens.Count = 8 Then
                Exit Do
            End If
        Loop
        If queens.Count = 8 Then
            data.GamesCompleted += 1
        End If
    End Sub

    Private Function CanCapture(fromCell As (Integer, Integer), toCell As (boardColumn As Integer, boardRow As Integer)) As Boolean
        Return fromCell.Item1 = toCell.Item1 OrElse
            fromCell.Item2 = toCell.Item2 OrElse
            Math.Abs(fromCell.Item1 - toCell.Item1) = Math.Abs(fromCell.Item2 - toCell.Item2)
    End Function

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Queen's Puzzle"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("On a chess board, there are a number of ways to place 8 Queens such that none of the Queens can capture each other.

Yer goal is to place 8 such Queens.

You will be presented with a chess board, and you enter the coordinates of the next Queen you want to play.

Naturally, you will be prevented from making illegal moves.")

        Common.OkPrompt()
    End Sub
End Module
