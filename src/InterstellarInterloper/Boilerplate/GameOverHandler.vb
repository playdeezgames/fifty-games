Friend Module GameOverHandler
    Friend Function Run(data As InterstellarInterloperData, random As Random) As Boolean
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]No Game In Progress![/]"}
        If data.Owners.Any Then
            ShowGameSummary(data)
        End If
        prompt.AddChoice(StartGameText)
        prompt.AddChoice(QuitText)
        Select Case AnsiConsole.Prompt(prompt)
            Case StartGameText
                StartGameHandler.Run(data, random)
            Case QuitText
                If ConfirmQuit() Then
                    Return False
                End If
        End Select
        Return True
    End Function

    Private Sub ShowGameSummary(data As InterstellarInterloperData)
        AnsiConsole.MarkupLine("Game Summary:")
        For index = 1 To data.Owners.Count
            Dim owner = index - 1
            Dim stars = data.Stars.Where(Function(star) star.Owner.HasValue AndAlso star.Owner.Value = owner)
            Dim fleets = data.Fleets.Where(Function(fleet) fleet.Owner = owner)
            AnsiConsole.MarkupLine($"Player #{index}:")
            AnsiConsole.MarkupLine($"  Stars Controlled: {stars.Count}")
            AnsiConsole.MarkupLine($"  Ships Controlled: {stars.Sum(Function(star) star.Ships) + fleets.Sum(Function(fleet) fleet.Ships)}")
        Next
    End Sub
End Module
