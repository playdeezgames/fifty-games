Public Module MoneyFace
    Public Sub Run(data As MoneyFaceData)
        Do
            AnsiConsole.Clear()
            If data.GamesPlayed > 0 Then
                AnsiConsole.MarkupLine($"High Score: {data.HighScore}")
                AnsiConsole.MarkupLine($"Games Played: {data.GamesPlayed}")
                AnsiConsole.MarkupLine($"Average Score: {data.TotalScore / data.GamesPlayed:f}")
            End If
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Moneyface![/]"}
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

    Private Sub PlayGame(data As MoneyFaceData)
        Dim random As New Random
        AnsiConsole.Clear()
        Const FieldColumns = 50
        Const FieldRows = 25
        Dim score = 0
        Dim gameStart = DateTimeOffset.Now
        Dim gameEnd = gameStart.AddMinutes(1.0)
        Dim faceX = random.Next(1, FieldColumns + 1)
        Dim faceY = random.Next(1, FieldRows + 1)
        Dim moneyX = random.Next(1, FieldColumns + 1)
        Dim moneyY = random.Next(1, FieldRows + 1)
        AnsiConsole.Cursor.Hide
        AnsiConsole.Cursor.SetPosition(1, 2)
        For row = 0 To FieldRows - 1
            AnsiConsole.MarkupLine(New String("."c, FieldColumns))
        Next
        Do While DateTimeOffset.Now < gameEnd
            Dim currentGameTime = DateTimeOffset.Now
            Dim timeRemaining = gameEnd - currentGameTime
            AnsiConsole.Cursor.SetPosition(1, 1)
            AnsiConsole.MarkupLine($"Time: {timeRemaining.Seconds} | Score: {score} ")
            AnsiConsole.Cursor.SetPosition(moneyX, moneyY + 1)
            AnsiConsole.Markup("$")
            AnsiConsole.Cursor.SetPosition(faceX, faceY + 1)
            AnsiConsole.Markup("☺")
            Do While currentGameTime.Second = DateTimeOffset.Now.Second AndAlso Not AnsiConsole.Console.Input.IsKeyAvailable
            Loop
            AnsiConsole.Cursor.SetPosition(faceX, faceY + 1)
            AnsiConsole.Markup(".")
            AnsiConsole.Cursor.SetPosition(moneyX, moneyY + 1)
            AnsiConsole.Markup(".")
            If AnsiConsole.Console.Input.IsKeyAvailable Then
                Dim keyInfo = AnsiConsole.Console.Input.ReadKey(True)
                Select Case keyInfo.Value.Key
                    Case ConsoleKey.RightArrow
                        If faceX < FieldColumns Then
                            faceX += 1
                        End If
                    Case ConsoleKey.LeftArrow
                        If faceX > 1 Then
                            faceX -= 1
                        End If
                    Case ConsoleKey.UpArrow
                        If faceY > 1 Then
                            faceY -= 1
                        End If
                    Case ConsoleKey.DownArrow
                        If faceY < FieldRows Then
                            faceY += 1
                        End If
                End Select
            End If
            If faceX = moneyX AndAlso faceY = moneyY Then
                score += 1
                moneyX = random.Next(1, FieldColumns + 1)
                moneyY = random.Next(1, FieldRows + 1)
            End If
        Loop
        AnsiConsole.Cursor.Show
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Final Score: {score}")
        data.GamesPlayed += 1
        data.TotalScore += score
        data.HighScore = Math.Max(score, data.HighScore)
        OkPrompt()
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Moneyface"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("In this game, you control a face (☺).

The object of the game is to collect as much money ($) as you can before the time runs out!

The clock starts at 60 seconds and that's how long you've got.

You get one point for every $ you pick up.")

        Common.OkPrompt()
    End Sub
End Module
