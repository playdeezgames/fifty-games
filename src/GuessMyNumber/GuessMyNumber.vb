Public Module GuessMyNumber
    Private Const PlayText = "Play!"
    Private Const HowToPlayText = "How to play?"
    Private Const QuitText = "Quit"
    Public Sub Run()
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Guess My Number(1-100)[/]"}
            prompt.AddChoices(PlayText, HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case PlayText
                    PlayGame()
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    If Common.Confirm("[red]Are you sure you want to quit?[/]") Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub

    Private Sub PlayGame()
        Dim random As New Random
        Dim target = random.Next(1, 101)
        Dim minimumGuess As Integer = 1
        Dim maximumGuess As Integer = 100
        Dim guessCount As Integer = 0
        Dim guess As Integer
        Do
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Guess My Number![/]"}
            For guess = minimumGuess To maximumGuess
                prompt.AddChoice(guess.ToString)
            Next
            guess = CInt(AnsiConsole.Prompt(prompt))
            guessCount += 1
            If guess = target Then
                Exit Do
            End If
            If guess < target Then
                AnsiConsole.MarkupLine($"[red]Yer of {guess} guess is too low![/]")
                minimumGuess = guess + 1
            End If
            If guess > target Then
                AnsiConsole.MarkupLine($"[red]Yer of {guess} guess is too high![/]")
                maximumGuess = guess - 1
            End If
            Common.OkPrompt()
        Loop
        AnsiConsole.MarkupLine("[lime]Yer correct![/]")
        AnsiConsole.MarkupLine($"It took you {guessCount} guesses!")
        Common.OkPrompt()
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Guess My Number(1-100)"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("I will choose a number betwen 1 and 100 inclusive. 

You will attempt to guess the number that I have chosen. 

After you have guessed, I will tell you if you are right, or too high or too low. 

Once you have guessed correctly, I will tell you how many guesses you made.")
        Common.OkPrompt()
    End Sub
End Module
