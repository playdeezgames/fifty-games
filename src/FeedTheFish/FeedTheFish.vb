Public Module FeedTheFish
    Private Const FeedFishText = "Feed the Fish"
    Private Const NewFishText = "Get a New Fish"
    Public Sub Run(data As FeedTheFishData, saveGame As Action)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            If data.FishesOverfed > 0 Then
                AnsiConsole.MarkupLine($"You have overfed {data.FishesOverfed} fish.")
            End If
            If data.FishesStarved > 0 Then
                AnsiConsole.MarkupLine($"You have starved {data.FishesStarved} fish.")
            End If
            If data.FishAcquired.HasValue Then
                If data.FishDied.HasValue Then
                    AnsiConsole.MarkupLine("Yer fish is dead!")
                    prompt.AddChoice(NewFishText)
                Else
                    If data.FedUntil.Value < DateTimeOffset.Now Then
                        data.FishDied = data.FedUntil
                        data.FishesStarved += 1
                        AnsiConsole.MarkupLine("Yer fish is has starved to death!")
                        prompt.AddChoice(NewFishText)
                    Else
                        AnsiConsole.MarkupLine("Yer fish is alive!")
                        prompt.AddChoice(FeedFishText)
                    End If
                End If
            Else
                AnsiConsole.MarkupLine("You have no fish!")
                prompt.AddChoice(NewFishText)
            End If
            prompt.AddChoices(HowToPlayText, QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case NewFishText
                    GetNewFish(data, saveGame)
                Case FeedFishText
                    FeedFish(data, saveGame)
                Case HowToPlayText
                    ShowInstructions()
                Case QuitText
                    If ConfirmQuit() Then
                        Exit Do
                    End If
            End Select
        Loop
    End Sub

    Private Sub FeedFish(data As FeedTheFishData, saveGame As Action)
        data.FedUntil = data.FedUntil.Value.AddDays(1.0)
        If data.FedUntil > DateTimeOffset.Now.AddDays(7.0) Then
            data.FishDied = DateTimeOffset.Now
            data.FishesOverfed += 1
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine("You overfed yer fish and it died!")
            OkPrompt()
        End If
        saveGame()
    End Sub

    Private Sub GetNewFish(data As FeedTheFishData, saveGame As Action)
        data.FishAcquired = DateTimeOffset.Now
        data.FedUntil = DateTimeOffset.Now.AddDays(1.0)
        data.FishDied = Nothing
        data.LastFeeding = Nothing
        saveGame()
    End Sub

    Private Sub ShowInstructions()
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("[olive]How To Play ""Feed the Fish"":[/]")
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine("You start out with no fish. 

If you don't have a fish, you can get a fish. 

If you have a fish, you can feed the fish. 

If you overfeed the fish, the fish will die. 

If you starve the fish, the fish will die. 

If you have a dead fish, you can get a new fish.")
        Common.OkPrompt()
    End Sub
End Module
