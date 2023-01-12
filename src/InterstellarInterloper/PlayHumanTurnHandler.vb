Friend Module PlayHumanTurnHandler
    Friend Function Run(data As InterstellarInterloperData) As Boolean
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine($"Turns remaining: {data.TurnsRemaining}")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[olive]Player #{data.OwnersTurn + 1}:[/]"}
        prompt.AddChoice(StarsText)
        If data.Fleets.Any(Function(fleet) fleet.Owner = data.OwnersTurn) Then
            prompt.AddChoice(FleetsText)
        End If
        prompt.AddChoice(MapText)
        prompt.AddChoice(EndTurnText)
        prompt.AddChoice(AbandonGameText)
        prompt.AddChoice(QuitText)
        Select Case AnsiConsole.Prompt(prompt)
            Case AbandonGameText
                If Confirm("[olive]Are you sure you want to abandon this game?[/]") Then
                    data.TurnsRemaining = 0
                End If
            Case EndTurnText
                If Confirm("[olive]Are you sure you want to end yer turn?[/]") Then
                    data.OwnersTurn += 1
                End If
            Case FleetsText
                FleetsHandler.Run(data)
            Case StarsText
                StarsHandler.Run(data)
            Case MapText
                MapHandler.Run(data)
            Case QuitText
                Return Not ConfirmQuit()
        End Select
        Return True
    End Function
End Module
