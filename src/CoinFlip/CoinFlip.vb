Public Module CoinFlip
    Public Sub Run(data As CoinFlipData)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Correct Guesses: {data.SuccessCount}")
            AnsiConsole.MarkupLine($"Incorrect Guesses: {data.FailureCount}")
            If (data.FailureCount + data.SuccessCount > 0) Then
                AnsiConsole.MarkupLine($"Success Rate: {100.0 * data.SuccessCount / (data.SuccessCount + data.FailureCount)}%")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Coin Toss!![/]"}
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

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Coin Flip"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("In this game, you guess whether or not a coin will show heads or tails when flipped.

I count the number of times you were right and wrong.")
        Common.OkPrompt()
    End Sub

    Private Const HeadsText = "Heads"
    Private Const TailsText = "Tails"

    Private Sub PlayGame(data As CoinFlipData)
        Dim random As New Random
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Heads or Tails?[/]"}
        prompt.AddChoice(HeadsText)
        prompt.AddChoice(TailsText)
        Dim correct = If(random.Next(2) > 0, HeadsText, TailsText)
        If AnsiConsole.Prompt(prompt) = correct Then
            AnsiConsole.MarkupLine($"Yer right, it was {correct}!")
            data.SuccessCount += 1
        Else
            AnsiConsole.MarkupLine($"Yer wrong, it was {correct}!")
            data.FailureCount += 1
        End If
        OkPrompt()
    End Sub
End Module
